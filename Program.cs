using ApiPedidos.Data;
using ApiPedidos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection; 

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json");
var key = builder.Configuration.GetSection("Jwt").GetSection("Key").ToString();
var keyBytes = Encoding.UTF8.GetBytes(key);

builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config => {
    config.RequireHttpsMetadata = false;
    config.SaveToken =true;
    config.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// esto es de Productos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
// 


builder.Services.AddControllers();
// Configuración de ASP.NET Core Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// cors config
builder.Services.AddCors(Options =>
{
    Options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Configuración de la serialización JSON
// Configuración de la serialización JSON
var jsonOptions = new JsonSerializerOptions
{
    ReferenceHandler = ReferenceHandler.Preserve
    // Aquí puedes configurar otras opciones de serialización JSON según sea necesario
};
// Agrega la configuración de serialización JSON al servicio
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        // Aquí puedes configurar otras opciones de serialización JSON según sea necesario
    });
var app = builder.Build();
// Aquí puedes usar la configuración de serialización JSON
app.Use(async (context, next) =>
{
    // Configura la serialización JSON aquí si es necesario
    var serializerOptions = context.RequestServices.GetRequiredService<IOptions<JsonSerializerOptions>>().Value;
    // Puedes configurar la serialización JSON aquí según tus necesidades

    await next();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("NuevaPolitica");

app.UseAuthorization();


app.MapControllers();

app.Run();
