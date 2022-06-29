using System.Windows.Forms;
using WebPrinter.Web;

namespace WebPrinter
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Minimized
                ? FormWindowState.Normal
                : FormWindowState.Minimized;

            this.Visible = this.WindowState == FormWindowState.Normal;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            WebHost.Host.Stop();
        }

        private void btn_Exit_Click(object sender, System.EventArgs e)
        {
            var result = MessageBox.Show(@"do you want close web api printer ?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
