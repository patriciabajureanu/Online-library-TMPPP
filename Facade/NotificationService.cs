using OnlineLibrary.Services;

public class NotificationService
{
     private readonly EmailService _emailService = new EmailService();

     public void SendBorrowEmail(string userEmail, string bookTitle)
     {
          var subject = "📚 Book Borrowed - OnlineLibrary";

          var html = GetBorrowTemplate(bookTitle);

          _emailService.SendEmail(userEmail, subject, html);
     }

     public void SendReservationEmail(string userEmail, string bookTitle)
     {
          var subject = "📌 Reservation Confirmed - OnlineLibrary";

          var html = GetReservationTemplate(bookTitle);

          _emailService.SendEmail(userEmail, subject, html);
     }

     // 🔽 adaugă metodele de template aici
     public string GetBorrowTemplate(string bookTitle)
     {
          return $@"
    <div style='font-family: Arial; padding:20px; background:#f4f4f4'>
        <div style='background:white; padding:20px; border-radius:10px'>
            <h2 style='color:#2e7d32;'>📚 Book Borrowed Successfully</h2>
            
            <p>You have successfully borrowed:</p>
            <h3>{bookTitle}</h3>

            <p>Enjoy reading! 📖</p>

            <hr />
            <small>OnlineLibrary Team</small>
        </div>
    </div>";
     }
     public string GetReservationTemplate(string bookTitle)
     {
          return $@"
    <div style='font-family: Arial; padding:20px; background:#f4f4f4'>
        <div style='background:white; padding:20px; border-radius:10px'>
            <h2 style='color:#1565c0;'>📌 Reservation Confirmed</h2>
            
            <p>You have reserved:</p>
            <h3>{bookTitle}</h3>

            <p>We will notify you when it becomes available.</p>

            <hr />
            <small>OnlineLibrary Team</small>
        </div>
    </div>";
     }
}