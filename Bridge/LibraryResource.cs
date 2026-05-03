namespace OnlineLibrary.Bridge
{
     public abstract class LibraryResource
     {
          protected readonly IContentDelivery _delivery;

          protected LibraryResource(IContentDelivery delivery)
          {
               _delivery = delivery;
          }

          public abstract string Access(string resourceId);
     }
}