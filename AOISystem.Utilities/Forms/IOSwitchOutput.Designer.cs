namespace AOISystem.Utilities.Forms
{
    partial class IOSwitchOutput
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblIONumber = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnStatus = new AOISystem.Utilities.Forms.SlideButton();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.lblIONumber, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.lblName, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.btnStatus, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(187, 30);
            this.tableLayoutPanel.TabIndex = 2;
            this.tableLayoutPanel.Resize += new System.EventHandler(this.IOSwitchOutput_Resize);
            // 
            // lblIONumber
            // 
            this.lblIONumber.AutoSize = true;
            this.lblIONumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIONumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIONumber.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIONumber.Location = new System.Drawing.Point(73, 0);
            this.lblIONumber.Name = "lblIONumber";
            this.lblIONumber.Size = new System.Drawing.Size(37, 30);
            this.lblIONumber.TabIndex = 3;
            this.lblIONumber.Text = "IO";
            this.lblIONumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblName.Location = new System.Drawing.Point(116, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(68, 30);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStatus
            // 
            this.btnStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnStatus.Checked = false;
            this.btnStatus.CheckStyleX = AOISystem.Utilities.Forms.CheckStyle.iOS;
            this.btnStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStatus.Location = new System.Drawing.Point(1, 1);
            this.btnStatus.Margin = new System.Windows.Forms.Padding(1);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(68, 28);
            this.btnStatus.TabIndex = 4;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // IOSwitchOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "IOSwitchOutput";
            this.Size = new System.Drawing.Size(187, 30);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblIONumber;
        private SlideButton btnStatus;


    }
}
