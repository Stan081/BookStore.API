using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Models;
using BookStore.Repository.DataContext;
using BookStore.Repository.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStore.API
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();

            //Use this when you want to seed

            //var host = CreateHostBuilder(args).Build();
            //using var scope = host.Services.CreateScope();
            //var services = scope.ServiceProvider;
            //try
            //{
            //    var context = services.GetRequiredService<BookStoreContext>();
            //    var userManager = services.GetRequiredService<UserManager<User>>();
            //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //    await context.Database.MigrateAsync();
            //    await Seeders.SeedRoles(userManager, roleManager);
            //}
            //catch(Exception ex)
            //{
            //    Console.Out.Write(ex);
            //}

            //await host.RunAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
