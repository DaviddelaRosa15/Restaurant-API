﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAppi.Core.Application.Dtos.Account;
using RestaurantAppi.Core.Application.Interfaces.Services;
using System.Web;
using System.Threading.Tasks;
using System;

namespace RestaurantAppi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var response = await _accountService.AuthenticateAsync(request);
            var refreshToken = response.RefreshToken;
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(14),
				SameSite = SameSiteMode.None
			};
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

			return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("registerWaiter")]
        public async Task<IActionResult> RegisterWaiterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterWaiterUserAsync(request, origin));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdminAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAdminUserAsync(request, origin));
        }
    }
}
