using OnlineLibrary.Models;

namespace OnlineLibrary.Iterator
{
     public class LoanIterator : IIterator<Loan>
     {
          private readonly UserLoanCollection _collection;
          private int _currentPosition;

          public LoanIterator(UserLoanCollection collection)
          {
               _collection = collection;
               _currentPosition = 0;
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