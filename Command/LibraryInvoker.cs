using System.Collections.Generic;

namespace OnlineLibrary.Command
{
     public class LibraryInvoker
     {
          private readonly Stack<ICommand> _history = new Stack<ICommand>();

          public void ExecuteCommand(ICommand command)
          {
               command.Execute();
               _history.Push(command);
          }

          public void UndoLastCommand()
          {
               if (_history.Count > 0)
               {
                    var command = _history.Pop();
                    command.Undo();
               }
          }
     }
}