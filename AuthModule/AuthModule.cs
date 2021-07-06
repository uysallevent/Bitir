using AuthModule.Business;
using AuthModule.Security.JWT;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthModule
{
    public class AuthModule : BaseModule.BaseModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddSingleton<ITokenHelper, JwtHelper>();
            services.AddScoped(typeof(AuthBusinessBase));
        }


    }
}
