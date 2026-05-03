public interface IEventListener
{
     void Update(string eventType, int bookId, string bookTitle, string username);
}