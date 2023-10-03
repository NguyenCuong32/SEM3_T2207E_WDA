using System;
using demo.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(demo.Areas.Identity.IdentityHostingStartup))]
namespace demo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<demoContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("demoContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<demoContext>();

                services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // Đường dẫn đến trang đăng nhập
                });
                services.AddControllersWithViews();
                services.AddRazorPages();

            });
        }
    }
}