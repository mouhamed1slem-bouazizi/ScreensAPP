using System.Net.NetworkInformation;
using System.IO;
using System.Data;
using System.Timers;
using System.Windows.Forms;

namespace ScreensAPP
{
    public partial class Form1 : Form
    {
        DataTable pingTable = new DataTable();
        List<string> IPaddress = new List<string>();
        List<PictureBox> pictureboxlist = new List<PictureBox>();
        private System.Timers.Timer timer;
        private NotifyIcon notifyIcon;

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeNotifyIcon();
        }

        private void InitializeTimer()
        {
            timer = new System.Timers.Timer(300000); // 300000 ms = 5 minutes
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.IsHandleCreated)
            {
                this.BeginInvoke((Action)PingAllIPs);
            }
        }

        private void FillPingTable()
        {
            pingTable.Columns.Add("ip", typeof(string));
            pingTable.Columns.Add("picturebox", typeof(string));

            for (int i = 0; i < IPaddress.Count; i++)
            {
                pingTable.Rows.Add(IPaddress[i], pictureboxlist[i]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var reader = new StreamReader(@"C:\IP\IP_servers.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    IPaddress.Add(line.Trim());
                }

                for (int i = 0; i < IPaddress.Count; i++)
                {
                    pictureboxlist.Add((PictureBox)Controls.Find("PictureBox" + (i + 1), true)[0]);
                }
                FillPingTable();

                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            PingAllIPs();
        }

        private void PingAllIPs()
        {
            Parallel.For(0, IPaddress.Count, (i) =>
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(IPaddress[i]);
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke((Action)delegate ()
                    {
                        pictureboxlist[i].SizeMode = PictureBoxSizeMode.Zoom;
                        pictureboxlist[i].BackColor = (pingReply.Status == IPStatus.Success) ? Color.Green : Color.Red;
                    });
                }
            });

            // Show notification after ping operation completes
            ShowNotification("Ping operation completed", "All IP addresses have been pinged.");
        }

        private void ShowNotification(string title, string message)
        {
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = message;
            notifyIcon.ShowBalloonTip(3000); // Show for 3 seconds
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InventoryScreens newForm = new InventoryScreens();
            newForm.FormClosed += (s, args) => this.Close();
            newForm.Show();
            this.Hide();
        }
    }
}