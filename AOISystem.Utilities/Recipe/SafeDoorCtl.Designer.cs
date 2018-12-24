namespace AOISystem.Utilities.Recipe
{
    partial class SafeDoorCtl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SafeDoorCtl));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitleText = new System.Windows.Forms.Label();
            this.tlpLow = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblEnable = new System.Windows.Forms.Label();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNumValue = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblStationValue = new System.Windows.Forms.Label();
            this.lblStation = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.tlpLow.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.lblTitleText, 0, 0);
            this.tlpMain.Controls.Add(this.tlpLow, 0, 1);
            this.tlpMain.Name = "tlpMain";
            // 
            // lblTitleText
            // 
            resources.ApplyResources(this.lblTitleText, "lblTitleText");
            this.lblTitleText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitleText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblTitleText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTitleText.Name = "lblTitleText";
            // 
            // tlpLow
            // 
            resources.ApplyResources(this.tlpLow, "tlpLow");
            this.tlpLow.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tlpLow.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tlpLow.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpLow.Name = "tlpLow";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.lblEnable, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkEnable, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // lblEnable
            // 
            resources.ApplyResources(this.lblEnable, "lblEnable");
            this.lblEnable.Name = "lblEnable";
            // 
            // chkEnable
            // 
            resources.ApplyResources(this.chkEnable, "chkEnable");
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.UseVisualStyleBackColor = true;
            this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnable_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lblNumValue, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblNum, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // lblNumValue
            // 
            resources.ApplyResources(this.lblNumValue, "lblNumValue");
            this.lblNumValue.Name = "lblNumValue";
            // 
            // lblNum
            // 
            resources.ApplyResources(this.lblNum, "lblNum");
            this.lblNum.Name = "lblNum";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblStationValue, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStation, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblStationValue
            // 
            resources.ApplyResources(this.lblStationValue, "lblStationValue");
            this.lblStationValue.Name = "lblStationValue";
            // 
            // lblStation
            // 
            resources.ApplyResources(this.lblStation, "lblStation");
            this.lblStation.Name = "lblStation";
            // 
            // SafeDoorCtl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.MinimumSize = new System.Drawing.Size(175, 93);
            this.Name = "SafeDoorCtl";
            this.Load += new System.EventHandler(this.SafeDoorCtl_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpLow.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblTitleText;
        private System.Windows.Forms.TableLayoutPanel tlpLow;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblEnable;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblStation;
        private System.Windows.Forms.Label lblNumValue;
        private System.Windows.Forms.Label lblStationValue;
    }
}
