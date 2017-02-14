using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Plugin.IO.SerialPort;


namespace Samples
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public async void Start()
        {
            var devs = await CrossSerialPort.Current.GetAvailableDevices();
            this.Devices = devs.ToList();
            this.OnPropertyChanged(nameof(this.Devices));
        }


        public IList<ISerialDevice> Devices { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
