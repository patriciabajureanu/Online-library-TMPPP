namespace OnlineLibrary.Command
{
     public class BorrowBookCommand : ICommand
     {
          private readonly LibraryManager _receiver;
          private readonly int _bookId;
          private readonly string _userEmail;
          private readonly string _role;

          public BorrowBookCommand(LibraryManager receiver, int bookId, string userEmail, string role)
          {
               _receiver = receiver;
               _bookId = bookId;
               _userEmail = userEmail;
               _role = role;
          }

          public void Execute()
          {
               _receiver.BorrowBook(_bookId, _userEmail, _role);
          }

          public void Undo()
          {
               _receiver.UndoBorrowBook(_bookId, _userEmail);
          }
     }
}