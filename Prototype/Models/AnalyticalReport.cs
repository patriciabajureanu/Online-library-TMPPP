using System;
using System.Collections.Generic;
using OnlineLibrary.Prototype.Interfaces;
using OnlineLibrary.Prototype.Registry;

namespace OnlineLibrary.Prototype.Models
{
     public class AnalyticalReport : ReportTemplate
     {
          public string ChartType { get; set; }
          public bool IncludeDataRaw { get; set; }

          // Constructor normal
          public AnalyticalReport(
              string title,
              string headerColor,
              List<string> sections,
              string chartType,
              bool includeDataRaw)
              : base(title, headerColor, sections)
          {
               ChartType = chartType;
               IncludeDataRaw = includeDataRaw;
          }

          // Copy constructor
          public AnalyticalReport(AnalyticalReport prototype)
              : base(prototype)
          {
               ChartType = prototype.ChartType;
               IncludeDataRaw = prototype.IncludeDataRaw;
          }

          // Clone method
          public override IPrototype Clone()
          {
               return new AnalyticalReport(this);
          }
     }


}