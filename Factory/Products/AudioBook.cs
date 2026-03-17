using OnlineLibrary.FactoryMethod.Interfaces;

namespace OnlineLibrary.FactoryMethod.Products
{
     public class AudioBook : ILibraryItem
     {
          private string Title;
          private int DurationMinutes;

          public AudioBook(string title, int durationMinutes)
          {
               Title = title;
               DurationMinutes = durationMinutes;
          }

          public string GetTitle()
          {
               return Title;
          }

          public string GetDescription()
          {
               return $"Audio Book: {Title}, Duration: {DurationMinutes} minutes";
          }
     }
}