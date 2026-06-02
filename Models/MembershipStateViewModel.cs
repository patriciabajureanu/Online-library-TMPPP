namespace OnlineLibrary.Models
{
     public class MembershipStateViewModel
     {
          public string Username { get; set; }

          public string Email { get; set; }

          public string Role { get; set; }

          public int ActiveLoans { get; set; }

          public string MembershipState { get; set; }
     }
}