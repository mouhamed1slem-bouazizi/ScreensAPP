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

namespace ScreensAPP
{
    public partial class InventoryScreens : Form
    {
        DataTable pingTable = new DataTable();
        List<string> IPaddressss = new List<string>();
        List<PictureBox> pictureboxlist = new List<PictureBox>();
        List<string> labellist = new List<string>();
        private System.Timers.Timer timer;
        public InventoryScreens()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            string user = "root";
            string pass = "123456";
            string host = "10.82.130.139";

            //Set up the SSH connection
            using (var client = new SshClient(host, user, pass))
            {
                //Start the connection
                client.Connect();
                var output = client.RunCommand("uptime | awk -F'( |,|:)+' '{print $6,$7\",\",$8,\"hours,\",$9,\"minutes.\"}'");
                client.Disconnect();
                MessageBox.Show(output.Result);
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            string user = "root";
            string pass = "123456";
            string host = "10.82.130.139";

            //Set up the SSH connection
            using (var client = new SshClient(host, user, pass))
            {
                //Start the connection
                client.Connect();
                var output1 = client.RunCommand("free -m | grep Mem: | awk '{print $3}'");
                var output2 = client.RunCommand("free -m | grep Mem: | awk '{print $2}'");
                var output3 = client.RunCommand("df -h | head -4 | grep /dev/ | awk '{print $5}'");

                double n1 = Int32.Parse(output1.Result);
                double n2 = Int32.Parse(output2.Result);

                client.Disconnect();
                double result = ((n1 * 100) / n2);
                double result1 = Math.Round(result, 2);
                string result2 = result1.ToString();
                string result44 = "Mem used: " + result2 + "%";
                string result55 = "DSK used: " + output3.Result;
                string result66 = result44 + "\n" + result55;

                MessageBox.Show(result66);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string user = "root";
            string pass = "123456";
            string host = "10.82.130.139";

            //Set up the SSH connection
            using (var client = new SshClient(host, user, pass))
            {
                //Start the connection
                client.Connect();
                var output1 = client.RunCommand("free -m | grep Mem: | awk '{print $4}'");
                var output2 = client.RunCommand("sudo sysctl -w vm.drop_caches=3 && sudo sync && echo 3 | sudo tee /proc/sys/vm/drop_caches");
                var output3 = client.RunCommand("free -m | grep Mem: | awk '{print $4}'");
                client.Disconnect();
                double n1 = Int32.Parse(output1.Result);
                double n2 = Int32.Parse(output3.Result);
                string mem1 = n1.ToString();
                string mem2 = n2.ToString();

                string result44 = $"Mem before refresh: {mem1} Mg";
                string result55 = $"Mem before refresh: {mem2} Mg";
                string result66 = result44 + "\n" + result55;

                MessageBox.Show(result66);
            }
        }

        private void InventoryScreens_Load(object sender, EventArgs e)
        {
            using (var reader = new StreamReader(@"C:\IP\IPaddressss.csv"))
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
                    labellist.Add("label"+(110+i).ToString());
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
            Parallel.For(0, IPaddressss.Count(), (i, loopState) =>
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(IPaddressss[i].ToString());
                this.BeginInvoke((Action)delegate ()
                {
                    pictureboxlist[i].SizeMode = PictureBoxSizeMode.Zoom;
                    pictureboxlist[i].BackColor = (pingReply.Status == IPStatus.Success) ? Color.Green : Color.Red;
                });
                string lbl = "label11";
                string user = "root";
                string pass = "123456";
                string host = IPaddressss[i];
                //Set up the SSH connection
                using (var client = new SshClient(host, user, pass))
                {
                    //Start the connection
                    client.Connect();
                    var output = client.RunCommand("uptime | awk -F'( |,|:)+' '{print $6,$7\",\",$8,\"hours,\",$9,\"minutes.\"}'");
                    client.Disconnect();
                    lbl = labellist[i];

                    foreach (Control c in this.Controls)
                    {
                        if (c.Name == labellist[i])
                        {
                            c.Invoke((Action)delegate
                            {
                                c.Text = output.Result;
                            });
                            break;
                        }
                    }

                    
                }
            });
        }
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
                    
                    

                

                
            
        }
    }
}
