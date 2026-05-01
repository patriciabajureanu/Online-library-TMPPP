using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
     [Table("Reservations")]
     public class Reservation
     {
          [Key]
          public int Id { get; set; }

          public int UserId { get; set; }

          public int BookId { get; set; }

          public DateTime ReservationDate { get; set; }

          public bool IsActive { get; set; }
     }
}