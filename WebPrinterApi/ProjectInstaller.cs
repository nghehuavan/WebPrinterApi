using System.Collections;
using System.ComponentModel;
using System.ServiceProcess;

namespace WebPrinterApi
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            //base.OnAfterInstall(savedState);

            using (var sc = new ServiceController(serviceInstaller.ServiceName))
            {
                sc.Start();
            }
        }

        //protected override void OnBeforeUninstall(IDictionary savedState)
        //{
        //    using (var sc = new ServiceController(serviceInstaller.ServiceName))
        //    {
        //        sc.Stop();
        //    }

        //    base.OnBeforeUninstall(savedState);
        //}
    }
}
