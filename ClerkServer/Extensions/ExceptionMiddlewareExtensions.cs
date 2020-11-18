using System.Net;
using ClerkServer.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace ClerkServer.Extensions {

	public static class ExceptionMiddlewareExtensions {

		/**
		 * Extends native exception handler.
		 */
		public static void UseClerkExceptionHandler(this IApplicationBuilder app) {
			app.UseExceptionHandler(appError => {
				appError.Run(async context => {
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";

					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature != null) {

						await context.Response.WriteAsync(new ErrorDetails {
							StatusCode = context.Response.StatusCode,
							Message = $"Internal server error: {contextFeature.Error.Message}",
							Details = contextFeature.Error.StackTrace
						}.ToString());
					}
				});
			});
		}

	}

}