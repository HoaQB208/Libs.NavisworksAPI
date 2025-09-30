using Autodesk.Navisworks.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libs.NavisworksAPI._SearchSet.Class
{
    public static class SearchConditionComparisonHelper
    {
        private static readonly Dictionary<SearchConditionComparison, string> ComparisonMap =
        new Dictionary<SearchConditionComparison, string>
        {
            { SearchConditionComparison.Equal, "=" },
            { SearchConditionComparison.NotEqual, "not equals" },
            { SearchConditionComparison.NumericLessThan, "<" },
            { SearchConditionComparison.NumericLessThanOrEqual, "<=" },
            { SearchConditionComparison.NumericGreaterThan, ">" },
            { SearchConditionComparison.NumericGreaterThanOrEqual, ">=" },
            { SearchConditionComparison.DisplayStringContains, "contains" },
            { SearchConditionComparison.None, "none" },
            { SearchConditionComparison.HasCategory, "has category" },
            { SearchConditionComparison.NotHasCategory, "not have category" },
            { SearchConditionComparison.HasProperty, "has property" },
            { SearchConditionComparison.NotHasProperty, "not have property" },
            { SearchConditionComparison.SameType, "same type" },
            { SearchConditionComparison.DisplayStringWildcard, "matches pattern" },
            { SearchConditionComparison.DateTimeWithinDay, "within same day" },
            { SearchConditionComparison.DateTimeWithinWeek, "within same week" }
        };

        public static string GetDisplayName(SearchConditionComparison comparison)
        {
            return ComparisonMap.TryGetValue(comparison, out var display) ? display : comparison.ToString();
        }

        public static List<string> GetDisplayNames()
        {
            return ComparisonMap.Values.ToList();
        }

        public static SearchConditionComparison GetSearchConditionComparison(string displayName)
        {
            foreach (var kvp in ComparisonMap)
            {
                if (string.Equals(kvp.Value, displayName, StringComparison.OrdinalIgnoreCase))
                    return kvp.Key;
            }
            return SearchConditionComparison.None;
        }
    }

}
