using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Plugin.IO.SerialPort
{
    public interface ISerialDeviceManager
    {
        //serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
        //serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
        //serialPort.BaudRate = 9600;
        //serialPort.Parity = SerialParity.None;
        //serialPort.StopBits = SerialStopBitCount.One;
        //serialPort.DataBits = 8;

        Task<IEnumerable<ISerialDevice>> GetAvailableDevices();
    }
}
/* MAC
 class MainClass
{
    public static void Main()
    {
        SerialPort usb = new SerialPort();


        usb.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        usb.Open();

        Console.WriteLine("Press any key to continue...");
        Console.WriteLine();

        Console.ReadKey();
        usb.Close();
    }

    private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
    {
        Console.WriteLine("HANDLER CALLED");
        SerialPort usb = (SerialPort)sender;
        Console.WriteLine(usb.ReadLine());
    }
}*/