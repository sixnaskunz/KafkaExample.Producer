namespace KafkaExample.Producer.Services;

/// <summary>
/// Represents a service for producing messages to Kafka.
/// </summary>
public class ProducerService
{
    private readonly ProducerConfig _config;
    private readonly ILogger<ProducerService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProducerService"/> class.
    /// </summary>
    /// <param name="options">The Kafka options.</param>
    /// <param name="logger">The logger.</param>
    public ProducerService(ILogger<ProducerService> logger, IOptions<KafkaOptions> options)
    {
        _logger = logger;
        _config = new ProducerConfig
        {
            BootstrapServers = options.Value.BootstrapServers
        };
    }

    /// <summary>
    /// Sends a message asynchronously to the specified Kafka topic.
    /// </summary>
    /// <param name="request">The request containing the topic and message.</param>
    /// <returns>The delivery result of the message.</returns>
    public async Task<DeliveryResult<Null, string>?> SendMessageAsync(RequestKafka request)
    {
        // If serializers are not specified, default serializers from
        // `Confluent.Kafka.Serializers` will be automatically used where
        // available. Note: by default strings are encoded as UTF8.
        using var producer = new ProducerBuilder<Null, string>(_config).Build();
        DeliveryResult<Null, string> deliveryResult = await producer.ProduceAsync(request.Topic, new Message<Null, string> { Value = request.Message });
        _logger.LogInformation("Delivered '{Value}' to '{TopicPartitionOffset}'", deliveryResult.Value, deliveryResult.TopicPartitionOffset);
        return deliveryResult;
    }
}
