using System.ServiceProcess;

namespace WebPrinterApi
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WebHost.Host.Start();
        }

        protected override void OnStop()
        {
            WebHost.Host.Stop();
            WebHost.Host.Dispose();
        }
    }
}
