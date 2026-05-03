namespace OnlineLibrary.Flyweight
{
     public class BookFormatFlyweight
     {
          public string FormatType { get; }
          public string Language { get; }
          public string Publisher { get; }

          public BookFormatFlyweight(string formatType, string language, string publisher)
          {
               FormatType = formatType;
               Language = language;
               Publisher = publisher;
          }
     }
}