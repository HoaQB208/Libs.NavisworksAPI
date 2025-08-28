using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI._SelectionTree.Class;
using System.Collections.Generic;
using System.Linq;

namespace Libs.NavisworksAPI._SelectionTree
{
    public class SelectionTreeHelper
    {
        public static List<SelectionModelItem> GetModelList()
        {
            List<SelectionModelItem> result = new List<SelectionModelItem>();

            Document doc = Application.ActiveDocument;
            if (doc != null && doc.Models.RootItems.Count() > 0)
            {
                var root = doc.Models.RootItems.First();
                foreach (var item in root.Children)
                {
                    var selection = new SelectionModelItem(item);
                    result.Add(selection);
                }
            }
            return result;
        }
    }
}
