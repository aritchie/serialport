using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Plugin.IO.SerialPort;
using PropertyChanged;
using Xamarin.Forms;


namespace Samples
{
    [ImplementPropertyChanged]
    public class DeviceViewModel : INotifyPropertyChanged
    {
        public DeviceViewModel(ISerialDevice device)
        {
            this.Text = device.PortName;

            this.Send = new Command(() =>
            {
                if (this.ConnectText == "Connect")
                    return;

                var bytes = Encoding.UTF8.GetBytes(this.CommandText);
                device.OutputStream.Write(bytes, 0, bytes.Length);
            });
            this.ToggleConnection = new Command(async () =>
            {
                try
                {
                    if (this.ConnectText == "Connect")
                    {
                        // TODO: set baud, handshake, stopbits,
                        await device.Open();
                        // TODO: start reading InputStream
                        this.ConnectText = "Disconnect";
                    }
                    else
                    {
                        device.Close();
                        this.ConnectText = "Connect";
                    }
                }
                catch (Exception ex)
                {
                    this.Text += Environment.NewLine + ex;
                    this.ConnectText = "Connect";
                }
            });
        }


        public ICommand Send { get; }
        public ICommand ToggleConnection { get; }
        public string CommandText { get; set; }
        public string ConnectText { get; set; } = "Connect";
        public string Text { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
