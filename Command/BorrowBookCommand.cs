namespace OnlineLibrary.Command
{
     public class BorrowBookCommand : ICommand
     {
          private readonly LibraryManager _receiver;
          private readonly string _bookId;

          public string Result { get; private set; }

          public BorrowBookCommand(LibraryManager receiver, string bookId)
          {
               _receiver = receiver;
               _bookId = bookId;
          }

          public void Execute()
          {
               Result = _receiver.BorrowBook(_bookId);
          }

          public void Undo()
          {
               Result = $"Undo: borrow action for book {_bookId} was cancelled.";
          }
     }
}