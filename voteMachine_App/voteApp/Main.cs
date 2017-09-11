﻿using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace voteApp
{
    public partial class frmMain : Form
    {
        string[] ports;
        private string serialData;

        public frmMain()
        {
            InitializeComponent();

            UpdateCOMportList();

            if (OpenCOM())
            {
                try
                {
                    serialPort.Write("S");
                }

                catch
                {

                }
                
            }

        }

        /* Open COM-Port */
        bool OpenCOM()
        {

            if (!serialPort.IsOpen)
            {
                foreach (string port in ports)
                {
                    try
                    {
                        serialPort.PortName = port;
                        serialPort.Open();

                        textBoxData.AppendText(
                            "Port " + serialPort.PortName + " öppnad!\r\n");

                        if (serialPort.IsOpen)
                        {
                            return true;
                        }
                    }

                    catch (Exception)
                    {
                        textBoxData.AppendText(
                            "Port " + serialPort.PortName + " kunde inte öppnas!\r\n");

                    }

                }

            }

            return false;
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
            for (int i = 0; i < textBoxInput.Lines.Length; i++)
            {
                serialPort.Write((i + 1).ToString() + textBoxInput.Lines[i] + '\0');

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
            serialPort.Write("C");
            textBoxInput.Text = "";
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            serialData = serialPort.ReadExisting();
            this.Invoke(new EventHandler(DisplayText));
        }

        /* Display serial data and misc */
        private void DisplayText(object o, EventArgs e)
        {
            /* TODO: Intercept RED/GREEN votes -- ADD to labels / graphics */

            /* Display data in textbox */
            textBoxData.AppendText(serialData);
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            serialPort.Write("S"); // Check status
        }

        private void comboBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
