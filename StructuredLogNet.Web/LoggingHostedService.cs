using System.Threading.Channels;
using Microsoft.Extensions.Hosting;

namespace StructuredLogNet.Web;

internal class LoggingHostedService : BackgroundService
{
    private readonly IStructuredLogger<LoggingHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogItemQueue _queue;
    private readonly FileLoggerTarget _loggerTarget;
    private volatile bool _ready = false;

    public LoggingHostedService(
        IStructuredLogger<LoggingHostedService> logger,
        ILogItemQueue queue,
        IServiceProvider serviceProvider,
        IHostApplicationLifetime lifetime
    )
    {
        _logger = logger;
        _queue = queue;

        logger.Info("Log to file", new[] { ("LogPath", appInfo.LogPath) });
        _loggerTarget = new FileLoggerTarget(appInfo.LogPath);

        _serviceProvider = serviceProvider;

        lifetime.ApplicationStarted.Register(() => _ready = true);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!_ready)
        {
            // App hasn't started yet, keep looping!
            await Task.Delay(1_000, stoppingToken);
        }

        _logger.Info("Ready to listen job");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var logItem = await _queue.DequeueAsync(stoppingToken);

                _loggerTarget.Log(logItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing Jobs.");
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Queued Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }
}

internal interface ILogItemQueue
{
    void Queue(LogItem item);

    Task<LogItem> DequeueAsync(CancellationToken cancellationToken);
}

internal class LogItemQueue : ILogItemQueue
{
    private readonly Channel<LogItem> _queue;

    public LogItemQueue()
    {
        var options = new BoundedChannelOptions(1000) { FullMode = BoundedChannelFullMode.Wait, };

        _queue = Channel.CreateBounded<LogItem>(options);
    }

    public void Queue(LogItem item)
    {
        _queue.Writer.TryWrite(item);
    }

    public async Task<LogItem> DequeueAsync(CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}

internal class QueueFileLoggerTarget : ILoggerTarget
{
    private readonly ILogItemQueue _queue;

    public QueueFileLoggerTarget(ILogItemQueue logItemQueue)
    {
        _queue = logItemQueue;
    }

    public void Log(LogItem item)
    {
        _queue.Queue(item);
    }
}
