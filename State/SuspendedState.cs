namespace OnlineLibrary.State
{
     public class SuspendedState : MembershipState
     {
          public SuspendedState(LibraryMembership membership) : base(membership) { }

          public override string Renew()
          {
               return "Cannot renew while membership is suspended.";
          }

          public override string Suspend()
          {
               return "Membership is already suspended.";
          }

          public override string Expire()
          {
               Membership.ChangeState(new ExpiredState(Membership));
               return "Suspended membership has expired.";
          }

          public override string Activate()
          {
               Membership.ChangeState(new ActiveState(Membership));
               return "Membership has been activated.";
          }
     }
}