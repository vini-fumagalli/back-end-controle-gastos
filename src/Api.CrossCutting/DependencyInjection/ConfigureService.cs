
using Api.Domain.Interfaces.Services;
using Api.Service.Services;

namespace Api.CrossCutting.DependencyInjection;

public class ConfigureService
{
    public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ILoginService, LoginService>();
        serviceCollection.AddTransient<IGastoService, GastoService>();
    }
}