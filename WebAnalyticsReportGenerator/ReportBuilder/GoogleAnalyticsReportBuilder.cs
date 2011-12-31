#region Copyright Dapeng Li, 2011
//
// All rights are reserved.  Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// Filename: GoogleAnalyticsReportBuilder.cs
//
// Created Date: 1/3/2011 12:33:36 PM
// Created By: Dapeng Li
//
#endregion

using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Google.GData.Analytics;

namespace WebAnalyticsReportGenerator
{
    /// <summary>
    /// GoogleAnalyticsReportBuilder.
    /// </summary>
    public class GoogleAnalyticsReportBuilder : IReportBuilder
    {
        private readonly string userName;
        private readonly string password;
        private readonly string tableId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleAnalyticsReportBuilder"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="tableId">The table id.</param>
        public GoogleAnalyticsReportBuilder(string userName, string password,
            string tableId)
        {
            this.userName = userName;
            this.password = password;
            this.tableId = tableId;
        }

        /// <summary>
        /// Gets the visit per date data feed.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public DataFeed GetVisitPerDateDataFeed(DateTime start, DateTime end)
        {
            return GetDataFeed(
                start,
                end,
                Constants.Dimensions.Date,
                FormatVariablesString(
                    Constants.Metrics.Visits, 
                    Constants.Metrics.Pageviews),
                string.Format("{0}{1}",
                    Constants.Sorting.Descending,
                    Constants.Dimensions.Date));
        }

        /// <summary>
        /// Gets the visit per city data feed.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public DataFeed GetVisitPerCityDataFeed(DateTime start, DateTime end)
        {
            return GetDataFeed(
                start,
                end,
                FormatVariablesString(
                    Constants.Dimensions.City,
                    Constants.Dimensions.Region,
                    Constants.Dimensions.Country),
                FormatVariablesString(
                    Constants.Metrics.Visits,
                    Constants.Metrics.Pageviews),
                string.Format("{0}{1}",
                    Constants.Sorting.Descending,
                    Constants.Metrics.Pageviews));
        }

        /// <summary>
        /// Gets the data feed.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="dimensionVariables">The dimension variables.</param>
        /// <param name="metricVariables">The metric variables.</param>
        /// <param name="sortByVariables">The sort by variables.</param>
        /// <returns></returns>
        public DataFeed GetDataFeed(DateTime start, DateTime end, 
            string dimensionVariables, string metricVariables, string sortByVariables)
        {
            // Configure GA API.
            AnalyticsService asv = 
                new AnalyticsService(Constants.Service.ApplicationName);

            // Client Login Authorization.
            asv.setUserCredentials(userName, password);

            // GA Data Feed query uri.
            String baseUrl = Constants.Service.QueryUrl;

            DataQuery query = new DataQuery(baseUrl);
            query.Ids = tableId;
            
            //query.Segment = "gaid::-1";
            //query.Filters = "ga:medium==referral";
            //query.NumberToRetrieve = 5;

            query.Dimensions = dimensionVariables;
            query.Metrics = metricVariables;
            query.Sort = sortByVariables;
            query.GAStartDate = start.ToString("yyyy-MM-dd");
            query.GAEndDate = end.ToString("yyyy-MM-dd");

            //Uri url = query.Uri;
            //Console.WriteLine("URL: " + url.ToString());

            // Send our request to the Analytics API and wait for the results to
            // come back.

            DataFeed feed = asv.Query(query);

            return feed;
        }

        #region IReportBuilder Members

        /// <summary>
        /// Builds the visit per date report.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public VisitPerDateReport BuildVisitPerDateReport(DateTime start, DateTime end)
        {
            DataFeed feed = GetVisitPerDateDataFeed(start, end);

            if (feed.Entries.Count == 0)
            {
                throw new Exception("No entries found");
            }
            else
            {
                DataSource dataSource = feed.DataSource;

                VisitPerDateReport report = new VisitPerDateReport(
                    dataSource.TableName.TrimEnd('/'), start, end);

                foreach (DataEntry entry in feed.Entries)
                {
                    DateTime date = DateTime.MinValue;
                    int visits = -1;
                    int pageViews = -1;

                    Dimension dateDimension = entry.Dimensions.FirstOrDefault(
                        d => d.Name == Constants.Dimensions.Date);

                    if (dateDimension != null)
                    {
                        date = DateTime.ParseExact(dateDimension.Value,
                            "yyyyMMdd", CultureInfo.InvariantCulture);
                    }

                    Metric visitsMetric = entry.Metrics.FirstOrDefault(
                        m => m.Name == Constants.Metrics.Visits);

                    if (visitsMetric != null)
                    {
                        visits = Convert.ToInt32(visitsMetric.Value);
                    }

                    Metric pageViewsMetric = entry.Metrics.FirstOrDefault(
                        m => m.Name == Constants.Metrics.Pageviews);

                    if (pageViewsMetric != null)
                    {
                        pageViews = Convert.ToInt32(pageViewsMetric.Value);
                    }

                    report.AddRecord(date, visits, pageViews);
                }

                return report;
            }
        }

        /// <summary>
        /// Builds the visit per region report.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public VisitPerRegionReport BuildVisitPerRegionReport(DateTime start, DateTime end)
        {
            DataFeed feed = GetVisitPerCityDataFeed(start, end);

            if (feed.Entries.Count == 0)
            {
                throw new Exception("No entries found");
            }
            else
            {
                DataSource dataSource = feed.DataSource;

                VisitPerRegionReport report = new VisitPerRegionReport(
                    dataSource.TableName.TrimEnd('/'), start, end);

                foreach (DataEntry entry in feed.Entries)
                {
                    string city = string.Empty;
                    string region = string.Empty;
                    string country = string.Empty;
                    int visits = -1;
                    int pageViews = -1;

                    Dimension cityDimension = entry.Dimensions.FirstOrDefault(
                        d => d.Name == Constants.Dimensions.City);

                    if (cityDimension != null)
                    {
                        city = cityDimension.Value;
                    }

                    Dimension regionDimension = entry.Dimensions.FirstOrDefault(
                        d => d.Name == Constants.Dimensions.Region);

                    if (regionDimension != null)
                    {
                        region = regionDimension.Value;
                    }

                    Dimension countryDimension = entry.Dimensions.FirstOrDefault(
                        d => d.Name == Constants.Dimensions.Country);

                    if (countryDimension != null)
                    {
                        country = countryDimension.Value;
                    }

                    Metric visitsMetric = entry.Metrics.FirstOrDefault(
                        m => m.Name == Constants.Metrics.Visits);

                    if (visitsMetric != null)
                    {
                        visits = Convert.ToInt32(visitsMetric.Value);
                    }

                    Metric pageViewsMetric = entry.Metrics.FirstOrDefault(
                        m => m.Name == Constants.Metrics.Pageviews);

                    if (pageViewsMetric != null)
                    {
                        pageViews = Convert.ToInt32(pageViewsMetric.Value);
                    }

                    report.AddRecord(city, region, country, visits, pageViews);
                }

                CorrectReportRegionNames(report);
                return report;
            }
        }

        #endregion

        /// <summary>
        /// Formats the variables string.
        /// </summary>
        /// <param name="variables">The variables.</param>
        /// <returns></returns>
        private static string FormatVariablesString(params string[] variables)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string variable in variables)
            {
                builder.AppendFormat("{0},", variable);
            }

            if (builder.Length > 0)
            {
                builder.Remove(builder.Length - 1, 1);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Corrects the report region names.
        /// </summary>
        /// <param name="report">The report.</param>
        private static void CorrectReportRegionNames(VisitPerRegionReport report)
        {
            // Google will return Hong Kong as (city: Hong Kong, region: (not set), country: Hong Kong)
            VisitPerRegionReportRecord hongKongRecord = report.Records.FirstOrDefault(
                r => r.Country == "Hong Kong");

            if (hongKongRecord != null)
            {
                hongKongRecord.City = "Hong Kong";
                hongKongRecord.Region = "Hong Kong";
                hongKongRecord.Country = "China";
            }

            // Google will return Macau as (city: Macau, region: (not set), country: Macau)
            VisitPerRegionReportRecord macauRecord = report.Records.FirstOrDefault(
                r => r.Country == "Macau");

            if (macauRecord != null)
            {
                macauRecord.City = "Macau";
                macauRecord.Region = "Macau";
                macauRecord.Country = "China";
            }

            // Google will return Taiwan as (city: [city name], region: [region name], country: Taiwan)
            VisitPerRegionReportRecord taiwanRecord = report.Records.FirstOrDefault(
                r => r.Country == "Taiwan");

            if (taiwanRecord != null)
            {
                taiwanRecord.Country = "China";
            }
        }
    }

    public class Constants
    {
        public class Service
        {
            public const string ApplicationName = "gaExportAPI_acctSample_v2.0";
            public const string QueryUrl = "https://www.google.com/analytics/feeds/data";
        }

        public class Dimensions
        {
            public const string Date = "ga:date";
            public const string City = "ga:city";
            public const string Region = "ga:region";
            public const string Country = "ga:country";
        }

        public class Metrics
        {
            public const string Visits = "ga:visits";
            public const string Pageviews = "ga:pageviews";
        }

        public class Sorting
        {
            public const string Ascending = "";
            public const string Descending = "-";
        }
    }
}
