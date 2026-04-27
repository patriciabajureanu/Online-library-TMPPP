namespace OnlineLibrary.Command
{
     public class ReturnBookCommand : ICommand
     {
          private readonly LibraryManager _receiver;
          private readonly string _bookId;

          public string Result { get; private set; }

          public ReturnBookCommand(LibraryManager receiver, string bookId)
          {
               _receiver = receiver;
               _bookId = bookId;
          }

          public void Execute()
          {
               Result = _receiver.ReturnBook(_bookId);
          }

          public void Undo()
          {
               Result = $"Undo: return action for book {_bookId} was cancelled.";
          }
     }
}