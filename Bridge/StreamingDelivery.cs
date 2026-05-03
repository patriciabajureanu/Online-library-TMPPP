namespace OnlineLibrary.Bridge
{
     public class StreamingDelivery : IContentDelivery
     {
          public string Deliver(string content, string resourceId)
          {
               return "Resource " + resourceId + " is now streaming online: " + content;
          }
     }
}