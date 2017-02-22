using System;
using System.Windows.Input;
using Plugin.IO.SerialPort;
using Xamarin.Forms;


namespace Samples
{
    public class DeviceItemViewModel
    {
        public DeviceItemViewModel(ISerialDevice device)
        {
            this.Device = device;
            this.GoToDevice = new Command(x =>
                App.Current.MainPage.Navigation.PushAsync(new DevicePage
                {
                    BindingContext = new DeviceViewModel(this.Device)
                })
            );
        }


        public ICommand GoToDevice { get; }
        public ISerialDevice Device { get; }
        public string PortName => this.Device.PortName;
        public string Identifier => this.Device.Identifier;
    }
}
