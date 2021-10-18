using A2HTransborderPositions.Interfaces;
using A2HTransborderPositions.Services;
using A2HTransborderPositions.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace A2HTransborderPositions
{
    public partial class App : Application
    {
        private static IServiceProvider __Services;
        private static IServiceCollection GetServices()
        {
            var services = new ServiceCollection();
            InitializeServices(services);
            return services;
        }
        private static void InitializeServices(IServiceCollection services)
        {
            services.AddTransient<IRepositoryService, RepositoryService>();

            services.AddTransient<IMixReaderService, Sharp7MixReaderService>();

            services.AddScoped<MainWindowViewModel>();
        }
        /// <summary> Сервисы </summary>
        public static IServiceProvider Services => __Services ??= GetServices()
            .BuildServiceProvider();
    }
}
