using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace voteApp
{
    public partial class WebForm : Form
    {
        const string regexIp = @"\d+\.\d+\.\d+\.\d+";
        const string regexMac = @".{2}\-.{2}\-.{2}\-.{2}\-.{2}\-.{2}";
        const string routerMac = "90-8d-78-b6-6d-48";

        public WebForm()
        {
            InitializeComponent();
            UpdateDeviceList();
            this.Text = "Hitta web!";
        }

        private void btnGetDevices_Click(object sender, EventArgs e)
        {
            UpdateDeviceList();
        }

        private void UpdateDeviceList()
        {
            listBoxDevices.Items.Clear();

            string[] result = GetARPResult();

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Trim();

                if (routerMac == Regex.Match(result[i], regexMac).Value)
                {
                    lblWebLink.Text = "http://" + Regex.Match(
                        result[i], regexIp).Value;
                }
            }

            listBoxDevices.Items.AddRange(result);
        }

        private static string[] GetARPResult()
        {
            Process p = null;
            string output = string.Empty;

            try
            {
                p = Process.Start(new ProcessStartInfo(
                    @"C:\Windows\SysWOW64\ARP.EXE", "-a")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                });

                output = p.StandardOutput.ReadToEnd();

                p.Close();
            }
            catch
            {

            }

            finally
            {
                if (p != null)
                {
                    p.Close();
                }
            }
            string[] stringSeparators = new string[] { "\n" };

            return output.Split(
                stringSeparators, StringSplitOptions.None); ;
        }

        private void listBoxDevices_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            try
            {
                string ip = Regex.Match(
                    listBoxDevices.Text, regexIp).Value;
                string mac = Regex.Match(
                    listBoxDevices.Text, regexMac).Value;

                tbIp.Text = ip;
                tbMac.Text = mac;

                lblWebLink.Text = "http://" + ip;
            }

            catch
            {
                tbIp.Text = "NONE";
                tbMac.Text = "NONE";
            }
        }

        private void lblWebLink_LinkClicked(
            object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(lblWebLink.Text);
        }
    }
}
