using System;
using System.ServiceProcess;

namespace WebPrinterApi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            if (!Environment.UserInteractive)
            {
                var servicesToRun = new ServiceBase[]
                {
                    new Service()
                };
                ServiceBase.Run(servicesToRun);
            }
            else // Debug mode
            {

                WebHost.Host.Start();
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
                // forces debug to keep VS running while we debug the service  
            }
        }
    }
}
