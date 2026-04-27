namespace OnlineLibrary.Bridge
{
     public abstract class LibraryResource
     {
          protected IContentDelivery delivery;

          protected LibraryResource(IContentDelivery delivery)
          {
               this.delivery = delivery;
          }

          public abstract string Access(string resourceId);
     }
}