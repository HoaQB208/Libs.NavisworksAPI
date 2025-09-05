using System.Collections.Generic;

namespace Libs.NavisworksAPI._SearchSet.Class
{
    public class CategoryProperties
    {
        public string CategoryName { get; set; }
        public List<string> Properties { get; set; } = new List<string>();
    }
}
