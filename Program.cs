using CheckListApp.Areas.Identity.Data;
using CheckListApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var Host = CreateHostBuilder(args).Build();
            //Seed db
            using (var scope = Host.Services.CreateScope())
            {
                bool changeFlag = false;
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    changeFlag = true;
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    changeFlag = true;
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }
                var _context = scope.ServiceProvider.GetRequiredService<CheckListAppContext>();
                //var userStore = new UserStore<CheckListAppUser>(_context); //almost the same as user manager but with more customization
                
                //var _context = new CheckListAppContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<CheckListAppContext>>());
                if (!await _context.Users.AnyAsync())
                {
                    changeFlag = true;
                    var SiteAdmin =
                    new CheckListAppUser
                    {
                        Email = "tuan1998pro@gmail.com",
                        NormalizedEmail = "TUAN1998PRO@GMAIL.COM",
                        EmailConfirmed = true,
                        UserName = "tuan1998pro@gmail.com",
                        NormalizedUserName = "TUAN1998PRO@GMAIL.COM",
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString("D")//Format specifier
                    };
                    var PasswordHashed = new PasswordHasher<CheckListAppUser>().HashPassword(SiteAdmin, "Iambatman98!");
                    SiteAdmin.PasswordHash = PasswordHashed;
                    UserManager<CheckListAppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<CheckListAppUser>>();
                    await userManager.CreateAsync(SiteAdmin);
                    await userManager.AddToRoleAsync(SiteAdmin, RolesAndOperationsConstants.AdministratorsRole);
                    
                }
                if(changeFlag) await _context.SaveChangesAsync();

            }
            Host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
