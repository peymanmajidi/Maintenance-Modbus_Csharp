using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSFGlasses
{
    public partial class ModeBusNewFRM : Form
    {
        public ModeBusNewFRM()
        {
            InitializeComponent();
        }

        //changes
        class Rack
        {
            public ushort counter;
            public ushort door;
            public ushort column;
            public ushort load;
            public Rack(ushort c, ushort d, ushort cl, ushort ld)
            {
                counter = c;
                door = d;
                column = cl;
                load = ld;
            }

        };
        //

        List<Rack> Racks = new List<Rack>();
        Rack CurrentRack;


        private void button117_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "DIS")
            {
                btnConnect.Text = "CNT";
                try
                {
                    _App.MBmaster.Dispose();
                    _App.MBmaster = null;
                    _App.Delta = false;
                    timer1.Enabled = false; ;
                }
                catch { }


                return;
            }

            this.Cursor = Cursors.WaitCursor;
            if (_App.DeltaConnect(txtConsoleIP.Text))
            {
                btnConnect.Text = " DIS";
                //Log("Rack(" + txtConsoleIP.Text + ") Succesfully Connected_");
                timer1.Enabled = true;
            }
            else
            {
                // Log("Not Connect", true);
            }
            this.Cursor = Cursors.Default;

            
        }
        public static List<byte> Doors = new List<byte>();

        private void timer1_Tick(object sender, EventArgs e)
        {
            _App.GenerateRegister(2000);

            int index = cmbRack.SelectedIndex;
            lblCounter.Text = _App.counters[index].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_App.Delta)
            {
                _App.WriteOnRegister(CurrentRack.column,(byte) numCol.Value);
                _App.WriteOnRegister(CurrentRack.door, (byte)numDoor.Value);


                //added code
                if(radioLoad.Checked)
                    _App.WriteOnRegister(CurrentRack.load, 1);
                else
                    _App.WriteOnRegister(CurrentRack.load, 0);

                //


                return;
            }
            if (_App.DeviceDoesntConnected())
                button1_Click(null, null);

        }

        private void ModeBusNewFRM_Load(object sender, EventArgs e)
        {
            // changes
            Racks.Add(new Rack(409, 408, 414, 500));
            Racks.Add(new Rack(2003, 416, 418, 501));
            Racks.Add(new Rack(2007, 420, 422, 502));
            Racks.Add(new Rack(2011, 424, 426, 503));
            Racks.Add(new Rack(2015, 428, 430, 504));
            Racks.Add(new Rack(2019, 432, 434, 505));
            Racks.Add(new Rack(2023, 436, 437, 506));
            Racks.Add(new Rack(2027, 439, 440, 507));
            //
            cmbRack.SelectedIndex = 0;

        }

        private void cmbRack_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentRack = 
            Racks[(cmbRack.SelectedIndex)];
            lblRackname.Text = cmbRack.SelectedItem.ToString();
            //MessageBox.Show("Door:" + CurrentRack.door);



        }
    }
}
