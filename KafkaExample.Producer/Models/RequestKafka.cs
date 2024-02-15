namespace KafkaExample.Producer.Models;

public class RequestKafka
{
    public required string Topic { get; set; }

    public required string Message { get; set; }
}