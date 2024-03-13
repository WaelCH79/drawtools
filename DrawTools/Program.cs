using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawTools.Helper;

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


            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalExceptionHandler.UnhandledException);
            //Application.Run(new FormParent(new FormApplicationList()));
            Application.Run(new FormParent(new Form1()));

        }
    }
}
