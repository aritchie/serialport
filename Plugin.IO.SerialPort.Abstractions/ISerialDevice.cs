using System;
using System.IO;
using System.Threading.Tasks;


namespace Plugin.IO.SerialPort
{
    public interface ISerialDevice
    {
        string PortName { get; }
        string Identifier { get; }
        Stream InputStream { get; }
        Stream OutputStream { get; }
        bool IsConnected { get; }

        uint BaudRate { get; set; }
        ushort DataBits { get; set; }
        Parity Parity { get; set; }
        StopBit StopBit { get; set; }
        Handshake Handshake { get; set; }

        Task Open();
        void Close();
    }
}
