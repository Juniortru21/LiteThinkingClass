using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Application;
using Infrastructure;
using Shared.Core;
using Azure.Storage.Blobs;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        EngineContext.Create();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddSwaggerGen(setup =>
        {
            // enables the form for putting the bearer token
            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            // it adds the bearer token to the requests
            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        var app = builder.Build();

        EngineContext.Current.Configure(app.Services);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
