using System;

public class AuditLogger
{
     public void LogAction(string userId, string action)
     {
          Console.WriteLine($"[AUDIT] {userId} -> {action}");
     }
}