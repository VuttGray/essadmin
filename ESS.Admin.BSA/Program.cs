using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Abstractions.Services;
using ESS.Admin.Core.Application.Services;
using ESS.Admin.DataAccess;
using ESS.Admin.DataAccess.Data;
using ESS.Admin.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// App model services
builder.Services.AddScoped<IMessageService, MessageService>();

// UI services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDevExpressBlazor();

// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IDbInitializer, EfDbInitializer>();

// Database
builder.Services.AddDbContextFactory<DataContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
    x.UseUpperSnakeCaseNamingConvention();
    x.UseLazyLoadingProxies();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
