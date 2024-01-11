using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using ToDoList.DAL;
using ToDoList.Interfaces;
using ToDoList.Services;

namespace ToDoList;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        ServiceCollection services = [];

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();
        ConfigureServices(services, configuration);
        _serviceProvider = services.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddDataAccessLayer(configuration.GetConnectionString("ToDoTasksDb") ?? throw new InvalidOperationException("Db Connection String is required"));
        services.AddScoped<ITasksService, TasksService>();
        services.AddScoped<ICommandManager, CommandManager>();
        services.AddSingleton<MainWindow>();
    }

    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow?.Show();
    }
}