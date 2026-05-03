namespace OnlineLibrary.Iterator
{
     public interface IIterator<T>
     {
          bool HasMore();
          T GetNext();
     }
}