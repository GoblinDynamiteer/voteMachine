using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace voteApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] ports;
        SerialPort serialPort;
        string serialData;
        public delegate void NextPrimeDelegate();

        public MainWindow()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            serialPort.BaudRate = 115200;
            this.serialPort.DataReceived += new
                SerialDataReceivedEventHandler(serialPort_DataReceived);

            UpdateCOMportList();

        }

        public void UpdateQuestion(
            string question, string optionRed, string optionGreen)
        {
            textQuestion.Text = question;
            lblGreenOption.Content = optionGreen;
            lblRedOption.Content = optionRed;
        }

        /* Update drop-down list with available COM-ports */
        void UpdateCOMportList(bool getDeviceNames = false)
        {
            ports = SerialPort.GetPortNames();

            Array.Sort(ports);

            listboxCOM.Items.Clear();

            /* Populate combobox with ports 
             * (port names and device names) */
            foreach (string port in ports)
            {
                listboxCOM.Items.Add(port);
            }

        }

        /* Open COM-port selected in list */
        private void btnOpenCOM_Click(
            object sender, RoutedEventArgs e)
        {
            string comPort = listboxCOM.Text;

            if (serialPort.PortName != comPort)
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }

                OpenCOM(comPort);

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

                    textboxStatus.AppendText(
                        "Port " + serialPort.PortName +
                        " öppnad!\r\n");
                }

                catch (Exception)
                {
                    textboxStatus.AppendText("Kunde inte öppna " 
                        + serialPort.PortName + "!");
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
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                new NextPrimeDelegate(DisplayText));
        }

        /* Display serial data and misc */
        private void DisplayText()
        {

            /* Display data in textbox */
            textboxStatus.AppendText(serialData);

            if (checkboxAutoScroll.IsChecked == true)
            {
                textboxStatus.ScrollToEnd();
            }

            if (serialData.Contains("votes"))
            {
                UpdateVoteLabels();
            }
        }

        private void Button_Click(
            object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Är du säker på att du vill återställa?", 
                "Återställning", 
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes && serialPort.IsOpen)
            {
                serialPort.WriteLine("C");
            }
        }

        private void btnNewQuestion_Click(
            object sender, RoutedEventArgs e)
        {
            Window newQuestion = new setQuestion(this, serialPort);
            newQuestion.Show();
        }

        /* Filter votes, update labels */
        private void UpdateVoteLabels()
        {
            string number = Regex.Match(
                serialData, @"\d+").Value;

            if (serialData.Contains("Green"))
            {
                lblGreenVotes.Content = number;
            }

            if (serialData.Contains("Red"))
            {
                lblRedVotes.Content = number;
            }

        }
    }
}
