namespace OnlineLibrary.Prototype
{
     public class AnalyticalReport : ReportTemplate
     {
          public string ChartType { get; set; }
          public bool IncludeDataRaw { get; set; }

          public AnalyticalReport()
          {
          }

          public AnalyticalReport(AnalyticalReport prototype)
              : base(prototype)
          {
               ChartType = prototype.ChartType;
               IncludeDataRaw = prototype.IncludeDataRaw;
          }

          public override IPrototype Clone()
          {
               return new AnalyticalReport(this);
          }
     }
}