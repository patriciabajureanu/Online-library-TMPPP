namespace OnlineLibrary.ChainOfResponsibility
{
     public class MembershipHandler : BaseHandler
     {
          public override AccessResult Handle(AccessRequest request)
          {
               if (string.IsNullOrEmpty(request.UserId))
               {
                    return new AccessResult(false, "User is not logged in.");
               }

               return base.Handle(request);
          }
     }
}