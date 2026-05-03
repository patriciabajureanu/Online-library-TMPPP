namespace OnlineLibrary.Models
{
     public class BookViewModel
     {
          public int Id { get; set; }
          public string Title { get; set; }
          public string FormatType { get; set; }
          public string Language { get; set; }
          public string Publisher { get; set; }
          public string Description { get; set; }
          public string ImagePath { get; set; }
          public int PublishedYear { get; set; }
          public string CategoryName { get; set; }

     }
}