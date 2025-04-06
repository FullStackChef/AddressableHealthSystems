namespace Shared;

public record CommunicationDeliveryEvent(string Id, string ResourceType = "Communication", string EventType = "NewMessage");

