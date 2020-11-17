using System;
using System.IO;
using System.Reflection;
using ClerkServer.Contracts;
using ClerkServer.Entities;
using ClerkServer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ClerkServer.Extensions {

	public static class ServiceExtensions {

		/*
		 * Registers CORS to allow all application origins.
		 */
		public static void ConfigureAllowAllCors(this IServiceCollection services) {
			services.AddCors(options => {
				options.AddPolicy("AllCORS",
					builder => builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
			});
		}

		/*
		 * Configures Swagger document and enables XML comments.
		 */
		public static void ConfigureSwagger(this IServiceCollection services) {
			services.AddSwaggerGen();
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo {
					Title = "Clerk Server API",
					Version = "v1",
					Description = "Akis Idrizi <aidrizi@outlook.com.gr>",
					License = new OpenApiLicense {
						Name = "GitHub",
						Url = new Uri("https://github.com/akidrizi"),
					}
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}

		/*
		 * Configures EF Core to operate with MySQL. 
		 */
		public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config) {
			var connectionString = config["ConnectionStrings:DefaultConnection"];
			services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
		}

		/*
		 * Registers generic repository wrapper in the IOC.
		 */
		public static void ConfigureRepositoryWrapper(this IServiceCollection services) {
			services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
		}

	}

}