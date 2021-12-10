using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.DataAccess;
using ESS.Admin.DataAccess.Data;
using ESS.Admin.DataAccess.Repositories;
using ESS.Admin.Worker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ESS.Admin.Worker
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IEmailService, EmailService>();
                    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
                    services.AddScoped<IDbInitializer, EfDbInitializer>();
                    services.AddDbContext<DataContext>(x =>
                    {
                        x.UseSqlite($"Filename={Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/source/dbs/EssDb.sqlite"}");
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
