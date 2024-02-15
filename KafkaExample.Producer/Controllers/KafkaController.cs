namespace KafkaExample.Producer.Controllers;

[ApiController]
[Route("[controller]")]
public class KafkaController(ProducerService producerService) : ControllerBase
{
    [HttpPost("SendMessage")]
    public async Task<DeliveryResult<Null, string>?> Post([FromBody] RequestKafka request)
    {
        DeliveryResult<Null, string>? deliveryResult = await producerService.SendMessageAsync(request);
        return deliveryResult;
    }
}
