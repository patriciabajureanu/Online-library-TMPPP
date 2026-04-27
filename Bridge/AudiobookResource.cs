using OnlineLibrary.Bridge;

namespace OnlineLibrary.Patterns.Bridge
{
     public class AudiobookResource : LibraryResource
     {
          public AudiobookResource(IContentDelivery delivery) : base(delivery) { }

          public override string Access(string resourceId)
          {
               return delivery.Deliver("Audiobook content", resourceId);
          }
     }
}