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
            {

            });
        }


        public async void Start()
        {
            var devs = await CrossSerialPort.Current.GetAvailableDevices();
            this.Devices = devs.ToList();
            this.OnPropertyChanged(nameof(this.Devices));
        }


        public ICommand OpenDevice { get; }
        public IList<ISerialDevice> Devices { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
