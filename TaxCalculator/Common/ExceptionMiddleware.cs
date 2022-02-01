using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TaxCalculator
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILoggerManager _logger;

		public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error occurred while processing request: {ex}");
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";

			string message = "Internal Server Error from exception middleware.";
			if (!string.IsNullOrEmpty(exception.Message))
            {
				message = $"ExceptionMiddleware: {exception.Message}";

			}

			// map response status to custom exception
			if (exception is UnauthorizedAccessException)
            {
				context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
			else
            {
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			}

			await context.Response.WriteAsync(new Error()
			{
				StatusCode = context.Response.StatusCode,
				Message = message
			}.ToString());
		}
	}
}
