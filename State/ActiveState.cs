using System;

namespace OnlineLibrary.State
{
     public class ActiveState : MembershipState
     {
          public ActiveState(LibraryMembership membership) : base(membership) { }

          public override string Renew()
          {
               Membership.ExpirationDate = Membership.ExpirationDate.AddMonths(1);
               return "Membership renewed successfully.";
          }

          public override string Suspend()
          {
               Membership.ChangeState(new SuspendedState(Membership));
               return "Membership has been suspended.";
          }

          public override string Expire()
          {
               Membership.ChangeState(new ExpiredState(Membership));
               return "Membership has expired.";
          }

          public override string Activate()
          {
               return "Membership is already active.";
          }
     }
}