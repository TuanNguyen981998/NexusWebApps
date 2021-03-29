using System;
using CheckListApp.Areas.Identity.Data;
using CheckListApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CheckListApp.Areas.Identity.IdentityHostingStartup))]
namespace CheckListApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<CheckListAppContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("CheckListAppContextConnection")));

            //    services.AddDefaultIdentity<CheckListAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<CheckListAppContext>();
            //});
        }
    }
}