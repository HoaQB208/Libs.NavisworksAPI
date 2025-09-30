using Autodesk.Navisworks.Api.ComApi;
using Autodesk.Navisworks.Api.Interop.ComApi;
using Autodesk.Navisworks.Api;

namespace Libs.NavisworksAPI._Appearance
{
    public static class ComAppearanceHelper
    {
        public static void AssignColorToSearchSet(string searchSetName, Color color, int transparencyPercent = 0)
        {
            // Lấy COM state
            InwOpState10 state = ComApiBridge.State;

            // Tìm selection set theo tên
            InwOpSelectionSet2 selSet = null;
            foreach (InwOpSelectionSet2 ss in state.SelectionSets())
            {
                if (ss.name.Equals(searchSetName, System.StringComparison.OrdinalIgnoreCase))
                {
                    selSet = ss;
                    break;
                }
            }
            if (selSet == null)
            {
                System.Windows.MessageBox.Show($"Không tìm thấy SearchSet: {searchSetName}");
                return;
            }

            //// Lấy Appearance Profiler
            //InwOpAppearanceProfiler profiler = (InwOpAppearanceProfiler)state.AppearanceProfiler;

            //// Tạo profile mới (nếu cần)
            //InwOpAppearanceProfile profile = profiler.ActiveProfile;
            //if (profile == null)
            //{
            //    profile = profiler.Profiles().Add("MyProfile");
            //    profiler.ActiveProfile = profile;
            //}

            //// Tạo AppearanceDefinition mới
            //InwOpAppearanceDefinition def = profile.AddDefinition();
            //def.SelectionSet = selSet;
            //def.Red = color.R / 255.0;
            //def.Green = color.G / 255.0;
            //def.Blue = color.B / 255.0;
            //def.Transparency = transparencyPercent / 100.0;

            //// Áp dụng profile
            //profiler.ApplyActive();
        }
    }
}
