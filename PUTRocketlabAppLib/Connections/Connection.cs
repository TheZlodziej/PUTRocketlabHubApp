namespace PUTRocketlabHubAppLib.Connections
{
    public abstract class Connection
    {
        public List<string> RawData { get; protected set; } = new();
        protected abstract Task ReadResponse();
        public abstract Task WriteMessage(string message);
    }
}
