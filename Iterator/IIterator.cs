namespace OnlineLibrary.Iterator
{
     public interface IIterator
     {
          Loan GetNext();
          bool HasMore();
     }
}