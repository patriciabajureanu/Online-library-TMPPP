using System.Collections.Generic;

namespace OnlineLibrary.Flyweight
{
     public static class BookFormatFactory
     {
          private static readonly Dictionary<string, BookFormatFlyweight> _formats =
               new Dictionary<string, BookFormatFlyweight>();

          public static BookFormatFlyweight GetOrCreate(string formatType, string language, string publisher)
          {
               string key = $"{formatType}_{language}_{publisher}";

               if (!_formats.ContainsKey(key))
               {
                    _formats[key] = new BookFormatFlyweight(formatType, language, publisher);
               }

               return _formats[key];
          }

          public static int CacheSize()
          {
               return _formats.Count;
          }
     }
}