using Confluent.Kafka;
using Game.Kafka.Contracts;
using System.Text.Json;

namespace Game.Wiki
{
    public class KafkaClient
    {
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly string _topic = "game.status";

        public async Task PublishProgressMessageAsync(ProgressMessage message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var json = JsonSerializer.Serialize(message);

            var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = json });
            Console.WriteLine($"Kafka: отправлено сообщение в {_topic} (offset {result.Offset})");
        }
    }
}
