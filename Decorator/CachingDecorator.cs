using System.Collections.Generic;
using OnlineLibrary.Decorator;

public class CachingDecorator : BookAccessDecorator
{
     private static readonly Dictionary<string, string> _cache = new Dictionary<string, string>();

     public CachingDecorator(IBookAccessService inner) : base(inner) { }

     public override string GetBookContent(string bookId)
     {
          if (_cache.ContainsKey(bookId))
          {
               return "[CACHE] " + _cache[bookId];
          }

          var content = base.GetBookContent(bookId);
          _cache[bookId] = content;
          return content;
     }
}