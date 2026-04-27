namespace OnlineLibrary.Bridge
{
     public class CloudDelivery : IContentDelivery
     {
          public string Deliver(string content, string resourceId)
          {
               return $"[Cloud] Resource {resourceId}: {content}";
          }
     }
}