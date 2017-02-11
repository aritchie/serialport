using System;
using System.IO.Ports;
using System.Threading.Tasks;


namespace Plugin.IO.SerialPort
{
    public class SerialPortImpl : ISerialPort
    {
        public Task<bool> Connect(SerialConnectionConfig config = null)
        {
            throw new NotImplementedException();
        }


        public void Disconnect()
        {
            throw new NotImplementedException();
        }


        public IObservable<object> Write(byte[] data, TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }


        public IObservable<byte[]> Read(uint bufferSize = 1024, TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }
    }
}