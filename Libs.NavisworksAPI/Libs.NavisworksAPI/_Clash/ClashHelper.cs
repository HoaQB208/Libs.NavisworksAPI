using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI._Clash.Class;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Libs.NavisworksAPI._Clash
{
    public class ClashHelper
    {
        public static ClashResultItem GetClashResult(SavedViewpoint viewpoint)
        {
            try
            {
                var cmt = viewpoint.Comments.FirstOrDefault(x => x.Body.StartsWith("Clash Name"));
                if (cmt != null)
                {
                    var clashItems = GetClashItems(cmt.Body);
                    if (clashItems.Count == 2)
                    {
                        return new ClashResultItem()
                        {
                            Id = cmt.Id,
                            Name = GetClashName(cmt.Body) ?? viewpoint.DisplayName,
                            Status = cmt.Status,
                            Author = cmt.Author,
                            ClashPoint = GetClashPoint(cmt.Body),
                            Item1 = clashItems[0],
                            Item2 = clashItems[1],
                        };
                    }
                }
            }
            catch { }
            return null;
        }

        public static string GetClashName(string cmtBody)
        {
            var match = Regex.Match(cmtBody, @"Clash Name:\s*(.+)");
            if (match.Success) return match.Groups[1].Value.Trim();
            return null;
        }

        public static Point3D GetClashPoint(string cmtBody)
        {
            var match = Regex.Match(cmtBody, @"Clash Point:\s*([\d\.\-]+)\s*m\s+([\d\.\-]+)\s*m\s+([\d\.\-]+)\s*m");
            if (match.Success)
            {
                double x = double.Parse(match.Groups[1].Value);
                double y = double.Parse(match.Groups[2].Value);
                double z = double.Parse(match.Groups[3].Value);
                return new Point3D(x, y, z);
            }
            return null;
        }

        public static List<ClashItem> GetClashItems(string cmtBody)
        {
            var matchItem1 = Regex.Match(cmtBody, @"Item 1:(.*?)Item 2:", RegexOptions.Singleline);
            var matchItem2 = Regex.Match(cmtBody, @"Item 2:(.*)", RegexOptions.Singleline);
            var item1 = ParseItem(matchItem1.Groups[1].Value);
            var item2 = ParseItem(matchItem2.Groups[1].Value);
            return new List<ClashItem>() { item1, item2 };
        }

        private static ClashItem ParseItem(string text)
        {
            var item = new ClashItem();

            // Element ID
            var mId = Regex.Match(text, @"Element ID:\s*(\d+)");
            if (mId.Success) item.ElementId = mId.Groups[1].Value;

            // Layer
            var mLayer = Regex.Match(text, @"Layer:\s*(.+)");
            if (mLayer.Success) item.Layer = mLayer.Groups[1].Value.Trim();

            // Path
            var mPath = Regex.Match(text, @"Path:\s*(.+?)(Quick Properties:)", RegexOptions.Singleline);
            if (mPath.Success) item.Path = mPath.Groups[1].Value.Trim().Replace("\r", "").Replace("\n", " ");

            // Item Name
            var mName = Regex.Match(text, @"Item Name:\s*(.+)");
            if (mName.Success) item.ItemName = mName.Groups[1].Value.Trim();

            return item;
        }
    }
}
