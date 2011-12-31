#region Copyright Dapeng Li, 2011
//
// All rights are reserved.  Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// Filename: VisitPerRegionReportRenderer.cs
//
// Created Date: 1/3/2011 4:46:10 PM
// Created By: Dapeng Li
//
#endregion

using System;
using System.Text;

namespace WebAnalyticsReportGenerator
{
    /// <summary>
    /// VisitPerRegionHtmlReportRenderer.
    /// </summary>
    public class VisitPerRegionHtmlReportRenderer : IVisitPerRegionReportRenderer
    {
        #region IVisitPerRegionReportRenderer Members

        /// <summary>
        /// Renders the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        public string Render(VisitPerRegionReport report)
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
        private void RenderHeader(StringBuilder builder, VisitPerRegionReport report)
        {
            builder.AppendFormat(
                "<span class='sectionHeader'>{0} (by visit region)</span><br /><br />",
                report.WebsiteName);
        }

        /// <summary>
        /// Renders the body.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="report">The report.</param>
        private void RenderBody(StringBuilder builder, VisitPerRegionReport report)
        {
            builder.Append(
                @"<table class='report'>
                    <tr>
                        <td>City</td>
                        <td>Region</td>
                        <td>Country</td>
                        <td>Visits</td>
                        <td>Pageviews&nbsp;&#9660;</td>
                        <td>% Pageviews</td>
                    </tr>");

            foreach (var record in report.Records)
            {
                builder.AppendFormat(
                    @"<tr>
                        <td class='leftJustify'>{0}</td>
                        <td class='leftJustify'>{1}</td>
                        <td class='leftJustify'>{2}</td>
                        <td>{3}</td>
                        <td>{4}</td>
                        <td>{5:P}</td>
                    </tr>",
                    record.City,
                    record.Region,
                    record.Country,
                    record.Visits,
                    record.Pageviews,
                    Convert.ToDecimal(record.Pageviews) / Convert.ToDecimal(report.TotalPageviews));
            }

            builder.AppendFormat(
                @"<tr>
                    <td></td>
                    <td></td>
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
        private void RenderFooter(StringBuilder builder, VisitPerRegionReport report)
        {
            builder.Append("<br />");
        }
    }
}
