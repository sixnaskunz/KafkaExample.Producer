namespace KafkaExample.Producer.Models;

public class KafkaOptions
{
    public const string Kafka = "Kafka";

    public required string BootstrapServers { get; set; }
}