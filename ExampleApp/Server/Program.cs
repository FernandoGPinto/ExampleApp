using ExampleApp.Server.Data;
using ExampleApp.Server.Mapping;
using ExampleApp.Server.Services;
using ExampleApp.Shared.Interfaces;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<LoanDbContext>(
    options => options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExampleApp;Trusted_Connection=True;"));
builder.Services.AddAutoMapper(config => config.AddProfile(new AutoMapperProfile()));
builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
