namespace AOISystem.Utilities.BarcodeScanner
{
    partial class BarcodeText
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
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtBarCode
            // 
            this.txtBarCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBarCode.Location = new System.Drawing.Point(0, 0);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(144, 22);
            this.txtBarCode.TabIndex = 0;
            // 
            // BarcodeText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBarCode);
            this.Name = "BarcodeText";
            this.Size = new System.Drawing.Size(144, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBarCode;
    }
}
