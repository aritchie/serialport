using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.IO.SerialPort;
using PropertyChanged;
using Xamarin.Forms;


namespace Samples
{
    [ImplementPropertyChanged]
    public class DeviceViewModel : INotifyPropertyChanged
    {
        readonly ISerialDevice device;


        public DeviceViewModel(ISerialDevice device)
        {
            this.device = device;
            this.Text = device.PortName;

            this.Send = new Command(() =>
            {
                if (this.ConnectText == "Connect")
                    return;

                var bytes = Encoding.UTF8.GetBytes(this.Command);
                device.OutputStream.Write(bytes, 0, bytes.Length);
            });

            this.Close = new Command(async () =>
            {
                if (!this.device.IsConnected)
                    return;

                this.cancelSrc?.Cancel();
                this.device.Close();
                await App.Current.MainPage.Navigation.PopAsync();
            });

            this.ToggleConnection = new Command(async () =>
            {
                try
                {
                    if (device.IsConnected)
                    {
                        device.Close();
                        this.cancelSrc?.Cancel();
                        this.AppendText("[DISCONNECTED]");
                        this.ConnectText = "Connect";
                    }
                    else
                    {
                        await device.Open();
                        this.ReadLoop();
                        this.AppendText("[CONNECTED]");
                        this.ConnectText = "Disconnect";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    this.ConnectText = "Connect";
                    this.AppendText("[EXCEPTION]: " + ex);
                }
            });
        }


        public ICommand Send { get; }
        public ICommand ToggleConnection { get; }
        public ICommand Close { get; }
        public string Command { get; set; }
        public string ConnectText { get; set; } = "Connect";
        public string Text { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        void AppendText(string message)
        {
            Debug.WriteLine("[MESSAGE]: " + message);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.Text = message + Environment.NewLine + this.Text;
            });
        }


        CancellationTokenSource cancelSrc;

        void ReadLoop()
        {
            this.cancelSrc = new CancellationTokenSource();
            Task.Factory.StartNew(async () =>
            {
                var buffer = new byte[2048];
                while (!this.cancelSrc.IsCancellationRequested)
                {
                    try
                    {
                        var read = await this.device.InputStream.ReadAsync(buffer, 0, buffer.Length, this.cancelSrc.Token);
                        if (read > 0)
                        {
                            var msg = Encoding.UTF8.GetString(buffer);
                            this.AppendText(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.AppendText("[READ ERROR]: " + ex);
                    }
                }
            }, this.cancelSrc.Token);
        }
    }
}
