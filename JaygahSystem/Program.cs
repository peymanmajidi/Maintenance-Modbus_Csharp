using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace SSFGlasses
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
            {
                MessageBox.Show("برنامه در حال اجرا است", "صنعت فرزانگان", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa-IR");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fa-IR");
            Application.EnableVisualStyles();
            //Application.Run(new ManagerHome());
            
            Application.Run(new MainForm());

            //  Application.Run(new LoginForm());
        }
    }
}