#region Copyright Dapeng Li, 2011
//
// All rights are reserved.  Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// Filename: IReportBuilder.cs
//
// Created Date: 1/3/2011 12:29:25 PM
// Created By: Dapeng Li
//
#endregion

using System;

namespace WebAnalyticsReportGenerator
{
    /// <summary>
    /// IReportBuilder.
    /// </summary>
    public interface IReportBuilder
    {
        /// <summary>
        /// Builds the visit per date report.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        VisitPerDateReport BuildVisitPerDateReport(DateTime start, DateTime end);

        /// <summary>
        /// Builds the visit per region report.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        VisitPerRegionReport BuildVisitPerRegionReport(DateTime start, DateTime end);
    }
}
