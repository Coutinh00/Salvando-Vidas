using AbrigoHub.Infrastructure.Data;
using AbrigoHub.Web.Services;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore.Infrastructure;
using AbrigoHub.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuração do banco de dados
builder.Services.AddDbContext<AbrigoHubContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"),
        oracleOptions => oracleOptions.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion21)));

// Configuração do serviço de clima
builder.Services.AddHttpClient<IWeatherService, WeatherService>(client =>
{
    var baseUrl = builder.Configuration["OpenWeather:BaseUrl"];
    if (string.IsNullOrEmpty(baseUrl))
    {
        throw new InvalidOperationException("OpenWeather:BaseUrl não configurada no appsettings.json");
    }
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Chamar o método de seeding para preencher o banco de dados dentro de um escopo de serviço
using (var scope = app.Services.CreateScope())
{
    await SeedData.EnsurePopulated(scope.ServiceProvider);
}

app.Run();
