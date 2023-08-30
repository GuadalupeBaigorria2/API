// Singleton : es un ejh de patron
// builder (se dice bilder)
// builder es una variable que representa la instancia de una clase
using Serilog;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// contenedor de servicios
// @sonarqube => Correciones de código

// Esto es el contenedor de servicios

var connectionString =
builder.Configuration.GetConnectionString("HotelListingDbConnection");
builder.Services.AddDbContext<HotelListingDbContext>(options =>
{
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

builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();

//Middleware Pipeline
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();

   app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

    // app.UseSwaggerUI(c =>
    // {
    //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    //     c.RoutePrefix = "";
    // });

    // app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
