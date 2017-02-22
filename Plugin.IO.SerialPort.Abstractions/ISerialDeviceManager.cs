using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Plugin.IO.SerialPort
{
    public interface ISerialDeviceManager
    {
        // TODO: device attached/detached events
        Task<IEnumerable<ISerialDevice>> GetAvailableDevices();
    }
}