using System;
using OnlineLibrary.Data;
using System.Linq;

namespace OnlineLibrary.State
{
     public class LibraryMembership
     {
          private MembershipState _state;
          private readonly OnlineLibraryDbContext _db;

          public string UserEmail { get; set; }

          public LibraryMembership(string userEmail, OnlineLibraryDbContext db)
          {
               UserEmail = userEmail;
               _db = db;

               SetInitialState();
          }

          private void SetInitialState()
          {
               int activeLoans = _db.Loans
                   .Count(l => l.UserEmail == UserEmail && !l.IsReturned);

               if (activeLoans >= 3)
                    _state = new SuspendedState(this);
               else
                    _state = new ActiveState(this);
          }

          public void ChangeState(MembershipState state)
          {
               _state = state;
          }

          public string Renew() => _state.Renew();
          public string Suspend() => _state.Suspend();
          public string Expire() => _state.Expire();
          public string Activate() => _state.Activate();

          public string GetStateName()
          {
               return _state.GetType().Name.Replace("State", "");
          }
     }
}