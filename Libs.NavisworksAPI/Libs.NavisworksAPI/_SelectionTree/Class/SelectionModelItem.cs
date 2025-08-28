using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI._Enum;

namespace Libs.NavisworksAPI._SelectionTree.Class
{
    public class SelectionModelItem
    {
        public ModelType ModelType { get; }
        public string Name { get; }
        public ModelItem Original { get; }

        public SelectionModelItem(ModelItem modelItem)
        {
            Original = modelItem;

            switch (modelItem.ClassDisplayName)
            {
                case "File":
                    ModelType = ModelType.File;
                    break;
            }

            Name = modelItem.DisplayName;
        }
    }
}
