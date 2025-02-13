using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using Microsoft.EntityFrameworkCore;
using Application.Interfaz;
using Application;
using Application.Login;
using Infrastructure.Persistence;
using Infrastructure;
using WebUI;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebUIServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
// Configuración de servicios
builder.Services.AddSingleton<IConfiguration>(builder.Configuration); 

// Registro de la clase ValidarUsuarioDat
builder.Services.AddScoped<IValidarUsuarioDat, ValidarUsuarioDat>();

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Middleware
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

