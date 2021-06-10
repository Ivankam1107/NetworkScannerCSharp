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

namespace NetworkScanner
{
    public partial class Form1 : Form
    {
        public Label[] labels;
        public ToolTip[] tooltips;
        private string result;
        public Color CNotExist, CHasArp, CPingSuccess, CPortAlive;
        public int PingTimeout, PortTimeout;
        public List<int> portList;

        public Form1()
        {
            InitializeComponent();
            init();
            tabControl1.TabPages[0].Controls.AddRange(labels);
            textBox1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();

        }
        public List<List<Dictionary<string, dynamic>>> ExistIPs;
        private void init()
        {
            CNotExist = ColorNotExist.BackColor;
            CHasArp = ColorHasARP.BackColor;
            CPingSuccess = ColorPingSuccess.BackColor;
            CPortAlive = ColorPortAlive.BackColor;
            PingTimeout = Convert.ToInt32(numericUpDown1.Value);
            PortTimeout = Convert.ToInt32(numericUpDown2.Value);
            labels = new Label[256];
            tooltips = new ToolTip[256];
            ExistIPs = new List<List<Dictionary<string, dynamic>>>();
            portList = new List<int>();
            openConfig();
            foreach (var item in listBox1.Items)
            {
                portList.Add(Convert.ToInt32(item));
            }
            for (int i = 0; i < 256; i++)
            {
                labels[i] = new Label();
                tooltips[i] = new ToolTip();
                /*existIPs.Add(new ExistIP());*/
                ExistIPs.Add(new List<Dictionary<string, dynamic>>() { new Dictionary<string, dynamic>()});
                labels[i].Text = i.ToString();
                labels[i].Location = new Point(28 + i % 10 * 30, 10 + (i / 10) * 20); // for example
                labels[i].AutoSize = true;
                labels[i].DoubleClick += new EventHandler(Label_Click);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            foreach(var port in listBox1.Items)
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
            numericUpDown3.Select(0,5);
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
            foreach(string port in ports)
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

        private string IPPrefix(string ip)
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
            foreach(var port in listBox1.Items)
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
                writer.WriteElementString("history", textBox2.Text);
                writer.WriteEndElement();
                writer.Flush();
            }
        }
        private bool openConfig()
        {
            try
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NetworkScanner");
                string filename = @"\Setting.xml";
                XmlDocument doc = new XmlDocument();
                doc.Load(path+filename);
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
            catch
            {
                return false;
            }
            return true;
        }
        private void removeList()
        {
            for (int x = listBox1.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = listBox1.SelectedIndices[x];
                listBox1.Items.RemoveAt(idx);
            }
        }
    }
}
