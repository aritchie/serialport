using System;


namespace Plugin.IO.SerialPort
{
    public enum Handshake
    {
        None,
        RequestToSend,
        RequestToSendXOnXOff,
        XOnXOff
    }
}
