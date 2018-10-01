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
    class _app
    {

        public  const byte JACK_START_ADDRESS = 1;
        public  const byte DOOR_START_ADDRESS = 25;
        public  const byte SENSOR_START_ADDRESS = 50;


        public static RackTBL CurrentRack=null;



        // just for test

        public class FaconServer
        {

            internal void OpenProject(string p)
            {

            }

            internal void Connect()
            {

            }

            internal string GetItem(string p, string p_2)
            {
                return "null";
            }

            internal void SetItem(string p, string p_2, string p_3)
            {
                // throw new NotImplementedException();
            }
        }
        //----------------------------------------------------------
        //*/
        enum Direction { LEFT, RIGHT };

        public static int _Counter = 0;

        public static  FaconServer facon = new FaconServer();
        public static bool islogin = true;
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

        public static string connection = js.Connection();
        public static int hostID = js.getID();
        public static string hostType = js.getType();


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

        public static string faconPath = "Tcp.PLC.Group";

        public static int numberOfMessage = 0;
        public static int userid = 1000;


        public static int tempMax = 60;
        public static string backUpMobileNumber = "09370047967";

        public static void permissionError()
        {
            _app.SystemError("شما مجوز لازم برای انجام این عملیات را ندارید");
            _app.playVoice("oo");
        }
       
        public static int GetFirstCellOfBrand( BrandTBL brand)
        {
            LinqDatabaseDataContext db=new LinqDatabaseDataContext();
            if ( _app.numberOfKalaOfBrand(brand.id) > ( brand.toCell - brand.fromCell ) )
                return -2; // out of load
            if ( brand == null )
                return 0;

            var cells= from k in db.KalaTBLs
                       where k.brand == brand.id
                       join l in db.KalaListTBLs on k.id equals l.kalaid
                       orderby l.cell
                       select new
                       {
                           l.cell
                       }

                       ;

            int cell=-1;


            for ( int i = brand.fromCell; i <= brand.toCell; i++ )
            {
                if ( cells.Where(c => c.cell == i).Count() > 0 )
                    continue;
                cell = i;
                break;

            }

            return cell;
        }

        public static bool[] getSensorsState()
        {
            _app.GenerateRegister(100);
            int x = _app.Sensor_Low;

            string binary = Convert.ToString(x, 2);
            string rBinary="";
            foreach (var b in binary)
            {
                rBinary = b.ToString() + rBinary.ToString();
            }
            binary = rBinary;
            int i = 0;
            bool[] result=new bool[21];
            foreach (var s in binary)
            {
                result[i++] = (s == '0' ? false : true); 
            }
            //MessageBox.Show(binary);
            x = _app.Sensor_High;

             binary = Convert.ToString(x, 2);
             rBinary = "";
            foreach (var b in binary)
            {
                rBinary = b.ToString() + rBinary.ToString();
            }
            binary = rBinary;
             i = 16;
         
            foreach (var s in binary)
            {
                result[i++] = (s == '0' ? false : true);
            }
           // MessageBox.Show(binary);
            return result;
        }


        public static int numberOfKalaOfBrand( int brandID )
        {
            LinqDatabaseDataContext db=new LinqDatabaseDataContext();
            var data=from l in db.KalaListTBLs
                     join k in db.KalaTBLs on l.kalaid equals k.id
                     select new
                     {
                         k.brand,
                         l.kalaid
                     };

            int numberOfKala= data.Where(d=>d.brand == brandID).Count();
            return numberOfKala;


        }


        public static void hosttableupdate()
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var host = (from h in db.HostTBLs
                            where h.id == _app.hostID
                            select h).ToList().Single();

                host.id = _app.hostID;
                host.hostname = _app.hostname();
                host.hostType = _app.hostType;
                host.ipAddress = _app.getIP();
                host.stat = true;
                db.SubmitChanges();
            }
            catch
            {

                try
                {
                    HostTBL host = new HostTBL();
                    host.id = _app.hostID;
                    host.hostname = _app.hostname();
                    host.hostType = _app.hostType;
                    host.ipAddress = _app.getIP();



                    host.stat = true;


                    db.HostTBLs.InsertOnSubmit(host);
                    db.SubmitChanges();
                }
                catch
                {
                    Environment.FailFast("");
                    return;
                }
            }
        }
        public static void trigEnable()
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {
                var tt = db.triggerTBLs.First();
                tt.hostID = _app.hostID;
                tt.ttr++;

                db.SubmitChanges();
            }
            catch
            {
                triggerTBL tt = new triggerTBL();
                tt.hostID = _app.hostID;
                tt.ttr++;
                db.triggerTBLs.InsertOnSubmit(tt);
                db.SubmitChanges();

            }

        }



        public static bool connectToDevice()
        {



            _app.deviceConnection = _app.myDevice.Connect_Net(_app.ip_ssf_console, 4370);
            _app.myDevice.RegEvent(555, 65535);
            return _app.deviceConnection;

        }


        public static void playVoice( string msg )
        {
            if ( _app.deviceConnection == false ) return;
            _app.myDevice.CancelOperation();

            switch ( msg )
            {
                case "wrong pass": _app.myDevice.PlayVoiceByIndex(1); break;
                case "ok": _app.myDevice.PlayVoiceByIndex(0); break;
                case "oo": _app.myDevice.PlayVoiceByIndex(10); break;
                case "warning": _app.myDevice.PlayVoiceByIndex(11); break;
                case "try again": _app.myDevice.PlayVoiceByIndex(4); break;
                case "wrong user": _app.myDevice.PlayVoiceByIndex(3); break;
                case "alert": _app.myDevice.PlayVoiceByIndex(13); break;

            }
        }





        public static void trigRequest()
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {
                var tt = db.triggerTBLs.First();

                tt.reqTrig = true;

                db.SubmitChanges();
            }
            catch
            {
                TrigNew();
                trigRequest();

            }

        }



        public static void trigClearHostname()
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);

            try
            {
                var tt = db.triggerTBLs.First();
                tt.host = null;
                db.SubmitChanges();

            }
            catch
            {
                TrigNew();
                trigClearHostname();

            }
        }

        public static bool TrigNew()
        {
            try
            {
                LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
                triggerTBL tt = new triggerTBL();
                tt.hostID = _app.hostID;
                tt.ttr = 0;
                tt.host = null;
                db.triggerTBLs.InsertOnSubmit(tt);
                db.SubmitChanges();
                return true;
            }
            catch
            {

                Environment.Exit(0);
                return false;
            }
        }

        public static void trigFourceOutstep2()
        {

            Thread.Sleep(3000);
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            var tt = db.triggerTBLs.First();
            tt.hostID = 0;
            tt.forceExit = false;
            db.SubmitChanges();





        }



        public static void trigFourceOut()
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var tt = db.triggerTBLs.First();
                tt.hostID = _app.hostID;
                tt.forceExit = true;
                db.SubmitChanges();
                Thread t = new Thread(trigFourceOutstep2);
                t.Start();

            }
            catch
            {
                trigClearHostname();
                trigFourceOut();

            }



        }


        public static void hostMakeOffline()
        {


            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var host = (from h in db.HostTBLs
                            where h.id == _app.hostID
                            select h).ToList().Single();


                host.id = _app.hostID;
                host.hostname = _app.hostname();
                host.hostType = _app.hostType;
                host.stat = false;

                db.SubmitChanges();


            }
            catch
            {
                TrigNew();
                hostMakeOffline();
            }
        }

        public static ModbusTCP.Master MBmaster;
        public static void GenerateRegister(int address, int size = 300)
        {
          //  try
            {
               
                _app.MBmaster.ReadHoldingRegister(0, 0, (ushort)address, (ushort)size);
             

            }
         //  catch { }
 
        }

  


        public static void GenerateCoil(int address, int size = 200)
        {
            try
            {
               
                _app.MBmaster.ReadCoils(0, 0, (ushort)address, (ushort)size);


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
                _app.data = data2;
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
            _app.WriteOnRegister(10, 0);
            _app.WriteOnRegister(11, 1);

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
                _app.Delta = true;
                return true;
            }
            catch (SystemException error)
            {
                _app.Delta = false;

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












        public static void SSF()
        {

    
           
           

            _answer = false;

        }

        public static bool[] Sensors=new bool[50];
        public static bool UpdateSensors()
        {
            try
            {

         
            string Page3 = _app.getStringFRomUrl("http://" + _app.ip_ssf_console+"/3");
            string output = _app.getBetween(Page3, "<body>", "</body");
            for(int i=0;i<output.Length;i++)
            {
                _app.Sensors[i] = output[i] == '1' ? false : true;
            }
                return true;
            }
            catch
            {

                
            }
            return false;
        }


        public static bool UpdateCounter()
        {
            try
            {


                //string Page3 = _variables.getStringFRomUrl("http://" + _variables.ip_ssf_console+"/9");
                //string output = _variables.getBetween(Page3, "<body>", "</body>");
                //int n = -1;
                //try
                //{
                //     n = Int32.Parse(output);
                //}
                //catch 
                //{


                //}

                _app._Counter = 0;
                    ///n;
                

            }
            catch
            {


            }
            return false;
        }


        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        public static string getStringFRomUrl(string urlAddress)
        {
           
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    string data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                return data;
            }
            }
            catch
            {


            }
            return "Err: 404  PAGE NOT FOUND";
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
        public static  string _get;
        public static bool _answer;
        public static bool newCommand = false;
        public static int target = -1;
        public static int targetDoor = -1;
        public static OperationMode _Mode;
        public  enum  OperationMode { Load = 99 , UnLoad}


        public static bool send( string get )

        {
            _get = get;
            //Thread.Sleep(500);
            try
            {

                //using (var wb = new WebClient())
                //{

                //    string send = "http://" + _app.ip_ssf_console + "/?" + _get;
                //    var response = wb.DownloadString(send);
                //    // MessageBox.Show(send);


                //}

                _answer = true;
                //  MessageBox.Show("done");
            }
            catch
            {


            }



            return true;
        }

        public static bool LoadColumnModbus(int col)
        {
            if (_app.Delta)
            {

                _app.WriteOnRegister(100, (byte)col);
                return true;
            }
            if (DeviceDoesntConnected())
                return LoadColumnModbus(col);
            return false;

        }

        public static bool LoadCellModbus(RackTBL rack, int cell,bool load=true)
        {
          
            if (_app.Delta)
            {
 
                int row = rack.y;
                int col = rack.x;
                int x, y;

                y = cell % col;
                x = (cell / col) +1;

                if (y == 0)
                {
                    y = col;
                    x--;
                }
               
                _app.WriteOnRegister(100, (byte)x);
                if (load)
                    _app.WriteOnRegister(102, (byte)y);
                else
                    _app.WriteOnRegister(101, (byte)y);

               

                return true;
            }
            if (DeviceDoesntConnected()) return LoadCellModbus( rack,  cell, load);
            return false;
        }
        public static bool UnLoadCellModbus(RackTBL rack, int cell)
        {
            return LoadCellModbus(rack, cell, false);
        }

        public static bool openTheDoorViaSSF( int cell, RackTBL rack, string mode )
        {
            int row=rack.y;
            int col=rack.x;
            int x , y;

            y = cell % col;
            x = ( cell / col );

            if ( y == 0 )
            {
                y = col;
                x--;
            }
            
             MessageBox.Show("y=" + y + " x: "+ x);
            _app.send("JC=1");
            _app.send("DC=1");
            if (mode == "load")
            {
                bool c = _app.send("ROW=" + x);
                bool r = _app.send("CLL=" + y);
               // _variables.newCommand = true;
               //_variables. target = x;
               // _variables.targetDoor = y;
               // _variables._Mode = OperationMode.Load;



            
             //   return r && c;
            }
            else
            {
                bool c = _app.send("ROW=" + x);
                 bool r = _app.send("COL=" + y);
                //_variables .newCommand = true;
                //_variables.target = x;
                //_variables.targetDoor = y;
                //_variables._Mode = OperationMode.UnLoad;




               
            }
            // MessageBox.Show(r.ToString());
            return true;
           
        }







        public static bool deleteDocument( int kalaid )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            DialogResult dialogResult = MessageBox.Show("آیا از حذف کالا اطمینان دارید؟", "هشدار حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if ( dialogResult == DialogResult.Yes )
            {

                try
                {

                    var kalalist = from k in db.KalaListTBLs
                                   where k.kalaid == kalaid
                                   select k;
                    db.KalaListTBLs.DeleteAllOnSubmit(kalalist);
                    db.SubmitChanges();

                    var kala = (from k in db.KalaTBLs
                                where k.id == kalaid
                                select k).ToList().Single();

                    db.KalaTBLs.DeleteOnSubmit(kala);
                    db.SubmitChanges();

                    return true;
                }
                catch
                {
                    return false;
                }


            }
            return false;
        }


        public enum SendSMSReturnType
        {

            None = -10,
            SendWasSuccessful = 0,
            InvalidUserNameOrPassword = 1,
            UserBlocked = 2,
            InvalidSenderNumber = 3,
            LimitationInDailySend = 4,
            LimitationInRecieverCount = 5,
            SenderLineIsInactive = 6,
            SMSContentFilteredWordsIsIncluded = 7,
            NoCredit = 8,
            SystemBeingUpdated = 9,
            NotImplemented = 10
        }



        public static bool mobilenumberChecker( string mobile )
        {

            if ( mobile.Length == 11 && mobile [ 0 ] == '0' && mobile [ 1 ] == '9' )
            {
                return true;
            }
            else
                return false;
        }



        public static string fetchUser( int p )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var user = (from u in db.UsersTBLs
                            where u.id == p
                            select u).ToList().Single();
                return user.fullname;
            }
            catch
            {
                return "[ کاربر ناشناس ] ";
            }
        }

        public static int fetchKalaCount( int p )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var kala = (from u in db.KalaListTBLs
                            where u.kalaid == (int)p
                            select u).ToList();


                return kala.Count;
            }
            catch
            {

            }
            return -1;

        }



        public static string fetchBrand( int p )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var brand = (from u in db.BrandTBLs
                             where u.id == p
                             select u).ToList().Single();
                return brand.Title;
            }
            catch
            {
                return "[ برند قید نشده ] ";
            }
        }

        public static string fetchGender( int p )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var gender = (from u in db.GenderTBLs
                              where u.id == p
                              select u).ToList().Single();
                return gender.Title;
            }
            catch
            {
                return "[ جنسیت قید نشده ] ";
            }
        }


        public static string fetchTypeGlasses( int p )
        {
            if ( p == 0 )
            {
                return "آفتابی";
            }
            return "طبی";
        }


        public static string fetchOrdererFullname( int p )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var order=(from o in db.orderedKalaTBLs
                           where o.id == p
                           select o).ToList().Single();

                return order.fullname;
            }
            catch
            {
                return "[ نام قید نشده ] ";
            }
        }
        public static string fetchOrdererWeight( int p )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var order=(from o in db.orderedKalaTBLs
                           where o.id == p
                           select o).ToList().Single();

                //  return (db.KalaTBLs.Where(x => x.id == order.kalaid)).ToList().Single().weight;


            }
            catch
            {

            }
            return "[ وزن قید نشده ] ";
        }


        public static int fetchBrandID( string str )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);


            try
            {
                var brand = (from u in db.BrandTBLs
                             where u.Title.ToLower().Contains(str.ToLower()) == true
                             select u).ToList().Single();
                //MessageBox.Show(brand.id.ToString());
                return brand.id;
            }
            catch
            {
                return -1;
            }

        }


        public static int fetchGenderID( string str )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {
                var gender = (from u in db.GenderTBLs
                              where u.Title.ToLower().Contains(str.ToLower()) == true
                              select u).ToList().Single();

                return gender.id;
            }
            catch
            {
                return -1;
            }

        }





        public static string fetchBrandPath( int p )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var brand = (from u in db.BrandTBLs
                             where u.id == p
                             select u).ToList().Single();

                string path= _app.programWorkingPath + "\\brand\\" + brand.id + ".jpg";
                //MessageBox.Show(path);
                if ( File.Exists(path) )
                {
                    return path;
                }
                else
                    return null;

            }
            catch
            {
                return null;
            }
        }



        public static string fetchKind( int p )
        {
            //LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            //try
            //{

            //    var kind = (from u in db.KindTBLs
            //                where u.id == p
            //                select u).ToList().Single();
            //    return kind.title;
            //}
            //catch
            //{

            //}
            return "[ دسته قید نشده ] ";
        }







        public static int fingerPassed = -1;

        public static int validateUser( int p )
        {


            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var user = (from u in db.UsersTBLs
                            where u.id==p
                            select u).ToList();

                if ( user.Any() )
                {
                    var u = user.Single();

                    return u.id;
                }
            }
            catch
            {

                return -1;
            }
            return -1;
        }



        public static string fetchDucumentName( int p )
        {
            //LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            //try
            //{

            //    var user = (from u in db.DocumentsTBLs
            //                where u.id == p
            //                select u).ToList().Single();
            //    return user.nameTarh;
            //}
            //catch (Exception ex)
            //{
            return "نام پرونده نا مشخص";
            // }
        }

        public static string fetchRackType( int typeid )
        {
            if ( typeid == 0 ) return "هوشمند اداری";
            else
                return "هوشمند سازمانی";
        }


        public static Bitmap brandPhoto( int id )
        {

            string path = _app.programWorkingPath + "\\brand\\";

            if ( Directory.Exists(path) == false )
            {
                Directory.CreateDirectory(path);
            }

            path += id + ".jpg";
            if ( File.Exists(path) )
            {

                try
                {


                    using ( var bmpTemp = Image.FromFile(path) )
                    {
                        Bitmap img = new Bitmap(bmpTemp);
                        return img;
                    }

                }
                catch
                {

                    return SSFGlasses.Properties.Resources.NoPicAvailable;

                }
            }
            return SSFGlasses.Properties.Resources.NoPicAvailable;
        }




        public static Bitmap kalaPhoto( int id )
        {
            LinqDatabaseDataContext db=new LinqDatabaseDataContext();
            var kala=db.KalaTBLs.Where(x=>x.id== id).SingleOrDefault();
            string path = _app.programWorkingPath + "\\kala\\";

            if ( Directory.Exists(path) == false )
            {
                Directory.CreateDirectory(path);
            }
            path += kala.id + "\\" + "main.jpg";
            if ( File.Exists(path) )
            {

                try
                {


                    using ( var bmpTemp = Image.FromFile(path) )
                    {
                        Bitmap img = new Bitmap(bmpTemp);
                        return img;
                    }

                }
                catch
                {

                    return SSFGlasses.Properties.Resources.NoPicAvailable;

                }
            }
            return SSFGlasses.Properties.Resources.NoPicAvailable;


        }




        public static Bitmap kalaBox( int id )
        {
            LinqDatabaseDataContext db=new LinqDatabaseDataContext();
            var kala=db.KalaTBLs.Where(x=>x.id== id).SingleOrDefault();
            string path = _app.programWorkingPath + "\\kala\\";

            if ( Directory.Exists(path) == false )
            {
                Directory.CreateDirectory(path);
            }
            path += kala.id + "\\" + "pack.jpg";
            if ( File.Exists(path) )
            {

                try
                {


                    using ( var bmpTemp = Image.FromFile(path) )
                    {
                        Bitmap img = new Bitmap(bmpTemp);
                        return img;
                    }

                }
                catch
                {

                    return SSFGlasses.Properties.Resources.NoPicAvailable;

                }
            }
            return SSFGlasses.Properties.Resources.NoPicAvailable;


        }







        public static string fetchRack( int rackid )
        {

            try
            {

                LinqDatabaseDataContext db = new LinqDatabaseDataContext(_app.connection);
                var q2 = (from r in db.RackTBLs
                          where r.id == rackid
                          select r).ToList().Single();

                return q2.tag + "(RACK0" + rackid.ToString() + ")";
            }
            catch
            {
                return "(پیدا نشد)";

            }
        }

        public static void boom( string str = "" )
        {
            MessageBox.Show("boom " + str);
        }

        public static void SystemError( string str, string title = "خطایی رخ داد" )
        {
            _app.playVoice("warning");
            MessageBox.Show(str, title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
        }


        public static int gridCount=20;
        public static bool fatekConnecion = false;
        public static bool saveRegnameFP = false;
        public static string ip_ssf_console= "192.168.1.10";


        public static int y = 7;
        public static int x = 8;
        public static void setVariable()
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {



                var cap = (from r in db.RackTBLs
                          let

                              xxx = r.x * r.y
                          select xxx).ToList().Sum();

                           
                          

                var setup = db.SetupTBLs.Single();
                var rack = from r in db.RackTBLs
                           select r;

                connection = js.Connection();
                if ( hostID == -1 )
                {
                    SystemError("فایل های پیکیربندی یافت نشد\nاین یک خطای مهلک است");
                    Application.Exit();
                }


                _app.capacity =(int) cap;
                //_variables.free = (int)cap - full;
                _app.programName = setup.programName;
                _app.security = setup.securityType;
                _app.programVersion = setup.version;
                _app.gridCount = ( int ) setup.gridCount;
                _app.CellManuallySet = ( bool ) setup.manualCellSet;
                _app.programTag = setup.tag;
                _app.documentMaximumCheckTime = ( int ) setup.readMaxTime;
                _app.officeEndTime = setup.endTime;
                _app.officeStartTime = setup.startTime;
                _app.thursdayisHolyday = ( bool ) setup.thursday;
                _app.programWorkingPath = setup.programWorkingPath;
                _app.smsLogin = ( int ) setup.smslogin;
                _app.smsTemp = ( bool ) setup.smsTemp;
                _app.smsRack90 = ( bool ) setup.smsRack90;
                _app.saveRegnameFP = ( bool ) setup.regNameFP;
                try
                {
                  
                    _app.CurrentRack = db.RackTBLs.FirstOrDefault();
                    _app.ip_ssf_console = _app.CurrentRack.ip;

                }
                catch 
                {

                  
                }
              


                _app.tempMax = ( int ) setup.tempMax;
                _app.y = db.RackTBLs.Where(x => x.id == 1).Select(x => x.x).Single();
                _app.x = db.RackTBLs.Where(x => x.id == 1).Select(x => x.x).Single();



                if ( _app.fatekConnecion == false )
                    _app.fatekConnecion = fatekTryConnect();


                _app.time = DateTime.Now.ToString("HH:mm:ss");
                System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

                DateTime todayDateTime = DateTime.Now;
                _app.date = pc.GetYear(todayDateTime) + "/" + ( pc.GetMonth(todayDateTime).ToString().Length == 1 ? "0" : "" ) + pc.GetMonth(todayDateTime) + "/" + ( pc.GetDayOfMonth(todayDateTime).ToString().Length == 1 ? "0" : "" ) + pc.GetDayOfMonth(todayDateTime);






                if ( Directory.Exists(_app.programWorkingPath) == false )
                {
                    Directory.CreateDirectory(_app.programWorkingPath);
                }

                numberOfMessageUpdate();
                _app.rackNo = ( int ) rack.Count();
            }
            catch
            {
                //   SystemError("خطای مهلک: مقداردهی اولیه متغیر ها");
            }
        }

        public static bool fatekTryConnect()
        {


            try
            {
                facon.OpenProject(programWorkingPath + @"\PLCServer.fcs");
                facon.Connect();
                //MessageBox.Show("openning ...");


                return true;
            }
            catch
            {
                return false;
            }


        }
        public static bool DeviceDoesntConnected(string more = null)
        {
            if (MessageBox.Show("اتصال با دستگاه برقرار نیست\nآیا میخواهید هم اکنون متصل شوید؟","Modbus",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation)==DialogResult.Yes)
            {
               return  _app.DeltaConnect();
            }
            return false;
        }
        public static bool JackOpen(int jack_number)
        {
            if (_app.Delta)
            {
                _app.WriteOnCoil(jack_number + _app.JACK_START_ADDRESS - 1, true);
                return true;
            }

            if (DeviceDoesntConnected()) return JackOpen(jack_number);
            return false;
        }

        public static bool JackClose(int jack_number)
        {
            if (_app.Delta)
            {
                _app.WriteOnCoil(jack_number + _app.JACK_START_ADDRESS - 1, false);
                return true;
            }

            if (DeviceDoesntConnected()) return JackClose(jack_number);
            return false;
        }


        public static bool DoorOpen(int door_number)
        {
            if (_app.Delta)
            {

                _app.WriteOnCoil(door_number + _app.DOOR_START_ADDRESS - 1, true);
                return true;
            }

            if (DeviceDoesntConnected()) return DoorOpen(door_number);
            return false;
        }

        public static bool DoorClose(int door_number)
        {
            if (_app.Delta)
            {
                _app.WriteOnCoil(door_number + _app.DOOR_START_ADDRESS - 1, false);
                return true;
            }

            if (DeviceDoesntConnected()) return DoorClose(door_number);
            return false;
        }





        public static string hostname()
        {
            try
            {
                string localComputerName = Dns.GetHostName();
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                return localComputerName;

            }
            catch
            {

                return " [ HOSTNAME ]";

            }

        }



        public static bool tempError = false;


        public static void numberOfMessageUpdate()
        {
            //LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            //var msg = (from d in db.MessageTBLs
            //           where d.read == false
            //           select d).ToList().Count();
            //_variables.numberOfMessage = ( int ) msg;
        }



        public static void smsRecieve()
        {



            //var sendServiceClient = new SendServiceClient();

            //var result = sendServiceClient.GetMessages("peyman.majidi", "1100047sms@",
            //    1, //1 daryafti  2 ersali
            //    new[] { "09370047967" }, 1, 1);

            //if (result != null )
            //{
            //    string a = result[0].Content;

            //    MessageBox.Show("پیامک شما دریافت شد "+a);
            //    _variables.resetJaygah();
            //}





        }


        public static bool sendSMS( string targetNumber, string textToSend )
        {
            try
            {
                Thread tr = new Thread(_app.internetCheck2);
                tr.Start();
                var sendServiceClient = new SendServiceClient();
                long[] recId = null;
                byte[] status = null;
                var result = sendServiceClient.SendSMS("peyman.majidi", "1100047sms@",
                    "50005345626", new[] { targetNumber },
                   textToSend, false, ref recId, ref status);

                if ( result == ( int ) SendSMSReturnType.SendWasSuccessful )
                {
                    // "ارسال پیامک با موفقیت انجام شد";
                    return true;
                }
                else
                {
                    string error;
                    switch ( result )
                    {
                        case -10: error = "نا مشخص"; break;
                        case 0: error = "ارسال با موفقیت انجام شد"; break;
                        case 1: error = "نام کاربر یا کلمه عبور نامعتبر می باشد"; break;
                        case 2: error = "کاربر مسدود شده است"; break;
                        case 3: error = "شماره فرستنده نامعتبر است"; break;
                        case 4: error = "محدودیت در ارسال روزانه"; break;
                        case 5: error = "تعداد گیرندگان حداکثر 100 شماره می باشد"; break;
                        case 6: error = "خط فرستنده غیرفعال است"; break;
                        case 7: error = "متن پیامک شامل کلمات فیلتر شده است"; break;
                        case 8: error = "اعتبار کافی نیست"; break;
                        case 9: error = "سامانه در حال بروز رسانی است"; break;
                        case 10: error = "پیاده سازی نشده است"; break;
                        default: error = "نا مشخص"; break;
                    }
                    MessageBox.Show(error, "ارسال انجام نشد: \r\n");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("ارسال پیامک انجام نشد");
                return false;
            }

        }


        public static string getIP()
        {
            return Dns.GetHostEntry(_app.hostname()).AddressList [ 0 ].ToString();
        }


        public static string getTemp() // دمای داخل دستگاه
        {
            try
            {

                string temp = (string)(_app.facon.GetItem(_app.faconPath, "R1018"));
                int dama = Int32.Parse(temp);
                if ( dama >= _app.tempMax && _app.tempError == false )
                {
                    _app.tempError = true;
                    LinqDatabaseDataContext db = new LinqDatabaseDataContext(_app.connection);
                    string prompt = " دمای دستگاه در تاریخ " + _app.date + " و ساعت " + _app.time + " از دمای مجاز(" + _app.tempMax + "° C) عبور کرد.\r\n\r\n@SanatFarzanegan";
                    _app.sendSMS(_app.backUpMobileNumber, prompt + "\r\nمحل نصب: " + _app.programTag);
                    _app.SystemError(prompt);
                    MessageTBL msg = new MessageTBL();
                    msg.type = "هشدار سیستم";
                    msg.title = prompt;
                    msg.read = false;
                    msg.archive = false;
                    msg.docoutid = -1;

                    msg.timestamp = _app.time + " - " + _app.date;
                    db.MessageTBLs.InsertOnSubmit(msg);

                    db.SubmitChanges();
                    _app.numberOfMessageUpdate();

                }

                return temp;
            }
            catch
            {
                return "-1";
            }
        }

        public static int getCell() // شماره سلول فعلی
        {
            try
            {

                string cellstr = (string)(_app.facon.GetItem(_app.faconPath, "C0"));
                int cell = Int32.Parse(cellstr);
                int currectCell = (cell / (int)4) + 1;
                return currectCell;

            }
            catch
            {
                return 0;
            }
        }
        public static string regFingerSetOperation = "R8888";
        public static string regAddOperation = "R9999";
        public static string regFingerPrintID = "R10000";
        public static zkemkeeper.CZKEMClass myDevice = new zkemkeeper.CZKEMClass();
        public static bool deviceConnection = false;

        public static string checkFinger() //  چک اثرانگشت
        {
            try
            {
                _app.facon.SetItem(_app.faconPath, regFingerSetOperation, "2"); // شروع عملیات چک اثرانگشت
                string read = (string)(_app.facon.GetItem(_app.faconPath, regFingerPrintID)); // شناسه اثرانگشت جدید
                return read;
            }
            catch
            {
                return "0";
            }
        }



        public static int howManOnline()
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(_app.connection);
            try
            {
                var hosts = from h in db.HostTBLs
                            where h.stat == true
                            select h;


                return hosts.ToList().Count();
            }
            catch
            {

            }
            return 0;
        }









        public static string addFinger() // افزودن اثرانگشت
        {
            if ( _app.hostType != "Server" ) return null;
            _app.facon.SetItem(_app.faconPath, "R260", "4");
            _app.facon.SetItem(_app.faconPath, regFingerSetOperation, "1"); // شروع عملیات ثبت اثرانگشت

            try
            {
                string read= (string)(_app.facon.GetItem(_app.faconPath, regAddOperation )); // چک اول
                int pass = Int32.Parse(read);
                if ( pass < 1 )
                {
                    _app.SystemError("سنسور اثرانگشت یافت نشد");
                    return null;
                }
                if ( pass >= 3 )
                {
                    if ( pass == 3 )
                        MessageBox.Show("خواندن اثر انگشت در مرحله اول پاس شد");
                    read = ( string ) ( _app.facon.GetItem(_app.faconPath, regAddOperation) ); // چک دوم
                    pass = Int32.Parse(read);
                    if ( pass >= 15 )
                    {
                        if ( pass == 15 )
                            MessageBox.Show("خواندن اثر انگشت در مرحله دوم پاس شد");
                        read = ( string ) ( _app.facon.GetItem(_app.faconPath, regAddOperation) ); // مقایسه نتیجه اول با دوم
                        pass = Int32.Parse(read);
                        if ( pass >= 127 )
                        {
                            MessageBox.Show("ثبت اثر انگشت جدید با موفقیت انجام شد");
                            read = ( string ) ( _app.facon.GetItem(_app.faconPath, regFingerPrintID) ); // شناسه اثرانگشت جدید
                            return read;
                        }
                        else
                        {
                            _app.SystemError("عدم مطابقت در اثرانگشت های وارد شده");
                            return null;
                        }
                    }
                    else
                    {
                        if ( MessageBox.Show("اسکن اثر انگشت شکست خورد", "", MessageBoxButtons.RetryCancel) == DialogResult.Cancel )
                            return null;
                        else
                            return _app.addFinger();
                    }
                }
                else
                    if ( MessageBox.Show("اسکن اثر انگشت شکست خورد", "", MessageBoxButtons.RetryCancel) == DialogResult.Cancel )
                    return null;
                else
                    return _app.addFinger();
            }
            catch
            {

            }

            _app.facon.SetItem(_app.faconPath, "R260", "2");
            return null;
        }

        public static bool networkCheck()
        {
            //  return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            return true;
        }

        public static bool internetCheck()
        {
            try
            {
                using ( var client = new WebClient() )
                {
                    using ( var stream = client.OpenRead("http://www.google.com") )
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }


        }


        public static void internetCheck2()
        {
            try
            {
                using ( var client = new WebClient() )
                {
                    using ( var stream = client.OpenRead("http://www.google.com") )
                    {

                    }
                }
            }
            catch
            {
                _app.SystemError("اینترنت قطع است\r\nفرایند ارسال اس ام اس نیاز به اتصال اینترنت دارد");
            }
        }



        public static void startMove( int cell, int rackid )
        {
            if ( _app.hostType != "Server" ) return;
            try
            {
                _app.facon.SetItem(_app.faconPath, "M401", "0");
            }
            catch { }
            //Thread.Sleep(100);
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(_app.connection);

            var rack = (from r in db.RackTBLs
                        where r.id == rackid
                        select r).ToList().Single();

            int x = cell % rack.x;
            int y = (cell / rack.x) + 1;
            if ( x == 0 ) { x = rack.x; y--; }

            //MessageBox.Show(String.Format("x:{0} y:{1} cell:{2} ",x,y,cell));
            try
            {
                _app.facon.SetItem(_app.faconPath, "R2000", "1");
                _app.facon.SetItem(_app.faconPath, "R2001", x.ToString());
                _app.facon.SetItem(_app.faconPath, "R2002", y.ToString());
                _app.facon.SetItem(_app.faconPath, "M401", "1");

            }
            catch
            {

            }
            // _variables.facon.SetItem(_variables.faconPath, "R260", "2");
        }

        public static void closeDoor()
        {
            _app.facon.SetItem(_app.faconPath, "R260", "1");
            try
            {
                _app.facon.SetItem(_app.faconPath, "T200", "60");
                _app.facon.SetItem(_app.faconPath, "M90", "0");
                _app.facon.SetItem(_app.faconPath, "R250", "5");

                _app.facon.SetItem(_app.faconPath, "R220", "12336");
                _app.facon.SetItem(_app.faconPath, "R221", "12336");


            }
            catch
            {

            }
            _app.facon.SetItem(_app.faconPath, "R260", "2");
        }

        public static void resetJaygah()
        {
            _app.playVoice("warning");
            MessageBox.Show("جایگاه مجدد راه اندازی میشود");

            _app.facon.SetItem(_app.faconPath, "M91", "1");
            Thread.Sleep(1000);
            _app.facon.SetItem(_app.faconPath, "M91", "0");


        }



        public static string rfidChecker()
        {
            //  string r220 = string.Concat((string)(_variables.facon.GetItem(_variables.faconPath, "R220")));


            string r220 = string.Concat((string)(_app.facon.GetItem(_app.faconPath, "R220")));
            string r221 = string.Concat((string)(_app.facon.GetItem(_app.faconPath, "R221")));
            string r250 = string.Concat((string)(_app.facon.GetItem(_app.faconPath, "R250")));
            if ( r220.CompareTo("12336") != 0 && r220.CompareTo("0") != 0 )
            {
                _app.facon.SetItem(_app.faconPath, "R250", "5");
                Thread.Sleep(500);
                //_variables.facon.SetItem(_variables.faconPath, "R220", "12336");
                //_variables.facon.SetItem(_variables.faconPath, "R221", "12336");
                //_variables.facon.SetItem(_variables.faconPath, "R250", "4");

                return ( r221.Split(' ') ) [ 0 ];


            }
            else
                return "not";

        }

        public static int userRFIDfetch( string rfid )
        {
            LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            try
            {

                var user = (from u in db.UsersTBLs
                            where u.rfid == rfid
                            select u).ToList().Single();
                return user.id;
            }
            catch
            {
                return -1;
            }
        }





        public static void masterDoorOpen()
        {
            if ( _app.hostType != "Server" ) return;
            // MessageBox.Show("Boom!!");
            _app.facon.SetItem(_app.faconPath, "R260", "1");
            try
            {

                _app.facon.SetItem(_app.faconPath, "M48", "1");

                Thread.Sleep(2000);
                _app.facon.SetItem(_app.faconPath, "M48", "0");







            }
            catch
            {

            }
            _app.facon.SetItem(_app.faconPath, "R260", "2");
        }



















        public static void doorOpen_facon() // facon
        {
            if ( _app.hostType != "Server" ) return;

            _app.facon.SetItem(_app.faconPath, "R260", "1");
            try
            {
                //_variables.facon.SetItem(_variables.faconPath, "T200", "60");
                _app.facon.SetItem(_app.faconPath, "R250", "4");
                _app.facon.SetItem(_app.faconPath, "M90", "1");
                Thread.Sleep(1000);
                _app.facon.SetItem(_app.faconPath, "M90", "0");
                Thread.Sleep(1000);
                _app.facon.SetItem(_app.faconPath, "R250", "4");
                _app.facon.SetItem(_app.faconPath, "R220", "12336");
                _app.facon.SetItem(_app.faconPath, "R221", "12336");





            }
            catch
            {

            }
            _app.facon.SetItem(_app.faconPath, "R260", "2");
        }

        public static string newRFID { get; set; }



        public static bool validateBarcodesameKala( string p )
        {
            //LinqDatabaseDataContext db = new LinqDatabaseDataContext(connection);
            //if (p.Length < 2) return false;
            //try
            //{

            //    var kala = (from u in db.KalaTBLs
            //                where u.barcodeStandard == p
            //                select u).ToList();

            //    if(kala.Any())
            //          return false;
            //}
            //catch 
            //{

            //}

            //try
            //{

            //    var kala = (from u in db.KalaListTBLs
            //                where u.barcode == p
            //                select u).ToList();

            //    if(kala.Any())
            //          return false;
            //}
            //catch
            //{

            //}

            return true;

        }
    }
}
