using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI._SearchSet.Class;
using System.Collections.Generic;
using System.Linq;

namespace Libs.NavisworksAPI._SearchSet
{
    public class SearchConditionHelper
    {
        public static List<CategoryProperties> GetAllCategoryAndProperties(Document doc)
        {
            var dict = new Dictionary<string, HashSet<string>>();
            foreach (var root in doc.Models.RootItems)
            {
                Traverse(root, dict);
            }
            // Convert về dạng List<CategoryProperties>
            return dict.Select(kvp => new CategoryProperties
            {
                CategoryName = kvp.Key,
                Properties = kvp.Value.OrderBy(x => x).ToList()
            }).OrderBy(x => x.CategoryName).ToList();
        }

        private static void Traverse(ModelItem item, Dictionary<string, HashSet<string>> dict)
        {
            foreach (PropertyCategory cat in item.PropertyCategories)
            {
                if (!dict.ContainsKey(cat.DisplayName))
                    dict[cat.DisplayName] = new HashSet<string>();

                foreach (DataProperty prop in cat.Properties)
                {
                    if (!string.IsNullOrEmpty(prop.DisplayName))
                        dict[cat.DisplayName].Add(prop.DisplayName);
                }
            }
            foreach (var child in item.Children)
            {
                Traverse(child, dict);
            }
        }
    }
}
