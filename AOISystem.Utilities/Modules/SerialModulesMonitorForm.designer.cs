namespace AOISystem.Utilities.Modules
{
    partial class SerialModulesMonitorForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSendClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbOnSendCommand = new System.Windows.Forms.RichTextBox();
            this.rtbOnReceiveCommand = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rtbSendCommand = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnResetCmd = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.ckbHex = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReceiveClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.rtbOnSendCommand, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.rtbOnReceiveCommand, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(521, 622);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btnSendClear, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(1, 251);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(519, 23);
            this.tableLayoutPanel5.TabIndex = 8;
            // 
            // btnSendClear
            // 
            this.btnSendClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendClear.Location = new System.Drawing.Point(470, 1);
            this.btnSendClear.Margin = new System.Windows.Forms.Padding(1);
            this.btnSendClear.Name = "btnSendClear";
            this.btnSendClear.Size = new System.Drawing.Size(48, 21);
            this.btnSendClear.TabIndex = 0;
            this.btnSendClear.Text = "Clear";
            this.btnSendClear.UseVisualStyleBackColor = true;
            this.btnSendClear.Click += new System.EventHandler(this.btnSendClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Send";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbOnSendCommand
            // 
            this.rtbOnSendCommand.BackColor = System.Drawing.Color.Aquamarine;
            this.rtbOnSendCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOnSendCommand.Location = new System.Drawing.Point(1, 276);
            this.rtbOnSendCommand.Margin = new System.Windows.Forms.Padding(1);
            this.rtbOnSendCommand.Name = "rtbOnSendCommand";
            this.rtbOnSendCommand.ReadOnly = true;
            this.rtbOnSendCommand.Size = new System.Drawing.Size(519, 224);
            this.rtbOnSendCommand.TabIndex = 4;
            this.rtbOnSendCommand.Text = "";
            // 
            // rtbOnReceiveCommand
            // 
            this.rtbOnReceiveCommand.BackColor = System.Drawing.Color.Aquamarine;
            this.rtbOnReceiveCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOnReceiveCommand.Location = new System.Drawing.Point(1, 26);
            this.rtbOnReceiveCommand.Margin = new System.Windows.Forms.Padding(1);
            this.rtbOnReceiveCommand.Name = "rtbOnReceiveCommand";
            this.rtbOnReceiveCommand.ReadOnly = true;
            this.rtbOnReceiveCommand.Size = new System.Drawing.Size(519, 223);
            this.rtbOnReceiveCommand.TabIndex = 3;
            this.rtbOnReceiveCommand.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(1, 502);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Custom Command";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.rtbSendCommand, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 522);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(519, 99);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // rtbSendCommand
            // 
            this.rtbSendCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSendCommand.Location = new System.Drawing.Point(1, 1);
            this.rtbSendCommand.Margin = new System.Windows.Forms.Padding(1);
            this.rtbSendCommand.Name = "rtbSendCommand";
            this.rtbSendCommand.Size = new System.Drawing.Size(517, 67);
            this.rtbSendCommand.TabIndex = 0;
            this.rtbSendCommand.Text = "";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnResetCmd, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSend, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.ckbHex, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1, 70);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(517, 28);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnResetCmd
            // 
            this.btnResetCmd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnResetCmd.Location = new System.Drawing.Point(186, 1);
            this.btnResetCmd.Margin = new System.Windows.Forms.Padding(1);
            this.btnResetCmd.Name = "btnResetCmd";
            this.btnResetCmd.Size = new System.Drawing.Size(111, 26);
            this.btnResetCmd.TabIndex = 3;
            this.btnResetCmd.Text = "Reset Cmd";
            this.btnResetCmd.UseVisualStyleBackColor = true;
            this.btnResetCmd.Click += new System.EventHandler(this.btnResetCmd_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSend.Location = new System.Drawing.Point(369, 1);
            this.btnSend.Margin = new System.Windows.Forms.Padding(1);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(111, 26);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ckbHex
            // 
            this.ckbHex.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbHex.AutoSize = true;
            this.ckbHex.Enabled = false;
            this.ckbHex.Location = new System.Drawing.Point(50, 3);
            this.ckbHex.Name = "ckbHex";
            this.ckbHex.Size = new System.Drawing.Size(49, 20);
            this.ckbHex.TabIndex = 2;
            this.ckbHex.Text = "Hex";
            this.ckbHex.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnReceiveClear, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(519, 23);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // btnReceiveClear
            // 
            this.btnReceiveClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReceiveClear.Location = new System.Drawing.Point(470, 1);
            this.btnReceiveClear.Margin = new System.Windows.Forms.Padding(1);
            this.btnReceiveClear.Name = "btnReceiveClear";
            this.btnReceiveClear.Size = new System.Drawing.Size(48, 21);
            this.btnReceiveClear.TabIndex = 0;
            this.btnReceiveClear.Text = "Clear";
            this.btnReceiveClear.UseVisualStyleBackColor = true;
            this.btnReceiveClear.Click += new System.EventHandler(this.btnReceiveClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Receive";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SerialModulesMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 622);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SerialModulesMonitorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Serial Modules Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialModulesMonitorForm_FormClosing);
            this.Load += new System.EventHandler(this.SerialModulesMonitorForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox rtbOnSendCommand;
        private System.Windows.Forms.RichTextBox rtbOnReceiveCommand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RichTextBox rtbSendCommand;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox ckbHex;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnSendClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnReceiveClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResetCmd;
    }
}