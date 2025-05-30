namespace MessagingService;

public enum MessagingDeliveryMode
{
    StoreAndForward,
    Direct
}

public class MessagingOptions
{
    public MessagingDeliveryMode DeliveryMode { get; set; } = MessagingDeliveryMode.StoreAndForward;
}
