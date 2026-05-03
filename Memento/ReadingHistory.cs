using System.Collections.Generic;

namespace OnlineLibrary.Memento
{
     public class ReadingHistory
     {
          private readonly Stack<ReadingSnapshot> _history = new Stack<ReadingSnapshot>();

          public void MakeBackup(ReadingSession session)
          {
               _history.Push(session.CreateSnapshot());
          }

          public ReadingSnapshot Undo()
          {
               if (_history.Count == 0)
                    return null;

               return _history.Pop();
          }
     }
}