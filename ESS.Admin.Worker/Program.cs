using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.DataAccess;
using ESS.Admin.DataAccess.Data;
using ESS.Admin.DataAccess.Repositories;
using ESS.Admin.Worker.Services;
using ESS.Admin.Worker.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

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
                    IConfiguration configuration = hostContext.Configuration;
                    MailSettings options = configuration.GetSection("EmailConf").Get<MailSettings>();
                    services.AddSingleton(options);
                    var connectionString = configuration.GetSection("ConnectionString").Value;
                    var tgClient = new TelegramBotClient(configuration.GetSection("TelegramBotToken").Value);

                    services.AddSingleton(tgClient);
                    services.AddScoped<IEmailService, EmailService>();
                    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
                    services.AddScoped<IDbInitializer, EfDbInitializer>();
                    services.AddDbContext<DataContext>(x =>
                    {
                        x.UseSqlServer(connectionString);
                        x.UseUpperSnakeCaseNamingConvention();
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
