namespace OnlineLibrary.Decorator
{
     public class AuthorizationDecorator : BookAccessDecorator
     {
          public AuthorizationDecorator(IBookAccessService inner) : base(inner)
          {
          }

          public override string GetBookContent(string bookId)
          {
               bool isAuthorized = true;

               if (!isAuthorized)
               {
                    return "Access denied: You are not authorized to read this book.";
               }

               return base.GetBookContent(bookId);
          }
     }
}