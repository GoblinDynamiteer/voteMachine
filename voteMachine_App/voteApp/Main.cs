﻿using System;
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
            for (int i = 0; i < textBoxInput.Lines.Length; i++)
            {
                serialPort.Write((i+1).ToString() + textBoxInput.Lines[i] + '\0');

                int tick = Environment.TickCount & Int32.MaxValue;

                while ((Environment.TickCount & Int32.MaxValue) - tick < 1500)
                {
                    ; // Do nothing
                }
            }

        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            lblLines.Text = textBoxInput.Lines.Length.ToString();
            lblStringSend.Text = "";

            for (int i = 0; i < textBoxInput.Lines.Length; i++)
            {
                lblStringSend.Text += (i + 1).ToString() + textBoxInput.Lines[i] + "\n\r";
            }
        }
    }
}
