using PUTRocketlabHubAppLib.DataStructures;

namespace PUTRocketlabHubAppLib.Connections.Serial
{
    public class IMUConnection : SerialConnection<IMUData>
    {
        public IMUConnection(string portName, int portBaudRate) : base(portName, portBaudRate)
        {
        }

        protected override void ProcessMessage(string message)
        {
            IMUData? data = IMUData.FromJson(message);

            if (data is null)
                return;

            Data.Add((IMUData) data);
            DataCallback?.Invoke((IMUData) data);
        }
    }
}
