namespace OnlineLibrary.Command
{
     public interface ICommand
     {
          void Execute();
          void Undo();
     }
}