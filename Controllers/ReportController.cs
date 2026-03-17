using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Prototype.Registry;
using OnlineLibrary.Prototype.Models;
using OnlineLibrary.Prototype.Interfaces;
using System.Collections.Generic;

namespace OnlineLibrary.Controllers
{
     public class ReportController : Controller
     {
          // Registry de prototipuri
          private static Dictionary<string, IPrototype> _reportRegistry =
              new Dictionary<string, IPrototype>()
          {
            {
                "BasicReport",
                new ReportTemplate(
                    "Library Activity Report",
                    "Blue",
                    new List<string> { "Introduction", "Books", "Users", "Loans" }
                )
            },

            {
                "AnalyticalReport",
                new AnalyticalReport(
                    "Library Analytics",
                    "Green",
                    new List<string> { "Statistics", "Trends", "Predictions" },
                    "BarChart",
                    true
                )
            }
          };

          [HttpPost]
          public IActionResult Clone(string prototypeKey, string customTitle)
          {
               if (!_reportRegistry.ContainsKey(prototypeKey))
               {
                    ViewBag.PrototypeResult = "Prototype not found.";
                    return View("~/Views/Home/Index.cshtml");
               }

               // Clone prototype
               var cloned = _reportRegistry[prototypeKey].Clone();

               // Cast pentru a putea modifica proprietățile
               if (cloned is ReportTemplate report)
               {
                    if (!string.IsNullOrEmpty(customTitle))
                         report.Title = customTitle;

                    ViewBag.PrototypeResult =
                        $"Report cloned: {report.Title} | Color: {report.HeaderColor} | Sections: {string.Join(", ", report.Sections)}";
               }

               return View("~/Views/Home/Index.cshtml");
          }
     }
}