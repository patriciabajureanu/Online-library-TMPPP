using OnlineLibrary.Models;

namespace OnlineLibrary.Iterator
{
     public class LoanIterator : IIterator
     {
          private UserLoanCollection _collection;
          private int _currentPosition = 0;

          public LoanIterator(UserLoanCollection collection)
          {
               _collection = collection;
          }

          public bool HasMore()
          {
               return _currentPosition < _collection.GetLoans().Count;
          }

          public Loan GetNext()
          {
               if (!HasMore())
                    return null;

               return _collection.GetLoans()[_currentPosition++];
          }
     }
}