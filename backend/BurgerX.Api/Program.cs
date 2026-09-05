using BurgerX.CrossCutting.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();
