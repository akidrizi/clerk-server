using System;
using System.Linq;
using ClerkServer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ClerkServer.IntegrationTests {

	public class ClerkServerFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class {

		private const string ConnectionString = @"server=localhost;userid=root;password=;database=clerk_test;";

		protected override void ConfigureWebHost(IWebHostBuilder builder) {
			builder.UseEnvironment("Testing");
			builder.ConfigureServices(services => {
				var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<RepositoryContext>));

				services.Remove(descriptor);

				services.AddDbContext<RepositoryContext>((options, context) => { context.UseMySql(ConnectionString); });

				var sp = services.BuildServiceProvider();

				using var scope = sp.CreateScope();
				var scopedServices = scope.ServiceProvider;
				var db = scopedServices.GetRequiredService<RepositoryContext>();

				db.Database.EnsureDeleted();
				db.Database.EnsureCreated();

				try {
					// SeedData.SeedWebAPI(db, userManager);
				} catch (Exception ex) {
					Console.WriteLine($"An error occurred seeding the database with test messages. Error: {ex.Message}");
				}
			});
		}

	}

}