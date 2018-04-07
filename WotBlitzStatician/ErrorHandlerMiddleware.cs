using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WotBlitzStatician
{
	public class ErrorHandlerMiddleware
    {
		private readonly RequestDelegate _next;
		private readonly ILogger<ErrorHandlerMiddleware> _logger;

		public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception e)
			{
				await HandleExceptionAsync(context, e, _logger);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
		{
			var code = HttpStatusCode.InternalServerError; // 500 if unexpected

			logger.LogError(default(EventId), exception, $"{context.Request.Method}: {context.Request.Path}");

			var result = JsonConvert.SerializeObject(new { error = exception.Message });
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;
			return context.Response.WriteAsync(result);
		}

	}

	public static class ErrorHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseErrorHandler(
			this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ErrorHandlerMiddleware>();
		}
	}
}
