namespace OnlineLibrary.Decorator
{
     public abstract class BookAccessDecorator : IBookAccessService
     {
          protected readonly IBookAccessService inner;

          protected BookAccessDecorator(IBookAccessService inner)
          {
               this.inner = inner;
          }

          public virtual string GetBookContent(string bookId)
          {
               return inner.GetBookContent(bookId);
          }
     }
}