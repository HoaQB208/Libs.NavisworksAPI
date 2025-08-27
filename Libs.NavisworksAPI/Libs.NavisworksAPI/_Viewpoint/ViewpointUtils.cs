using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI._Viewpoint.Class;
using System.Collections.Generic;
using System.Linq;

namespace Libs.NavisworksAPI._Viewpoint
{
    public class ViewpointUtils
    {
        public static List<SavedViewpoint> GetAllViewpoints(GroupViewpoint root)
        {
            if (root == null) return new List<SavedViewpoint>();
            return root.Viewpoints.Concat(root.Groups.SelectMany(g => GetAllViewpoints(g))).ToList();
        }

        public static GroupViewpoint GetRootViewpoints()
        {
            var doc = Autodesk.Navisworks.Api.Application.ActiveDocument;
            var root = new GroupViewpoint("Root", null);
            foreach (var savedItem in doc.SavedViewpoints.Value)
            {
                if (savedItem is GroupItem group) root.Groups.Add(GetGroupViewpoint(group));
                else if (savedItem is SavedViewpoint viewpoint) root.Viewpoints.Add(viewpoint);
            }
            return root;
        }

        private static GroupViewpoint GetGroupViewpoint(GroupItem groupItem)
        {
            var groupViewpoint = new GroupViewpoint(groupItem.DisplayName, groupItem);
            foreach (var savedItem in groupItem.Children)
            {
                if (savedItem is GroupItem gr) groupViewpoint.Groups.Add(GetGroupViewpoint(gr));
                else if (savedItem is SavedViewpoint vp) groupViewpoint.Viewpoints.Add(vp);
            }
            return groupViewpoint;
        }
    }
}
