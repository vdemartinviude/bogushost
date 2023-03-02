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
        await _robot.Exec3Async(new MediatedNavigationRequest()
        {
            Parameters = new NavigateRequestParameters()
            {
                Url = "http://www.uol.com",
            }
        }, stoppingToken);

        await _robot.Exec3Async(new MediatedQuitDriverRequest(), stoppingToken);

        _applicationLifetime.StopApplication();
    }
}