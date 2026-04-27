namespace OnlineLibrary.Flyweight
{
     public interface IBookFormat
     {
          string FormatType { get; }
          string Language { get; }
          string Publisher { get; }
     }
}