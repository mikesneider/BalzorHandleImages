using BalzorHandleImages.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using BalzorHandleImages.Services;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IBlobFactory>(ctx =>
{
    var connectionString = builder.Configuration.GetConnectionString("AzureBlobConnectionString");
    return new BlobFactory(connectionString);
});
builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
