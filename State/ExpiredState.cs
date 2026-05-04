using System;

namespace OnlineLibrary.State
{
     public class ExpiredState : MembershipState
     {
          public ExpiredState(LibraryMembership membership) : base(membership) { }

          public override string Renew()
          {
               // nu mai folosim expiration date
               Membership.ChangeState(new ActiveState(Membership));
               return "Expired membership renewed and activated.";
          }

          public override string Suspend()
          {
               return "Cannot suspend an expired membership.";
          }

          public override string Expire()
          {
               return "Membership is already expired.";
          }

          public override string Activate()
          {
               Membership.ChangeState(new ActiveState(Membership));
               return "Membership has been activated.";
          }
     }
}