using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;

namespace SSFGlasses
{
    public class js
    {
        public static string Connection()
        {
            string line=null;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config\\connection.cfg");
                line = file.ReadLine();
                file.Close();
                line += "@";
            }
            catch
            {
               
            }

           
            return line;
        }


        public static int getID()
        {
            string line;
            int _id = -1;
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config\\id.cfg");
                line = file.ReadLine();
                file.Close();
                _id = Int32.Parse(line);


            }
            catch
            {
                line = null;
            }
            return _id;
        }



        public static string getType()
        {
            string line="Client";
           
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config\\type.cfg");
                line = file.ReadLine();
                file.Close();          
            }
            catch
            {               
            }
            return line;
        }





    }
}
