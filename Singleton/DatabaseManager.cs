using System;

namespace OnlineLibrary.Singleton
{
     public class DatabaseManager
     {
          // Instanță statică Lazy pentru thread-safe singleton
          private static readonly Lazy<DatabaseManager> _instance =
              new Lazy<DatabaseManager>(() => new DatabaseManager());

          private string _connectionString;
          private object _lock = new object();

          // Constructor privat
          private DatabaseManager()
          {
               _connectionString = "Server=localhost;Database=OnlineLibrary;Trusted_Connection=True;";
          }

          // Acces static la instanță
          public static DatabaseManager Instance => _instance.Value;

          // Metode de exemplu
          public void ExecuteQuery(string sql)
          {
               lock (_lock)
               {
                    Console.WriteLine($"Executing query: {sql}");
                    // Aici ai logica reală de execuție, ex: folosind ADO.NET
               }
          }

          public string GetConnectionState()
          {
               return "Connected"; // exemplu simplu
          }
     }
}