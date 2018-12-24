namespace AOISystem.Utilities.Forms
{
    partial class DirectoryTreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectoryTreeView));
            this.directoryIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // directoryIcons
            // 
            this.directoryIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("directoryIcons.ImageStream")));
            this.directoryIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.directoryIcons.Images.SetKeyName(0, "Desktop.ico");
            this.directoryIcons.Images.SetKeyName(1, "Computer.ico");
            this.directoryIcons.Images.SetKeyName(2, "Closed Folder.ico");
            this.directoryIcons.Images.SetKeyName(3, "Open Folder.ico");
            this.directoryIcons.Images.SetKeyName(4, "fixed drive.ico");
            this.directoryIcons.Images.SetKeyName(5, "My Documents.ico");
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList directoryIcons;
    }
}
