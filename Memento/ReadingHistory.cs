namespace OnlineLibrary.Memento
{
     public class ReadingHistory
     {
          private ReadingSnapshot _backup;

          public void MakeBackup(ReadingSession session)
          {
               _backup = session.CreateSnapshot();
          }

          public void Undo(ReadingSession session)
          {
               if (_backup != null)
               {
                    session.SetState(_backup.Restore());
               }
          }
     }
}