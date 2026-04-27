using System.Collections.Generic;

namespace OnlineLibrary.Observer
{
     public class EventManager
     {
          private readonly List<EventListener> _listeners = new List<EventListener>();

          public void Subscribe(EventListener listener)
          {
               _listeners.Add(listener);
          }

          public void Unsubscribe(EventListener listener)
          {
               _listeners.Remove(listener);
          }

          public void Notify(string bookId)
          {
               foreach (var listener in _listeners)
               {
                    listener.Update(bookId);
               }
          }
     }
}