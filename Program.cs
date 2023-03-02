using BogusHost;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TheRobot;
using TheRobot.DriverService;
using TheRobot.PipelineExceptionHandler;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        //services.Configure<HostOptions>(hostOptions =>
        //{
        //    hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        //});
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("TheRobot")));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatorPipelineBehavior<,>));
        services.AddSingleton<WebDriverService>();
        services.AddSingleton<Robot>();
    })
    .Build();

host.Run();