namespace OnlineLibrary.Bridge
{
     public class DownloadDelivery : IContentDelivery
     {
          public string Deliver(string content, string resourceId)
          {
               return "Resource " + resourceId + " is ready for download: " + content;
          }
     }
}