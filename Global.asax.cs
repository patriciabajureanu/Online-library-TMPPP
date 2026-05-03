using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using OnlineLibrary.Adapter.Adaptee;
using OnlineLibrary.Adapter.Adapters;
using OnlineLibrary.Adapter.Interfaces;
using OnlineLibrary.Adapter.Services;
using OnlineLibrary.Data;
using Rotativa;

namespace OnlineLibrary
{
     public class MvcApplication : System.Web.HttpApplication
     {
          protected void Application_Start()
          {
               Database.SetInitializer<OnlineLibraryDbContext>(null);
               // MVC standard
               AreaRegistration.RegisterAllAreas();
               RouteConfig.RegisterRoutes(RouteTable.Routes);

               // ----------------------
               // 1️⃣ Inițializare Adapter PDF
               // ----------------------
               var pdfReader = new ExternalPdfReader();           // Adaptee (SDK extern)
               var adapter = new PdfReaderAdapter(pdfReader);     // Adapter
               var libraryService = new LibraryReaderService(adapter); // Clientul care folosește adapter-ul

               // ----------------------
               // 2️⃣ Salvăm serviciul în Application pentru controller-e
               // ----------------------
               Application["LibraryService"] = libraryService;
          }
     }
}