using System;
using System.Windows.Forms;

namespace voteApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            OpenCOM();
        }

        void OpenCOM()
        {
            if (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.Open();
                }

                catch (Exception)
                {
                    MessageBox.Show("Kan inte öppna " + serialPort.PortName);
                }

            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            serialPort.Write(textBoxInput.Text + '\0');

        }
    }
}
