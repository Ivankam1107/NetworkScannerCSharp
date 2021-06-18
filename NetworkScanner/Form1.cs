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
        public static ToolTip[] tooltips;
        private string result;
        public static Color CNotExist, CHasArp, CPingSuccess, CPortAlive, CDifferentHistory;
        public static int PingTimeout, PortTimeout;
        public static List<int> portList;
        public Dictionary<string, Dictionary<string, Dictionary<string, SortedSet<dynamic>>>> HISTORY;
        public static Dictionary<string, Dictionary<string, SortedSet<dynamic>>> CURRENT;
        public static Dictionary<string, Dictionary<string, SortedSet<dynamic>>> SUBNET;
        public static string subnet;
        public static string dnsServerIP;
        public static string Status;
        public Form1()
        {
            InitializeComponent();
            labels = new Label[256];
            tooltips = new ToolTip[256];
            for (int i = 0; i < 256; i++)
            {
                labels[i] = new Label();
                tooltips[i] = new ToolTip();
                /*existIPs.Add(new ExistIP());*/
                labels[i].Text = i.ToString();
                labels[i].Location = new Point(28 + i % 10 * 30, 10 + (i / 10) * 20); // for example
                labels[i].AutoSize = true;
                labels[i].DoubleClick += new EventHandler(Label_Click);
            }
            textBox3.Text = GetDnsAdress();
            tabControl1.TabPages[0].Controls.AddRange(labels);
            init();
            textBox1.Select();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            init();
            string ip = textBox1.Text;
            subnet = IPPrefix(ip);
            for (int i = 0; i < 256; i++)
            {
                PingScan.failIPs.Add(subnet + i.ToString());
            }
            GetSubnetHistory(ip);
            if (checkBox1.Checked) new PingScan();
            if (checkBox2.Checked) new PortScan();
            if (checkBox3.Checked) new DNSScan();
            result = IPPrefix(ip);
        }

        private void init()
        {
            Status = "Init";
            CNotExist = ColorNotExist.BackColor;
            CHasArp = ColorHasARP.BackColor;
            CPingSuccess = ColorPingSuccess.BackColor;
            CPortAlive = ColorPortAlive.BackColor;
            CDifferentHistory = ColorDifferentHistory.BackColor;
            PingTimeout = Convert.ToInt32(numericUpDown1.Value);
            PortTimeout = Convert.ToInt32(numericUpDown2.Value);
            dnsServerIP = textBox3.Text;
            PingScan.failIPs = new List<string>();
            HISTORY = new Dictionary<string, Dictionary<string, Dictionary<string, SortedSet<dynamic>>>>();
            CURRENT = new Dictionary<string, Dictionary<string, SortedSet<dynamic>>>();
            if (File.Exists(textBox2.Text))
            {
                try
                {
                    string json = File.ReadAllText(textBox2.Text);
                    HISTORY = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, SortedSet<dynamic>>>>>(json);
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
            for (int i = 0; i < 256; i++)
            {
                tooltips[i].Dispose();
                tooltips[i] = new ToolTip();
            }

        }
        public void GetSubnetHistory(string ip)
        {
            try
            {
                SUBNET = HISTORY[subnet];
            }
            catch
            {
                SUBNET = new Dictionary<string, Dictionary<string, SortedSet<dynamic>>>();
            }
            Console.WriteLine(JsonConvert.SerializeObject(SUBNET));
        }

        public void MergeCurrHistory(string ip)
        {
            //Current For Test
            //string tempJson = "{\"192.168.1.2\":{\"Hostname\":[\"iamhost\"],\"PortAlive\":[22,4431]}}";
            //CURRENT = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, SortedSet<dynamic>>>>(tempJson);
            ////////////////////////////
            try
            {
                HISTORY.Add(subnet, new Dictionary<string, Dictionary<string, SortedSet<dynamic>>>());
            }
            catch
            {

            }
            foreach (string IP in CURRENT.Keys)
            {
                try
                {
                    HISTORY[subnet].Add(IP, new Dictionary<string, SortedSet<dynamic>>());
                }
                catch
                {

                }
                foreach (string Key in CURRENT[IP].Keys)
                {
                    try
                    {
                        HISTORY[subnet][IP].Add(Key, new SortedSet<dynamic>());
                    }
                    catch
                    {

                    }
                    foreach (var value in CURRENT[IP][Key])
                    {
                        try
                        {
                            HISTORY[subnet][IP][Key].Add(value);
                        }
                        catch
                        {

                        }
                    }
                }
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {

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
        public static void DisplayTips(string ip="")
        {
            int tag = Convert.ToInt32(ip.ToString().Split('.')[3]);
            string tips = "";
            if (!SUBNET.ContainsKey(ip) && CURRENT.ContainsKey(ip))
            {
                //show curr[ip] info
                labels[tag].BackColor = CPingSuccess;
                if (CURRENT[ip].ContainsKey("Mac"))
                {
                    labels[tag].BackColor = CHasArp;
                    tips += String.Format("{0}", CURRENT[ip]["Mac"].ElementAt(0))+"\r\n";
                }
                if (CURRENT[ip].ContainsKey("Hostname"))
                {
                    labels[tag].BackColor = CHasArp;
                    tips += String.Format("{0}", CURRENT[ip]["Hostname"].ElementAt(0)) + "\r\n";
                }
                if (CURRENT[ip].ContainsKey("PortAlive"))
                {
                    labels[tag].BackColor = CPortAlive;
                    tips += String.Format("Alive Ports:{0}", JsonConvert.SerializeObject(CURRENT[ip]["PortAlive"]));
                }

            }
            else if(SUBNET.ContainsKey(ip) && !CURRENT.ContainsKey(ip))
            {
                //show sub[ip] info
                labels[tag].BackColor = CDifferentHistory;
                tips = "History:\r\n";
                if (SUBNET[ip].ContainsKey("Mac"))
                {
                    tips += String.Format("  {0}", JsonConvert.SerializeObject(SUBNET[ip]["Mac"])) + "\r\n";
                }
                if (SUBNET[ip].ContainsKey("Hostname"))
                {
                    tips += String.Format("  {0}", JsonConvert.SerializeObject(SUBNET[ip]["Hostname"])) + "\r\n";
                }
                if (SUBNET[ip].ContainsKey("PortAlive"))
                {
                    tips += String.Format("  Alive Ports:{0}", JsonConvert.SerializeObject(SUBNET[ip]["PortAlive"]));
                }
            }
            else if(SUBNET.ContainsKey(ip) && CURRENT.ContainsKey(ip))
            {
                //Compare
                if((CURRENT[ip].ContainsKey("Mac") && SUBNET[ip].ContainsKey("Mac")) || (CURRENT[ip].ContainsKey("Hostname") && SUBNET[ip].ContainsKey("Hostname")) || (CURRENT[ip].ContainsKey("PortAlive") && SUBNET[ip].ContainsKey("PortAlive")))
                {
                    labels[tag].BackColor = CDifferentHistory;
                    tips = ShowDiif(ip);
                }
            }
            else
            {
                return;
            }
            Form1.labels[tag].BeginInvoke((MethodInvoker)delegate
            {
                tooltips[tag].SetToolTip(Form1.labels[tag], tips);
            });
        }
        private static string ShowDiif(string ip="")
        {
            string tips="";

            
            return tips;
        }
    }
}
