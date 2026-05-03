using System.Collections.Generic;

namespace OnlineLibrary.Observer
{
     public class EventManager
     {
          private readonly Dictionary<string, List<IEventListener>> _listeners =
               new Dictionary<string, List<IEventListener>>();

          public void Subscribe(string eventType, IEventListener listener)
          {
               if (!_listeners.ContainsKey(eventType))
               {
                    _listeners[eventType] = new List<IEventListener>();
               }

               _listeners[eventType].Add(listener);
          }

          public void Notify(string eventType, int bookId, string bookTitle, string username)
          {
               if (!_listeners.ContainsKey(eventType))
                    return;

               foreach (var listener in _listeners[eventType])
               {
                    listener.Update(eventType, bookId, bookTitle, username);
               }
          }
     }
}