namespace TrancborderTimeCalc;

public partial class App : Application
{
    private static IServiceProvider __Services;
    private static IServiceCollection GetServices()
    {
        var services = new ServiceCollection();
        InitServices(services);
        return services;
    }
    private static void InitServices(IServiceCollection services)
    {
        services.AddSingleton<ISettings, Settings>();

        services.AddTransient<IDialogService, WindowDialogService>();

        services.AddScoped<IRepositoryService, RepositoryService>();
        services.AddScoped<IGeneratorService, GeneratorService>();

        services.AddScoped<MainWindowViewModel>();
    }
    /// <summary> Сервисы </summary>
    public static IServiceProvider Services => __Services ??= GetServices()
        .BuildServiceProvider();
}

