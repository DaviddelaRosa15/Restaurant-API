using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using RestaurantAppi.Infrastructure.Identity;

namespace RestaurantAppi.WebApi.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (MyAuthenticationFailedException)
			{
				// Detener el procesamiento de la cadena de middleware
				return;
			}
		}
	}
}
