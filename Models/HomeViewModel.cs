using System.Collections.Generic;

namespace OnlineLibrary.Models
{
     public class HomeViewModel
     {
          public List<BookViewModel> FeaturedBooks { get; set; }
          public int TotalBooks { get; set; }
          public int TotalSharedFormats { get; set; }
          public BookViewModel BestBook { get; set; }
     }
}