using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI._Viewpoint.Class;
using Newtonsoft.Json;

namespace Libs.NavisworksAPI._Camera
{
    public class CameraHelper
    {
        public static Point3D GetLookAt(Viewpoint viewpoint)
        {
            // Focal Distance
            double focalDis = viewpoint.HasFocalDistance ? viewpoint.FocalDistance : 10;
            Point3D pos = viewpoint.Position;
            // Get current view direction
            var cameraJson = viewpoint.GetCamera();
            var camera = JsonConvert.DeserializeObject<CameraData>(cameraJson);
            Vector3D oViewDir = camera.ViewDirection;
            // Loot At
            return new Point3D(pos.X + oViewDir.X * focalDis, pos.Y + oViewDir.Y * focalDis, pos.Z + oViewDir.Z * focalDis);
        }
    }
}
