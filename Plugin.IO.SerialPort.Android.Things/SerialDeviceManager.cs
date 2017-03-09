using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Things.Pio;


namespace Plugin.IO.SerialPort
{
    public class SerialDeviceManager : ISerialDeviceManager
    {
        readonly PeripheralManagerService manager = new PeripheralManagerService();


        public Task<IEnumerable<ISerialDevice>> GetAvailableDevices()
        {
            //this.manager.UartDeviceList
            throw new NotImplementedException();
        }
    }
}