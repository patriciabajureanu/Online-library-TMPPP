using System;

namespace OnlineLibrary.Models
{
     public class Notification
     {
          public int Id { get; set; }

          public string Message { get; set; }

          public string Username { get; set; }

          public DateTime CreatedAt { get; set; }

          public bool IsRead { get; set; }
     }
}