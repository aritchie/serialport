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
            this.FindDevices = new Command(async () =>
            {
                this.IsLoading = true;
                var devs = await CrossSerialPort.Current.GetAvailableDevices();
                this.Devices = devs.Select(x => new DeviceItemViewModel(x)).ToList();
                this.IsLoading = false;
                this.NoDevicesFound = this.Devices.Count == 0;

                this.OnPropertyChanged(String.Empty);
            });
        }


        public ICommand FindDevices { get; }
        public bool NoDevicesFound { get; private set; }
        public bool IsLoading { get; private set; }


        public IList<DeviceItemViewModel> Devices { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
