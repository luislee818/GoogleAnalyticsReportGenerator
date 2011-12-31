#region Copyright Dapeng Li, 2011
//
// All rights are reserved.  Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// Filename: HtmlReportsHelper.cs
//
// Created Date: 1/3/2011 3:49:05 PM
// Created By: Dapeng Li
//
#endregion

using System.IO;
using System.Text;

namespace WebAnalyticsReportGenerator
{
    /// <summary>
    /// HtmlReportsHelper.
    /// </summary>
    public class HtmlReportsHelper
    {
        /// <summary>
        /// Wraps the styles.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public static string WrapStyles(string body, string styleSheetPath)
        {
            string styles = File.ReadAllText(styleSheetPath);

            StringBuilder builder = new StringBuilder();

            builder.AppendFormat(
                @"<html>
                    <head>
                        <meta charset='UTF-8'>
                        <style type='text/css'>
                        {0}
                        </style>
                    </head>",
                 styles);

            builder.AppendFormat(
                    @"<body>{0}
                    </body>
                     </html>",
                     body);

            return builder.ToString();
        }
    }
}
