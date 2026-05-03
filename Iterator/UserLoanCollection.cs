using System.Collections.Generic;
using OnlineLibrary.Models;

namespace OnlineLibrary.Iterator
{
     public class UserLoanCollection : IIterableCollection<Loan>
     {
          private readonly List<Loan> _loans;

          public UserLoanCollection(List<Loan> loans)
          {
               _loans = loans;
          }

          public List<Loan> GetLoans()
          {
               return _loans;
          }

          public IIterator<Loan> CreateIterator()
          {
               return new LoanIterator(this);
          }
     }
}