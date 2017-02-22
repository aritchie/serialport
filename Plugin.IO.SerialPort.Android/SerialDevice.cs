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
        readonly UsbAccessory device;
        SerialStream serialStream;


        public SerialDevice(UsbAccessory device, UsbManager manager)
        {
            this.device = device;
            this.manager = manager;
        }


        public Stream InputStream => this.serialStream;

        public Stream OutputStream => this.serialStream;

        public bool IsConnected { get; private set; }

        public string PortName => "USB";
        public string Identifier => "";
        public uint BaudRate { get; set; }
        public uint DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBit StopBit { get; set; }


        public void Close()
        {
            this.serialStream?.Dispose();
        }

        public async Task Open()
        {
            if (!this.manager.HasPermission(this.device))
            {
                //this.manager.RequestPermission();
                // TODO: callback
            }
            var descriptor = this.manager.OpenAccessory(this.device).FileDescriptor;
            this.serialStream = new SerialStream(descriptor);
        }
    }
}
#endif