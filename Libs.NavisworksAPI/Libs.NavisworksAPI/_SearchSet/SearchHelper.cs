using Autodesk.Navisworks.Api;

namespace Libs.NavisworksAPI._SearchSet
{
    public class SearchHelper
    {
        public static ModelItem GetModelItem(string elementId)
        {
            var doc = Application.ActiveDocument;
            var search = new Search();
            search.Selection.SelectAll();
            search.SearchConditions.Add(SearchCondition.HasPropertyByDisplayName("Element ID", "Value").EqualValue(VariantData.FromDisplayString(elementId)));
            var items = search.FindAll(doc, false);
            return items.Count > 0 ? items[0] : null;
        }
    }
}
