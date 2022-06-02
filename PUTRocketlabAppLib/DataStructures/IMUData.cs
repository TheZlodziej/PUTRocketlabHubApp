using Newtonsoft.Json;
using System.Numerics;

namespace PUTRocketlabHubAppLib.DataStructures
{
    public struct IMUData
    {
        public Vector3? Magnetometer { get; set; }
        public Vector3? Gyroscope { get; set; }
        public Vector3? Acceleration { get; set; }
        public Vector3? AverageAcceleration { get; set; }
        public Vector3? MaxAcceleration { get; set; }
        public static IMUData? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<IMUData>(json);
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
