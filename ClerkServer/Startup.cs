using ClerkServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClerkServer {

	public class Startup {

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.ConfigureSwagger();
			services.ConfigureAllowAllCors();

			services.ConfigureMySqlContext(Configuration);
			services.ConfigureRepositoryWrapper();
			
			services.ConfigureRandomUserAPIClient();
			
			services.AddControllers().AddJsonOptions(options => {
				options.JsonSerializerOptions.IgnoreNullValues = true;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseClerkSwagger();
			app.UseCors("AllCORS");
			
			// Forward headers behind reverse proxy.
			app.UseForwardedHeaders(new ForwardedHeadersOptions {
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});
			
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}

	}

}