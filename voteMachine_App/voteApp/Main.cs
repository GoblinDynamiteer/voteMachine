using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace voteApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            OpenCOM();

            // BYGG CHECK CONNECTION...
            // SVAR FRÅN voteMachine
        }

        /* Open COM-Port */
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

        /* Update drop-down list with available COM-ports */
        void UpdateCOMportList()
        {
            string[] ports = SerialPort.GetPortNames();

            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(ports);

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            serialPort.Write(textBoxInput.Text + '\0');

        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            lblLines.Text = textBoxInput.Lines.Length.ToString();
        }
    }
}
