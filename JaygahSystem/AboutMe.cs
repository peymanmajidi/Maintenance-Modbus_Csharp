using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSFGlasses
{





    public partial class AboutMe : Form
    {


        public class AdvancedCursors
        {

            [DllImport("User32.dll")]
            private static extern IntPtr LoadCursorFromFile(String str);

            public static Cursor Create(string filename)
            {
                IntPtr hCursor = LoadCursorFromFile(filename);

                if (!IntPtr.Zero.Equals(hCursor))
                {
                    return new Cursor(hCursor);
                }
                else
                {

                }
                return null;
            }
        }
        public AboutMe()
        {
            InitializeComponent();
        }
  
        private void AboutMe_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\" + "Mario.ani";

            if (File.Exists(path))
                Cursor = AdvancedCursors.Create(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ssfco.vip/");

            // done
        }
    }
}
