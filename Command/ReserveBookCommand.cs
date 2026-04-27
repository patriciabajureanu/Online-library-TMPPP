namespace OnlineLibrary.Command
{
     public class ReserveBookCommand : ICommand
     {
          private readonly LibraryManager _receiver;
          private readonly string _bookId;

          public string Result { get; private set; }

          public ReserveBookCommand(LibraryManager receiver, string bookId)
          {
               _receiver = receiver;
               _bookId = bookId;
          }

          public void Execute()
          {
               Result = _receiver.ReserveBook(_bookId);
          }

          public void Undo()
          {
               Result = $"Undo: reserve action for book {_bookId} was cancelled.";
          }
     }
}