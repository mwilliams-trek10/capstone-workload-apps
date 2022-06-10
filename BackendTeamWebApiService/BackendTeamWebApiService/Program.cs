using System.Reflection;
using BackendTeamWebApiService;
using BackendTeamWebApiService.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var dockerEnvironment = Environment.GetEnvironmentVariable("DockerEnvironment");

builder.WebHost.ConfigureAppConfiguration((context, configurationBuilder) =>
{
    // https://localhost:7030;
    string environment = string.IsNullOrEmpty(dockerEnvironment)
        ? context.HostingEnvironment.EnvironmentName
        : dockerEnvironment;
    
    // https://localhost:7124;
    context.Configuration = configurationBuilder
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{environment}.json")
        .Build();
}).ConfigureServices((context, services) =>
{
    ServiceBuilder.AddServices(services, context);
});

//builder.WebHost.UseSetting("https_port", "8081");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

var configurationOptions = app.Services.GetService<IOptions<ConfigurationOptions>>();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// --launch-profile Development