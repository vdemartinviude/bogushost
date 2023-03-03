using OpenQA.Selenium.Internal;
using TheRobot;
using TheRobot.MediatedRequests;
using TheRobot.WebRequestsParameters;

namespace BogusHost;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Robot _robot;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public Worker(ILogger<Worker> logger, Robot robot, IHostApplicationLifetime applicationLifetime)
    {
        _logger = logger;
        _robot = robot;
        _applicationLifetime = applicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var result = await _robot.Execute(new MediatedNavigationRequest()
        {
            Url = "http://www.uol.com",
        }, stoppingToken);

        await result.Match(async x =>
        {
            if (x.SeverityLevel == TheRobot.Responses.SeverityLevel.Critical)
            {
                _logger.LogCritical("Oh my god an Critical Error Has occurred");
                await _robot.Execute(new MediatedQuitDriverRequest(), stoppingToken);
            }
            return;
        }, y => Task.CompletedTask);

        _applicationLifetime.StopApplication();
    }
}