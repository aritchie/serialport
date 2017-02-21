using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Native = Windows.Devices.SerialCommunication.SerialDevice;


namespace Plugin.IO.SerialPort
{
    public class SerialDeviceManager : ISerialDeviceManager
    {
        public async Task<IEnumerable<ISerialDevice>> GetAvailableDevices()
        {
            var aqs = Native.GetDeviceSelector();
            var natives = await DeviceInformation.FindAllAsync(aqs);
            return natives.Select(x => new SerialDevice(x));
        }
    }
}
/*
 <Capabilities>
    <Capability Name="internetClient" />
    <DeviceCapability Name="serialcommunication" >
      <Device Id="any">
        <Function Type="name:serialPort"/>
      </Device>
    </DeviceCapability>
   </Capabilities>
*/
