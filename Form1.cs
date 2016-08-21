using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace DekkInfo
{
    public partial class Form1 : Form
    {
        // Initalize
        public Form1()
        {
            InitializeComponent();

            textSystemUptime.Text = GetSystemUptime();
            textHostname.Text = System.Environment.MachineName;
            textExternal.Text = new WebClient().DownloadString("http://icanhazip.com");
            textInternal.Text = GetLocalIP();
            textCpuLoad.Text = GetPerformanceCPU();
            textMemory.Text = GetPerformanceRam();
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

            // Refresh timer for 1 second
            timer1 = new Timer { Interval = 1000, Enabled = true };
            timer1.Tick += new EventHandler(Refresh);
        }

        public static string GetLocalIP()
        {
            // Get local IP address
            string ipv4Address = String.Empty;
            foreach (IPAddress currentIPAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (currentIPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipv4Address = currentIPAddress.ToString();
                    break;
                }
            }
            // Return ip address
            return ipv4Address;
        }

        public static string GetPerformanceCPU()
        {
            // Performance counter for CPU
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            // Return CPU value
            return perfCpuCount.NextValue() + "%";
        }

        public static string GetPerformanceRam()
        {
            // Performance counter for RAM
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");

            // Return RAM value
            return perfMemCount.NextValue() + "MB";
        }

        public static string GetSystemUptime()
        {
            // Get the system uptime
            PerformanceCounter perfupTimeCount = new PerformanceCounter("System", "System Up Time");
            perfupTimeCount.NextValue();
            TimeSpan uptimeSpan = TimeSpan.FromSeconds(perfupTimeCount.NextValue());

            // Return uptime
            return string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds", uptimeSpan.Days, uptimeSpan.Hours, uptimeSpan.Minutes, uptimeSpan.Seconds);
        }

        private void Refresh(object sender, EventArgs e)
        {
            // Fill input fields
            textSystemUptime.Text = GetSystemUptime();
            textHostname.Text = System.Environment.MachineName;
            textExternal.Text = new WebClient().DownloadString("http://icanhazip.com");
            textInternal.Text = GetLocalIP();
            textCpuLoad.Text = GetPerformanceCPU();
            textMemory.Text = GetPerformanceRam();
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            // Visit website
            System.Diagnostics.Process.Start("http://www.bdekker.nl");
        }

        private void textExternal_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

