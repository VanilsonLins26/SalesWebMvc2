﻿using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using SalesWebMvc.Data;
using SalesWebMvc.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySql(builder
    .Configuration
    .GetConnectionString("SalesWebMvcContext"),
    ServerVersion.AutoDetect(builder
    .Configuration
    .GetConnectionString("SalesWebMvcContext"))));

var enUS = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS }
};



builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<SalesRecordService>();
builder.Services.AddScoped<DepartmentService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRequestLocalization(localizationOptions);



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{   
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<SalesWebMvcContext>();
        var seedingService = new SeedingService(context);
        seedingService.Seed();
    }
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();





app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
