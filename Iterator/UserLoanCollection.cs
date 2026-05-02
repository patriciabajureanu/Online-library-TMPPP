using System.Collections.Generic;
using OnlineLibrary.Models;

namespace OnlineLibrary.Iterator
{
     public class UserLoanCollection : IIterableCollection
     {
          private List<Loan> _loans = new List<Loan>();

          public void AddLoan(Loan loan)
          {
               _loans.Add(loan);
          }

          public List<Loan> GetLoans()
          {
               return _loans;
          }

          public IIterator CreateIterator()
          {
               return new LoanIterator(this);
          }
     }
}