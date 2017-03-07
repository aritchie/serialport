using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Native = System.IO.Ports.SerialPort;
using NativeParity = System.IO.Ports.Parity;
using NativeHandshake = System.IO.Ports.Handshake;


namespace Plugin.IO.SerialPort
{
    public class SerialDevice : ISerialDevice
    {
        readonly Native serialPort;


        public SerialDevice(string portName)
        {
            this.serialPort = new Native(portName);
        }


        public string PortName => this.serialPort.PortName;
        public string Identifier => "";
        public bool IsConnected => this.serialPort.IsOpen;


        public Stream InputStream => this.serialPort.BaseStream;
        public Stream OutputStream => this.serialPort.BaseStream;


        public bool IsRequestToSendEnabled
        {
            get { return this.serialPort.RtsEnable; }
            set { this.serialPort.RtsEnable = value; }
        }


        public TimeSpan WriteTimeout
        {
            get { return TimeSpan.FromMilliseconds(this.serialPort.WriteTimeout); }
            set { this.serialPort.WriteTimeout = Convert.ToInt32(value.TotalMilliseconds); }
        }


        public TimeSpan ReadTimeout
        {
            get { return TimeSpan.FromMilliseconds(this.serialPort.ReadTimeout); }
            set { this.serialPort.ReadTimeout = Convert.ToInt32(value.TotalMilliseconds); }
        }


        public Encoding Encoding
        {
            get { return this.serialPort.Encoding; }
            set { this.serialPort.Encoding = value; }
        }


        public bool IsDataTerminalReadyEnabled
        {
            get { return this.serialPort.DtrEnable; }
            set { this.serialPort.DtrEnable = value; }
        }


        Handshake handshake;
        public Handshake Handshake
        {
            get { return this.handshake; }
            set
            {
                this.handshake = value;
                this.serialPort.Handshake = (NativeHandshake)Enum.Parse(typeof(NativeHandshake), value.ToString());
            }
        }


        public ushort DataBits
        {
            get { return (ushort)this.serialPort.DataBits; }
            set { this.serialPort.DataBits = value; }
        }


        public uint BaudRate
        {
            get { return (uint)this.serialPort.BaudRate; }
            set { this.serialPort.BaudRate = (int)value; }
        }


        Parity parity;
        public Parity Parity
        {
            get { return this.parity; }
            set
            {
                this.parity = value;
                this.serialPort.Parity = (NativeParity)Enum.Parse(typeof(NativeParity), value.ToString());
            }
        }


        StopBit stopBit;
        public StopBit StopBit
        {
            get { return this.stopBit; }
            set
            {
                this.stopBit = value;
                this.serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), value.ToString());
            }
        }


        public Task Open()
        {
            this.serialPort.Open();
            return Task.FromResult(new object());
        }


        public void Close() => this.serialPort.Close();
        public int Read(byte[] buffer, int offset, int count) => this.serialPort.Read(buffer, offset, count);
        public void Write(byte[] buffer, int offset, int count) => this.serialPort.Write(buffer, offset, count);
        public Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancelToken)
            => this.serialPort.BaseStream.ReadAsync(buffer, offset, count, cancelToken);
        public Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancelToken)
            => this.serialPort.BaseStream.WriteAsync(buffer, offset, count, cancelToken);
        public void WriteLine(string msg) => this.serialPort.WriteLine(msg);
        public string ReadLine() => this.serialPort.ReadLine();
    }
}

