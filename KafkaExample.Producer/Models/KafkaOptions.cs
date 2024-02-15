namespace KafkaExample.Producer.Models;

public class KafkaOptions
{
    public const string Kafka = "Kafka";

    public required string BootstrapServers { get; set; }
    public string? RequestTimeoutMs { get; set; }
    public string? MessageTimeoutMs { get; set; }
    public string? TransactionTimeoutMs { get; set; }
}