using TheRobot;
using TheRobot.MediatedRequests;
using TheRobot.WebRequestsParameters;

namespace BogusHost;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Robot _robot;

    public Worker(ILogger<Worker> logger, Robot robot)
    {
        _logger = logger;
        _robot = robot;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _robot.Exec3Async(new MediatedNavigationRequest()
        {
            Parameters = new NavigateRequestParameters()
            {
                Url = "http://www.uol.com"
            }
        });

        //await _robot.Exec3Async(new MediatedQuitDriverRequest());
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}