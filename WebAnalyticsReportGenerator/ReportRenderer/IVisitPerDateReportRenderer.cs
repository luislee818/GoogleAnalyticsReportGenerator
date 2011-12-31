#region Copyright Dapeng Li, 2011
//
// All rights are reserved.  Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// Filename: IVisitPerDateReportRenderer.cs
//
// Created Date: 1/3/2011 2:39:37 PM
// Created By: Dapeng Li
//
#endregion


namespace WebAnalyticsReportGenerator
{
    /// <summary>
    /// IVisitPerDateReportRenderer.
    /// </summary>
    public interface IVisitPerDateReportRenderer
    {
        /// <summary>
        /// Renders the specified report.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <returns></returns>
        string Render(VisitPerDateReport report);
    }
}
