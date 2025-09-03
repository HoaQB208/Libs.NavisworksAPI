using Autodesk.Navisworks.Api;
using Libs.NavisworksAPI.Utils;
using Newtonsoft.Json;

namespace Libs.NavisworksAPI._Viewpoint.Class
{
    public class CameraData
    {
        public string Type { get; set; }
        public int Version { get; set; }
        public double UnitsToMetersScaleFactor { get; set; }

        [JsonConverter(typeof(Vector3DConverter))]
        public Vector3D WorldUpDirection { get; set; }

        [JsonConverter(typeof(Vector3DConverter))]
        public Vector3D WorldFrontDirection { get; set; }

        [JsonConverter(typeof(Vector3DConverter))]
        public Vector3D WorldRightDirection { get; set; }

        [JsonConverter(typeof(Point3DConverter))]
        public Point3D Position { get; set; }

        [JsonConverter(typeof(Vector3DConverter))]
        public Vector3D ViewDirection { get; set; }

        [JsonConverter(typeof(Vector3DConverter))]
        public Vector3D UpDirection { get; set; }

        public string Projection { get; set; }
        public double VerticalExtent { get; set; }
        public double HorizontalExtent { get; set; }
        public double TargetDistance { get; set; }
        public double NearDistance { get; set; }
        public string NearDistanceType { get; set; }
        public double FarDistance { get; set; }
        public string FarDistanceType { get; set; }
        public double UpOffset { get; set; }
        public double RightOffset { get; set; }
        public string ImageFit { get; set; }
        public double HorizontalScale { get; set; }
        public double ApertureDiameter { get; set; }
        public double ShutterSpeed { get; set; }

        /// <summary>
        /// Điểm mà Camera ngắm vào: Camera.Position + Camera.ViewDirection * TargetDistance
        /// </summary>
        /// <returns></returns>
        public Point3D GetTarget()
        {
            return Position + ViewDirection.Normalize() * TargetDistance;
        }

        public void ChangeTargetDistance(double delta)
        {
            // Đưa Position tiến gần về phía Target 1 nửa
            TargetDistance += delta;
            Position = Position + ViewDirection.Normalize() * TargetDistance;
        }
    }
}
