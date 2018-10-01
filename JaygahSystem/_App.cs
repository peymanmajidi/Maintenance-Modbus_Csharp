using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using ModbusTCP;
using SSFGlasses.ServiceReference2;
using System.IO;
using System.Net;
using System.Threading;
using System.Drawing;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Collections;

namespace SSFGlasses
{
    class _App
    {

        public  const byte JACK_START_ADDRESS = 1;
        public  const byte DOOR_START_ADDRESS = 25;
        public  const byte SENSOR_START_ADDRESS = 50;

        public static int _Counter = 0;

        public static string fullname = "برنامه نویس";
        public static int priority = 1;
        public static string type = "inbound";
        public static string semat = "تحقیق و توسعه";
        public static bool canOpen=false;
        public static int currentPosition = 8;
        //  public static Direction currentDirection = Direction.RIGHT;
        public static string programWorkingPath=@"C:\SSFGlasses\";

        public static string date = "تاریخ امروز";
        public static string time = "زمان";
        //public static string  = "تاریخ امروز";

        public static string username = "test";
        public static string password = "";


        public static int capacity = 0;
        public static int free = 0;
        public static int rackNo=0;



        public static string programName = "نام برنامه";
        public static string programVersion = "نسخه";

        public static string lastlogin="none";
        public static int visit=0;

        public static bool CellManuallySet = false;
        public static int documentMaximumCheckTime = 0;
        public static string officeEndTime = "00:00";
        public static string officeStartTime = "00:00";
        public static bool thursdayisHolyday = false;


        public static bool smsTemp = false;
        public static bool smsRack90 = false;
        public static int smsLogin = 0;

        public static string security = "nothing";

        public static string programTag { get; set; }

        
        public static ModbusTCP.Master MBmaster;
        public static void GenerateRegister(int address, int size = 300)
        {
          //  try
            {
               
                _App.MBmaster.ReadHoldingRegister(0, 0, (ushort)address, (ushort)size);
             

            }
         //  catch { }
 
        }

  


        public static void GenerateCoil(int address, int size = 200)
        {
            try
            {
               
                _App.MBmaster.ReadCoils(0, 0, (ushort)address, (ushort)size);


            }
            catch { }

        }
        public static ushort ReadStartAdr(string address)
        {
            // Convert hex numbers into decimal
            if (address.IndexOf("0x", 0, address.Length) == 0)
            {
                string str = address.Replace("0x", "");
                ushort hex = Convert.ToUInt16(str, 16);
                return hex;
            }
            else
            {
                return Convert.ToUInt16(address);
            }
        }
        public static void WriteOnRegister(ushort StartAddress, 
            byte data)
        {
           try
            {
              
                byte[] data2 = { 0, data };
                ushort address = ReadStartAdr((StartAddress).ToString());
               
                MBmaster.WriteSingleRegister(3, 0, address , data2);
                _App.data = data2;
                ShowAs();
            }
           catch
            {


            }
        }

        public static void WriteOnCoil(int StartAddress_int, bool data)
        {
            try
            {
               
                ushort StartAddress = ushort.Parse(StartAddress_int.ToString());
                
            //   MessageBox.Show("id:" + 5 + " Unit:" + 0 + " start:" + StartAddress + " data:" + data);
                MBmaster.WriteSingleCoils(5, 0, StartAddress,data);
                
            }
            catch
            {
            }
        }



        public static string registerValue = null;
        public static byte[] data;
        public static bool Delta=false;
        public static bool[] sensors=new bool[21];
        public static bool[] doors = new bool[21];
        public static bool[] jacks = new bool[21];



        public static void makeDeviceEnable()
        {
            _App.WriteOnRegister(10, 0);
            _App.WriteOnRegister(11, 1);

        }


        // device controller
        public static int Enable; // for ensble device checker on the modbus. each 10sec device checks if connection ok is not. if not reset the connection. it happen on device not in the program
        public static int Flag; // flag set to 0 and device mae it 1; if this is not 1 connection is lost
        public static int Rack_Counter = 0;
        public static int Sensor_High = 0;
        public static int Sensor_Low = 0;
        public static int Sensor_Rota01 = 0;

        public static int[] counters = new int[8];

        public static void ShowAs()
        {
         // try
            {

           //      MessageBox.Show("Test show as ...");

                bool[] bits = new bool[1];
                int[] word = new int[1];

                //if (radio == RadioType.coil)
                //{
                //    BitArray bitArray = new BitArray(data);
                //    bits = new bool[bitArray.Count];
                //    bitArray.CopyTo(bits, 0);
                //    bitArray.CopyTo(jacks, JACK_START_ADDRESS);
                //    bitArray.CopyTo(doors, DOOR_START_ADDRESS);
                //    bitArray.CopyTo(sensors, SENSOR_START_ADDRESS);

                //}
                //if (radio == RadioType.register)
                //{


                if (data.Length < 2) return;

                word = new int[data.Length / 2];
                for (int x = 0; x < data.Length; x = x + 2)
                {
                    word[x / 2] = data[x] * 256 + data[x + 1];
                }


    


           //     Sensor_Rota01 = word[100];
               

                //    //_variables.Flag = word[10];
                //    //_variables.Enable = word[11];
                //    int sub = 2000;

                //    _app.counters[0] = word[ 2003 - sub];
                //    _app.counters[1] = word[2003 - sub];
                //    _app.counters[2] = word[2007 - sub];

                //    _app.counters[3] = word[ 2011 - sub];
                //    _app.counters[4] = word[ 2015 - sub];
                //    _app.counters[5] = word[2019 - sub];
                //    _app.counters[6] = word[ 2023 - sub];
                //    _app.counters[7] = word[2027 - sub];

                //    //_variables.Sensor_Low = word[7];
                //    //_variables.Sensor_High = word[8];


                //}








            }
          // catch
            {


            }




        }
        public static int firstBit=99;

     

        public static bool DeltaConnect(string catchedIP=null)
        {
           try
            {
                string ip  = catchedIP;
                MBmaster = new Master(ip, 502);
                MBmaster.OnResponseData += new ModbusTCP.Master.ResponseData(MBmaster_OnResponseData);
                _App.Delta = true;
                return true;
            }
            catch (SystemException error)
            {
                _App.Delta = false;

                if (MessageBox.Show("اتصال به دستگاه شکست خورد\nخطای برگشت داده شده:\n" + error.Message + "\n\nبرای تلاش دوباره کلیک کنید", "Fatal Error - ModBus", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    DeltaConnect();
            }

            return false;



        }




        public static void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values)
        {
            // MessageBox.Show(ID + " " + unit + function + values.ToString());
            data = values;
            ShowAs();
        }






        


        

        public static int gridCount=20;
        public static bool fatekConnecion = false;
        public static bool saveRegnameFP = false;
        public static string ip_ssf_console= "192.168.1.10";




        public static bool DeviceDoesntConnected(string more = null)
        {
            if (MessageBox.Show("اتصال با دستگاه برقرار نیست\nآیا میخواهید هم اکنون متصل شوید؟","Modbus",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation)==DialogResult.Yes)
            {
               return  _App.DeltaConnect();
            }
            return false;
        }
        public static bool JackOpen(int jack_number)
        {
            if (_App.Delta)
            {
                _App.WriteOnCoil(jack_number + _App.JACK_START_ADDRESS - 1, true);
                return true;
            }

            if (DeviceDoesntConnected()) return JackOpen(jack_number);
            return false;
        }

        public static bool JackClose(int jack_number)
        {
            if (_App.Delta)
            {
                _App.WriteOnCoil(jack_number + _App.JACK_START_ADDRESS - 1, false);
                return true;
            }

            if (DeviceDoesntConnected()) return JackClose(jack_number);
            return false;
        }



        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }


        public static bool DoorOpen(int door_number)
        {
            if (_App.Delta)
            {

                _App.WriteOnCoil(door_number + _App.DOOR_START_ADDRESS - 1, true);
                return true;
            }

            if (DeviceDoesntConnected()) return DoorOpen(door_number);
            return false;
        }

        public static bool DoorClose(int door_number)
        {
            if (_App.Delta)
            {
                _App.WriteOnCoil(door_number + _App.DOOR_START_ADDRESS - 1, false);
                return true;
            }

            if (DeviceDoesntConnected()) return DoorClose(door_number);
            return false;
        }



        
    }
}
