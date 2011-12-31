#region Copyright Dapeng Li, 2011
//
// All rights are reserved.  Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// Filename: IVisitPerRegionReportRenderer.cs
//
// Created Date: 1/3/2011 4:45:39 PM
// Created By: Dapeng Li
//
#endregion


namespace WebAnalyticsReportGenerator
{
    /// <summary>
    /// IVisitPerRegionReportRenderer.
    /// </summary>
    public interface IVisitPerRegionReportRenderer
    {
        /// <summary>
        /// Renders the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        string Render(VisitPerRegionReport report);
    }
}
