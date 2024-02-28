using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTools
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();

            // Check command line
            if (args.Length > 1)
            {
                MessageBox.Show("Incorrect number of arguments. Usage: DrawTools.exe [file]", "DrawTools");
            }


            FormApplicationList form = new FormApplicationList();
            Application.Run(new FormParent(new FormApplicationList()));

        }
    }
}
