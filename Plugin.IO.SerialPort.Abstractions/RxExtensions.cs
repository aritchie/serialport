using System;


namespace Plugin.IO.SerialPort
{
    public static class RxExtensions
    {
        // REQUEST - write then read

        //public IObservable<object> Write(byte[] data, TimeSpan? timeout = null)
        //{
        //    var dr = new DataWriter(this.device.OutputStream);
        //    dr.WriteBuffer(data.AsBuffer());
        //    return null;
        //}


        //public IObservable<byte[]> Read(uint bufferSize = 1024, TimeSpan? timeout = null)
        //{
        //    return Observable.Create<byte[]>(async ob =>
        //    {
        //        var go = true;
        //        var buffer = new byte[bufferSize].AsBuffer();
        //        var read = await this.device.InputStream.ReadAsync(buffer, bufferSize, InputStreamOptions.None);

        //        while (go && read.Length > 0)
        //        {
        //            ob.OnNext(buffer.ToArray());
        //            read = await this.device.InputStream.ReadAsync(buffer, bufferSize, InputStreamOptions.None);
        //        }
        //        ob.OnCompleted();
        //        // error or try to re-read?

        //        return () => go = false;
        //    });
        //}
    }
}
