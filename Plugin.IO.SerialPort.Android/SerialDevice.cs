#if __ANDROID__ || __IOS__
using System;
using System.IO;
using System.Threading.Tasks;
using Android.Hardware.Usb;


namespace Plugin.IO.SerialPort
{
    public class SerialDevice : ISerialDevice
    {
        readonly UsbManager manager;
        readonly UsbDevice device;
        UsbDeviceConnection connection;


        public SerialDevice(UsbDevice device, UsbManager manager)
        {
            this.device = device;
            this.manager = manager;
        }


        public Stream InputStream { get; private set; }

        public Stream OutputStream { get; private set; }

        public bool IsConnected { get; private set; }

        public string PortName => "USB";
        public uint BaudRate { get; set; }
        public uint DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBit StopBit { get; set; }


        public void Close()
        {
            this.connection?.Close();
            this.connection = null;
        }

        public async Task Open()
        {
            this.connection = this.manager.OpenDevice(this.device);
            var i = this.device.GetInterface(0);
            var e = i.GetEndpoint(0);
            //this.connection.SetConfiguration(new UsbConfiguration())
            //this.connection.ControlTransfer(UsbAddressing.In, 0, 0, 0, buffer, 0, buffer.Length)
            //this.manager.GetAccessoryList();
            //this.manager.RequestPermission(this.device, PendingIntent.GetActivity());
        }
    }
}
#endif