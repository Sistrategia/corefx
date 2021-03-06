// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Xunit;

namespace System.Globalization.Tests
{
    public class DateTimeFormatInfoGetAbbreviatedMonthName
    {
        private const int MinMonth = 1;
        private const int MaxMonth = 13;

        public static IEnumerable<object[]> GetAbbreviatedMonthName_TestData()
        {
            string[] englishAbbreviatedMonthNames = new string[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "" };
            yield return new object[] { DateTimeFormatInfo.InvariantInfo, englishAbbreviatedMonthNames };
            yield return new object[] { new CultureInfo("en-US").DateTimeFormat, englishAbbreviatedMonthNames };
            yield return new object[] { new DateTimeFormatInfo(), englishAbbreviatedMonthNames };

            // ActiveIssue(2103)
            if (!PlatformDetection.IsUbuntu1510 && !PlatformDetection.IsUbuntu1604 && !PlatformDetection.IsFedora23)
            {
                yield return new object[] { new CultureInfo("fr-FR").DateTimeFormat, new string[] { "", "janv.", "f\u00E9vr.", "mars", "avr.", "mai", "juin", "juil.", "ao\u00FBt", "sept.", "oct.", "nov.", "d\u00E9c.", "" } };
            }
        }

        [Theory]
        [MemberData(nameof(GetAbbreviatedMonthName_TestData))]
        public void GetAbbreviatedMonthName(DateTimeFormatInfo info, string[] expected)
        {
            for (int i = MinMonth; i <= MaxMonth; ++i)
            {
                Assert.Equal(expected[i], info.GetAbbreviatedMonthName(i));
            }
        }

        [Fact]
        public void GetAbbreviatedMonthName_Invalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>("month", () => new DateTimeFormatInfo().GetAbbreviatedMonthName(MinMonth - 1)); // Month is invalid
            Assert.Throws<ArgumentOutOfRangeException>("month", (() => new DateTimeFormatInfo().GetAbbreviatedMonthName(MaxMonth + 1))); // Month is invalid
        }
    }
}
