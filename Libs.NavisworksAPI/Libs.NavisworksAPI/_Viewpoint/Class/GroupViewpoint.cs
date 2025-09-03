using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI.Utils;
using System.Collections.Generic;

namespace Libs.NavisworksAPI._Viewpoint.Class
{
    public class GroupViewpoint : ViewModelBase
    {
        public string Name { get; }
        public GroupItem Original { get; }
        public List<GroupViewpoint> Groups { get; set; } = new List<GroupViewpoint>();
        public List<SelectViewpoint> Viewpoints { get; set; } = new List<SelectViewpoint>();

        public GroupViewpoint(string name, GroupItem original)
        {
            Name = name;
            Original = original;
        }

        // IsExpanded
        private bool _IsExpanded = false;
        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set
            {
                _IsExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }
    }
}
