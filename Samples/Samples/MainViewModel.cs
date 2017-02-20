using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.IO.SerialPort;
using Xamarin.Forms;


namespace Samples
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.OpenDevice = new Command<ISerialDevice>(x =>
                App.Current.MainPage.Navigation.PushAsync(new DevicePage
                {
                    BindingContext = new DeviceViewModel(x)
                })
            );
            this.FindDevices = new Command(async () =>
            {
                var devs = await CrossSerialPort.Current.GetAvailableDevices();
                this.Devices = devs.ToList();
                if (this.Devices.Count > 0)
                {
                    this.NoDevicesFound = false;
                    this.OnPropertyChanged(nameof(this.Devices));
                }
                else
                {
                    this.NoDevicesFound = true;
                }
                this.OnPropertyChanged(nameof(NoDevicesFound));
            });
        }


        public ICommand FindDevices { get; }
        public ICommand OpenDevice { get; }
        public bool NoDevicesFound { get; private set; }
        public IList<ISerialDevice> Devices { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
