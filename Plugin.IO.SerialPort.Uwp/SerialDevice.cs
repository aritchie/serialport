using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using Native = Windows.Devices.SerialCommunication.SerialDevice;


namespace Plugin.IO.SerialPort
{
    public class SerialDevice : ISerialDevice
    {
        readonly DeviceInformation deviceInfo;
        DataReader reader;
        DataWriter writer;
        Native device;


        public SerialDevice(DeviceInformation deviceInfo)
        {
            this.deviceInfo = deviceInfo;
        }


        public string PortName => this.deviceInfo.Name;
        public string Identifier => this.deviceInfo.Id;
        public Stream InputStream { get; private set; }
        public Stream OutputStream { get; private set; }
        public bool IsConnected => this.device != null;

        public uint BaudRate { get; set; } = 115200;
        public bool IsDataTerminalReadyEnabled { get; set; }
        public ushort DataBits { get; set; } = 8;
        public Parity Parity { get; set; } = Parity.None;
        public StopBit StopBit { get; set; } = StopBit.One;
        public Handshake Handshake { get; set; } = Handshake.None;
        public bool IsRequestToSendEnabled { get; set; }
        public TimeSpan ReadTimeout { get; set; } = TimeSpan.FromSeconds(1);
        public TimeSpan WriteTimeout { get; set; } = TimeSpan.FromSeconds(1);
        public Encoding Encoding { get; set; } = Encoding.ASCII;


        public async Task Open()
        {
            this.device = await Native.FromIdAsync(this.deviceInfo.Id);
            this.device.BaudRate = this.BaudRate;
            this.device.DataBits = this.DataBits;
            this.device.IsDataTerminalReadyEnabled = this.IsDataTerminalReadyEnabled;
            this.device.WriteTimeout = this.WriteTimeout;
            this.device.ReadTimeout = this.ReadTimeout;
            this.device.IsRequestToSendEnabled = this.IsRequestToSendEnabled;
            this.device.Handshake = (SerialHandshake)Enum.Parse(typeof(SerialHandshake), this.Handshake.ToString());
            this.device.Parity = (SerialParity)Enum.Parse(typeof(SerialParity), this.Parity.ToString());
            this.device.StopBits = (SerialStopBitCount)Enum.Parse(typeof(SerialStopBitCount), this.StopBit.ToString());

            this.InputStream = this.device.InputStream.AsStreamForRead();
            this.OutputStream = this.device.OutputStream.AsStreamForWrite();

            this.writer = new DataWriter(this.device.OutputStream);
            this.reader = new DataReader(this.device.InputStream)
            {
                InputStreamOptions = InputStreamOptions.Partial
            };

            //this.device.BreakSignalState
            //this.device.BytesReceived
            //this.device.CarrierDetectState
            //this.device.UsbProductId
            //this.device.UsbVendorId
        }


        public void Close()
        {
            this.device?.Dispose();
            this.device = null;
        }


        public string ReadLine()
        {
            var sb = new StringBuilder();
            var s = this.reader.ReadString(256);
            while (!s.Contains(Environment.NewLine))
            {
                sb.Append(s);
                s = this.reader.ReadString(256);
            }
            return sb.ToString();
        }


        public void WriteLine(string msg) => this.writer.WriteString(msg);


        public int Read(byte[] buffer, int offset, int count)
        {
            var read = this.reader.ReadBuffer((uint)count);
            read.CopyTo(0, buffer, offset, count);
            return (int)read.Length;
        }


        public void Write(byte[] buffer, int offset, int count)
            => this.writer.WriteBuffer(buffer.AsBuffer(), (uint)offset, (uint)count);


        public async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancelToken)
        {
            var read = await this.reader.LoadAsync((uint) count).AsTask(cancelToken);
            this.reader.ReadBuffer(read).CopyTo(0, buffer, offset, count);
            return (int)read;
        }


        public async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancelToken)
        {
            this.writer.WriteBytes(buffer);
            await this.writer.FlushAsync();
        }
    }
}