﻿using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Native = Windows.Devices.SerialCommunication.SerialDevice;


namespace Plugin.IO.SerialPort
{
    public class SerialDevice : ISerialDevice
    {
        readonly DeviceInformation deviceInfo;
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
        public ushort DataBits { get; set; } = 8;
        public Parity Parity { get; set; } = Parity.None;
        public StopBit StopBit { get; set; } = StopBit.One;
        public Handshake Handshake { get; set; } = Handshake.None;


        public async Task Open()
        {
            this.device = await Native.FromIdAsync(this.deviceInfo.Id);
            this.device.BaudRate = this.BaudRate;
            this.device.DataBits = this.DataBits;
            //this.device.BreakSignalState
            //this.device.BytesReceived
            //this.device.CarrierDetectState
            //this.device.UsbProductId
            //this.device.UsbVendorId
            //this.device.ReadTimeout
            //this.device.WriteTimeout
            this.device.Handshake = (SerialHandshake)Enum.Parse(typeof(SerialHandshake), this.Handshake.ToString());
            this.device.Parity = (SerialParity)Enum.Parse(typeof(SerialParity), this.Parity.ToString());
            this.device.StopBits = (SerialStopBitCount)Enum.Parse(typeof(SerialStopBitCount), this.StopBit.ToString());

            this.InputStream = this.device.InputStream.AsStreamForRead();
            this.OutputStream = this.device.OutputStream.AsStreamForWrite();
        }


        public void Close()
        {
            this.device?.Dispose();
            this.device = null;
        }
    }
}
