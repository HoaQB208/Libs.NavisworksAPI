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
                if (savedItem is GroupItem group)
                {
                    var groupViewpoint = GetGroupViewpoint(group);
                    if (groupViewpoint.Groups.Count > 0 || groupViewpoint.Viewpoints.Count > 0)
                    {
                        root.Groups.Add(groupViewpoint);
                    }
                }
                else if (savedItem is SavedViewpoint viewpoint) root.Viewpoints.Add(new SelectViewpoint(viewpoint));
            }
            return root;
        }

        private static GroupViewpoint GetGroupViewpoint(GroupItem groupItem)
        {
            var group = new GroupViewpoint(groupItem.DisplayName, groupItem);
            foreach (var savedItem in groupItem.Children)
            {
                if (savedItem is GroupItem gr)
                {
                    var subGroup = GetGroupViewpoint(gr);
                    if (subGroup.Groups.Count > 0 || subGroup.Viewpoints.Count > 0)
                    {
                        group.Groups.Add(subGroup);
                    }
                }
                else if (savedItem is SavedViewpoint vp) group.Viewpoints.Add(new SelectViewpoint(vp));
            }
            return group;
        }
    }
}
