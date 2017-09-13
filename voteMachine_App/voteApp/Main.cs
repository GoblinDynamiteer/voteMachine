using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;
using System.Text.RegularExpressions;

namespace voteApp
{
    public partial class frmMain : Form
    {
        string[] ports;
        private string serialData;

        public frmMain()
        {
            InitializeComponent();

            /* Update port list and fill port comboBox */
            UpdateCOMportList(); 

            if (ports.Length > 0)
            {
                foreach (string port in ports)
                {
                    if (OpenCOM(port))
                    {
                        break;
                    }
                }
            }

            else
            {
                textBoxData.AppendText(
                        "No ports available!");
            }

        }

        /* Open COM-Port */
        bool OpenCOM(string portName)
        {
            bool success = true;

            if (!serialPort.IsOpen)
            {

                try
                {
                    serialPort.PortName = portName;
                    serialPort.Open();

                    textBoxData.AppendText(
                        "Port " + serialPort.PortName + " öppnad!\r\n");
                }

                catch (Exception)
                {
                    textBoxData.AppendText(
                        "Port " + serialPort.PortName + " kunde inte öppnas!\r\n");

                    success = false;
                }

            }

            return success;
        }

        /* Update drop-down list with available COM-ports */
        void UpdateCOMportList()
        {
            ports = SerialPort.GetPortNames();

            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(ports);

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            lblQuestion.Text = "";

            for (int i = 0; i < textBoxInput.Lines.Length; i++)
            {
                serialPort.Write((i + 1).ToString() + textBoxInput.Lines[i] + '\0');
                lblQuestion.Text += textBoxInput.Lines[i] + "\r\n";

                int tick = Environment.TickCount & Int32.MaxValue;

                while ((Environment.TickCount & Int32.MaxValue) - tick < 1500)
                {
                    ; // Do nothing
                }
            }

        }

        /* Clear voteMachine Display and textbox */
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("C");
                textBoxInput.Text = "";
            }

        }

        /* Event method for serial data available */
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            serialData = serialPort.ReadLine();

            this.Invoke(new EventHandler(DisplayText));
        }

        /* Display serial data and misc */
        private void DisplayText(object o, EventArgs e)
        {

            /* Display data in textbox */
            textBoxData.AppendText(serialData + "\r\n");

            if (serialData.Contains("votes"))
            {
                UpdateVoteLabels();
            }
        }

        /* Filter votes, update labels */
        private void UpdateVoteLabels()
        {
            string number = Regex.Match(serialData, @"\d+").Value;

            if (serialData.Contains("Green"))
            {
                lblGreenVotes.Text = number;
            }

            if (serialData.Contains("Red"))
            {
                lblRedVotes.Text = number;
            }

        }

        /* Event method for status button */
        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("S"); // Check status
            }
        }

        private void comboBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.Close();

            textBoxData.AppendText("Byter port " + serialPort.PortName + "->" 
                + comboBoxPorts.Text + "\r\n");

            OpenCOM(comboBoxPorts.Text);

            serialPort.Write("S"); // Check status
        }
    }
}
