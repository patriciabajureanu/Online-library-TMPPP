using OnlineLibrary.Bridge;

namespace OnlineLibrary.Patterns.Bridge
{
     public class EbookResource : LibraryResource
     {
          public EbookResource(IContentDelivery delivery) : base(delivery) { }

          public override string Access(string resourceId)
          {
               return delivery.Deliver("Ebook content", resourceId);
          }
     }
}