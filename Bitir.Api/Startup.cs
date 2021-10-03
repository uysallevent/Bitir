using Bitir.Data;
using Bitir.Data.Contexts;
using Core.Extensions;
using Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ModulerApi.ModuleIntegration;
using System.Collections.Generic;

namespace ModulerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().ConfigureApplicationPartManager(manager =>
            {
                // Clear all auto detected controllers.
                manager.ApplicationParts.Clear();
                // Add feature provider to allow "internal" controller
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

            // Register a convention allowing to us to prefix routes to modules.
            services.AddTransient<IPostConfigureOptions<MvcOptions>, ModuleRoutingMvcOptionsPostConfigure>();
            services.AddModule<BaseModule.Startup>("BaseModule",Configuration);
            services.AddModule<AuthModule.Startup>("AuthModule", Configuration);
            services.AddModule<ProductModule.Startup>("ProductModule", Configuration);
            services.AddDataServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "BitirApi V1");
            });
            // Adds endpoints defined in modules
            var modules = app.ApplicationServices.GetRequiredService<IEnumerable<ModuleIntegration.Module>>();
            foreach (var module in modules)
            {
                app.Map($"/{module.RoutePrefix}", builder =>
                {
                    builder.UseRouting();
                    module.Startup.Configure(builder, env);
                });
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BitirMainContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }
    }
}