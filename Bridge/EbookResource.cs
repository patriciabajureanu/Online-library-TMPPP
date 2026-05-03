namespace OnlineLibrary.Bridge
{
     public class EbookResource : LibraryResource
     {
          public EbookResource(IContentDelivery delivery) : base(delivery)
          {
          }

          public override string Access(string resourceId)
          {
               return _delivery.Deliver("Ebook content", resourceId);
          }
     }
}