namespace ProductsMicroService.BusinessLogicLayer.RabbitMQ;
public record ProductNameUpdateMessage(Guid ProductId, string NewProductName);
public record ProductDeleteMessage(Guid ProductId, string ProductName);
