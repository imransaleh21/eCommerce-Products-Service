namespace ProductsMicroService.BusinessLogicLayer.RabbitMQ;

public interface IRabbitMQPublisher
{
    Task PublishMessageAsync<T>(string routingKey, T message);
}
