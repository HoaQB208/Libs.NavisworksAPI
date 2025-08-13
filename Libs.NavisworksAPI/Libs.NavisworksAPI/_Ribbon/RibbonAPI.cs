using Autodesk.Windows;

namespace Libs.NavisworksAPI._Ribbon
{
    public class RibbonAPI
    {
        public static RibbonTab CreateRibbon(string name)
        {
            // Lấy control ribbon chính
            var ribbon = ComponentManager.Ribbon;
            // Tạo tab mới nếu chưa có
            var newTab = ribbon.FindTab(name);
            if (newTab == null)
            {
                newTab = new RibbonTab { Id = name, Title = name };
                ribbon.Tabs.Add(newTab);
            }
            return newTab;
        }
    }
}
