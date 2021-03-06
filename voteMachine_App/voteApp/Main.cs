﻿using System;
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
        bool updateGreenOpt, updateRedOpt, updateQuestion;

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

            lblQuestion.Text = "";
            updateGreenOpt = false;
            updateRedOpt = false;
            updateQuestion = false;

        }

        /* Display errors in data textbox */
        private void DisplayError(Error error, string extra = "")
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
                        "Cannot open port " + extra + "!");
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

        #region Button methods

        /* Event method for send button */
        private void btnSend_Click(object sender, EventArgs e)
        {
            string command = "";

            if (updateRedOpt)
            {
                updateRedOpt = false;
                command += "R" + textBoxRedOpt.Text + "\r\n";
                lblVoteOptRed.Text = textBoxRedOpt.Text;
            }

            if (updateGreenOpt)
            {
                updateGreenOpt = false;
                command += "G" + textBoxGreenOpt.Text + "\r\n";
                lblVoteOptGreen.Text = textBoxGreenOpt.Text;
            }

            if (updateQuestion)
            {
                lblQuestion.Text = textBoxInput.Text;

                int lineCount = 1;
                foreach (string line in textBoxInput.Lines)
                {
                    command += lineCount++ + line + "\r\n";
                }
                updateQuestion = false;
            }

            if (command != "" && serialPort.IsOpen)
            {
                serialPort.WriteLine(command);
            }

        }

        /* Event method for status button */
        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine("S"); // Check status
            }
        }

        /* Clear voteMachine Display 
        * and textbox */
        private void btnClear_Click(
            object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine("C");
                textBoxInput.Text = "";

                textBoxGreenOpt.Text = "";
                textBoxRedOpt.Text = "";

                lblQuestion.Text = "";

                lblVoteOptGreen.Text = "Green";
                lblVoteOptRed.Text = "Red";

                lblGreenVotes.Text = "0";
                lblRedVotes.Text = "0";
            }
        }

        /* Event method  */
        private void btnWeb_Click(
            object sender, EventArgs e)
        {
            Form webForm = new WebForm();
            webForm.Show();
        }

        /* Event method for Open Com button */
        private void lblComOpen_Click(
            object sender, EventArgs e)
        {
            serialPort.Close();

            string newPort = (
                comboBoxPorts.SelectedItem
                as ComboboxItem).Value.ToString();

            textBoxData.AppendText("Byter port " +
                serialPort.PortName + "-> " + newPort + "\r\n");

            if (OpenCOM(newPort))
            {
                serialPort.Write("S"); // Check status
            }
        }

        #endregion

        #region ComPort/serialport methods

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
                    DisplayError(Error.COMOpenError,
                        serialPort.PortName);
                    success = false;
                }

            }

            return success;
        }

        /* Event method for serial data available */
        private void serialPort_DataReceived(
            object sender, SerialDataReceivedEventArgs e)
        {
            serialData = serialPort.ReadLine();

            this.Invoke(new EventHandler(DisplayText));
        }

        /* Update drop-down list with available COM-ports */
        void UpdateCOMportList(bool getDeviceNames = false)
        {
            ports = SerialPort.GetPortNames();

            Array.Sort(ports);

            comboBoxPorts.Items.Clear();

            /* Populate combobox with ports 
             * (port names and device names) */
            foreach (string port in ports)
            {
                ComboboxItem item = new ComboboxItem();

                item.Text = port + (getDeviceNames ? ": " + SerialPortDeviceName(port) : "");
                item.Value = port;

                comboBoxPorts.Items.Add(item);
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

        #endregion

        private void textBoxGreenOpt_TextChanged(object sender, EventArgs e)
        {
            updateGreenOpt = true;
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            updateQuestion = true;
        }

        private void textBoxRedOpt_TextChanged(object sender, EventArgs e)
        {
            updateRedOpt = true;
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
