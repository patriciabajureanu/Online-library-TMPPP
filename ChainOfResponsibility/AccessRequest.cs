namespace OnlineLibrary.ChainOfResponsibility
{
     public class AccessRequest
     {
          public string UserId { get; set; }
          public int BookId { get; set; }

          public AccessRequest(string userId, int bookId)
          {
               UserId = userId;
               BookId = bookId;
          }
     }
}