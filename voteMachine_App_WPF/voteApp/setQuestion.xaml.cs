using System.IO.Ports;
using System.Windows;

namespace voteApp
{
    /// <summary>
    /// Interaction logic for setQuestion.xaml
    /// </summary>
    public partial class setQuestion : Window
    {
        SerialPort serialPort;
        MainWindow mainWindow;
        string redTextDefault = "RÖTT VAL";
        string greenTextDefault = "GRÖNT VAL";
        string inputTextDefault = "Fråga";

        public setQuestion(MainWindow mainWindow, SerialPort serialPort)
        {
            InitializeComponent();
            this.serialPort = serialPort;
            this.mainWindow = mainWindow;

            textBoxGreenOpt.Text = greenTextDefault;
            textBoxRedOpt.Text = redTextDefault;
            textBoxInput.Text = inputTextDefault;
        }

        private void btnSendQuestion_Click(
            object sender, RoutedEventArgs e)
        {
            string command = "";

            char[] replaceChars = { 'å', 'ä', 'ö', 'Å', 'Ä', 'Ö' };
            char[] replaceWith = { 'a', 'a', 'o', 'A', 'A', 'O' };

            command += "R" + textBoxRedOpt.Text + "\r\n";
            command += "G" + textBoxGreenOpt.Text + "\r\n";

            for (int i = 0; i < textBoxInput.LineCount || i < 4; i++)
            {
                command += (i+1) + textBoxInput.GetLineText(i) + "\r\n";
            }

            /* Replace ÅÄÖåäö with AaOo */
            for (int i = 0; i < replaceChars.Length; i++)
            {
                command = command.Replace(replaceChars[i], replaceWith[i]);
            }

            /* Send command if serialport is open */
            if (command != "" && serialPort.IsOpen)
            {
                serialPort.WriteLine(command);
            }

            /* Update labels in main window */
            mainWindow.UpdateQuestion(
                textBoxInput.Text, textBoxRedOpt.Text, textBoxGreenOpt.Text);

            this.Close();
        }

        private void btnCancel_Click(
            object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBoxRedOpt_GotFocus(
            object sender, RoutedEventArgs e)
        {
            if (textBoxRedOpt.Text == redTextDefault)
            {
                textBoxRedOpt.Clear();
            }
        }

        private void textBoxRedOpt_LostFocus(
            object sender, RoutedEventArgs e)
        {
            if (textBoxRedOpt.Text == "")
            {
                textBoxRedOpt.Text = redTextDefault;
            }
        }

        private void textBoxGreenOpt_GotFocus(
            object sender, RoutedEventArgs e)
        {
            if (textBoxGreenOpt.Text == greenTextDefault)
            {
                textBoxGreenOpt.Clear();
            }
        }

        private void textBoxGreenOpt_LostFocus(
            object sender, RoutedEventArgs e)
        {
            if (textBoxGreenOpt.Text == "")
            {
                textBoxGreenOpt.Text = greenTextDefault;
            }
        }

        private void textBoxInput_GotFocus(
            object sender, RoutedEventArgs e)
        {
            if (textBoxInput.Text == inputTextDefault)
            {
                textBoxInput.Text = "";
            }
        }
    }
}
