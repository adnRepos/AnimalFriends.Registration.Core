using AnimalFriends.Registration.API.Data;
using AnimalFriends.Registration.API.Models;
using AnimalFriends.Registration.API.Services;
using FluentValidation;
using System;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using AnimalFriends.Registration.API.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//============================Logging/Cors============================

builder.Services.AddLocalization();
// logging 
builder.Services.AddLogging(options =>
{
    options.AddSimpleConsole(options =>
    {
        options.IncludeScopes = true;
        options.SingleLine = false;
        options.TimestampFormat = "dd MMM HH:mm:ss ";
        options.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
    });
});

// Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
    });
});
//============================Logging/Cors============================


//============================ENV Configs============================
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

//============================ENV Configs============================


//============================Register Services============================

builder.Services
       .AddSingleton<IDbConfig, DbConfig>()
       .AddSingleton<IDb, Db>()
       .AddScoped<IRegistrationData, RegistrationData>()
       .AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddAutoMapper(typeof(MapperProfile));
//============================Register Services============================


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
