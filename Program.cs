using BogusHost;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TheRobot;
using TheRobot.DriverService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("TheRobot")));
        services.AddSingleton<WebDriverService>();
        services.AddSingleton<Robot>();
    })
    .Build();

host.Run();