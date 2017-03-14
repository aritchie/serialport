using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Things.Pio;


namespace Plugin.IO.SerialPort
{
    public class SerialDeviceManager : ISerialDeviceManager
    {
        readonly PeripheralManagerService manager = new PeripheralManagerService();


        public Task<IEnumerable<ISerialDevice>> GetAvailableDevices()
        {
            var devices = this.manager
                .UartDeviceList
                .Select(x => new SerialDevice(this.manager, x))
                .ToList();

            return Task.FromResult<IEnumerable<ISerialDevice>>(devices);
        }
    }
}