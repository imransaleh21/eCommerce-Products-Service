namespace ProductsMicroService.BusinessLogicLayer.RabbitMQ;
public record ProductNameUpdateMsg(Guid ProductId, string NewProductName);
