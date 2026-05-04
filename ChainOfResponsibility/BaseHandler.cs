namespace OnlineLibrary.ChainOfResponsibility
{
     public abstract class BaseHandler : IHandler
     {
          private IHandler _next;

          public IHandler SetNext(IHandler handler)
          {
               _next = handler;
               return handler;
          }

          public virtual AccessResult Handle(AccessRequest request)
          {
               if (_next != null)
                    return _next.Handle(request);

               return new AccessResult(true, "Access granted.");
          }
     }
}