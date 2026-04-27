namespace OnlineLibrary.Bridge
{
     public class StreamingDelivery : IContentDelivery
     {
          public string Deliver(string content, string resourceId)
          {
               return $"[Streaming] Resource {resourceId}: {content}";
          }
     }
}