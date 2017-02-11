using System;


namespace Plugin.IO.SerialPort
{
    public class SerialConnectionConfig
    {
        public Handshake Handshake { get; set; } = Handshake.None;
        public Parity Parity { get; set; } = Parity.None;
        public uint BaudRate { get; set; } = 9600;
        public byte ComPort { get; set; } = 1;
        public ushort DataBits { get; set; } = 8;
    }
}
