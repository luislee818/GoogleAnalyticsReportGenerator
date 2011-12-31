#region Copyright Dapeng Li, 2011
//
// All rights are reserved.  Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// Filename: VisitPerDateReportHtmlRenderer.cs
//
// Created Date: 1/3/2011 2:40:19 PM
// Created By: Dapeng Li
//
#endregion

using System;
using System.Text;

namespace WebAnalyticsReportGenerator
{
    /// <summary>
    /// VisitPerDateReportHtmlRenderer
    /// </summary>
    public class VisitPerDateReportHtmlRenderer : IVisitPerDateReportRenderer
    {
        #region IVisitPerDateReportRenderer Members

        /// <summary>
        /// Renders the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        public string Render(VisitPerDateReport report)
        {
            StringBuilder builder = new StringBuilder();

            RenderHeader(builder, report);

            RenderBody(builder, report);

            RenderFooter(builder, report);

            return builder.ToString();
        }

        #endregion

        /// <summary>
        /// Renders the header.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="report">The report.</param>
        private void RenderHeader(StringBuilder builder, VisitPerDateReport report)
        {
            builder.AppendFormat(
                "<span class='sectionHeader'>{0} (by visit date)</span><br /><br />",
                report.WebsiteName);
        }

        /// <summary>
        /// Renders the body.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="report">The report.</param>
        private void RenderBody(StringBuilder builder, VisitPerDateReport report)
        {
            builder.Append(
                @"<table class='report'>
                    <tr>
                        <td>Date&nbsp;&#9660;</td>
                        <td>Visits</td>
                        <td>Pageviews</td>
                        <td>% Pageviews</td>
                    </tr>");

            foreach (var record in report.Records)
            {
                builder.AppendFormat(
                    @"<tr>
                        <td class='leftJustify'>{0}</td>
                        <td>{1}</td>
                        <td>{2}</td>
                        <td>{3:P}</td>
                    </tr>",
                    GetFormattedDate(record.Date),
                    record.Visits,
                    record.Pageviews,
                    Convert.ToDecimal(record.Pageviews) / Convert.ToDecimal(report.TotalPageviews));
            }

            builder.AppendFormat(
                @"<tr>
                    <td></td>
                    <td>{0}</td>
                    <td>{1}</td>
                    <td>{2:P}</td>
                </tr>",
                report.TotalVisits,
                report.TotalPageviews,
                1);

            builder.Append(@"</table>");
        }

        /// <summary>
        /// Renders the footer.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="report">The report.</param>
        private void RenderFooter(StringBuilder builder, VisitPerDateReport report)
        {
            builder.Append("<br />");
        }

        /// <summary>
        /// Gets the formatted date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        private string GetFormattedDate(DateTime date)
        {
            return string.Format("{0:MM/dd/yyyy} {1}",
                date,
                GetWeekendDescription(date));
        }

        /// <summary>
        /// Gets the weekend description.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        private string GetWeekendDescription(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return "(Sat)";
                case DayOfWeek.Sunday:
                    return "(Sun)";
                default:
                    return string.Empty;
            }
        }
    }
}
