using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Interop.ComApi;
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


        /// <summary>
        /// Find SavedViewpoint using COM
        /// </summary>
        /// <param name="view_coll">Statrt with ComApiBridge.State.SavedViews()</param>
        /// <param name="parentPath">Start with empty</param>
        /// <param name="targetFullPath">GetFullPath(savedViewpoint)</param>
        /// <returns>null if not found</returns>
        public static InwOpView FindSavedViewpoint(InwSavedViewsColl view_coll, string targetFullPath, string parentPath = "")
        {
            foreach (InwOpSavedView item in view_coll)
            {
                switch (item.Type)
                {
                    case nwESavedViewType.eSavedViewType_View:
                        {
                            var view = (InwOpView)item;
                            var curPath = parentPath + "\\" + view.name;
                            if (curPath.Equals(targetFullPath))
                            {
                                return view;
                            }
                        }
                        break;
                    case nwESavedViewType.eSavedViewType_Folder:
                        {
                            var folder = (InwOpFolderView)item;
                            var curPath = parentPath + "\\" + folder.name;
                            var view = FindSavedViewpoint(folder.SavedViews(), targetFullPath, curPath);
                            if (view != null) return view;
                        }
                        break;
                }
            }
            return null;
        }

        public static string GetFullPath(SavedViewpoint savedViewpoint)
        {
            List<string> names = new List<string>() { savedViewpoint.DisplayName };
            GroupItem parent = savedViewpoint.Parent;
            while (parent != null)
            {
                names.Insert(0, parent.DisplayName);
                parent = parent.Parent;
            }
            return string.Join("\\", names);
        }
    }
}
