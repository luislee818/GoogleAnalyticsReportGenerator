#region Copyright Dapeng Li, 2011
//
// All rights are reserved.  Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// Filename: VisitPerCityReport.cs
//
// Created Date: 1/3/2011 12:12:08 PM
// Created By: Dapeng Li
//
#endregion

using System;
using System.Collections.Generic;

namespace WebAnalyticsReportGenerator
{
    /// <summary>
    /// VisitPerRegionReport
    /// </summary>
    public class VisitPerRegionReport
    {
        /// <summary>
        /// Gets or sets the name of the website.
        /// </summary>
        /// <value>
        /// The name of the website.
        /// </value>
        public string WebsiteName { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the total visits.
        /// </summary>
        /// <value>
        /// The total visits.
        /// </value>
        public int TotalVisits { get; set; }

        /// <summary>
        /// Gets or sets the total pageviews.
        /// </summary>
        /// <value>
        /// The total pageviews.
        /// </value>
        public int TotalPageviews { get; set; }

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        public IList<VisitPerRegionReportRecord> Records { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitPerRegionReport"/> class.
        /// </summary>
        /// <param name="websiteName">Name of the website.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public VisitPerRegionReport(string websiteName, DateTime start, DateTime end)
        {
            this.WebsiteName = websiteName;
            this.StartDate = start;
            this.EndDate = end;
            this.TotalVisits = 0;
            this.TotalPageviews = 0;
            this.Records = new List<VisitPerRegionReportRecord>();
        }

        /// <summary>
        /// Adds the record.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <param name="region">The region.</param>
        /// <param name="country">The country.</param>
        /// <param name="visits">The visits.</param>
        /// <param name="pageviews">The pageviews.</param>
        public void AddRecord(string city, string region, string country, int visits,
            int pageviews)
        {
            this.Records.Add(
                new VisitPerRegionReportRecord() { 
                    City = city,
                    Region = region,
                    Country = country,
                    Visits = visits,
                    Pageviews = pageviews
                });

            this.TotalVisits += visits;
            this.TotalPageviews += pageviews;
        }
    }

    /// <summary>
    /// VisitPerRegionReportRecord.
    /// </summary>
    public class VisitPerRegionReportRecord
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the visits.
        /// </summary>
        /// <value>
        /// The visits.
        /// </value>
        public int Visits { get; set; }

        /// <summary>
        /// Gets or sets the pageviews.
        /// </summary>
        /// <value>
        /// The pageviews.
        /// </value>
        public int Pageviews { get; set; }
    }
}
