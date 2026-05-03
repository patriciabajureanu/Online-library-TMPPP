namespace OnlineLibrary.Bridge
{
     public class CloudDelivery : IContentDelivery
     {
          public string Deliver(string content, string resourceId)
          {
               return "Resource " + resourceId + " is available in cloud: " + content;
          }
     }
}