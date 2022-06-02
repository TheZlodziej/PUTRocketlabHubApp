using PUTRocketlabHubAppLib.Connections;

namespace PUTRocketlabHubAppLib.Calibration
{
    public abstract class Calibration<T> where T: Connection
    {
        public T Connection { get; set; }
        
        public Calibration(T connection) 
            => Connection = connection;
        public abstract Task Start();

        public abstract Task CheckCalibration();

        public async virtual Task EndCalibration()
        {
            if (Connection is null)
                return;

            await Connection.WriteMessage(Constant.Calibration.EndCalibration);
        }
    }
}
