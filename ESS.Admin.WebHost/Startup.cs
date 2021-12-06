using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.DataAccess.Data;
using ESS.Admin.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using ESS.Admin.DataAccess;
using ESS.Admin.WebHost.Mappers;

namespace ESS.Admin.WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appOptions = Configuration.Get<AppOptions>();
            services.AddSingleton(appOptions);
            services.Configure<AppOptions>(Configuration);

            services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbInitializer, EfDbInitializer>();

            // Database
            services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlite("Filename=EssDb.sqlite");
                x.UseUpperSnakeCaseNamingConvention();
                x.UseLazyLoadingProxies();
            });

            // Mapper
            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped<IMessageMapper, MessageMapper>();

            // Swagger
            services.AddOpenApiDocument(options =>
            {
                options.Title = "Email Sending Service Administration API Doc";
                options.Version = "1.0";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3(x => { x.DocExpansion = "list"; });
            app.UseReDoc(x => x.Path = "/redoc");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
         
            dbInitializer.InitializeDb();
        }
    }
}
