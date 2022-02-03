using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Abstractions.Services;
using ESS.Admin.Core.Application.Services;
using ESS.Admin.DataAccess;
using ESS.Admin.DataAccess.Data;
using ESS.Admin.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESS.Admin.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appOptions = Configuration.Get<AppOptions>();
            services.AddSingleton(appOptions);
            services.Configure<AppOptions>(Configuration);

            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbInitializer, EfDbInitializer>();

            // Database
            services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlServer(appOptions.ConnectionString);
                x.UseUpperSnakeCaseNamingConvention();
                x.UseLazyLoadingProxies();
            });

            // Services
            services.AddScoped<IMessageService, MessageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
            });

            dbInitializer.InitializeDb();
        }
    }
}
