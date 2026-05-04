namespace OnlineLibrary.ChainOfResponsibility
{
     public class AccessResult
     {
          public bool Granted { get; set; }
          public string Message { get; set; }

          public AccessResult(bool granted, string message)
          {
               Granted = granted;
               Message = message;
          }
     }
}