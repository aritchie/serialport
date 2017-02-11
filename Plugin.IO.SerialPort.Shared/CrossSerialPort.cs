using System;


namespace Plugin.IO.SerialPort
{
    public static class CrossSerialPort
    {
        static ISerialDeviceManager instance;
        public static ISerialDeviceManager Current
        {
            get
            {
#if PORTABLE
                if (instance == null)
                    throw new ArgumentException("[Plugin.IO.SerialPort] No platform plugin found.  Did you install the nuget package in your app project as well?");

                return instance;
#else
                instance = instance ?? new SerialDeviceManager();
                return instance;
#endif
            }
            set { instance = value; }
        }
    }
}
