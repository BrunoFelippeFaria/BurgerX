using System.Text.Json.Serialization;

using BurgerX.Api.Middlewares;
using BurgerX.CrossCutting.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependencies(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    ));

var app = builder.Build();
app.UseMiddleware<ExceptionHandler>();
app.MapControllers();
app.Run();
