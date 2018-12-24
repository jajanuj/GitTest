namespace AOISystem.Utilities.Threading
{
    partial class frmTaskMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaskMonitor));
            this.dgvTaskMonitor = new System.Windows.Forms.DataGridView();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnClearIsCompleted = new System.Windows.Forms.Button();
            this.btnClearIsCanceled = new System.Windows.Forms.Button();
            this.btnClearIsFaulted = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskMonitor)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTaskMonitor
            // 
            this.dgvTaskMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTaskMonitor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskMonitor.Location = new System.Drawing.Point(0, 37);
            this.dgvTaskMonitor.Name = "dgvTaskMonitor";
            this.dgvTaskMonitor.ReadOnly = true;
            this.dgvTaskMonitor.RowHeadersVisible = false;
            this.dgvTaskMonitor.RowTemplate.Height = 24;
            this.dgvTaskMonitor.Size = new System.Drawing.Size(810, 292);
            this.dgvTaskMonitor.TabIndex = 1;
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHide.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnHide.ForeColor = System.Drawing.Color.Black;
            this.btnHide.Image = ((System.Drawing.Image)(resources.GetObject("btnHide.Image")));
            this.btnHide.Location = new System.Drawing.Point(723, 8);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(75, 23);
            this.btnHide.TabIndex = 32;
            this.btnHide.Text = "  Hide";
            this.btnHide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnClearIsCompleted
            // 
            this.btnClearIsCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearIsCompleted.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearIsCompleted.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClearIsCompleted.ForeColor = System.Drawing.Color.Black;
            this.btnClearIsCompleted.Location = new System.Drawing.Point(12, 8);
            this.btnClearIsCompleted.Name = "btnClearIsCompleted";
            this.btnClearIsCompleted.Size = new System.Drawing.Size(131, 23);
            this.btnClearIsCompleted.TabIndex = 33;
            this.btnClearIsCompleted.Text = "Clear IsCompleted";
            this.btnClearIsCompleted.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearIsCompleted.UseVisualStyleBackColor = true;
            this.btnClearIsCompleted.Click += new System.EventHandler(this.btnClearIsCompleted_Click);
            // 
            // btnClearIsCanceled
            // 
            this.btnClearIsCanceled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearIsCanceled.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearIsCanceled.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClearIsCanceled.ForeColor = System.Drawing.Color.Black;
            this.btnClearIsCanceled.Location = new System.Drawing.Point(149, 8);
            this.btnClearIsCanceled.Name = "btnClearIsCanceled";
            this.btnClearIsCanceled.Size = new System.Drawing.Size(131, 23);
            this.btnClearIsCanceled.TabIndex = 34;
            this.btnClearIsCanceled.Text = "Clear IsCanceled";
            this.btnClearIsCanceled.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearIsCanceled.UseVisualStyleBackColor = true;
            this.btnClearIsCanceled.Click += new System.EventHandler(this.btnClearIsCanceled_Click);
            // 
            // btnClearIsFaulted
            // 
            this.btnClearIsFaulted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearIsFaulted.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearIsFaulted.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClearIsFaulted.ForeColor = System.Drawing.Color.Black;
            this.btnClearIsFaulted.Location = new System.Drawing.Point(286, 8);
            this.btnClearIsFaulted.Name = "btnClearIsFaulted";
            this.btnClearIsFaulted.Size = new System.Drawing.Size(131, 23);
            this.btnClearIsFaulted.TabIndex = 35;
            this.btnClearIsFaulted.Text = "Clear IsFailed";
            this.btnClearIsFaulted.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearIsFaulted.UseVisualStyleBackColor = true;
            this.btnClearIsFaulted.Click += new System.EventHandler(this.btnClearIsFaulted_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Location = new System.Drawing.Point(423, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(131, 23);
            this.btnRefresh.TabIndex = 36;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmTaskMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 329);
            this.ControlBox = false;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClearIsFaulted);
            this.Controls.Add(this.btnClearIsCanceled);
            this.Controls.Add(this.btnClearIsCompleted);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.dgvTaskMonitor);
            this.Name = "frmTaskMonitor";
            this.Text = "Task Monitor";
            this.Activated += new System.EventHandler(this.frmTaskMonitor_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTaskMonitor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskMonitor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTaskMonitor;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnClearIsCompleted;
        private System.Windows.Forms.Button btnClearIsCanceled;
        private System.Windows.Forms.Button btnClearIsFaulted;
        private System.Windows.Forms.Button btnRefresh;
    }
}