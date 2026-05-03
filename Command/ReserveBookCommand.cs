namespace OnlineLibrary.Command
{
     public class ReserveBookCommand : ICommand
     {
          private readonly LibraryManager _receiver;
          private readonly int _bookId;
          private readonly string _userEmail;

          public ReserveBookCommand(LibraryManager receiver, int bookId, string userEmail)
          {
               _receiver = receiver;
               _bookId = bookId;
               _userEmail = userEmail;
          }

          public void Execute()
          {
               _receiver.ReserveBook(_bookId, _userEmail);
          }

          public void Undo()
          {
               _receiver.CancelReservation(_bookId, _userEmail);
          }
     }
}