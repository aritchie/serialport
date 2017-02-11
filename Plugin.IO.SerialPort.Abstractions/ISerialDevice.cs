using System;
using System.IO;


namespace Plugin.IO.SerialPort
{
    public interface ISerialDevice
    {

        //Stream InputStream { get; }
        //Stream OutputStream { get; }
        IObservable<object> Write(byte[] data, TimeSpan? timeout = null);
        IObservable<byte[]> Read(uint bufferSize = 1024, TimeSpan? timeout = null);
    }
}
