using OnlineLibrary.Bridge;

namespace OnlineLibrary.Patterns.Bridge
{
     public class MagazineResource : LibraryResource
     {
          public MagazineResource(IContentDelivery delivery) : base(delivery) { }

          public override string Access(string resourceId)
          {
               return delivery.Deliver("Magazine content", resourceId);
          }
     }
}