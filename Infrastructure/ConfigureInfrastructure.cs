using Application.Interfaz;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class ConfigureInfrastructure
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {

        services.AddSingleton<IValidarUsuarioDat, ValidarUsuarioDat>();
        services.AddSingleton<IRegistroPersonasDat, RegistroPersonasDat>();
        return services;
    }
}
