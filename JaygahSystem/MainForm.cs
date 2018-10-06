using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SSFGlasses
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void lbltitlebar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button32_Click(object sender, EventArgs e)
        {

        }


        //changes
        class Rack
        {
            public string Title;
            public ushort Counter;
            public ushort Door;
            public ushort Jack;
            public ushort Column;
            public ushort Load;
            public ushort RegSensorAddr;
            public ushort PM;
            public ushort Close_All_Doors;
            public ushort Close_All_Jacks;
            public ushort Door_Enable;
            public ushort Keeper;

        };
        bool demo = false;
        List<Rack> Racks = new List<Rack>();
        Rack CurrentRack;
        private void PMForm_Load(object sender, EventArgs e)
        {

            _App.MBmaster = new ModbusTCP.Master();

            IntilizeRacks();
            IntilizeConfigFile();
            txtConsoleIP.Text = _App.ip_ssf_console;
            txtLog.Text = @"peyman@superuser#";
            grpDoors.Enabled = grpJacks.Enabled = false;

            cmbRack.DataSource = Racks.Select(r => r.Title).ToList();
            cmbRack.SelectedIndex = 0;



        }

        private void IntilizeConfigFile()
        {
            string path = "connection_info.txt";
            if (File.Exists(path))
            {
                try
                {
                    var text = File.ReadAllText(path);
                    var data = JObject.Parse(text);
                    var ip = _App.ip_ssf_console = data["IP"].Value<string>();
                    var title = data["AppTitle"].Value<string>();
                    int interval = data["Interval"].Value<int>();
                    startAddress = data["Start"].Value<int>();
                    if (data["Demo"] != null)
                    {
                        var demo = data["Demo"].Value<bool>();
                        this.demo = demo;
                    }

                    lbltitlebar.Text = title;
                    txtConsoleIP.Text = ip;
                    refreshTimer.Interval = interval;

                }
                catch
                {

                    MessageBox.Show("فایل تنظمیمات خراب شده است\r\nconnection_info.txt Dammaged", "Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Application.Exit();
                }


            }
        }




        private void IntilizeRacks() // TODO Racks
        {
            Racks.Add(new Rack()
            {
                Title = "#1 Rack Alpha (α)",
                Counter = 2028,
                Door = 408,
                Column = 414,
                Load = 500,
                RegSensorAddr = 50,
                PM = 101,
                Close_All_Jacks = 102,
                Keeper = 103,
                Door_Enable = 596,
                Jack = 100
            });
            //-------------------------------------------------------------------------
            Racks.Add(new Rack() { 
                Title = "#2 Rack Beta (β)",
                Counter = 1484,
                Door = 408,
                Column = 414,
                Load = 501,
                RegSensorAddr = 1481,
                PM = 551,
                
                Close_All_Jacks = 552,
                Keeper = 553,
                Door_Enable = 419,
                Jack = 550
            });
            //-------------------------------------------------------------------------

            Racks.Add(new Rack()
            {
                Title = "#3 Rack Gamma (γ)",
                Counter = 1546,
                Door = 416,
                Column = 418,
                Load = 502,
                RegSensorAddr = 1544,
                PM = 1546,
                Close_All_Jacks = 562,
                Keeper = 563,
                Door_Enable = 423,
                Jack = 560
            });
            //-------------------------------------------------------------------------

            Racks.Add(new Rack() { 
                Title = "#4 Rack Delta (δ)",
                Counter = 1579,
                Door = 424,
                Column = 426,
                Load = 503,
                RegSensorAddr = 1576,
                PM = 571,                
                Close_All_Jacks = 572,
                Keeper = 573,
                Door_Enable = 427,
                Jack = 570
            });
            //-------------------------------------------------------------------------

            Racks.Add(new Rack() { 
                Title = "#5 Rack Epsilon (ε)",
                Counter = 1611,
                Door = 428,
                Column = 430,
                Load = 504,
                RegSensorAddr = 1610,
                PM = 1611,
                Close_All_Jacks = 582,
                Keeper = 58,
                Door_Enable = 429,
                Jack = 580
            });
            //-------------------------------------------------------------------------



        }

        private void btnMakeUpdate_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void button109_Click(object sender, EventArgs e)
        {
            if (_App.Delta)
            {
                _App.WriteOnRegister(CurrentRack.Column, (byte)0);
                _App.WriteOnRegister(CurrentRack.Door, (byte)0);


                return;
            }
            if (_App.DeviceDoesntConnected())
                button1_Click(null, null);



        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (_App.Delta)
            {
                _App.WriteOnRegister(CurrentRack.Column, (byte)0);
                _App.WriteOnRegister(CurrentRack.Door, (byte)0);


                return;
            }
            if (_App.DeviceDoesntConnected())
                button1_Click(null, null);
        }

        private void button110_Click(object sender, EventArgs e)
        {

            Log(Error: true);

        }

        public bool OpenDoor(int door)
        {
            if (_App.Delta == false)
            {

                return false;
            }
            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            return false;
        }

        public bool CloseDoor(int door)
        {
            if (_App.Delta == false)
            {

                return false;
            }

            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            return false;
        }


        public bool OpenJack(int jack)
        {
            if (_App.Delta == false)
            {

                return false;
            }

            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            return false;
        }

        public bool CloseJack(int jack)
        {
            if (_App.Delta == false)
            {

                return false;
            }

            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            _App.WriteOnRegister(CurrentRack.Column, (byte)0);
            return false;
        }


        private void button13_Click(object sender, EventArgs e)
        {

            int i = 1;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void Log(string msg = "", bool Error = false)
        {
            if (Error)
                txtLog.Text += "\r\nErr: Something goes wrong";
            else
                txtLog.Text += "\r\n" + msg;
            txtLog.Refresh();
        }





        private void button1_Click(object sender, EventArgs e)
        {

            int i = 2;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 3;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 4;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i = 5;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = 6;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = 7;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int i = 8;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int i = 9;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int i = 10;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int i = 11;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i = 12;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int i = 13;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int i = 14;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int i = 15;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int i = 16;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            int i = 17;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);

        }

        private void button17_Click(object sender, EventArgs e)
        {
            int i = 18;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);

        }

        private void button16_Click(object sender, EventArgs e)
        {
            int i = 19;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int i = 20;
            if (OpenDoor(door: i))
                Log("\r\nDoor number " + i + " OPENED successfuly");
            else
                Log("\r\n/!\\ Something goes wrong");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int i = 21;
            if (OpenDoor(door: i))
                Log("Door number " + i + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 1);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 2);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 3);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 4);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 5);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 6);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 7);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 8);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 9);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 10);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 11);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 12);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 13);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 14);
        }

        private void button32_Click_1(object sender, EventArgs e)
        {
            CloseDoor(door: 15);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 16);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 17);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 18);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 19);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 20);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            CloseDoor(door: 21);
        }


        private void button112_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void button113_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtLog.Text
                + "<< LOG");
            Log("- Copied -");
        }

        private void button114_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Disonnect")
            {
                btnConnect.Text = "Connect";
                try
                {
                    _App.MBmaster.Dispose();
                    _App.MBmaster = null;
                    _App.Delta = false;
                    refreshTimer.Enabled = false; ;
                    picLED.Image = Properties.Resources.off;
                    grpDoors.Enabled = grpJacks.Enabled = false;
                    Log("Disonnected;");

                }
                catch { }


                return;
            }
            Log("Try to Connect [wait] ...");
            this.Cursor = Cursors.WaitCursor;
            if (_App.DeltaConnect(txtConsoleIP.Text))
            {
                btnConnect.Text = "Disonnect";
                Log("Connected;");
                grpDoors.Enabled = grpJacks.Enabled = true;

                picLED.Image = Properties.Resources.on;

                //Log("Rack(" + txtConsoleIP.Text + ") Succesfully Connected_");
                refreshTimer.Enabled = true;
            }
            else
            {
                Log("Not Connect");
            }
            this.Cursor = Cursors.Default;
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://" + _App.ip_ssf_console);
        }

        private void button115_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (_App.PingHost(txtConsoleIP.Text))
            {
                Log("PING successfuly     [ OK ]");
            }
            else
                Log("Err: PING error");
            this.Cursor = Cursors.Default;
        }


        private void button117_Click(object sender, EventArgs e)
        {

            refreshTimer_Tick(null, e); // help me



        }

        private void PlayDemo()
        {
            var sen1 = Convert.ToString(new Random().Next(10, 128), 2).Select(s => s.Equals('1')).ToList();
            var sen2 = Convert.ToString(new Random().Next(0, 255), 2).Select(s => s.Equals('1')).ToList();
            var sen3 = Convert.ToString(new Random().Next(0, 255), 2).Select(s => s.Equals('1')).ToList();

            for (int j = 0; j < 16 - sen1.Count; j++)
                sen1.Add(false);
            for (int j = 0; j < 16 - sen2.Count; j++)
                sen2.Add(false);
            for (int j = 0; j < 16 - sen3.Count; j++)
                sen3.Add(false);

            int i = 0;

            sensor01.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor02.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor03.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor04.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor05.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor06.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor07.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor08.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
            i = 0;
            sensor09.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor10.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor11.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor12.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor13.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor14.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor15.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor16.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
            i = 0;
            sensor17.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor18.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor19.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor20.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
            sensor21.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
        }
        int startAddress = 2000;
        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (demo)
            {
                PlayDemo();
                return;
            }
            try
            {

                _App.GenerateRegister(startAddress);

                int index = cmbRack.SelectedIndex;

                //_App.UpdateSensors();
                var sen1 = Convert.ToString(_App.GetBack[startAddress + CurrentRack.RegSensorAddr], 2).Select(s => s.Equals('1')).ToList();
                var sen2 = Convert.ToString(_App.GetBack[startAddress + CurrentRack.RegSensorAddr + 1], 2).Select(s => s.Equals('1')).ToList();
                var sen3 = Convert.ToString(_App.GetBack[startAddress + CurrentRack.RegSensorAddr + 2], 2).Select(s => s.Equals('1')).ToList();
                int i = 0;


                for (int j = 0; j < 16 - sen1.Count; j++)
                    sen1.Add(false);
                for (int j = 0; j < 16 - sen2.Count; j++)
                    sen2.Add(false);
                for (int j = 0; j < 16 - sen3.Count; j++)
                    sen3.Add(false);

                sensor01.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor02.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor03.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor04.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor05.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor06.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor07.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor08.BackColor = sen1[i++] == true ? Color.Lime : Color.Gainsboro;
                i = 0;
                sensor09.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor10.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor11.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor12.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor13.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor14.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor15.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor16.BackColor = sen2[i++] == true ? Color.Lime : Color.Gainsboro;
                i = 0;
                sensor17.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor18.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor19.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor20.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
                sensor21.BackColor = sen3[i++] == true ? Color.Lime : Color.Gainsboro;
            }
            catch
            { }
        }


        private void chkAutoUpdate_Click(object sender, EventArgs e)
        {
            bool auto = refreshTimer.Enabled;
            if (auto)
            {
                auto = refreshTimer.Enabled = false;
                chkAutoUpdate.Image = SSFGlasses.Properties.Resources.switches;
            }
            else
            {
                auto = refreshTimer.Enabled = true;
                chkAutoUpdate.Image = SSFGlasses.Properties.Resources.switches_on;
            }
        }

        private void button56_Click(object sender, EventArgs e)
        {
            int j = 1;

            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button58_Click(object sender, EventArgs e)
        {
            int j = 2;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button62_Click(object sender, EventArgs e)
        {
            int j = 3;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button66_Click(object sender, EventArgs e)
        {
            int j = 4;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button63_Click(object sender, EventArgs e)
        {
            int j = 5;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button59_Click(object sender, EventArgs e)
        {
            int j = 6;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button61_Click(object sender, EventArgs e)
        {
            int j = 7;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button65_Click(object sender, EventArgs e)
        {
            int j = 8;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            int j = 9;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button60_Click(object sender, EventArgs e)
        {
            int j = 10;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button57_Click(object sender, EventArgs e)
        {
            int j = 11;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            int j = 12;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button55_Click(object sender, EventArgs e)
        {
            int j = 13;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button54_Click(object sender, EventArgs e)
        {
            int j = 14;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            int j = 15;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button52_Click(object sender, EventArgs e)
        {
            int j = 16;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button51_Click(object sender, EventArgs e)
        {
            int j = 17;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            int j = 18;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button49_Click(object sender, EventArgs e)
        {
            int j = 19;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            int j = 20;
            if (OpenJack(jack: j))
                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            int j = 21;
            if (OpenJack(jack: j))

                Log("Jack number " + j + " OPENED successfuly");
            else
                Log(Error: true);
        }

        private void button98_Click(object sender, EventArgs e)
        {
            int jf = 1;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button100_Click(object sender, EventArgs e)
        {
            int jf = 2;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button104_Click(object sender, EventArgs e)
        {
            int jf = 3;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button108_Click(object sender, EventArgs e)
        {
            int jf = 4;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button105_Click(object sender, EventArgs e)
        {
            int jf = 5;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button101_Click(object sender, EventArgs e)
        {
            int jf = 6;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button103_Click(object sender, EventArgs e)
        {
            int jf = 7;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button107_Click(object sender, EventArgs e)
        {
            int jf = 8;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button106_Click(object sender, EventArgs e)
        {
            int jf = 9;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button102_Click(object sender, EventArgs e)
        {
            int jf = 10;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button99_Click(object sender, EventArgs e)
        {
            int jf = 11;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button88_Click(object sender, EventArgs e)
        {
            int jf = 12;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button97_Click(object sender, EventArgs e)
        {
            int jf = 13;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button96_Click(object sender, EventArgs e)
        {
            int jf = 14;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button95_Click(object sender, EventArgs e)
        {
            int jf = 15;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button94_Click(object sender, EventArgs e)
        {
            int jf = 16;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button93_Click(object sender, EventArgs e)
        {
            int jf = 17;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button92_Click(object sender, EventArgs e)
        {
            int jf = 18;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button91_Click(object sender, EventArgs e)
        {
            int jf = 19;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button90_Click(object sender, EventArgs e)
        {
            int jf = 20;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void button89_Click(object sender, EventArgs e)
        {
            int jf = 21;
            if (CloseJack(jack: jf))
                Log("Jack number " + jf + " CLOSED successfuly");
            else
                Log(Error: true);
        }

        private void sensor21_Click(object sender, EventArgs e)
        {

        }

        private void button67_Click(object sender, EventArgs e)
        {
            Log(Error: true);
        }

        private void picLED_Click(object sender, EventArgs e)
        {

            picLED.Image = Properties.Resources.on;



        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (_App.Delta)
            {
                _App.WriteOnRegister(CurrentRack.Column, (byte)0);
                _App.WriteOnRegister(CurrentRack.Door, (byte)0);


                return;
            }
            if (_App.DeviceDoesntConnected())
                button1_Click(null, null);
        }

        private void sensor01_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            new AboutMe().ShowDialog();
        }

        private void cmbRack_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentRack = Racks[cmbRack.SelectedIndex];
            lblCurrentRack.Text = CurrentRack.Title;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
