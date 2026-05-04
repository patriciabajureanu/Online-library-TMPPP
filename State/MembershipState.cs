namespace OnlineLibrary.State
{
     public abstract class MembershipState
     {
          protected LibraryMembership Membership;

          protected MembershipState(LibraryMembership membership)
          {
               Membership = membership;
          }

          public abstract string Renew();
          public abstract string Suspend();
          public abstract string Expire();
          public abstract string Activate();
     }
}