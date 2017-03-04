using System;
using System.Reactive.Linq;
using System.Threading;


namespace Plugin.IO.SerialPort.Rx
{
    public static class RxExtensions
    {
        public static IObservable<byte[]> ReadLoop(this ISerialDevice device, int bufferSize)
        {
            return Observable.Create<byte[]>(async ob =>
            {
                var cancelSrc = new CancellationTokenSource();
                var buffer = new byte[bufferSize];

                while (!cancelSrc.IsCancellationRequested)
                {
                    var read = await device.ReadAsync(buffer, 0, buffer.Length, cancelSrc.Token);
                    if (read > 0)
                        ob.OnNext(buffer);
                }
                return cancelSrc.Cancel;
            });
        }


        public static IObservable<byte[]> Request(this ISerialDevice device, byte[] sendBytes, int readBufferSize)
        {
            return Observable.Create<byte[]>(ob =>
            {
                var buffer = new byte[readBufferSize];
                device.Write(sendBytes, 0, sendBytes.Length);
                device.Read(buffer, 0, buffer.Length);
                ob.OnNext(buffer);
                ob.OnCompleted();

                return () =>
                {

                };
            });
        }
    }
}
