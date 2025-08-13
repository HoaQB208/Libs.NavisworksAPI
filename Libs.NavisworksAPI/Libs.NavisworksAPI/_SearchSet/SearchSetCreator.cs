using Autodesk.Navisworks.Api;

namespace Libs.NavisworksAPI._SearchSet
{
    public class SearchSetCreator
    {
        public static void CreateSearchSet(string setName, string category, string propertyName, string conditionName, string value)
        {
            Document doc = Autodesk.Navisworks.Api.Application.ActiveDocument;

            // Tạo Search
            Search search = new Search();
            search.Selection.SelectAll();

            SearchCondition condition = SearchCondition
                .HasPropertyByDisplayName("Element", "Category")
                .DisplayStringContains("Walls");

            search.SearchConditions.Add(condition);

            // Tạo Search Set
            SelectionSet searchSet = new SelectionSet(search)
            {
                DisplayName = "My Wall Search Set"
            };





          

        }
    }
}
