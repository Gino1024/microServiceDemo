using KafkaHandler;

namespace ms.logger.worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            KafkaTopicChecker kafkaTopicChecker = new KafkaTopicChecker();
            var isExist = kafkaTopicChecker.TopicExists("localhost:9092", "logs");
            Console.WriteLine("Log Exists:" + isExist);
            KafkaSubscriber kafkaSubscriber = new KafkaSubscriber("localhost:9092", "logs");
            KafkaPublisher kafkaPublisher = new KafkaPublisher("localhost:9092", "logs");
            //kafkaPublisher.Publish("Hello World2");
            kafkaSubscriber.Start();


            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }

}
