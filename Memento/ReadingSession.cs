namespace OnlineLibrary.Memento
{
     public class ReadingSession
     {
          private string _state;

          public void SetState(string state)
          {
               _state = state;
          }

          public ReadingSnapshot CreateSnapshot()
          {
               return new ReadingSnapshot(_state);
          }

          public string GetState()
          {
               return _state;
          }
     }
}