using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.Things.Pio;


namespace Plugin.IO.SerialPort
{
    public class SerialDevice : ISerialDevice
    {
        readonly PeripheralManagerService manager;
        UartDevice device;


        public SerialDevice(PeripheralManagerService manager, string portName)
        {
            this.manager = manager;
            this.PortName = portName;
        }


        public string PortName { get; }
        public string Identifier { get; }


        public bool IsConnected { get; private set; }


        uint baudRate = 9600;
        public uint BaudRate
        {
            get { return this.baudRate; }
            set
            {
                this.baudRate = value;
                this.device?.SetBaudrate((int)value);
            }
        }


        ushort dataBits = 8;
        public ushort DataBits
        {
            get { return this.dataBits; }
            set
            {
                this.dataBits = value;
                this.device?.SetDataSize(this.dataBits);
            }
        }


        public bool IsRequestToSendEnabled { get; set; }
        public bool IsDataTerminalReadyEnabled { get; set; }
        public TimeSpan ReadTimeout { get; set; }
        public TimeSpan WriteTimeout { get; set; }

        Parity parity = Parity.None;
        public Parity Parity
        {
            get { return this.parity; }
            set
            {
                this.parity = value;
                //this.device?.SetParity()
            }
        }


        StopBit stopBit = StopBit.One;
        public StopBit StopBit
        {
            get { return this.stopBit; }
            set
            {
                this.stopBit = value;

            }
        }


        public Handshake Handshake { get; set; }


        public Task Open()
        {
            if (this.IsConnected)
                return Task.CompletedTask;

            this.device = this.manager.OpenUartDevice(this.PortName);
            this.IsConnected = true;
            //this.device.SetHardwareFlowControl()
            //this.device.RegisterUartDeviceCallback(new UartDeviceCallback())
            return Task.CompletedTask;
        }

        public void Close()
        {
            this.device?.Close();
            this.device = null;
            this.IsConnected = false;
        }


        public Stream InputStream { get; }
        public Stream OutputStream { get; }
        public Encoding Encoding { get; set; }
        public void WriteLine(string msg)
        {
            throw new NotImplementedException();
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public int Read(byte[] buffer, int offset, int count) => this.device.Read(buffer, count);
        public void Write(byte[] buffer, int offset, int count) => this.device.Write(buffer, count);


        public Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }


        public Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }
    }
}