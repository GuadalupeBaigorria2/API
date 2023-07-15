// Singleton : es un ejh de patron
// builder (se dice bilder)
// builder es una variable que representa la instancia de una clase
using Serilog;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// contenedor de servicios
// @sonarqube => Correciones de código

// Esto es el contenedor de servicios

var connectionString= 
builder.Configuration.GetConnectionString("HotelListingDbConnection");
builder.Services.AddDbContext<HotelListingDbContext>(options => {
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
     b => b.AllowAnyHeader()
     .AllowAnyOrigin()
    .AllowAnyMethod());
});

// La configuración del logger la leo desde el archivo de configuración
// Luego de esto, actualizar el archivo de configuración appsettings.json
// Y borrar, lo siguiente, reemplazando por lo que esta actualmente.
//   "Logging": {
//     "LogLevel": {
//       "Default": "Information",
//       "Microsoft.AspNetCore": "Warning"
//     }
//   },
builder.Host.UseSerilog((ctx, lc) => 
lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

//Middleware Pipeline
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
