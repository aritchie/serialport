using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Hardware.Usb;


namespace Plugin.IO.SerialPort
{
    public class SerialDeviceManager : ISerialDeviceManager
    {
        public Task<IEnumerable<ISerialDevice>> GetAvailableDevices()
        {
            //return System.IO.Ports.SerialPo;
            var manager = (UsbManager) Application.Context.GetSystemService(Context.UsbService);
            //manager.DeviceList.Select(x => x.Value.)
            //manager.RequestPermission(device, pendingintent);
            return null;
        }
    }
}
/*
UsbManager manager = (UsbManager) getSystemService(Context.USB_SERVICE);
List<UsbSerialDriver> availableDrivers = UsbSerialProber.getDefaultProber().findAllDrivers(manager);
if (availableDrivers.isEmpty()) {
  return;
}

// Open a connection to the first available driver.
UsbSerialDriver driver = availableDrivers.get(0);
UsbDeviceConnection connection = manager.openDevice(driver.getDevice());
if (connection == null) {
  // You probably need to call UsbManager.requestPermission(driver.getDevice(), ..)
  return;
}

// Read some data! Most have just one port (port 0).
UsbSerialPort port = driver.getPorts().get(0);
try {
  port.open(connection);
  port.setParameters(115200, 8, UsbSerialPort.STOPBITS_1, UsbSerialPort.PARITY_NONE);

  byte buffer[] = new byte[16];
  int numBytesRead = port.read(buffer, 1000);
  Log.d(TAG, "Read " + numBytesRead + " bytes.");
} catch (IOException e) {
  // Deal with error.
} finally {
  port.close();
}


// res/xml/device_filter.xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <!-- 0x0403 / 0x6001: FTDI FT232R UART -->
    <usb-device vendor-id="1027" product-id="24577" />

    <!-- 0x0403 / 0x6015: FTDI FT231X -->
    <usb-device vendor-id="1027" product-id="24597" />

    <!-- 0x2341 / Arduino -->
    <usb-device vendor-id="9025" />

    <!-- 0x16C0 / 0x0483: Teensyduino  -->
    <usb-device vendor-id="5824" product-id="1155" />

    <!-- 0x10C4 / 0xEA60: CP210x UART Bridge -->
    <usb-device vendor-id="4292" product-id="60000" />

    <!-- 0x067B / 0x2303: Prolific PL2303 -->
    <usb-device vendor-id="1659" product-id="8963" />

    <!-- 0x1a86 / 0x7523: Qinheng CH340 -->
    <usb-device vendor-id="6790" product-id="29987" />
</resources>


<activity
    android:name="..."
    ...>
  <intent-filter>
    <action android:name="android.hardware.usb.action.USB_DEVICE_ATTACHED" />
  </intent-filter>
  <meta-data
      android:name="android.hardware.usb.action.USB_DEVICE_ATTACHED"
      android:resource="@xml/device_filter" />
</activity>
*/
