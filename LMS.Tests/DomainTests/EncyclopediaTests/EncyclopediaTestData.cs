using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Tests.DomainTests.EncyclopediaTests
{
    public static class EncyclopediaTestData
    {
        public static IEnumerable<object[]> ParseAuthorsCases =>
            new List<object[]>
            {
            new object[] { new string[] { "1" }, "1" },
            new object[] { new string[] { "1", "2" }, "1 and 2" },
            new object[] { new string[] { "1", "2", "3" }, "1, 2, and 3" },
            new object[] { new string[] { "1", "2", "3", "4" }, "1, 2, 3, and 4" },
            new object[] { new string[] { "1", "2", "3", "4", "5" }, "1, 2, 3, 4, and 5" },
            new object[] { new string[] { "1", "2", "3", "4", "5", "6" }, "1, 2, 3, 4, 5, and 6" },
            };

        public static IEnumerable<object[]> CardinalToOrdCases =>
            new List<object[]>
            {
            new object[] { 1, "1st" },
            new object[] { 2, "2nd" },
            new object[] { 3, "3rd" },
            new object[] { 4, "4th" },
            new object[] { 5, "5th" },
            new object[] { 6, "6th" },
            new object[] { 7, "7th" },
            new object[] { 8, "8th" },
            new object[] { 9, "9th" },
            new object[] { 10, "10th" },
            new object[] { 11, "11th" },
            new object[] { 12, "12th" },
            new object[] { 13, "13th" },
            new object[] { 14, "14th" },
            new object[] { 15, "15th" },
            new object[] { 16, "16th" },
            new object[] { 17, "17th" },
            new object[] { 18, "18th" },
            new object[] { 19, "19th" },
            new object[] { 20, "20th" },
            new object[] { 101, "101st" },
            new object[] { 102, "102nd" },
            new object[] { 103, "103rd" },
            new object[] { 104, "104th" },
            new object[] { 105, "105th" }
            };
    }
}
