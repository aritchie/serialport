using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Native = System.IO.Ports.SerialPort;


namespace Plugin.IO.SerialPort
{
    public class SerialDeviceManager : ISerialDeviceManager
    {
        public Task<IEnumerable<ISerialDevice>> GetAvailableDevices()
        {
            var devices = Native
                .GetPortNames()
                .Select(x => new SerialDevice(x));

            return Task.FromResult<IEnumerable<ISerialDevice>>(devices);
        }
    }
}
