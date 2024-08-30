using Microsoft.Extensions.Logging;

public class KafkaLoggerProvider : ILoggerProvider
{
  private readonly string _bootstrapServers;
  private readonly string _topic;
  private readonly LogLevel _minLogLevel;

  public KafkaLoggerProvider(string bootstrapServers, string topic, LogLevel minLogLevel)
  {
    _bootstrapServers = bootstrapServers;
    _topic = topic;
    _minLogLevel = minLogLevel;
  }

  public ILogger CreateLogger(string categoryName)
  {
    Console.WriteLine($"createLogger {categoryName}");
    return new KafkaLogger<object>(_bootstrapServers, _topic, categoryName, _minLogLevel);
  }

  public void Dispose()
  {
    // 清理资源
  }
}
