using PUTRocketlabHubAppLib.Connections.Serial;
using System.Numerics;

namespace PUTRocketlabHubAppLib.Calibration
{
    public class MagnetometerCalibration : Calibration<IMUConnection>
    {
        public float NewMin { get; set; } = -40f;
        public float NewMax { get; set; } = 40f;
        public List<Vector3> MagnetometerData { get; set; } = new();
        public List<Vector3> CalibratedMagnetometerData { get; set; } = new();

        public MagnetometerCalibration(IMUConnection connection) : base(connection)
        { }

        private void GetMagnetometerData()
        {
            if (Connection is null)
                return;

            foreach (var data in Connection.Data)
            {
                if (data.Magnetometer is null)
                    continue;

                MagnetometerData.Add((Vector3)data.Magnetometer);
            }
        }

        private async Task Calibrate()
        {
            await SendResponse(MagnetometerData.MinXYZ(), MagnetometerData.MaxXYZ());
            CalibratedMagnetometerData = Utils.MathF.MapValues(MagnetometerData, NewMin, NewMax);
        }

        private async Task SendResponse(Vector3 min, Vector3 max)
        {
            if (Connection is null)
                return;

            string response = MakeCalibrationResponse(min.X, max.X, min.Y, max.Y, min.Z, max.Z);
            await Connection.WriteMessage(response);
        }

        private string MakeCalibrationResponse(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
        {
            return string.Format("{0} {1},{2},{3},{4},{5},{6},{7},{8}",
                Constant.Calibration.Magnetometer.Headers.WriteCalibrationData, // 0
                xMin.ToUSString(), // 1
                yMin.ToUSString(), // 2
                zMin.ToUSString(), // 3
                xMax.ToUSString(), // 4
                yMax.ToUSString(), // 5
                zMax.ToUSString(), // 6
                NewMin.ToUSString(), // 7
                NewMax.ToUSString() // 8
            );
        }

        public override async Task Start()
        {
            if (Connection is null)
                return;

            MagnetometerData.Clear();
            await Connection.WriteMessage(Constant.Calibration.Magnetometer.Headers.GetData);
            GetMagnetometerData();
            await Calibrate();
        }

        public override async Task CheckCalibration()
        {
            if (Connection is null)
                return;

            await Connection.WriteMessage(Constant.Calibration.Magnetometer.Headers.CheckCalibration);
        }
    }
}
