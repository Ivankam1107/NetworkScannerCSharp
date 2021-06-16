
namespace NetworkScanner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ColorPortAlive = new System.Windows.Forms.PictureBox();
            this.ColorPingSuccess = new System.Windows.Forms.PictureBox();
            this.ColorHasARP = new System.Windows.Forms.PictureBox();
            this.ColorNotExist = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label11 = new System.Windows.Forms.Label();
            this.ColorDifferentHistory = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorPortAlive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorPingSuccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorHasARP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorNotExist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorDifferentHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(159, 533);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(348, 597);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(340, 571);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Scan";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(237, 532);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 535);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(147, 20);
            this.textBox1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.numericUpDown3);
            this.tabPage2.Controls.Add(this.numericUpDown2);
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(340, 571);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Option";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(6, 362);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(316, 107);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Files Setting:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(129, 56);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(74, 23);
            this.button5.TabIndex = 27;
            this.button5.Text = "Open";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(56, 56);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(67, 23);
            this.button4.TabIndex = 26;
            this.button4.Text = "Save";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(56, 26);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(147, 20);
            this.textBox2.TabIndex = 24;
            this.textBox2.Text = ".\\History.json";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Config:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "History:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(44, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Port Exist!";
            this.label8.Visible = false;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(47, 56);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(68, 20);
            this.numericUpDown3.TabIndex = 14;
            this.numericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Click += new System.EventHandler(this.PortClick);
            this.numericUpDown3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox1_Enter);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(202, 186);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown2.TabIndex = 10;
            this.numericUpDown2.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(15, 186);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Port Timeout(ms):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ping Timeout(ms):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(121, 72);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Remove";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(121, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormatString = "N0";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "22",
            "23",
            "3389",
            "443",
            "80"});
            this.listBox1.Location = new System.Drawing.Point(202, 23);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 0;
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.applicationListBox_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ColorDifferentHistory);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.ColorPortAlive);
            this.groupBox1.Controls.Add(this.ColorPingSuccess);
            this.groupBox1.Controls.Add(this.ColorHasARP);
            this.groupBox1.Controls.Add(this.ColorNotExist);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(6, 226);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 130);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color:";
            // 
            // ColorPortAlive
            // 
            this.ColorPortAlive.BackColor = System.Drawing.Color.Lime;
            this.ColorPortAlive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ColorPortAlive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorPortAlive.Location = new System.Drawing.Point(104, 79);
            this.ColorPortAlive.Name = "ColorPortAlive";
            this.ColorPortAlive.Size = new System.Drawing.Size(19, 18);
            this.ColorPortAlive.TabIndex = 20;
            this.ColorPortAlive.TabStop = false;
            this.ColorPortAlive.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // ColorPingSuccess
            // 
            this.ColorPingSuccess.BackColor = System.Drawing.Color.Aqua;
            this.ColorPingSuccess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ColorPingSuccess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorPingSuccess.Location = new System.Drawing.Point(104, 57);
            this.ColorPingSuccess.Name = "ColorPingSuccess";
            this.ColorPingSuccess.Size = new System.Drawing.Size(19, 18);
            this.ColorPingSuccess.TabIndex = 19;
            this.ColorPingSuccess.TabStop = false;
            this.ColorPingSuccess.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // ColorHasARP
            // 
            this.ColorHasARP.BackColor = System.Drawing.Color.Yellow;
            this.ColorHasARP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ColorHasARP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorHasARP.Location = new System.Drawing.Point(104, 35);
            this.ColorHasARP.Name = "ColorHasARP";
            this.ColorHasARP.Size = new System.Drawing.Size(19, 18);
            this.ColorHasARP.TabIndex = 18;
            this.ColorHasARP.TabStop = false;
            this.ColorHasARP.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // ColorNotExist
            // 
            this.ColorNotExist.BackColor = System.Drawing.Color.Transparent;
            this.ColorNotExist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ColorNotExist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorNotExist.Location = new System.Drawing.Point(104, 13);
            this.ColorNotExist.Name = "ColorNotExist";
            this.ColorNotExist.Size = new System.Drawing.Size(19, 18);
            this.ColorNotExist.TabIndex = 11;
            this.ColorNotExist.TabStop = false;
            this.ColorNotExist.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Not Exist:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Ping Success:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Port alive:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Has ARP:";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "setting.xml";
            this.saveFileDialog1.Filter = "*.xml|*.*";
            this.saveFileDialog1.InitialDirectory = ".\\";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "setting.xml";
            this.openFileDialog1.Filter = "*.xml|*.*";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.InitialDirectory = ".\\";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Different History:";
            // 
            // ColorDifferentHistory
            // 
            this.ColorDifferentHistory.BackColor = System.Drawing.Color.Orange;
            this.ColorDifferentHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ColorDifferentHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorDifferentHistory.Location = new System.Drawing.Point(104, 103);
            this.ColorDifferentHistory.Name = "ColorDifferentHistory";
            this.ColorDifferentHistory.Size = new System.Drawing.Size(19, 18);
            this.ColorDifferentHistory.TabIndex = 22;
            this.ColorDifferentHistory.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 613);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorPortAlive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorPingSuccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorHasARP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorNotExist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorDifferentHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox ColorNotExist;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox ColorPortAlive;
        private System.Windows.Forms.PictureBox ColorPingSuccess;
        private System.Windows.Forms.PictureBox ColorHasARP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox ColorDifferentHistory;
        private System.Windows.Forms.Label label11;
    }
}

