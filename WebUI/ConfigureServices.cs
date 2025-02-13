using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace WebUI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
    {

        // CUSTOMISE DEFAULT API BEHAVIOUR
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        //CORS
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.WithOrigins().WithMethods("POST").AllowAnyHeader();
                    });
        });

        //SERVICES
        services.AddDataProtection();
        services.AddMemoryCache();
        services.AddOptions();

        //SWAGGER
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            //Modificar
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Prueba Técncica",
                Version = "v1",
                Description = "servicio API rest para el servicio de usuarios y personas"
            });
        });

        return services;
    }
}
