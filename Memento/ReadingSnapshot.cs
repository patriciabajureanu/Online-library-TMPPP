namespace OnlineLibrary.Memento
{
     public class ReadingSnapshot
     {
          private readonly string _state;

          public ReadingSnapshot(string state)
          {
               _state = state;
          }

          public string Restore()
          {
               return _state;
          }
     }
}