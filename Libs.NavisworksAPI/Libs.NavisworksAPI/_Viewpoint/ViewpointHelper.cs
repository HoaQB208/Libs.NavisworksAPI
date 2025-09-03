using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI._Viewpoint.Class;
using System.Collections.Generic;
using System.Linq;

namespace Libs.NavisworksAPI._Viewpoint
{
    public class ViewpointHelper
    {
        public static List<SelectViewpoint> GetAllViewpoints(GroupViewpoint root)
        {
            if (root == null) return new List<SelectViewpoint>();
            return root.Viewpoints.Concat(root.Groups.SelectMany(g => GetAllViewpoints(g))).ToList();
        }

        public static GroupViewpoint GetRootViewpoints()
        {
            var doc = Autodesk.Navisworks.Api.Application.ActiveDocument;
            var root = new GroupViewpoint("Saved Viewpoints", null);
            foreach (var savedItem in doc.SavedViewpoints.Value)
            {
                if (savedItem is GroupItem group) root.Groups.Add(GetGroupViewpoint(group));
                else if (savedItem is SavedViewpoint viewpoint) root.Viewpoints.Add(new SelectViewpoint(viewpoint));
            }
            return root;
        }

        private static GroupViewpoint GetGroupViewpoint(GroupItem groupItem)
        {
            var groupViewpoint = new GroupViewpoint(groupItem.DisplayName, groupItem);
            foreach (var savedItem in groupItem.Children)
            {
                if (savedItem is GroupItem gr) groupViewpoint.Groups.Add(GetGroupViewpoint(gr));
                else if (savedItem is SavedViewpoint vp) groupViewpoint.Viewpoints.Add(new SelectViewpoint(vp));
            }
            return groupViewpoint;
        }
    }
}
