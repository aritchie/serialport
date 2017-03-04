using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Plugin.IO.SerialPort
{
    public interface ISerialDevice
    {
        string PortName { get; }
        string Identifier { get; }
        bool IsConnected { get; }

        uint BaudRate { get; set; }
        ushort DataBits { get; set; }
        bool IsRequestToSendEnabled { get; set; }
        bool IsDataTerminalReadyEnabled { get; set; }
        TimeSpan ReadTimeout { get; set; }
        TimeSpan WriteTimeout { get; set; }
        Parity Parity { get; set; }
        StopBit StopBit { get; set; }
        Handshake Handshake { get; set; }

        Task Open();
        void Close();

        Stream InputStream { get; }
        Stream OutputStream { get; }

        Encoding Encoding { get; set; }
        void WriteLine(string msg);
        string ReadLine();

        int Read(byte[] buffer, int offset, int count);
        void Write(byte[] buffer, int offset, int count);
        Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancelToken);
        Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancelToken);
    }
}
