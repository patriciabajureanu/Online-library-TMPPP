namespace OnlineLibrary.Bridge
{
     public class AudiobookResource : LibraryResource
     {
          public AudiobookResource(IContentDelivery delivery) : base(delivery)
          {
          }

          public override string Access(string resourceId)
          {
               return _delivery.Deliver("Audiobook content", resourceId);
          }
     }
}