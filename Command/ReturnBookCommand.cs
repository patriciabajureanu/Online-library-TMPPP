namespace OnlineLibrary.Command
{
     public class ReturnBookCommand : ICommand
     {
          private readonly LibraryManager _receiver;
          private readonly int _loanId;
          private readonly string _userEmail;

          public ReturnBookCommand(LibraryManager receiver, int loanId, string userEmail)
          {
               _receiver = receiver;
               _loanId = loanId;
               _userEmail = userEmail;
          }

          public void Execute()
          {
               _receiver.ReturnBook(_loanId, _userEmail);
          }

          public void Undo()
          {
               _receiver.UndoReturnBook(_loanId, _userEmail);
          }
     }
}