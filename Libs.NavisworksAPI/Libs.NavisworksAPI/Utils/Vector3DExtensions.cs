using Autodesk.Navisworks.Api;

namespace Libs.NavisworksAPI.Utils
{
    public static class Vector3DExtensions
    {
        public static Vector3D Normalize(this Vector3D v)
        {
            double length = System.Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            if (length < 1e-10) return new Vector3D(0, 0, 0); // tránh chia 0
            return new Vector3D(v.X / length, v.Y / length, v.Z / length);
        }

        public static Vector3D Multiply(this Vector3D v, double scalar)
        {
            return new Vector3D(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }

        public static Point3D Add(this Point3D p, Vector3D v)
        {
            return new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }
    }
}
