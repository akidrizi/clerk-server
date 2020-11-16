using Microsoft.AspNetCore.Builder;

namespace ClerkServer.Extensions {

	public static class ApplicationBuilderExtensions {

		/*
		 * Sets up Swagger JSON path and serve at app root.
		 */
		public static void UseClerkSwagger(this IApplicationBuilder app) {
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clerk Server API");
				c.RoutePrefix = string.Empty;
			});
		}

	}

}