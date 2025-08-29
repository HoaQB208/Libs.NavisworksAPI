using Autodesk.Navisworks.Api;

namespace Libs.NavisworksAPI._Clash.Class
{
    public class ClashResultItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CommentStatus Status { get; set; }
        public string Author { get; set; }
        public Point3D ClashPoint { get; set; }

        public ClashItem Item1 { get; set; }
        public ClashItem Item2 { get; set; }
    }
}
