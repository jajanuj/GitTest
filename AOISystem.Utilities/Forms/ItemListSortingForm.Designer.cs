namespace AOISystem.Utilities.Forms
{
    partial class ItemListSortingForm
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelSub1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelSub3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanelSub2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanelSub1.SuspendLayout();
            this.tableLayoutPanelSub3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tableLayoutPanelSub2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelSub1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelSub2, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(318, 479);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // tableLayoutPanelSub1
            // 
            this.tableLayoutPanelSub1.ColumnCount = 2;
            this.tableLayoutPanelSub1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSub1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanelSub1.Controls.Add(this.tableLayoutPanelSub3, 1, 0);
            this.tableLayoutPanelSub1.Controls.Add(this.dataGridView, 0, 0);
            this.tableLayoutPanelSub1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSub1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelSub1.Name = "tableLayoutPanelSub1";
            this.tableLayoutPanelSub1.RowCount = 1;
            this.tableLayoutPanelSub1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSub1.Size = new System.Drawing.Size(312, 423);
            this.tableLayoutPanelSub1.TabIndex = 1;
            // 
            // tableLayoutPanelSub3
            // 
            this.tableLayoutPanelSub3.ColumnCount = 1;
            this.tableLayoutPanelSub3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSub3.Controls.Add(this.btnDown, 0, 1);
            this.tableLayoutPanelSub3.Controls.Add(this.btnUp, 0, 0);
            this.tableLayoutPanelSub3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSub3.Location = new System.Drawing.Point(253, 3);
            this.tableLayoutPanelSub3.Name = "tableLayoutPanelSub3";
            this.tableLayoutPanelSub3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tableLayoutPanelSub3.RowCount = 2;
            this.tableLayoutPanelSub3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSub3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSub3.Size = new System.Drawing.Size(56, 417);
            this.tableLayoutPanelSub3.TabIndex = 0;
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDown.Location = new System.Drawing.Point(3, 228);
            this.btnDown.Margin = new System.Windows.Forms.Padding(3, 15, 3, 15);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(50, 100);
            this.btnDown.TabIndex = 3;
            this.btnDown.Text = "﹀";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnUp.Location = new System.Drawing.Point(3, 98);
            this.btnUp.Margin = new System.Windows.Forms.Padding(3, 15, 3, 15);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(50, 100);
            this.btnUp.TabIndex = 2;
            this.btnUp.Text = "︿";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItem});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 3);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 30;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(244, 417);
            this.dataGridView.TabIndex = 1;
            // 
            // tableLayoutPanelSub2
            // 
            this.tableLayoutPanelSub2.ColumnCount = 2;
            this.tableLayoutPanelSub2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSub2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSub2.Controls.Add(this.btnOK, 0, 0);
            this.tableLayoutPanelSub2.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanelSub2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSub2.Location = new System.Drawing.Point(3, 432);
            this.tableLayoutPanelSub2.Name = "tableLayoutPanelSub2";
            this.tableLayoutPanelSub2.RowCount = 1;
            this.tableLayoutPanelSub2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSub2.Size = new System.Drawing.Size(312, 44);
            this.tableLayoutPanelSub2.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(5, 3);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(146, 38);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(161, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(146, 38);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // colItem
            // 
            this.colItem.FillWeight = 300F;
            this.colItem.HeaderText = "Item";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            this.colItem.Width = 300;
            // 
            // JudgePriorityConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 479);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ItemListSortingForm";
            this.Text = "Item List Sorting Form";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanelSub1.ResumeLayout(false);
            this.tableLayoutPanelSub3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tableLayoutPanelSub2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSub1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSub3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSub2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
    }
}