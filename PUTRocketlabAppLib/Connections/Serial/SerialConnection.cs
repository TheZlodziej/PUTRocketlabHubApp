using System.IO.Ports;

namespace PUTRocketlabHubAppLib.Connections.Serial
{
    /*
     * SerialConnection is a class template for connecting with PUT Rocketlab boards
     * they allow you to send message and recieve response, convert it to type T in
     * later implemented function ProcessMessage().
     * It also allows you to connect external callback function (DataCallback).
     */
    public abstract class SerialConnection<T> : Connection where T : struct
    {
        public static readonly List<int> BaudRates = new() { 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 57600, 115200, 128000, 256000 };
        public delegate void ProcessData(T data);
        public ProcessData? DataCallback { get; set; }
        public SerialPort Port { get; set; } = new();
        public List<T> Data { get; private set; } = new();
        public SerialConnection(string portName, int portBaudRate)
        {
            Port.PortName = portName;
            Port.BaudRate = portBaudRate;
            Port.ReadTimeout = Constant.Connections.ReadTimeout;
            Port.WriteTimeout = Constant.Connections.WriteTimout;
        }

        protected override async Task ReadResponse()
        {
            // TODO: implement handling wrong message; maybe abstract bool that checks if can convert???
            using StreamReader reader = new(Port.BaseStream);
            reader.BaseStream.Flush();

            while (true)
            {
                var messageTask = reader.ReadLineAsync();
                await messageTask.WaitAsync(TimeSpan.FromMilliseconds(reader.BaseStream.ReadTimeout));

                if (messageTask.IsCanceled)
                {
                    throw new TimeoutException();
                }

                string? message = messageTask.Result;

                if (string.IsNullOrEmpty(message))
                    return;

                if (message == Constant.Connections.EndOfMessage)
                    return;

                RawData.Add(message);

                try
                {
                    ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error occured when processing a message: {ex.Message}");
                }
            }
        }

        public override async Task WriteMessage(string message) // TODO: do wszystkich async dac cancellation token jako parametr
        {
            if (Port.IsOpen)
                throw new Exception("Port is already open.");

            Port.Open();

            Port.WriteLine(message);
            await ReadResponse();

            Port.Close();
        }

        protected abstract void ProcessMessage(string message); // internal calculations & adds to data
    }
}
