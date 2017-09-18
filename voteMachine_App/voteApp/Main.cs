using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Linq;
using System.Management;


namespace voteApp
{
    public partial class frmMain : Form
    {
        string[] ports;
        private string serialData;

        enum Error
        {
            COMPortNotOpen,
            COMOpenError
        }

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

            foreach (string port in ports)
            {
                ComboboxItem item = new ComboboxItem();

                item.Text = port + ": " + SerialPortDeviceName(port);
                item.Value = port;

                comboBoxPorts.Items.Add(item);
            }

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

        private void DisplayError(Error error)
        {
            textBoxData.AppendText("Error: ");

            switch (error)
            {
                case Error.COMPortNotOpen:
                    textBoxData.AppendText("COM-port not open!");
                    break;

                case Error.COMOpenError:
                    textBoxData.AppendText("Cannot open COM-port!");
                    break;

                default:
                    break;
            }

            textBoxData.AppendText("\r\n");
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

            string newPort = (comboBoxPorts.SelectedItem as ComboboxItem).Value.ToString();

            textBoxData.AppendText("Byter port " + serialPort.PortName + "->" 
                + newPort + "\r\n");

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
