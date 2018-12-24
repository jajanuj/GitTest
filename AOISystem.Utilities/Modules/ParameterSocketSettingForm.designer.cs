namespace AOISystem.Utilities.Modules
{
    partial class ParameterSocketSettingForm
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
            this.grpSetting = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtReceiveTimeout = new AOISystem.Utilities.Forms.NumTextBox();
            this.txtPort = new AOISystem.Utilities.Forms.NumTextBox();
            this.lblReceiveTimeout = new System.Windows.Forms.Label();
            this.lblComPort = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.grpSetting.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSetting
            // 
            this.grpSetting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpSetting.Controls.Add(this.tableLayoutPanel);
            this.grpSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSetting.Location = new System.Drawing.Point(0, 0);
            this.grpSetting.Name = "grpSetting";
            this.grpSetting.Size = new System.Drawing.Size(232, 98);
            this.grpSetting.TabIndex = 5;
            this.grpSetting.TabStop = false;
            this.grpSetting.Text = "Settings";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.txtAddress, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.txtReceiveTimeout, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.txtPort, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblReceiveTimeout, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.lblComPort, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.lblPort, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(226, 76);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // txtAddress
            // 
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddress.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtAddress.Location = new System.Drawing.Point(116, 3);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(107, 23);
            this.txtAddress.TabIndex = 13;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // txtReceiveTimeout
            // 
            this.txtReceiveTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceiveTimeout.FilterFlag = AOISystem.Utilities.Forms.FilterType.IntegralPos;
            this.txtReceiveTimeout.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtReceiveTimeout.Location = new System.Drawing.Point(116, 53);
            this.txtReceiveTimeout.Name = "txtReceiveTimeout";
            this.txtReceiveTimeout.Size = new System.Drawing.Size(107, 23);
            this.txtReceiveTimeout.TabIndex = 11;
            this.txtReceiveTimeout.TabStop = false;
            this.txtReceiveTimeout.Tag = "";
            this.txtReceiveTimeout.TextChanged += new System.EventHandler(this.txtTimeout_TextChanged);
            // 
            // txtPort
            // 
            this.txtPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPort.FilterFlag = AOISystem.Utilities.Forms.FilterType.IntegralPos;
            this.txtPort.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPort.Location = new System.Drawing.Point(116, 28);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(107, 23);
            this.txtPort.TabIndex = 12;
            this.txtPort.TabStop = false;
            this.txtPort.Tag = "";
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // lblReceiveTimeout
            // 
            this.lblReceiveTimeout.AutoSize = true;
            this.lblReceiveTimeout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiveTimeout.Location = new System.Drawing.Point(3, 50);
            this.lblReceiveTimeout.Name = "lblReceiveTimeout";
            this.lblReceiveTimeout.Size = new System.Drawing.Size(107, 26);
            this.lblReceiveTimeout.TabIndex = 10;
            this.lblReceiveTimeout.Tag = "";
            this.lblReceiveTimeout.Text = "ReceiveTimeout :";
            this.lblReceiveTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComPort.Location = new System.Drawing.Point(3, 0);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(107, 25);
            this.lblComPort.TabIndex = 0;
            this.lblComPort.Text = "IP Address :";
            this.lblComPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPort.Location = new System.Drawing.Point(3, 25);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(107, 25);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port :";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ParameterSocketSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 98);
            this.Controls.Add(this.grpSetting);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParameterSocketSettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmParameterSerialSetting_FormClosing);
            this.Load += new System.EventHandler(this.ParameterSerialSettingForm_Load);
            this.grpSetting.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSetting;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.Label lblPort;
        private AOISystem.Utilities.Forms.NumTextBox txtReceiveTimeout;
        private System.Windows.Forms.Label lblReceiveTimeout;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Forms.NumTextBox txtPort;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

    }
}