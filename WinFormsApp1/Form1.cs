using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            main_();
            Size = new Size(410, 220);
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            label1.Text = "private ip";
            label2.Text = "public ip";
            label3.Text = "mac";
            label4.Text = "dns";
            label1.Font = new Font("dotum", 14);
            label2.Font = new Font("dotum", 14);
            label3.Font = new Font("dotum", 14);
            label4.Font = new Font("dotum", 14);
            label5.Font = new Font("dotum", 14);
            label6.Font = new Font("dotum", 14);
            label7.Font = new Font("dotum", 14);
            label8.Font = new Font("dotum", 14);
            label1.Location = new Point(10, 20);
            label2.Location = new Point(10, 50);
            label3.Location = new Point(10, 80);
            label4.Location = new Point(10, 110);
            label5.Location = new Point(100, 20);
            label6.Location = new Point(100, 50);
            label7.Location = new Point(100, 80);
            label8.Location = new Point(100, 110);
            button1.Text = "copy";
            button2.Text = "copy";
            button3.Text = "copy";
            button4.Text = "copy";
            button5.Text = "network";
            button6.Text = "ping";
            button7.Text = "speedtest";
            button1.Location = new Point(300, 20);
            button2.Location = new Point(300, 50);
            button3.Location = new Point(300, 80);
            button4.Location = new Point(300, 110);
            button5.Location = new Point(50, 140);
            button6.Location = new Point(150, 140);
            button7.Location = new Point(250, 140);
            /*
            2023.06
            net7 / visual studio 2022 ver 17.6
            chatgpt
            */
        }

        void main_()
        {
            RegistryKey localmachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
            var opensubkey = localmachine.OpenSubKey("software\\microsoft\\windows nt\\currentversion");
            int currentbuild = Convert.ToInt32(opensubkey.GetValue("currentbuild"));
            if (currentbuild < 19045)
            {
                MessageBox.Show("windows 10 update 22H2" + Environment.NewLine + "윈도우 업데이트 필요");
            }
            ip();
            dns();
            mac();
            localmachine.Dispose();
        }

        void ip()
        {
            IPAddress[] ipaddress = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ipaddress2 in ipaddress)
            {
                if (ipaddress2.AddressFamily == AddressFamily.InterNetwork)
                {
                    label5.Text = ipaddress2.ToString();
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                WebClient webclient = new WebClient();
                label6.Text = webclient.DownloadString("http://checkip.amazonaws.com").Trim();
                webclient.Dispose();
            }
        }

        void dns()
        {
            NetworkInterface[] networkinterface = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkinterface2 in networkinterface)
            {
                IPInterfaceProperties ipinterfaceproperties = networkinterface2.GetIPProperties();
                IPAddressCollection ipaddresscollection = ipinterfaceproperties.DnsAddresses;
                if (ipaddresscollection.Count > 0)
                {
                    label8.Text = ipaddresscollection[0].ToString();
                    break;//list stop
                }
            }
        }

        void mac()
        {
            NetworkInterface[] networkinterface = NetworkInterface.GetAllNetworkInterfaces();
            PhysicalAddress physicaladdress = networkinterface[0].GetPhysicalAddress();
            label7.Text = BitConverter.ToString(physicaladdress.GetAddressBytes());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            string chrome = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\google\\chrome\\application";
            string msedge = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\microsoft\\edge\\application";
            string url = "http://www.speedtest.net";
            if (Directory.Exists(chrome))
            {
                process.StartInfo.FileName = chrome + "\\chrome";
                process.StartInfo.Arguments = "/new-window /incognito" + string.Empty.PadLeft(1) + url;
                process.Start();
            }
            else
            {
                process.StartInfo.FileName = msedge + "\\msedge";
                process.StartInfo.Arguments = "/new-window /inprivate" + string.Empty.PadLeft(1) + url;
                process.Start();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = "/c" + string.Empty.PadLeft(1) + "ping -t" + string.Empty.PadLeft(1) + label8.Text;
            process.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = "/c" + string.Empty.PadLeft(1) + "ncpa.cpl";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label8.Text == string.Empty == false)
            {
                Clipboard.SetText(label8.Text);
                MessageBox.Show(Clipboard.GetText());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label7.Text == string.Empty == false)
            {
                Clipboard.SetText(label7.Text);
                MessageBox.Show(Clipboard.GetText());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label6.Text == string.Empty == false)
            {
                Clipboard.SetText(label6.Text);
                MessageBox.Show(Clipboard.GetText());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label5.Text == string.Empty == false)
            {
                Clipboard.SetText(label5.Text);
                MessageBox.Show(Clipboard.GetText());
            }
        }
    }
}