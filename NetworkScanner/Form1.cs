using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Net.NetworkInformation;

namespace NetworkScanner
{
    public partial class Form1 : Form
    {
        public static Label[] labels;
        public static ToolTip[] toolTips;
        private string result;
        public static Color CNotExist, CHasArp, CPingSuccess, CPortAlive, CDifferentHistory;
        public static int PingTimeout, PortTimeout;
        public static List<int> portList;
        /*public Dictionary<string, Dictionary<string, Dictionary<string, SortedSet<dynamic>>>> HISTORY;
        public static Dictionary<string, Dictionary<string, SortedSet<dynamic>>> CURRENT;*/
        public class JsonClass
        {
            public string IP { get; set; }
            public string Mac { get; set; }
            public string Hostname { get; set; }
            public SortedSet<int> AlivePort { get; set; }
            public string Lastseen { get; set; }
            public JsonClass(string ip)
            {
                IP = ip;
                Mac = "";
                Hostname = "";
                AlivePort = new SortedSet<int>();
                Lastseen = "";
            }
        }
        public static HashSet<JsonClass> HISTORY, CURRENT;
        public static string subnet;
        public static string dnsServerIP;
        public static string Status;
        public Form1()
        {
            InitializeComponent();
            labels = new Label[256];
            toolTips = new ToolTip[256];
            for (int i = 0; i < 256; i++)
            {
                labels[i] = new Label();
                toolTips[i] = new ToolTip();
                /*existIPs.Add(new ExistIP());*/
                labels[i].Text = i.ToString();
                labels[i].Location = new Point(28 + i % 10 * 30, 10 + (i / 10) * 20); // for example
                labels[i].AutoSize = true;
                labels[i].DoubleClick += new EventHandler(Label_Click);
            }
            textBox3.Text = GetDnsAdress();
            tabControl1.TabPages[0].Controls.AddRange(labels);
            Resize();
            this.Size = new Size(this.Width, tabControl1.Location.Y + tabControl1.Height + 50);
            init();
            textBox1.Select();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text;
            subnet = IPPrefix(ip);
            init();
            for (int i = 0; i < 256; i++)
            {
                PingScan.failIPs.Add(subnet + i.ToString());
            }
            label14.ForeColor = Color.Red;
            Status = "Ping";
            label14.Text = Status;
            if (checkBox1.Checked) new PingScan();
            Status = "Port Scan";
            label14.Text = Status;
            if (checkBox2.Checked) new PortScan();
            Status = "Hostname Lookup";
            label14.Text = Status;
            if (checkBox3.Checked) new DNSScan();
            result = IPPrefix(ip);
            Status = "Finish";
            label14.ForeColor = Color.Lime;
            label14.Text = Status;
        }
       
        private void init()
        {
            Status = "Init";
            label14.Text = Status;
            CNotExist = ColorNotExist.BackColor;
            CHasArp = ColorHasARP.BackColor;
            CPingSuccess = ColorPingSuccess.BackColor;
            CPortAlive = ColorPortAlive.BackColor;
            CDifferentHistory = ColorDifferentHistory.BackColor;
            PingTimeout = Convert.ToInt32(numericUpDown1.Value);
            PortTimeout = Convert.ToInt32(numericUpDown2.Value);
            dnsServerIP = textBox3.Text;
            PingScan.failIPs = new List<string>();
            HISTORY = new HashSet<JsonClass>();
            CURRENT = new HashSet<JsonClass>();
            if (File.Exists(textBox2.Text))
            {
                try
                {
                    string json = File.ReadAllText(textBox2.Text);
                    HISTORY = JsonConvert.DeserializeObject<Dictionary<string, HashSet<JsonClass>>>(json)[subnet];
                }
                catch
                {

                }
            }
            portList = new List<int>();
            openConfig();
            foreach (var item in listBox1.Items)
            {
                portList.Add(Convert.ToInt32(item));
            }
            foreach (Label label in labels)
            {
                label.BackColor = DefaultBackColor;
            }

        }
        public void MergeCurrHistory(string ip)
        {
            try
            {
                File.WriteAllText(textBox2.Text, JsonConvert.SerializeObject(HISTORY));
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var port in listBox1.Items)
            {
                if (Convert.ToInt32(port) == numericUpDown3.Value)
                {
                    label8.Visible = true;
                    return;
                }
            }
            listBox1.Items.Add(numericUpDown3.Value);
            label8.Visible = false;
        }

        private void listBox1_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                removeList();
            }
        }

        private void PortClick(object sender, EventArgs e)
        {
            numericUpDown3.Select(0, 5);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveConfig();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            Clipboard.SetText(result + label.Text);
        }

        private void applicationListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                for (int x = listBox1.SelectedIndices.Count - 1; x >= 0; x--)
                {
                    int idx = listBox1.SelectedIndices[x];
                    listBox1.Items.RemoveAt(idx);
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog openFileDialog = sender as OpenFileDialog;
            XmlDocument doc = new XmlDocument();
            doc.Load(openFileDialog.FileName);
            XmlNodeList nodelist = doc.SelectNodes("/Setting");
            string[] ports = nodelist[0].SelectSingleNode("portList").InnerText.Split(',');
            listBox1.Items.Clear();
            foreach (string port in ports)
            {
                listBox1.Items.Add(port);
            }
            numericUpDown1.Value = Convert.ToInt32(nodelist[0].SelectSingleNode("pingTimeout").InnerText);
            numericUpDown2.Value = Convert.ToInt32(nodelist[0].SelectSingleNode("portTimeout").InnerText);
            ColorNotExist.BackColor = Color.FromArgb(Convert.ToInt32(nodelist[0].SelectSingleNode("colorNotExist").InnerText));
            ColorHasARP.BackColor = Color.FromArgb(Convert.ToInt32(nodelist[0].SelectSingleNode("colorHasARP").InnerText));
            ColorPingSuccess.BackColor = Color.FromArgb(Convert.ToInt32(nodelist[0].SelectSingleNode("colorPingSuccess").InnerText));
            ColorPortAlive.BackColor = Color.FromArgb(Convert.ToInt32(nodelist[0].SelectSingleNode("colorPortAlive").InnerText));
            textBox2.Text = nodelist[0].SelectSingleNode("history").InnerText;
        }

        public static string IPPrefix(string ip)
        {
            string[] prefix = ip.Split('.');
            return prefix[0] + "." + prefix[1] + "." + prefix[2] + ".";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            removeList();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            colorDialog1.Color = pictureBox.BackColor;
            colorDialog1.ShowDialog();
            if (colorDialog1.Color == Color.White)
            {
                pictureBox.BackColor = Color.Transparent;
            }
            else
            {
                pictureBox.BackColor = colorDialog1.Color;
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = checkBox3.Checked;
        }

        private void saveConfig()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NetworkScanner");
            string filename = @"\Setting.xml";
            try
            {
                Directory.CreateDirectory(path);
            }
            catch
            {
                saveFileDialog1.ShowDialog();
                path = saveFileDialog1.InitialDirectory;
            }
            string portList = "";
            foreach (var port in listBox1.Items)
            {
                portList += port.ToString() + ',';
            }
            using (XmlWriter writer = XmlWriter.Create(path + filename))
            {
                writer.WriteStartElement("Setting");
                writer.WriteElementString("portList", portList.Substring(0, portList.Length - 1));
                writer.WriteElementString("pingTimeout", numericUpDown1.Value.ToString());
                writer.WriteElementString("portTimeout", numericUpDown2.Value.ToString());
                writer.WriteElementString("colorNotExist", ColorNotExist.BackColor.ToArgb().ToString());
                writer.WriteElementString("colorHasARP", ColorHasARP.BackColor.ToArgb().ToString());
                writer.WriteElementString("colorPingSuccess", ColorPingSuccess.BackColor.ToArgb().ToString());
                writer.WriteElementString("colorPortAlive", ColorPortAlive.BackColor.ToArgb().ToString());
                writer.WriteElementString("colorDifferentHistory", ColorDifferentHistory.BackColor.ToArgb().ToString());
                writer.WriteElementString("history", textBox2.Text);
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        private dynamic GetXmlValue(XmlNodeList nodelist, string key)
        {
            try
            {
                return Convert.ToInt32(nodelist[0].SelectSingleNode(key)?.InnerText);
            }
            catch
            {
                return nodelist[0].SelectSingleNode(key)?.InnerText;
            }

        }

        private void Resize()
        {
            textBox1.Location = new Point(6, labels[250].Location.Y+18);
            button1.Location = new Point(button1.Location.X, textBox1.Location.Y);
            label14.Location = new Point(label14.Location.X, textBox1.Location.Y);
            ////////////////////////////////////////////////////////////////////////////
            checkBox1.Location = new Point(34, textBox1.Location.Y + 23);
            checkBox2.Location = new Point(checkBox2.Location.X,checkBox1.Location.Y);
            checkBox3.Location = new Point(checkBox3.Location.X, checkBox1.Location.Y);
            textBox3.Location = new Point(textBox3.Location.X,checkBox1.Location.Y);
            tabControl1.Size = new Size(
                labels[9].Location.X+2*labels[9].Width+labels[0].Location.X,
                checkBox1.Location.Y+checkBox1.Height*2+labels[0].Location.Y+10);
        }
        private bool openConfig()
        {
            try
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NetworkScanner");
                string filename = @"\Setting.xml";
                XmlDocument doc = new XmlDocument();
                Dictionary<string, string> config = new Dictionary<string, string>();
                doc.Load(path + filename);
                XmlNodeList nodelist = doc.SelectNodes("/Setting");
                string[] ports = nodelist[0].SelectSingleNode("portList").InnerText.Split(',');
                listBox1.Items.Clear();
                foreach (string port in ports)
                {
                    listBox1.Items.Add(port);
                }
                numericUpDown1.Value = (GetXmlValue(nodelist, "pingTimeout") != 0) ? GetXmlValue(nodelist, "pingTimeout") : numericUpDown1.Value;
                numericUpDown2.Value = (GetXmlValue(nodelist, "portTimeout") != 0) ? GetXmlValue(nodelist, "portTimeout") : numericUpDown2.Value;
                ColorNotExist.BackColor = (GetXmlValue(nodelist, "colorNotExist") != 0) ? Color.FromArgb(GetXmlValue(nodelist, "colorNotExist")) : ColorNotExist.BackColor;
                ColorHasARP.BackColor = (GetXmlValue(nodelist, "colorHasARP") != 0) ? Color.FromArgb(GetXmlValue(nodelist, "colorHasARP")) : ColorHasARP.BackColor;
                ColorPingSuccess.BackColor = (GetXmlValue(nodelist, "colorPingSuccess") != 0) ? Color.FromArgb(GetXmlValue(nodelist, "colorPingSuccess")) : ColorPingSuccess.BackColor;
                ColorPortAlive.BackColor = (GetXmlValue(nodelist, "colorPortAlive") != 0) ? Color.FromArgb(GetXmlValue(nodelist, "colorPortAlive")) : ColorPortAlive.BackColor;
                ColorDifferentHistory.BackColor = (GetXmlValue(nodelist, "colorDifferentHistory") != 0) ? Color.FromArgb(GetXmlValue(nodelist, "colorDifferentHistory")) : ColorDifferentHistory.BackColor;
                textBox2.Text = (GetXmlValue(nodelist, "histroy") != 0) ? GetXmlValue(nodelist, "histroy") : textBox2.Text;
            }
            catch
            {
                return false;
            }
            return true;
        }
        private static string GetDnsAdress()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                    IPAddressCollection dnsAddresses = ipProperties.DnsAddresses;
                    return dnsAddresses.First().ToString();
                }
            }

            throw new InvalidOperationException("Unable to find DNS Address");
        }
        private void removeList()
        {
            for (int x = listBox1.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = listBox1.SelectedIndices[x];
                listBox1.Items.RemoveAt(idx);
            }
        }
        public static string s_lockObject = "";
        public static void DisplayTips(string ip="")
        {
            
            int tag = Convert.ToInt32(ip.ToString().Split('.')[3]);
            string tips = "";
            JsonClass curr;
            lock (s_lockObject)
            {
                curr = CURRENT.FirstOrDefault(x => x.IP == ip);
            }
            
            List<JsonClass> historys = HISTORY.Where(x => x.IP == ip).ToList();
            int hostnameMaxLen = 8, alivePortMaxLen=11; 
            int tempLength;
            if (curr != null && historys.Count==0)
            {
                //show curr[ip] info
                if (Status == "Ping")
                {
                    labels[tag].BackColor = CPingSuccess;
                    return;
                }
                tempLength = curr.Hostname.Length;
                if (hostnameMaxLen < tempLength) hostnameMaxLen = tempLength;
                tempLength = GetAlivePorts(curr.AlivePort).Length;
                if (alivePortMaxLen < tempLength) alivePortMaxLen = tempLength;
                tips += String.Format("{4,1}| {0,-17} | {1,-" + hostnameMaxLen + "} | {2,-" + alivePortMaxLen + "} | {3,-19} |\r\n", curr.Mac, curr.Hostname, GetAlivePorts(curr.AlivePort), curr.Lastseen, "*");
                if(curr.AlivePort.Count>0) labels[tag].BackColor = CPortAlive;
            }
            else if (curr == null && historys.Count != 0)
            {
                //show sub[ip] info
                labels[tag].BackColor = CDifferentHistory;
                tempLength = historys.Select(x => x.Hostname.Length).Max();
                if (hostnameMaxLen < tempLength) hostnameMaxLen = tempLength;
                tempLength = historys.Select(x => GetAlivePorts(x.AlivePort).Length).Max();
                if (alivePortMaxLen < tempLength) alivePortMaxLen = tempLength;
                foreach(JsonClass history in historys)
                {
                    tips += String.Format("{4,1}| {0,-17} | {1,-" + hostnameMaxLen + "} | {2,-" + alivePortMaxLen + "} | {3,-19} |\r\n", history.Mac, history.Hostname, GetAlivePorts(history.AlivePort), history.Lastseen, "");
                }
            }
            else if (curr != null && historys.Count != 0)
            {
                
                /*int hostnameMaxLen = 8, alivePortMaxLen = 11;
                int tempLength;*/
                tempLength = historys.Select(x => x.Hostname.Length).Max();
                if (hostnameMaxLen < tempLength) hostnameMaxLen = tempLength;
                tempLength = historys.Select(x => GetAlivePorts(x.AlivePort).Length).Max();
                if (alivePortMaxLen < tempLength) alivePortMaxLen = tempLength;
                if(!historys.Select(x=>x.Hostname).Contains(curr.Hostname)) labels[tag].BackColor = CDifferentHistory;
                foreach(SortedSet<int> alivePorts in historys.Select(x => x.AlivePort))
                {
                    if (!curr.AlivePort.Except(alivePorts).Any()) labels[tag].BackColor = CDifferentHistory;
                }
                tips += String.Format("{4,1}| {0,-17} | {1,-" + hostnameMaxLen + "} | {2,-" + alivePortMaxLen + "} | {3,-19} |\r\n", curr.Mac, curr.Hostname, GetAlivePorts(curr.AlivePort), curr.Lastseen, "*");
                foreach (JsonClass history in historys)
                {
                    tips += String.Format("{4,1}| {0,-17} | {1,-" + hostnameMaxLen + "} | {2,-" + alivePortMaxLen + "} | {3,-19} |\r\n", history.Mac, history.Hostname, GetAlivePorts(history.AlivePort), history.Lastseen, "");
                }
                
                //compare
                //MessageBox.Show(ip);
            }
            else
            {
                return;
            }
            if (labels[tag].BackColor == CPingSuccess) return;
            tips = String.Format("{4,1}| {0,-17} | {1,-" + hostnameMaxLen + "} | {2,-" + alivePortMaxLen + "} | {3,-19} |\r\n", "Mac Address", "Hostname","Alive Ports" ,"Last Seen","*")+tips;
            toolTips[tag].OwnerDraw = true;
            toolTips[tag].Draw += new DrawToolTipEventHandler(toolTip1_Draw);
            toolTips[tag].Popup += new PopupEventHandler(toolTip1_Popup);
            toolTips[tag].UseAnimation = true;
            toolTips[tag].AutoPopDelay = 500;
            toolTips[tag].AutomaticDelay = 500;
            Form1.labels[tag].BeginInvoke((MethodInvoker)delegate
            {
                toolTips[tag].SetToolTip(labels[tag], tips);
            });
            void toolTip1_Popup(object sender, PopupEventArgs e)
            {
                // on popip set the size of tool tip
                e.ToolTipSize = TextRenderer.MeasureText(tips, new Font("Courier New", 8.0f));
            }
            void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
            {
                Font f = new Font("Courier New", 8.0f);
                e.DrawBackground();
                e.DrawBorder();
                tips = e.ToolTipText;
                e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2, 2));
            }
        }
        
        private static string GetAlivePorts(SortedSet<int> ports)
        {
            string alivePorts = JsonConvert.SerializeObject(ports).Replace("[", "]").Replace("]", "");
            return alivePorts;
        }
    }
}
