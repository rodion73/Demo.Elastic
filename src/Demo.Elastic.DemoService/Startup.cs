using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.Elastic.DemoService
{
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    [SuppressMessage("Usage", "CA1801:Review unused parameters")]
    [SuppressMessage("Usage", "IDE0060:Remove unused parameters")]
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => {
                // configure CORS here
            });

            services.AddOpenApiDocument(configure => {
                configure.Title = "Demo.Elastic.DemoService";
            });
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseOpenApi();

            app.UseSwaggerUi3(configure => {
                configure.DocumentTitle = "Demo.Elastic.DemoService";
            });
        }
    }
}
