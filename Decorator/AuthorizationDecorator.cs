namespace OnlineLibrary.Decorator
{
     public class AuthorizationDecorator : BookAccessDecorator
     {
          private readonly bool _hasAccess;

          public AuthorizationDecorator(IBookAccessService inner, bool hasAccess) : base(inner)
          {
               _hasAccess = hasAccess;
          }

          public override string GetBookContent(string bookId)
          {
               if (!_hasAccess)
                    return "Access denied. You must borrow this book first.";

               return base.GetBookContent(bookId);
          }
     }
}