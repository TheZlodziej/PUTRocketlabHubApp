namespace PUTRocketlabHubAppLib
{
    public static class Constant
    {
        public static class Connections
        {
            public const string EndOfMessage = "|";
            public const int ReadTimeout = 60 * 1000; // ms
            public const int WriteTimout = 60 * 1000; // ms
        }

        public static class Calibration 
        {
            public const string EndCalibration = "endConfig";
            public const string CheckCalibration = "checkCalibration";

            public static class Magnetometer
            {
                public static class Headers
                {
                    public const string GetData = "getMagCalibrationData"; // "getMagData"
                    public const string CheckCalibration = "getCalibratedMagData";
                    public const string WriteCalibrationData = "writeCalibration";
                    public const string ReadCalibrationData = "readCalibration"; // for debugging - daje te max, min, itd.
                }
            }
        }
    }
}
