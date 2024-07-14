using System.Net.NetworkInformation;
using System.IO;
using System.Data;

namespace ScreensAPP
{
    public partial class Form1 : Form
    {
        DataTable pingTable = new DataTable();
        List<string> IPaddress = new List<string>();
        List<PictureBox> pictureboxlist = new List<PictureBox>();
        private System.Timers.Timer timer;

        public Form1()
        {
            InitializeComponent();
        }

        private void FillPingTable()
        {
            pingTable.Columns.Add("ip", typeof(string));
            pingTable.Columns.Add("picturebox", typeof(string));
            pingTable.Rows.Add();

            for (int i = 0; i < IPaddress.Count; i++)
            {
                pingTable.Rows.Add(IPaddress[i], pictureboxlist[i]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var reader = new StreamReader(@"C:\IP\IPaddress.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('\n');

                    IPaddress.Add(values[0]);
                }

                for (int i = 1; i < IPaddress.Count + 1; i++)
                {
                    pictureboxlist.Add((PictureBox)Controls.Find("PictureBox" + i, true)[0]);
                }
                FillPingTable();

                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            Parallel.For(0, IPaddress.Count(), (i, loopState) =>
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(IPaddress[i].ToString());
                this.BeginInvoke((Action)delegate ()
                {
                    pictureboxlist[i].SizeMode = PictureBoxSizeMode.Zoom;
                    pictureboxlist[i].BackColor = (pingReply.Status == IPStatus.Success) ? Color.Green : Color.Red;
                });

            });
        }

        
    }
}
