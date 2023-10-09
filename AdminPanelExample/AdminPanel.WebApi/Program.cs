using AdminPanel.DAL;
using AdminPanel.WebApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.ConfugureServices(builder.Configuration);

builder.Services.AddDbSupport(builder.Configuration);

var app = builder.Build();

app.Configure(app.Environment);

app.Run();
