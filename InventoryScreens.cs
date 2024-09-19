using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using Renci.SshNet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.DataFormats;

namespace ScreensAPP
{
    public partial class InventoryScreens : Form
    {
        DataTable pingTable = new DataTable();
        List<string> IPaddressss = new List<string>();
        List<PictureBox> pictureboxlist = new List<PictureBox>();
        List<string> Uplabellist = new List<string>();
        List<string> Memlabellist = new List<string>();
        List<string> timelabellist = new List<string>();
        List<string> maclabellist = new List<string>();
        List<string> fidslabellist = new List<string>();
        private System.Timers.Timer timer;
        public InventoryScreens()
        {
            InitializeComponent();
            label5031.Click += new EventHandler(label_Click);
            label5001.Click += new EventHandler(label_Click);
        }

        private void label_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Label clickedLabel = sender as System.Windows.Forms.Label;
            if (clickedLabel != null)
            {
                Clipboard.SetText(clickedLabel.Text);
                MessageBox.Show("Text copied to clipboard: " + clickedLabel.Text);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string user = "root";
            string pass = "123456";
            string host = "10.82.130.139";

            //Set up the SSH connection
            using (var client = new SshClient(host, user, pass))
            {
                //Start the connection
                client.Connect();
                var output = client.RunCommand("/opt/fids/fidsclient restart");
                client.Disconnect();
                MessageBox.Show(output.Result);
            }
        }

        private void InventoryScreens_Load(object sender, EventArgs e)
        {
            using (var reader = new StreamReader(@"C:\IP\IP_Screens_List.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\n');

                    IPaddressss.Add(values[0]);
                }

                for (int i = 1; i < IPaddressss.Count + 1; i++)
                {
                    pictureboxlist.Add((PictureBox)Controls.Find("PictureBox" + i, true)[0]);
                    Uplabellist.Add("label" + (2000 + i).ToString());
                    Memlabellist.Add("label" + (3000 + i).ToString());
                    timelabellist.Add("label" + (4000 + i).ToString());
                    maclabellist.Add("label" + (5000 + i).ToString());
                    fidslabellist.Add("label" + (6000 + i).ToString());
                }

                FillPingTable();

                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void FillPingTable()
        {
            pingTable.Columns.Add("ip", typeof(string));
            pingTable.Columns.Add("picturebox", typeof(string));
            pingTable.Rows.Add();

            for (int i = 0; i < IPaddressss.Count; i++)
            {
                pingTable.Rows.Add(IPaddressss[i], pictureboxlist[i]);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            Parallel.For(0, IPaddressss.Count, (i, loopState) =>
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(IPaddressss[i]);
                this.BeginInvoke((Action)delegate ()
                {
                    pictureboxlist[i].SizeMode = PictureBoxSizeMode.Zoom;
                    pictureboxlist[i].BackColor = (pingReply.Status == IPStatus.Success) ? Color.Green : Color.Red;
                });

                // Skip SSH connection if ping fails
                if (pingReply.Status != IPStatus.Success)
                {
                    return;
                }

                string user = "root";
                string pass = "123456";
                string host = IPaddressss[i];

                // Set up the SSH connection
                using (var client = new SshClient(host, user, pass))
                {
                    try
                    {
                        // Start the connection
                        client.Connect();
                        var Upoutput = client.RunCommand("uptime | awk -F'( |,|:)+' '{print $6,$7\",\",$8,\"hours,\",$9,\"minutes.\"}'");
                        var output1 = client.RunCommand("free -m | grep Mem: | awk '{print $2}'");
                        var output2 = client.RunCommand("sudo sysctl -w vm.drop_caches=3 && sudo sync && echo 3 | sudo tee /proc/sys/vm/drop_caches");
                        var output3 = client.RunCommand("free -m | grep Mem: | awk '{print $4}'");
                        var timeoutput = client.RunCommand("date +\"Time: %d %B %T\"");
                        var macoutput = client.RunCommand("ip address show | grep -m 1 ether | awk '{print $2}'");
                        var fidsoutput = client.RunCommand("/opt/fids/fidsclient status | grep FIDS | awk '{print $1,$2,$3,$4}'");

                        client.Disconnect();

                        UpdateControlText(Uplabellist[i], Upoutput.Result);
                        UpdateControlText(Memlabellist[i], $"Mem Free: {((double)Int32.Parse(output3.Result) * 100 / Int32.Parse(output1.Result)).ToString("0.00")}%");
                        UpdateControlText(timelabellist[i], timeoutput.Result);
                        UpdateControlText(maclabellist[i], macoutput.Result);
                        UpdateControlText(fidslabellist[i], fidsoutput.Result);
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception as needed
                        Console.WriteLine($"Failed to connect to {host}: {ex.Message}");
                    }
                }
            });
        }

        private void UpdateControlText(string controlName, string text)
        {
            foreach (var tabPage in new[] { tabPage1, tabPage2, tabPage3, tabPage4, tabPage5, tabPage6, tabPage7 })
            {
                var control = tabPage.Controls.Find(controlName, true).FirstOrDefault();
                if (control != null)
                {
                    control.Invoke((Action)delegate
                    {
                        control.Text = text;
                    });
                    break;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        
            
        

    }
}