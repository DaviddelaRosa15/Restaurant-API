using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantAppi.Core.Application.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using RestaurantAppi.Core.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;

namespace RestaurantAppi.WebApi.Middlewares
{
	public class TokenMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly JWTSettings _jwtSettings;

		public TokenMiddleware(RequestDelegate next, IOptions<JWTSettings> jwtSettings)
		{
			_next = next;
			_jwtSettings = jwtSettings.Value;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var authResult = await context.AuthenticateAsync();

			if (authResult?.Failure?.GetType() == typeof(SecurityTokenExpiredException))
			{
				var cookie = context.Request.Cookies["refreshToken"];
				var expiredToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
				
				if(cookie != null)
				{
					try
					{
						using (var scope = context.RequestServices.CreateScope())
						{
							var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
							var refreshService = scope.ServiceProvider.GetRequiredService<IRefreshTokenService>();
							var token = await refreshService.GetByToken(cookie);


							if (token != null && token.IsActive)
							{
								var refreshToken = await accountService.GenerateJWToken(token.UserId);
								var refresh = new JwtSecurityTokenHandler().WriteToken(refreshToken);
								context.Request.Headers["Authorization"] = "Bearer " + refresh;
							}
						}

					}
					catch (Exception)
					{
						// Error al validar el token
					}
				}
				
			}

			await _next(context);
		}
	}
}
