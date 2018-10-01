namespace SSFGlasses
{
    partial class ModeBusNewFRM
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModeBusNewFRM));
            this.cmbRack = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.numDoor = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.lblRackname = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtConsoleIP = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numCol = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioUnload = new System.Windows.Forms.RadioButton();
            this.radioLoad = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDoor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbRack
            // 
            this.cmbRack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRack.Font = new System.Drawing.Font("B Nazanin", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmbRack.FormattingEnabled = true;
            this.cmbRack.Items.AddRange(new object[] {
            "جایگاه alpha (α) شماره یک",
            "جایگاه beta (β) شماره دو",
            "جایگاه gamma (γ) شماره سه",
            "جایگاه delta (δ) شماره چهار",
            "جایگاه epsilon (ε) شماره پنج",
            "جایگاه zeta (ζ) شماره شش",
            "جایگاه eta (η) شماره هفت",
            "جایگاه theta (θ) شماره هشت"});
            this.cmbRack.Location = new System.Drawing.Point(618, 142);
            this.cmbRack.Name = "cmbRack";
            this.cmbRack.Size = new System.Drawing.Size(283, 34);
            this.cmbRack.TabIndex = 0;
            this.cmbRack.SelectedIndexChanged += new System.EventHandler(this.cmbRack_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(598, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 76);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجوی واحد سلولی:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(143, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "درب";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(227, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "ستون";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(618, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 44);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numDoor
            // 
            this.numDoor.Font = new System.Drawing.Font("B Nazanin", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.numDoor.Location = new System.Drawing.Point(679, 260);
            this.numDoor.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.numDoor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDoor.Name = "numDoor";
            this.numDoor.Size = new System.Drawing.Size(106, 37);
            this.numDoor.TabIndex = 1;
            this.numDoor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numDoor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(593, 320);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 59);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "وضعیت کانتر:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(770, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "انتخاب جایگاه:";
            // 
            // lblCounter
            // 
            this.lblCounter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCounter.Font = new System.Drawing.Font("B Titr", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblCounter.Location = new System.Drawing.Point(618, 344);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(95, 61);
            this.lblCounter.TabIndex = 4;
            this.lblCounter.Text = "10";
            this.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRackname
            // 
            this.lblRackname.BackColor = System.Drawing.Color.Black;
            this.lblRackname.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblRackname.ForeColor = System.Drawing.Color.White;
            this.lblRackname.Location = new System.Drawing.Point(156, 438);
            this.lblRackname.Name = "lblRackname";
            this.lblRackname.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRackname.Size = new System.Drawing.Size(352, 29);
            this.lblRackname.TabIndex = 5;
            this.lblRackname.Text = "جایگاه zeta (ζ) شماره شش";
            this.lblRackname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button117
            // 
            this.btnConnect.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnConnect.Location = new System.Drawing.Point(520, 69);
            this.btnConnect.Name = "button117";
            this.btnConnect.Size = new System.Drawing.Size(92, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.button117_Click);
            // 
            // txtConsoleIP
            // 
            this.txtConsoleIP.BackColor = System.Drawing.SystemColors.Info;
            this.txtConsoleIP.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtConsoleIP.Location = new System.Drawing.Point(618, 69);
            this.txtConsoleIP.Name = "txtConsoleIP";
            this.txtConsoleIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtConsoleIP.Size = new System.Drawing.Size(283, 27);
            this.txtConsoleIP.TabIndex = 7;
            this.txtConsoleIP.Text = "192.168.1.5";
            this.txtConsoleIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timer1
            // 
            this.timer1.Interval = 900;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numCol
            // 
            this.numCol.Font = new System.Drawing.Font("B Nazanin", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.numCol.Location = new System.Drawing.Point(789, 261);
            this.numCol.Maximum = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.numCol.Name = "numCol";
            this.numCol.Size = new System.Drawing.Size(106, 37);
            this.numCol.TabIndex = 8;
            this.numCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCol.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1005, 566);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(126, 57);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioUnload);
            this.groupBox3.Controls.Add(this.radioLoad);
            this.groupBox3.Location = new System.Drawing.Point(598, 422);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(308, 59);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "نوع فرمان";
            // 
            // radioUnload
            // 
            this.radioUnload.AutoSize = true;
            this.radioUnload.Location = new System.Drawing.Point(20, 23);
            this.radioUnload.Name = "radioUnload";
            this.radioUnload.Size = new System.Drawing.Size(49, 17);
            this.radioUnload.TabIndex = 1;
            this.radioUnload.Text = "تخلیه";
            this.radioUnload.UseVisualStyleBackColor = true;
            // 
            // radioLoad
            // 
            this.radioLoad.AutoSize = true;
            this.radioLoad.Checked = true;
            this.radioLoad.Location = new System.Drawing.Point(90, 23);
            this.radioLoad.Name = "radioLoad";
            this.radioLoad.Size = new System.Drawing.Size(63, 17);
            this.radioLoad.TabIndex = 0;
            this.radioLoad.TabStop = true;
            this.radioLoad.Text = "بارگذاری";
            this.radioLoad.UseVisualStyleBackColor = true;
            // 
            // ModeBusNewFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 635);
            this.Controls.Add(this.lblRackname);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.numCol);
            this.Controls.Add(this.txtConsoleIP);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numDoor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbRack);
            this.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "ModeBusNewFRM";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ModeBusNewFRM_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDoor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numDoor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Label lblRackname;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtConsoleIP;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numCol;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioUnload;
        private System.Windows.Forms.RadioButton radioLoad;
    }
}