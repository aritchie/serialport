using System;
using System.IO;
using System.Threading.Tasks;


namespace Plugin.IO.SerialPort
{
    public interface ISerialDevice
    {
        string PortName { get; }
        Stream InputStream { get; }
        Stream OutputStream { get; }
        bool IsConnected { get; }

        uint BaudRate { get; set; }
        Parity Parity { get; set; }
        StopBit StopBit { get; set; }
        //Handshake Handshake { get; set; }

        //usb.PortName = "/dev/tty.usbmodem1421";
        //usb.BaudRate = 9600;
        //usb.Parity = Parity.None;
        //usb.DataBits = 8;
        //usb.StopBits = StopBits.One;
        //IObservable<object> Write(byte[] data, TimeSpan? timeout = null);
        //IObservable<byte[]> Read(uint bufferSize = 1024, TimeSpan? timeout = null);

        Task Open();
        void Close();
    }
}
