namespace OnlineLibrary.Bridge
{
     public class MagazineResource : LibraryResource
     {
          public MagazineResource(IContentDelivery delivery) : base(delivery)
          {
          }

          public override string Access(string resourceId)
          {
               return _delivery.Deliver("Magazine content", resourceId);
          }
     }
}