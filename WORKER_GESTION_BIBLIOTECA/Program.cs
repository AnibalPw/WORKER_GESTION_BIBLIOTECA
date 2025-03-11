//using WORKER_GESTION_BIBLIOTECA;

//var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();

//var host = builder.Build();
//host.Run();

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WORKER_GESTION_BIBLIOTECA;
using WORKER_GESTION_BIBLIOTECA.Repositories;

var builder = Host.CreateApplicationBuilder(args);

// Registrar servicios
builder.Services.AddSingleton<IPrestamoRepository, PrestamoRepository>();
builder.Services.AddHostedService<Worker>();

// Registrar logger
builder.Services.AddLogging();

var host = builder.Build();
host.Run();

//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using WORKER_GESTION_BIBLIOTECA;
//using WORKER_GESTION_BIBLIOTECA.Repositories;

//var builder = Host.CreateApplicationBuilder(args);

//// Agregar configuración
//builder.Configuration.AddJsonFile("appsettings.json", optional: false);

//// Registrar servicios
//builder.Services.AddHostedService<Worker>();
//builder.Services.AddScoped<IPrestamoRepository, PrestamoRepository>();



//var host = builder.Build();
//host.Run();
