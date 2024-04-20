using System.Collections.Generic;
namespace KafkaHandler
{
  public interface IKafkaHandler
  {
    void CreateTopic();
    bool CheckTopicExists();
    void DeleteTopic();
    IEnumerable<string> GetTopics();
    void StartSubscriber();
  }
}