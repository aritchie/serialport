using System;


namespace Plugin.IO.SerialPort
{
    public static class CrossSerialPort
    {
        static ISerialPort instance;
        public static ISerialPort Current
        {
            get
            {
#if PORTABLE
                if (instance == null)
                    throw new ArgumentException("[Acr.Ble] No platform plugin found.  Did you install the nuget package in your app project as well?");

                return instance;
#else
                instance = instance ?? new SerialPortImpl();
                return instance;
#endif
            }
            set { instance = value; }
        }
    }
}
