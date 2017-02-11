using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;


namespace Plugin.IO.SerialPort
{
    public class SerialPortImpl : ISerialPort
    {
        SerialDevice device;
        public static readonly int[] ValidBaudRates = { 4800, 9600, 19200, 38400, 57600, 115200, 230400 };
        public static readonly int[] ValidComPorts = { 1, 2, 3, 4, 5, 6, 7, 8 };


        // detect baud rate
        // loop through baud rates and echo test

        public async Task<bool> Connect(SerialConnectionConfig config)
        {
            config = config ?? new SerialConnectionConfig();

            var aqs = SerialDevice.GetDeviceSelector("COM" + config.ComPort);
            var devices = await DeviceInformation.FindAllAsync(aqs);
            if (!devices.Any())
                return false;

            var id = devices.First().Id;
            this.device = await SerialDevice.FromIdAsync(id);
            this.device.BaudRate = config.BaudRate;
            this.device.DataBits = config.DataBits;
            this.device.Handshake = (SerialHandshake)Enum.Parse(typeof(SerialHandshake), config.Handshake.ToString());
            this.device.Parity = (SerialParity)Enum.Parse(typeof(SerialParity), config.Parity.ToString());
            this.device.StopBits = SerialStopBitCount.One;
            //this.device.ErrorReceived
            //this.device.PinChanged
            //this.reader = new DataReader(this.device.InputStream);
            return true;
        }


        public void Disconnect()
        {
            this.device.Dispose();
        }


        public IObservable<object> Write(byte[] data, TimeSpan? timeout = null)
        {
            var dr = new DataWriter(this.device.OutputStream);
            dr.WriteBuffer(data.AsBuffer());
            return null;
        }


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
/*
private async Task ReadAsync(CancellationToken cancellationToken)
{
    Task<UInt32> loadAsyncTask;

    uint ReadBufferLength = 1024;

    // If task cancellation was requested, comply
    cancellationToken.ThrowIfCancellationRequested();

    // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
    dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

    // Create a task object to wait for data on the serialPort.InputStream
    loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);

    // Launch the task and wait
    UInt32 bytesRead = await loadAsyncTask;
    if (bytesRead > 0)
    {
        rcvdText.Text = dataReaderObject.ReadString(bytesRead);
        status.Text = "bytes read successfully!";
    }
}


    private async void sendTextButton_Click(object sender, RoutedEventArgs e)
{
    // ...

    // Create the DataWriter object and attach to OutputStream
    dataWriteObject = new DataWriter(serialPort.OutputStream);

    //Launch the WriteAsync task to perform the write
    await WriteAsync();

    // ..

    dataWriteObject.DetachStream();
    dataWriteObject = null;
}

private async Task WriteAsync()
{
    Task<UInt32> storeAsyncTask;

    // ...

    // Load the text from the sendText input text box to the dataWriter object
    dataWriteObject.WriteString(sendText.Text);

    // Launch an async task to complete the write operation
    storeAsyncTask = dataWriteObject.StoreAsync().AsTask();

    // ...
}


private async void comPortInput_Click(object sender, RoutedEventArgs e)
{
    // ...

    // Create cancellation token object to close I/O operations when closing the device
    ReadCancellationTokenSource = new CancellationTokenSource();

    // ...
}

private async void rcvdText_TextChanged(object sender, TextChangedEventArgs e)
{
    // ...

    await ReadAsync(ReadCancellationTokenSource.Token);

    // ...
}

private async Task ReadAsync(CancellationToken cancellationToken)
{
    Task<UInt32> loadAsyncTask;

    uint ReadBufferLength = 1024;

    cancellationToken.ThrowIfCancellationRequested();

    // ...

}

private void CancelReadTask()
{
    if (ReadCancellationTokenSource != null)
    {
        if (!ReadCancellationTokenSource.IsCancellationRequested)
        {
            ReadCancellationTokenSource.Cancel();
        }
    }
}
Closing the device
When closing the connection with the device, we cancel all pending I/O operations and safely dispose of all the objects.

In this sample, we proceed to also refresh the list of devices connected.

private void closeDevice_Click(object sender, RoutedEventArgs e)
{
    try
    {
        CancelReadTask();
        CloseDevice();
        ListAvailablePorts(); //Refresh the list of available devices
    }
    catch (Exception ex)
    {
       // ...
    }
}

private void CloseDevice()
{
    if (serialPort != null)
    {
        serialPort.Dispose();
    }

    // ...
}
     */
