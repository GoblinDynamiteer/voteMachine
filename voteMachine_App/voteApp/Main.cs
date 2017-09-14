using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Linq;
using System.Management;
using System.ComponentModel;

namespace voteApp
{
    public partial class frmMain : Form
    {
        string[] ports;
        private string serialData;

        enum Error
        {
            COMPortNotOpen,
            COMOpenError,
            COMNoPortsAvailable
        }

        public frmMain()
        {
            InitializeComponent();

            /* Update port list and fill port 
             * comboBox */
            UpdateCOMportList(); 

            /* Open first available port */
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

            else // No COM-ports available
            {
                DisplayError(Error.COMNoPortsAvailable);
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
                        "Port " + serialPort.PortName + 
                        " öppnad!\r\n");
                }

                catch (Exception)
                {
                    DisplayError(Error.COMOpenError);
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

            /* Populate combobox with ports 
             * (port names and device names) */
            foreach (string port in ports)
            {
                ComboboxItem item = new ComboboxItem();

                item.Text = port + ": " + SerialPortDeviceName(port);
                item.Value = port;

                comboBoxPorts.Items.Add(item);
            }

        }

        /* Event method for send button */
        private void btnSend_Click(object sender, EventArgs e)
        {
            lblQuestion.Text = "";
            progressBar.Visible = true;
            textBoxInput.ReadOnly = true;

            progressBar.Maximum = 100;
            progressBar.Step = 1;
            progressBar.Value = 0;
            backgroundWorkerSendText.RunWorkerAsync();

        }

        /* Clear voteMachine Display 
         * and textbox */
        private void btnClear_Click(
            object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write("C");
                textBoxInput.Text = "";

                lblGreenVotes.Text = "0";
                lblRedVotes.Text = "0";

            }

        }

        /* Display errors in data textbox */
        private void DisplayError(Error error)
        {
            textBoxData.AppendText("Error: ");

            switch (error)
            {
                case Error.COMPortNotOpen:
                    textBoxData.AppendText(
                        "COM-port not open!");
                    break;

                case Error.COMOpenError:
                    textBoxData.AppendText(
                        "Cannot open COM-port!");
                    break;

                case Error.COMNoPortsAvailable:
                    textBoxData.AppendText(
                        "No COM-ports available!");
                    break;

                default:
                    break;
            }

            textBoxData.AppendText("\r\n");
        }

        /* Event method for serial data available */
        private void serialPort_DataReceived(
            object sender, SerialDataReceivedEventArgs e)
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
            string number = Regex.Match(
                serialData, @"\d+").Value;

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

        /* Event method for comboBox change */
        private void comboBoxPorts_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            serialPort.Close();

            string newPort = (
                comboBoxPorts.SelectedItem 
                as ComboboxItem).Value.ToString();

            textBoxData.AppendText("Byter port " + 
                serialPort.PortName + "->" + newPort + "\r\n");

            if (OpenCOM(newPort))
            {
                serialPort.Write("S"); // Check status
            }

        }

        /* Gets device name for COM-port */
        private string SerialPortDeviceName(string portName)
        {
            using (var searcher = new ManagementObjectSearcher
               ("SELECT * FROM WIN32_SerialPort"))
            {
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();

                foreach (var port in ports)
                {
                    if (portName == port["DeviceID"].ToString())
                    {
                        return port["Name"].ToString();
                    }
                }
            }

            return "Not Found";
        }

        private void backgroundWorkerSendText_DoWork(
            object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;

            for (int i = 0; i < textBoxInput.Lines.Length; i++)
            {
                serialPort.Write((i + 1).ToString() +
                    textBoxInput.Lines[i] + '\0');

                lblQuestion.Text += textBoxInput.Lines[i] + "\r\n";

                int tick = Environment.TickCount & Int32.MaxValue;

                while ((Environment.TickCount & Int32.MaxValue) -
                    tick < 1500)
                {
                    ; // Do nothing
                }

                backgroundWorker.ReportProgress(
                    (i+1) / textBoxInput.Lines.Length * 100);
            }

        }

        private void backgroundWorkerSendText_ProgressChanged(
            object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerSendText_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void comboBoxPorts_MouseDoubleClick(
            object sender, MouseEventArgs e)
        {
            UpdateCOMportList();
        }
    }


    /* Override combobox ToString, 
     * to have different text/value */
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

}
