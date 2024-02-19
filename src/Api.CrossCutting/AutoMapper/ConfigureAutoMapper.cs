namespace Api.CrossCutting.AutoMapper;

public class ConfigureAutoMapper
{
    public static void ConfigureDependenciesAutoMapper(IServiceCollection service)
    {
        service.AddAutoMapper(typeof(GastoProfile));
        service.AddAutoMapper(typeof(LoginProfile));
    }
}