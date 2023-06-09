﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Interfaces.Repositories;
using RestaurantAppi.Core.Application.Interfaces.Services;
using RestaurantAppi.Core.Domain.Settings;
using RestaurantAppi.Infrastructure.Identity.Entities;
using RestaurantAppi.Infrastructure.Identity.Services;
using RestaurantAppi.Infrastructure.Persistence.Contexts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace RestaurantAppi.Infrastructure.Identity
{
	//Extension Method - Decorator
	public static class ServiceRegistration
	{
		public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			var connection = configuration.GetConnectionString("PostgreSQL");
			var password = Environment.GetEnvironmentVariable("PassCockroachDB");
			var host = Environment.GetEnvironmentVariable("HostCockroachDB");
			connection = connection.Replace("#", password);
			connection = connection.Replace("ServerHost", host);
			#region Contexts
			if (configuration.GetValue<bool>("UseInMemoryDatabase"))
			{
				services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
			}
			else
			{
				services.AddDbContext<IdentityContext>(options =>
				{
					options.EnableSensitiveDataLogging();
					options.UseNpgsql(connection,
					m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
				});
			}
			#endregion

			#region Identity
			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/User";
				options.AccessDeniedPath = "/User/AccessDenied";
			});

			services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
					ValidIssuer = configuration["JWTSettings:Issuer"],
					ValidAudience = configuration["JWTSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
				};
				options.Events = new JwtBearerEvents()
				{
					OnAuthenticationFailed = async c =>
					{
						if (c.Exception.GetType() == typeof(SecurityTokenExpiredException))
						{
							var cookie = c.Request.Cookies["refreshToken"];
							var expiredToken = c.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

							if (cookie != null)
							{
								try
								{
									var serviceProvider = c.HttpContext.RequestServices;
									var accountService = serviceProvider.GetRequiredService<IAccountService>();
									var refreshService = serviceProvider.GetRequiredService<IRefreshTokenService>();
									var token = await refreshService.GetByToken(cookie);


									if (token != null && token.IsActive)
									{
										var refreshToken = await accountService.GenerateJWToken(token.UserId);
										var refresh = new JwtSecurityTokenHandler().WriteToken(refreshToken);
										c.Request.Headers["Authorization"] = "Bearer " + refresh;
										throw new MyAuthenticationFailedException("Token Refrescado");
									}
								}
								catch (Exception)
								{
									// Error al validar el token
								}
							}
						}
						else
						{
							c.NoResult();
							c.Response.StatusCode = 500;
							c.Response.ContentType = "text/plain";
						}
						
					},
					OnChallenge = c =>
					{
						c.HandleResponse();
						c.Response.StatusCode = 401;
						c.Response.ContentType = "application/json";
						var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "Usted no se ha logueado" });
						return c.Response.WriteAsync(result);
					},
					OnForbidden = c =>
					{
						c.Response.StatusCode = 403;
						c.Response.ContentType = "application/json";
						var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "Usted no está autorizado para usar este endpoint" });
						return c.Response.WriteAsync(result);
					}
				};

			});
			#endregion

			#region Services
			services.AddTransient<IAccountService, AccountService>();
			#endregion
		}
	}
}
