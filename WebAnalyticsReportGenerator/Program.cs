using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace WebAnalyticsReportGenerator
{
    public class DataFeedExample
    {
        private static readonly string userName = ConfigurationManager.AppSettings["GAUsername"];
        private static readonly string password = ConfigurationManager.AppSettings["GAPassword"];
        private static readonly string site1TableId = ConfigurationManager.AppSettings["Site1TableId"];
        private static readonly string site2TableId = ConfigurationManager.AppSettings["Site2TableId"];
        private static readonly string startDateString = ConfigurationManager.AppSettings["StartDate"];
        private static readonly string endDateString = ConfigurationManager.AppSettings["EndDate"];
        private static readonly string styleSheetPath = ConfigurationManager.AppSettings["StyleSheetPath"];
        private static readonly string outputFolderPath = ConfigurationManager.AppSettings["OutputFolderPath"];

        public static void Main(String[] args)
        {
            try
            {
                DateTime start;

                if (string.IsNullOrEmpty(startDateString) ||
                    !DateTime.TryParse(startDateString, out start))
                {
                    start = DateTime.Now.AddDays(-7);    // assuming this app runs on Mondays
                }

                DateTime end;

                if (string.IsNullOrEmpty(endDateString) ||
                    !DateTime.TryParse(endDateString, out end))
                {
                    end = DateTime.Now.AddDays(-1);    // assuming this app runs on Mondays
                }

                string outputFileName = string.Format(@"{0}\VistorReport_{1:ddMMyyyy}.html",
                    outputFolderPath,
                    DateTime.Now);

                Trace.WriteLine(string.Format(
                    "[{0}] Generating Site 1 & Site 2 visitor reports from {1:MM/dd/yyyy} to {2:MM/dd/yyyy}",
                    DateTime.Now, start, end));

                StringBuilder htmlBody = new StringBuilder();

                VisitPerDateReportHtmlRenderer visitPerDateReportRenderer =
                   new VisitPerDateReportHtmlRenderer();

                VisitPerRegionHtmlReportRenderer visitPerRegionReportRenderer =
                    new VisitPerRegionHtmlReportRenderer();

                Trace.Indent();

                // Site 1 visit by date report
                Trace.WriteLine("Creating Site 1 report builder...");

                IReportBuilder site1ReportBuilder = new GoogleAnalyticsReportBuilder(
                    userName,
                    password,
                    site1TableId);

                Trace.WriteLine("Generating Site 1 visit by date report...");

                VisitPerDateReport site1VisitPerDateReport =
                    site1ReportBuilder.BuildVisitPerDateReport(
                        start, end);

                Trace.WriteLine("Rendering Site 1 visit by date report...");
                htmlBody.Append(visitPerDateReportRenderer.Render(site1VisitPerDateReport));

                // Site 1 visit by region report
                Trace.WriteLine("Generating Site 1 visit by region report...");

                VisitPerRegionReport site1VisitPerRegionReport =
                    site1ReportBuilder.BuildVisitPerRegionReport(
                        start, end);

                Trace.WriteLine("Rendering Site 1 visit by region report...");
                htmlBody.Append(visitPerRegionReportRenderer.Render(site1VisitPerRegionReport));

                // Site 2 visit by date report
                Trace.WriteLine("Creating Site 2 report builder...");

                IReportBuilder site2ReportBuilder = new GoogleAnalyticsReportBuilder(
                    userName,
                    password,
                    site2TableId);

                Trace.WriteLine("Generating Site 2 visit by date report...");

                VisitPerDateReport site2VisitPerDateReport =
                    site2ReportBuilder.BuildVisitPerDateReport(
                        start, end);

                Trace.WriteLine("Rendering Site 2 visit by date report...");
                htmlBody.Append(visitPerDateReportRenderer.Render(site2VisitPerDateReport));

                // Site 1 visit by region report
                Trace.WriteLine("Generating Site 2 visit by region report...");

                VisitPerRegionReport site2VisitPerRegionReport =
                    site2ReportBuilder.BuildVisitPerRegionReport(
                        start, end);

                Trace.WriteLine("Rendering Site 2 visit by region report...");
                htmlBody.Append(visitPerRegionReportRenderer.Render(site2VisitPerRegionReport));

                Trace.WriteLine("Adding styles to final output...");
                string html = HtmlReportsHelper.WrapStyles(htmlBody.ToString(), styleSheetPath);

                Trace.WriteLine(string.Format("Wrting output to file {0}", outputFileName));
                File.WriteAllText(outputFileName, html);

                Trace.Unindent();

                Trace.WriteLine(string.Format("[{0}] Report generation completed.", DateTime.Now));
                Trace.WriteLine(string.Empty);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception Caught!");
                Trace.WriteLine(string.Format("[{0}] {1}", DateTime.Now, ex.Message));
            }
        }
    }
}
