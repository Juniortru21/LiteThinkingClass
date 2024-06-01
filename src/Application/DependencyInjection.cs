using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddExceptionHandler<ForbiddenExceptionHandler>();
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<InternalServerExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddSingleton(x => new BlobServiceClient(configuration.GetSection("AzureStorage:ConnectionString").Value));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        return services;
    }
}
