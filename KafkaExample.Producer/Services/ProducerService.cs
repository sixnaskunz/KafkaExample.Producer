namespace KafkaExample.Producer.Services;

public class ProducerService
{
    private readonly ProducerConfig _config;
    private readonly ILogger<ProducerService> _logger;

    public ProducerService(IOptions<KafkaOptions> options, ILogger<ProducerService> logger)
    {
        _config = new ProducerConfig
        {
            BootstrapServers = options.Value.BootstrapServers
        };
        _logger = logger;
    }
    public async Task<DeliveryResult<Null, string>?> SendMessageAsync(RequestKafka request)
    {
        DeliveryResult<Null, string>? deliveryResult = null;

        // If serializers are not specified, default serializers from
        // `Confluent.Kafka.Serializers` will be automatically used where
        // available. Note: by default strings are encoded as UTF8.
        using var producer = new ProducerBuilder<Null, string>(_config).Build();
        try
        {
            deliveryResult = await producer.ProduceAsync(request.Topic, new Message<Null, string> { Value = request.Message });
            _logger.LogInformation("Delivered '{Value}' to '{TopicPartitionOffset}'", deliveryResult.Value, deliveryResult.TopicPartitionOffset);
        }
        catch (ProduceException<Null, string> e)
        {
            _logger.LogError("Delivery failed: {Reason}", e.Error.Reason);
        }
        return deliveryResult;
    }
}
