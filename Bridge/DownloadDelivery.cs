namespace OnlineLibrary.Bridge
{
     public class DownloadDelivery : IContentDelivery
     {
          public string Deliver(string content, string resourceId)
          {
               return $"[Download] Resource {resourceId}: {content}";
          }
     }
}