namespace OnlineLibrary.Decorator
{
     public abstract class BookAccessDecorator : IBookAccessService
     {
          protected readonly IBookAccessService _inner;

          protected BookAccessDecorator(IBookAccessService inner)
          {
               _inner = inner;
          }

          public virtual string GetBookContent(string bookId)
          {
               return _inner.GetBookContent(bookId);
          }
     }
}