using System;

namespace OnlineLibrary.Models
{
     public class Reservation
     {
          public int Id { get; set; }

          public int BookId { get; set; }

          public string UserEmail { get; set; }

          public DateTime ReservedAt { get; set; }

          public virtual Book Book { get; set; }
     }
}