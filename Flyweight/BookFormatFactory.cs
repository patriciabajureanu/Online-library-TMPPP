using System.Collections.Generic;

namespace OnlineLibrary.Flyweight
{
     public class BookFormatFactory
     {
          private readonly Dictionary<string, IBookFormat> _cache = new Dictionary<string, IBookFormat>();

          public IBookFormat GetOrCreate(string formatType, string language, string publisher)
          {
               string key = $"{formatType}_{language}_{publisher}";

               if (!_cache.ContainsKey(key))
               {
                    _cache[key] = new BookFormat(formatType, language, publisher);
               }

               return _cache[key];
          }

          public int CacheSize()
          {
               return _cache.Count;
          }
     }
}