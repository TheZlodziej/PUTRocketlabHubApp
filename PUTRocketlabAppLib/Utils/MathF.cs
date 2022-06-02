using System.Numerics;

namespace PUTRocketlabHubAppLib
{
    public static partial class Utils
    {
        public static class MathF
        {
            public static List<Vector3> MapValues(List<Vector3> values, float outMin, float outMax)
            {
                // TODO: test tego
                List<Vector3> mapped = new();

                Vector3 min = values.MinXYZ();
                Vector3 max = values.MaxXYZ();

                foreach (var value in values)
                {
                    mapped.Add(MapValues(value, min.X, max.X, min.Y, max.Y, min.Z, max.Z, outMin, outMax));
                }

                return mapped;
            }

            public static float MapValue(float value, float inMin, float inMax, float outMin, float outMax)
            {
                return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
            }

            public static Vector3 MapValues(Vector3 value, float inXMin, float inXMax, float inYMin, float inYMax, float inZMin, float inZMax, float outMin, float outMax)
            {
                float newX = MapValue(value.X, inXMin, inXMax, outMin, outMax);
                float newY = MapValue(value.Y, inYMin, inYMax, outMin, outMax);
                float newZ = MapValue(value.Z, inZMin, inZMax, outMin, outMax);

                return new(newX, newY, newZ);
            }

            public static float[] MapValues(float[] values, float outMin, float outMax)
            {
                float inMin = values.Min();
                float inMax = values.Max();

                for(int i=0; i<values.Length; i++)
                {
                    values[i] = MapValue(values[i], inMin, inMax, outMin, outMax);
                }

                return values;
            }
        }
    }
}
