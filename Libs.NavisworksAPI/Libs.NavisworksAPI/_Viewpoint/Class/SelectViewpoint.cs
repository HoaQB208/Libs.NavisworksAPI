using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI.Utils;

namespace Libs.NavisworksAPI._Viewpoint.Class
{
    public class SelectViewpoint : ViewModelBase
    {
        public SavedViewpoint Viewpoint { get; }

        public SelectViewpoint(SavedViewpoint viewpoint)
        {
            Viewpoint = viewpoint;
        }

        // IsSelected
        private bool _IsSelected = false;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}
