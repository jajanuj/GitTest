namespace AOISystem.Utilities.Modules.Light.Common
{
    partial class LightChannelControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpChannel = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRename = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ledLighSwitch = new AOISystem.Utilities.Forms.LedRound();
            this.nudLightValue = new AOISystem.Utilities.Forms.NumericUpDownSlider();
            this.grpChannel.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpChannel
            // 
            this.grpChannel.ContextMenuStrip = this.contextMenuStrip;
            this.grpChannel.Controls.Add(this.tableLayoutPanel);
            this.grpChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpChannel.Location = new System.Drawing.Point(0, 0);
            this.grpChannel.Name = "grpChannel";
            this.grpChannel.Size = new System.Drawing.Size(300, 100);
            this.grpChannel.TabIndex = 0;
            this.grpChannel.TabStop = false;
            this.grpChannel.Text = "Channel";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRename});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 48);
            // 
            // tsmiRename
            // 
            this.tsmiRename.Name = "tsmiRename";
            this.tsmiRename.Size = new System.Drawing.Size(152, 22);
            this.tsmiRename.Text = "Rename";
            this.tsmiRename.Click += new System.EventHandler(this.tsmiRename_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.18792F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.81208F));
            this.tableLayoutPanel.Controls.Add(this.ledLighSwitch, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.nudLightValue, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(294, 79);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // ledLighSwitch
            // 
            this.ledLighSwitch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ledLighSwitch.Location = new System.Drawing.Point(232, 3);
            this.ledLighSwitch.Name = "ledLighSwitch";
            this.ledLighSwitch.On = false;
            this.ledLighSwitch.Padding = new System.Windows.Forms.Padding(1);
            this.ledLighSwitch.Size = new System.Drawing.Size(59, 73);
            this.ledLighSwitch.TabIndex = 2;
            this.ledLighSwitch.Text = "ledRound1";
            this.ledLighSwitch.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ledLighSwitch.TextFont = new System.Drawing.Font("Lucida Sans Unicode", 14F, System.Drawing.FontStyle.Bold);
            this.ledLighSwitch.Click += new System.EventHandler(this.ledLighSwitch_Click);
            // 
            // nudLightValue
            // 
            this.nudLightValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.nudLightValue.DecimalPlaces = 0;
            this.nudLightValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudLightValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLightValue.LeftLabel = "Min";
            this.nudLightValue.Location = new System.Drawing.Point(3, 3);
            this.nudLightValue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudLightValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudLightValue.MinimumSize = new System.Drawing.Size(225, 46);
            this.nudLightValue.Name = "nudLightValue";
            this.nudLightValue.NodeName = "Value";
            this.nudLightValue.RightLabel = "Max";
            this.nudLightValue.Size = new System.Drawing.Size(225, 73);
            this.nudLightValue.TabIndex = 0;
            this.nudLightValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudLightValue.WaitMouseDownUp = false;
            this.nudLightValue.ValueChanged += new System.EventHandler(this.nudLightValue_ValueChanged);
            // 
            // LightChannelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpChannel);
            this.Name = "LightChannelControl";
            this.Size = new System.Drawing.Size(300, 100);
            this.grpChannel.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpChannel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private Forms.NumericUpDownSlider nudLightValue;
        private Forms.LedRound ledLighSwitch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiRename;
    }
}
