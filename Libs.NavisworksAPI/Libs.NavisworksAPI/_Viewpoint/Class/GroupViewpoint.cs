using Autodesk.Navisworks.Api;
using System.Collections.Generic;

namespace Libs.NavisworksAPI._Viewpoint.Class
{
    public class GroupViewpoint
    {
        public string Name { get; }
        public GroupItem Original { get; }
        public List<GroupViewpoint> Groups { get; set; } = new List<GroupViewpoint>();
        public List<SavedViewpoint> Viewpoints { get; set; } = new List<SavedViewpoint>();

        public GroupViewpoint(string name, GroupItem original)
        {
            Name = name;
            Original = original;
        }
    }
}
