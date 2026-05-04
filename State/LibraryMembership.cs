using System;

namespace OnlineLibrary.State
{
     public class LibraryMembership
     {
          private MembershipState _state;

          public string MemberId { get; set; }
          public DateTime ExpirationDate { get; set; }

          public LibraryMembership(string memberId)
          {
               MemberId = memberId;
               ExpirationDate = DateTime.Now.AddMonths(1);
               _state = new ActiveState(this);
          }

          public void ChangeState(MembershipState state)
          {
               _state = state;
          }

          public string Renew()
          {
               return _state.Renew();
          }

          public string Suspend()
          {
               return _state.Suspend();
          }

          public string Expire()
          {
               return _state.Expire();
          }

          public string Activate()
          {
               return _state.Activate();
          }

          public string GetStateName()
          {
               return _state.GetType().Name.Replace("State", "");
          }
     }
}