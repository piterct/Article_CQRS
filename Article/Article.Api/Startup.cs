using Article.Api.Helpers.DependencyInjectionConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace Article.Api
{
    public class Startup
    {
        public string Env { get; set; }
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            this.Env = environment.EnvironmentName;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{Env}.json", optional: true); ;

            builder.AddEnvironmentVariables();
            builder.Build();

            builder.Build();

            services.AddControllers();

            services.AddMvc();

            services.AddResponseCompression();
            services.AddResponseCaching();

            services.AddMvcCore(options =>
            {
                options.CacheProfiles.Add("Default", new CacheProfile
                {
                    Duration = 30,
                    Location = ResponseCacheLocation.Any
                });
            });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Article"
                });
            });

            ConfigureDI(services, Configuration);

        }

        public static void ConfigureDI(IServiceCollection services, IConfiguration configuration)
        {
            //sign the services to di container   
            DependencyRegister.AddScoped(services, configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCompression();

            // Enable middleware to serve generated Swagger as a JSON endpoint.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
