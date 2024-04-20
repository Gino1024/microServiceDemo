using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace KafkaHandler
{

  public class KafkaHandler : IKafkaHandler
  {


    private readonly string _bootstrapServers;
    private readonly string _topic;

    /// <summary>
    /// 初始化 KafkaHandler 類別的新執行個體。
    /// </summary>
    /// <param name="bootstrapServers">Kafka 伺服器的啟動位置。</param>
    /// <param name="topic">要訂閱或發布訊息的主題。</param>
    public KafkaHandler(string bootstrapServers, string topic)
    {
      _bootstrapServers = bootstrapServers;
      _topic = topic;
    }

    /// <summary>
    /// 建立指定的主題。
    /// </summary>
    public void CreateTopic()
    {
      var config = new AdminClientConfig { BootstrapServers = _bootstrapServers };

      using (var adminClient = new AdminClientBuilder(config).Build())
      {
        var topicSpecification = new TopicSpecification
        {
          Name = _topic,
          NumPartitions = 1,
          ReplicationFactor = 1
        };

        adminClient.CreateTopicsAsync(new List<TopicSpecification> { topicSpecification }).Wait();
      }
    }

    /// <summary>
    /// 檢查指定的主題是否存在。
    /// </summary>
    /// <returns>如果主題存在則為 true，否則為 false。</returns>
    public bool CheckTopicExists()
    {
      var config = new AdminClientConfig { BootstrapServers = _bootstrapServers };

      using (var adminClient = new AdminClientBuilder(config).Build())
      {
        var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(20));
        return metadata.Topics.Any(t => t.Topic == _topic);
      }
    }
    /// <summary>
    /// 刪除指定的主題。
    /// </summary>
    public void DeleteTopic()
    {
      var config = new AdminClientConfig { BootstrapServers = _bootstrapServers };

      using (var adminClient = new AdminClientBuilder(config).Build())
      {
        adminClient.DeleteTopicsAsync(new[] { _topic }).Wait();
      }
    }

    /// <summary>
    /// 取得所有可用的主題。
    /// </summary>
    /// <returns>所有可用的主題。</returns>
    public IEnumerable<string> GetTopics()
    {
      var config = new AdminClientConfig { BootstrapServers = _bootstrapServers };

      using (var adminClient = new AdminClientBuilder(config).Build())
      {
        var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
        return metadata.Topics.Select(topic => topic.Topic);
      }
    }
    /// <summary>
    /// 開始訂閱指定主題的訊息。
    /// </summary>
    public void StartSubscriber()
    {
      var config = new ConsumerConfig
      {
        BootstrapServers = _bootstrapServers,
        GroupId = "my-consumer-group",
        AutoOffsetReset = AutoOffsetReset.Earliest
      };


      using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
      {
        consumer.Subscribe(_topic);

        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {

          e.Cancel = true;
          cts.Cancel();
        };

        try
        {
          while (true)
          {
            var message = consumer.Consume(cts.Token);
            Console.WriteLine($"Received message: {message.Value}");
          }
        }
        catch (OperationCanceledException)
        {
          consumer.Close();
        }
      }
    }

    /// <summary>
    /// 發布指定的訊息到主題。
    /// </summary>
    /// <param name="message">要發布的訊息。</param>
    public void PublishMessage(string message)
    {
      var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

      using (var producer = new ProducerBuilder<Null, string>(config).Build())
      {
        producer.Produce(_topic, new Message<Null, string> { Value = message });
        producer.Flush(TimeSpan.FromSeconds(10));
      }
    }
  }
}



