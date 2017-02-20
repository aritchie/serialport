using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Hardware.Usb;


namespace Plugin.IO.SerialPort
{
    public class SerialDeviceManager : ISerialDeviceManager
    {
        readonly UsbManager manager;


        public SerialDeviceManager()
        {
            this.manager = (UsbManager)Application.Context.GetSystemService(Context.UsbService);
        }

        public Task<IEnumerable<ISerialDevice>> GetAvailableDevices()
        {
            var devices = this.manager.DeviceList.Select(x => new SerialDevice(x.Value, this.manager));
            return Task.FromResult<IEnumerable<ISerialDevice>>(devices);
        }
    }
}
/*
 * http://stackoverflow.com/questions/10183794/how-to-communicate-with-a-usb-device
UsbManager manager = (UsbManager) getSystemService(Context.USB_SERVICE);
List<UsbSerialDriver> availableDrivers = UsbSerialProber.getDefaultProber().findAllDrivers(manager);
if (availableDrivers.isEmpty()) {
  return;
}


// Read some data! Most have just one port (port 0).
UsbSerialPort port = driver.getPorts().get(0);
try {
  port.open(connection);
  port.setParameters(115200, 8, UsbSerialPort.STOPBITS_1, UsbSerialPort.PARITY_NONE);

  byte buffer[] = new byte[16];
  int numBytesRead = port.read(buffer, 1000);
  Log.d(TAG, "Read " + numBytesRead + " bytes.");
} catch (IOException e) {
  // Deal with error.
} finally {
  port.close();
}


// res/xml/device_filter.xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <!-- 0x0403 / 0x6001: FTDI FT232R UART -->
    <usb-device vendor-id="1027" product-id="24577" />

    <!-- 0x0403 / 0x6015: FTDI FT231X -->
    <usb-device vendor-id="1027" product-id="24597" />

    <!-- 0x2341 / Arduino -->
    <usb-device vendor-id="9025" />

    <!-- 0x16C0 / 0x0483: Teensyduino  -->
    <usb-device vendor-id="5824" product-id="1155" />

    <!-- 0x10C4 / 0xEA60: CP210x UART Bridge -->
    <usb-device vendor-id="4292" product-id="60000" />

    <!-- 0x067B / 0x2303: Prolific PL2303 -->
    <usb-device vendor-id="1659" product-id="8963" />

    <!-- 0x1a86 / 0x7523: Qinheng CH340 -->
    <usb-device vendor-id="6790" product-id="29987" />
</resources>


<activity
    android:name="..."
    ...>
  <intent-filter>
    <action android:name="android.hardware.usb.action.USB_DEVICE_ATTACHED" />
  </intent-filter>
  <meta-data
      android:name="android.hardware.usb.action.USB_DEVICE_ATTACHED"
      android:resource="@xml/device_filter" />
</activity>


private static final String ACTION_USB_PERMISSION = "com.multitools.andres.LCView";
    UsbDevice device;
    //Pide permisos al usuario para comunicacion con el dispositivo USB
    private final BroadcastReceiver mUsbReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();
            if (ACTION_USB_PERMISSION.equals(action)) {
                synchronized (this) {
                    UsbDevice device = (UsbDevice)intent.getParcelableExtra(UsbManager.EXTRA_DEVICE);
                    if (intent.getBooleanExtra(UsbManager.EXTRA_PERMISSION_GRANTED, false)) {
                        if(device != null){
                            //call method to set up device communication
                        }
                    } 
                    else {
                        Log.d(TAG, "permission denied for device " + device);
                    }
                }
            }
        }
    };

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(DEBUG) Log.i(TAG, "onCreate() -> MainMenu");

        actionBar = getActionBar();                     //obtengo el ActionBar
        actionBar.setDisplayHomeAsUpEnabled(true);      //el icono de la aplicacion funciona como boton HOME
        //Menu
        setListAdapter(new ArrayAdapter<String>(MainMenu.this, android.R.layout.simple_list_item_1, MenuNames));

        //USB
        if(DEBUG) Log.i(TAG, "Setting UsbManager -> MainMenu");
        UsbManager mUsbManager = (UsbManager) getSystemService(Context.USB_SERVICE);
        PendingIntent mPermissionIntent;

        if(DEBUG) Log.i(TAG, "Setting PermissionIntent -> MainMenu");
        mPermissionIntent = PendingIntent.getBroadcast(this, 0, new Intent(ACTION_USB_PERMISSION), 0);
        if(DEBUG) Log.i(TAG, "Setting IntentFilter -> MainMenu");
        IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
        if(DEBUG) Log.i(TAG, "Setting registerReceiver -> MainMenu");
        registerReceiver(mUsbReceiver, filter);
        if(DEBUG) Log.i(TAG, "Setting requestPermission -> MainMenu");
        mUsbManager.requestPermission(device, mPermissionIntent);

    }
*/
