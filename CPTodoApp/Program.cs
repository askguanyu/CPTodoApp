using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace CPTodoApp
{
    static class Program
    {
        private static Mutex mutex;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            using (mutex = new Mutex(true, AppConstants.AppName, out createdNew))
            {
                if (!createdNew)
                {
                    // Only allow one instance
                    return;
                }

                SetHiDpiAwareness();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

        private static void SetHiDpiAwareness()
        {
            var section = ConfigurationManager.GetSection("System.Windows.Forms.ApplicationConfigurationSection") as NameValueCollection;
            if (section != null)
            {
                section["DpiAwareness"] = "PerMonitorV2";
                // section["EnableWindowsFormsHighDpiAutoResizing"] = "false";
            }
        }
    }
}
