using Authentication.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Authentication
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseInMemoryDatabase("DBInMemory");
            });

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength =4 ;

            }).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentitySecurity.cookie";
                config.LoginPath = "/Home/Login";
            });

            //services.AddAuthentication("FoundationCookie")
            //    .AddCookie("FoundationCookie", config =>
            //    {
            //        config.Cookie.Name = "FoundationSecurity.cookie";
            //        config.LoginPath = "/Home/Authenticate";
            //    });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
