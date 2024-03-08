using NLog;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace DrawTools.Helper
{
    public class GlobalExceptionHandler
    {
        public static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ILogger logger = LogManager.GetLogger("DrawTools_logger");
            logger.Error(e.ExceptionObject.ToString());
            LogManager.Flush();
            // Display user-friendly message
           // MessageBox.Show("An error has occurred. Please try again later.");
        }
    }
}
