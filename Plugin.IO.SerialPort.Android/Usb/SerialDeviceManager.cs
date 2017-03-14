//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Android.App;
//using Android.Content;
//using Android.Hardware.Usb;


//namespace Plugin.IO.SerialPort
//{
//    //https://developer.android.com/guide/topics/connectivity/usb/accessory.html
//    public class SerialDeviceManager : ISerialDeviceManager
//    {
//        readonly UsbManager manager;


//        public SerialDeviceManager()
//        {
//            this.manager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
//        }

//        public Task<IEnumerable<ISerialDevice>> GetAvailableDevices()
//        {
//            var devices = this.manager.GetAccessoryList().Select(acc => new SerialDevice(acc, this.manager));
//            return Task.FromResult<IEnumerable<ISerialDevice>>(devices);
//        }
//    }
//}
///*
// * http://stackoverflow.com/questions/10183794/how-to-communicate-with-a-usb-device
//UsbManager manager = (UsbManager) getSystemService(Context.USB_SERVICE);
//List<UsbSerialDriver> availableDrivers = UsbSerialProber.getDefaultProber().findAllDrivers(manager);
//if (availableDrivers.isEmpty()) {
//  return;
//}


//// Read some data! Most have just one port (port 0).
//UsbSerialPort port = driver.getPorts().get(0);
//try {
//  port.open(connection);
//  port.setParameters(115200, 8, UsbSerialPort.STOPBITS_1, UsbSerialPort.PARITY_NONE);

//  byte buffer[] = new byte[16];
//  int numBytesRead = port.read(buffer, 1000);
//  Log.d(TAG, "Read " + numBytesRead + " bytes.");
//} catch (IOException e) {
//  // Deal with error.
//} finally {
//  port.close();
//}


//<activity
//    android:name="..."
//    ...>
//  <intent-filter>
//    <action android:name="android.hardware.usb.action.USB_DEVICE_ATTACHED" />
//  </intent-filter>
//  <meta-data
//      android:name="android.hardware.usb.action.USB_DEVICE_ATTACHED"
//      android:resource="@xml/device_filter" />
//</activity>

//    @Override
//    public void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        if(DEBUG) Log.i(TAG, "onCreate() -> MainMenu");

//        actionBar = getActionBar();                     //obtengo el ActionBar
//        actionBar.setDisplayHomeAsUpEnabled(true);      //el icono de la aplicacion funciona como boton HOME
//        //Menu
//        setListAdapter(new ArrayAdapter<String>(MainMenu.this, android.R.layout.simple_list_item_1, MenuNames));

//        //USB
//        if(DEBUG) Log.i(TAG, "Setting UsbManager -> MainMenu");
//        UsbManager mUsbManager = (UsbManager) getSystemService(Context.USB_SERVICE);
//        PendingIntent mPermissionIntent;

//        if(DEBUG) Log.i(TAG, "Setting PermissionIntent -> MainMenu");
//        mPermissionIntent = PendingIntent.getBroadcast(this, 0, new Intent(ACTION_USB_PERMISSION), 0);
//        if(DEBUG) Log.i(TAG, "Setting IntentFilter -> MainMenu");
//        IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
//        if(DEBUG) Log.i(TAG, "Setting registerReceiver -> MainMenu");
//        registerReceiver(mUsbReceiver, filter);
//        if(DEBUG) Log.i(TAG, "Setting requestPermission -> MainMenu");
//        mUsbManager.requestPermission(device, mPermissionIntent);

//    }
//*/
