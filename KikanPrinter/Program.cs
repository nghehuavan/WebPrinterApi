using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;
using KikanPrinter.Web;

namespace KikanPrinter
{
    static class Program
    {
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (!IsAdministrator())
            {
                // Restart and run as admin
                var exeName = Process.GetCurrentProcess().MainModule?.FileName;
                var startInfo = new ProcessStartInfo(exeName) { Verb = "runas", Arguments = "restart" };
                Process.Start(startInfo);
                Application.Exit();
            }
            else
            {
                WebHost.Host.Start();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
        }
    }
}
