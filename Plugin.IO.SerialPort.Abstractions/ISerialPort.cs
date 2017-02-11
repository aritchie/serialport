using System;
using System.Threading.Tasks;


namespace Plugin.IO.SerialPort
{
    public interface ISerialPort
    {
        //serialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
        //serialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
        //serialPort.BaudRate = 9600;
        //serialPort.Parity = SerialParity.None;
        //serialPort.StopBits = SerialStopBitCount.One;
        //serialPort.DataBits = 8;

        Task<bool> Connect(SerialConnectionConfig config = null);
        void Disconnect();
    }
}
/* MAC
 class MainClass
{
    public static void Main()
    {
        SerialPort usb = new SerialPort();
        usb.PortName = "/dev/tty.usbmodem1421";
        usb.BaudRate = 9600;
        usb.Parity = Parity.None;
        usb.DataBits = 8;
        usb.StopBits = StopBits.One;

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