using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TAK.Web.Data;

[assembly: HostingStartup(typeof(TAK.Web.Areas.Identity.IdentityHostingStartup))]
namespace TAK.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TAKWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TAKWebContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 //   .AddEntityFrameworkStores<TAKWebContext>();
            });
        }
    }
}