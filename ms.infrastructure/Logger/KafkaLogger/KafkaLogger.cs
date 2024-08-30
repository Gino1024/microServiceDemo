using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;

public class KafkaLogger<T> : ILogger<T>
{
  private readonly string _categoryName;
  private readonly IProducer<Null, string> _producer;
  private readonly string _topic;
  private readonly LogLevel _minLogLevel;

  public KafkaLogger(string bootstrapServers, string topic, string categoryName, LogLevel minLogLevel)
  {
    Console.WriteLine("create kafkalogger");
    _topic = topic;
    _minLogLevel = minLogLevel;
    _categoryName = categoryName;

    var config = new ProducerConfig { BootstrapServers = bootstrapServers };
    _producer = new ProducerBuilder<Null, string>(config).Build();
  }

  public IDisposable BeginScope<TState>(TState state)
  {
    return null; // 如果需要作用域日志记录，可以在这里实现
  }

  public bool IsEnabled(LogLevel logLevel)
  {
    // 定义你希望记录的日志级别
    return logLevel >= _minLogLevel;
  }

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
  {
    Console.WriteLine($"Logged to Kafka: {logLevel}");
    if (!IsEnabled(logLevel))
    {
      return;
    }

    var message = $"[{logLevel}] - {_categoryName}: {formatter(state, exception)}";
    try
    {
      _producer.Produce(_topic, new Message<Null, string> { Value = message });
      Console.WriteLine($"Logged to Kafka: {message}");
    }
    catch (ProduceException<Null, string> e)
    {
      Console.WriteLine($"Failed to log to Kafka: {e.Error.Reason}");
    }
  }
}
