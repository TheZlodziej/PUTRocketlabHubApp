using System.Globalization;
using System.Numerics;

namespace PUTRocketlabHubAppLib
{
    public static partial class Utils
    {
        public static string ToUSString(this float value)
        {
            return value.ToString(CultureInfo.GetCultureInfo("en-US"));
        }

        public static Vector3 MinXYZ(this List<Vector3> values)
        {
            static float byX(Vector3 value) => value.X;
            static float byY(Vector3 value) => value.Y;
            static float byZ(Vector3 value) => value.Z;

            float xMin = values.OrderBy(byX).FirstOrDefault().X;
            float yMin = values.OrderBy(byY).FirstOrDefault().Y;
            float zMin = values.OrderBy(byZ).FirstOrDefault().Z;

            return new(xMin, yMin, zMin);
        }

        public static Vector3 MaxXYZ(this List<Vector3> values)
        {
            static float byX(Vector3 value) => value.X;
            static float byY(Vector3 value) => value.Y;
            static float byZ(Vector3 value) => value.Z;

            float xMax = values.OrderBy(byX).LastOrDefault().X;
            float yMax = values.OrderBy(byY).LastOrDefault().Y;
            float zMax = values.OrderBy(byZ).LastOrDefault().Z;

            return new(xMax, yMax, zMax);
        }
    }
}
