namespace AOISystem.Utilities.Modules
{
    partial class ParameterSerialSettingForm
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
            this.gbPortSettings = new System.Windows.Forms.GroupBox();
            this.ntxbReadTimeout = new AOISystem.Utilities.Forms.NumTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ntxbWriteTimeout = new AOISystem.Utilities.Forms.NumTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxbReadDelayTime = new AOISystem.Utilities.Forms.NumTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ntxbWriteIntervalTime = new AOISystem.Utilities.Forms.NumTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gbPortSettings.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPortSettings
            // 
            this.gbPortSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbPortSettings.Controls.Add(this.tableLayoutPanel);
            this.gbPortSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPortSettings.Location = new System.Drawing.Point(0, 0);
            this.gbPortSettings.Name = "gbPortSettings";
            this.gbPortSettings.Size = new System.Drawing.Size(401, 174);
            this.gbPortSettings.TabIndex = 5;
            this.gbPortSettings.TabStop = false;
            this.gbPortSettings.Text = "COM Serial Port Settings";
            // 
            // ntxbReadTimeout
            // 
            this.ntxbReadTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbReadTimeout.FilterFlag = AOISystem.Utilities.Forms.FilterType.IntegralPos;
            this.ntxbReadTimeout.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbReadTimeout.Location = new System.Drawing.Point(297, 3);
            this.ntxbReadTimeout.Name = "ntxbReadTimeout";
            this.ntxbReadTimeout.Size = new System.Drawing.Size(95, 23);
            this.ntxbReadTimeout.TabIndex = 11;
            this.ntxbReadTimeout.TabStop = false;
            this.ntxbReadTimeout.Tag = "";
            this.ntxbReadTimeout.TextChanged += new System.EventHandler(this.ntxbTimeout_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(199, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 30);
            this.label2.TabIndex = 10;
            this.label2.Text = "Read Timeout:";
            // 
            // cmbPortName
            // 
            this.cmbPortName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cmbPortName.Location = new System.Drawing.Point(101, 3);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(92, 24);
            this.cmbPortName.TabIndex = 1;
            this.cmbPortName.SelectedIndexChanged += new System.EventHandler(this.cmbPortName_SelectedIndexChanged);
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(101, 33);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(92, 24);
            this.cmbBaudRate.TabIndex = 3;
            this.cmbBaudRate.SelectedIndexChanged += new System.EventHandler(this.cmbPortName_SelectedIndexChanged);
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cmbStopBits.Location = new System.Drawing.Point(101, 123);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(92, 24);
            this.cmbStopBits.TabIndex = 9;
            this.cmbStopBits.SelectedIndexChanged += new System.EventHandler(this.cmbPortName_SelectedIndexChanged);
            // 
            // cmbParity
            // 
            this.cmbParity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.cmbParity.Location = new System.Drawing.Point(101, 63);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(92, 24);
            this.cmbParity.TabIndex = 5;
            this.cmbParity.SelectedIndexChanged += new System.EventHandler(this.cmbPortName_SelectedIndexChanged);
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
            this.cmbDataBits.Location = new System.Drawing.Point(101, 93);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(92, 24);
            this.cmbDataBits.TabIndex = 7;
            this.cmbDataBits.SelectedIndexChanged += new System.EventHandler(this.cmbPortName_SelectedIndexChanged);
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComPort.Location = new System.Drawing.Point(3, 0);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(92, 30);
            this.lblComPort.TabIndex = 0;
            this.lblComPort.Text = "COM Port:";
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStopBits.Location = new System.Drawing.Point(3, 120);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(92, 32);
            this.lblStopBits.TabIndex = 8;
            this.lblStopBits.Text = "Stop Bits:";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBaudRate.Location = new System.Drawing.Point(3, 30);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(92, 30);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataBits.Location = new System.Drawing.Point(3, 90);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(92, 30);
            this.lblDataBits.TabIndex = 6;
            this.lblDataBits.Text = "Data Bits:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 30);
            this.label1.TabIndex = 4;
            this.label1.Text = "Parity:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ntxbWriteTimeout
            // 
            this.ntxbWriteTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbWriteTimeout.FilterFlag = AOISystem.Utilities.Forms.FilterType.IntegralPos;
            this.ntxbWriteTimeout.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbWriteTimeout.Location = new System.Drawing.Point(297, 33);
            this.ntxbWriteTimeout.Name = "ntxbWriteTimeout";
            this.ntxbWriteTimeout.Size = new System.Drawing.Size(95, 23);
            this.ntxbWriteTimeout.TabIndex = 13;
            this.ntxbWriteTimeout.TabStop = false;
            this.ntxbWriteTimeout.Tag = "";
            this.ntxbWriteTimeout.TextChanged += new System.EventHandler(this.ntxbTimeout_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(199, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 30);
            this.label3.TabIndex = 12;
            this.label3.Text = "Write Timeout:";
            // 
            // ntxbReadDelayTime
            // 
            this.ntxbReadDelayTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbReadDelayTime.FilterFlag = AOISystem.Utilities.Forms.FilterType.IntegralPos;
            this.ntxbReadDelayTime.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbReadDelayTime.Location = new System.Drawing.Point(297, 63);
            this.ntxbReadDelayTime.Name = "ntxbReadDelayTime";
            this.ntxbReadDelayTime.Size = new System.Drawing.Size(95, 23);
            this.ntxbReadDelayTime.TabIndex = 15;
            this.ntxbReadDelayTime.TabStop = false;
            this.ntxbReadDelayTime.Tag = "";
            this.ntxbReadDelayTime.TextChanged += new System.EventHandler(this.ntxbTimeout_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(199, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 30);
            this.label4.TabIndex = 14;
            this.label4.Text = "Read Delay Time:";
            // 
            // ntxbWriteIntervalTime
            // 
            this.ntxbWriteIntervalTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbWriteIntervalTime.FilterFlag = AOISystem.Utilities.Forms.FilterType.IntegralPos;
            this.ntxbWriteIntervalTime.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbWriteIntervalTime.Location = new System.Drawing.Point(297, 93);
            this.ntxbWriteIntervalTime.Name = "ntxbWriteIntervalTime";
            this.ntxbWriteIntervalTime.Size = new System.Drawing.Size(95, 23);
            this.ntxbWriteIntervalTime.TabIndex = 17;
            this.ntxbWriteIntervalTime.TabStop = false;
            this.ntxbWriteIntervalTime.Tag = "";
            this.ntxbWriteIntervalTime.TextChanged += new System.EventHandler(this.ntxbTimeout_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(199, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 30);
            this.label5.TabIndex = 16;
            this.label5.Text = "Write Interval Time:";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.Controls.Add(this.ntxbWriteIntervalTime, 3, 3);
            this.tableLayoutPanel.Controls.Add(this.lblComPort, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.cmbPortName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.ntxbReadDelayTime, 3, 2);
            this.tableLayoutPanel.Controls.Add(this.label5, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.ntxbWriteTimeout, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.lblBaudRate, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.ntxbReadTimeout, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.cmbBaudRate, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.label4, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.lblDataBits, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.cmbParity, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.lblStopBits, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.cmbDataBits, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.cmbStopBits, 1, 4);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(395, 152);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // ParameterSerialSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 174);
            this.Controls.Add(this.gbPortSettings);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParameterSerialSettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmParameterSerialSetting_FormClosing);
            this.Load += new System.EventHandler(this.ParameterSerialSettingForm_Load);
            this.gbPortSettings.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPortSettings;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.Label label1;
        private AOISystem.Utilities.Forms.NumTextBox ntxbReadTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Forms.NumTextBox ntxbWriteIntervalTime;
        private System.Windows.Forms.Label label5;
        private Forms.NumTextBox ntxbReadDelayTime;
        private System.Windows.Forms.Label label4;
        private Forms.NumTextBox ntxbWriteTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

    }
}