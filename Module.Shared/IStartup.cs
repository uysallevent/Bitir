using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Module.Shared
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration = null);

        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}