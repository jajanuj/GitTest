namespace AOISystem.Utilities.Recipe
{
    partial class RecipeEditorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipeEditorControl));
            this.dgvRecipeList = new System.Windows.Forms.DataGridView();
            this.tlpSelect = new System.Windows.Forms.TableLayoutPanel();
            this.btnRecipeChange = new System.Windows.Forms.Button();
            this.cboCurrentRecipeChange = new System.Windows.Forms.ComboBox();
            this.lblCurrentRecipe = new System.Windows.Forms.Label();
            this.tlpView = new System.Windows.Forms.TableLayoutPanel();
            this.lblRecipeNo = new System.Windows.Forms.Label();
            this.lblRecipeID = new System.Windows.Forms.Label();
            this.tlpEdit = new System.Windows.Forms.TableLayoutPanel();
            this.btnRecipeCopy = new System.Windows.Forms.Button();
            this.btnRecipeEdit = new System.Windows.Forms.Button();
            this.btnRecipeDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipeList)).BeginInit();
            this.tlpSelect.SuspendLayout();
            this.tlpView.SuspendLayout();
            this.tlpEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRecipeList
            // 
            this.dgvRecipeList.AllowUserToAddRows = false;
            this.dgvRecipeList.AllowUserToDeleteRows = false;
            this.dgvRecipeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dgvRecipeList, "dgvRecipeList");
            this.dgvRecipeList.MultiSelect = false;
            this.dgvRecipeList.Name = "dgvRecipeList";
            this.dgvRecipeList.ReadOnly = true;
            this.dgvRecipeList.RowHeadersVisible = false;
            this.dgvRecipeList.RowTemplate.Height = 24;
            this.dgvRecipeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecipeList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRecipeList_CellMouseDoubleClick);
            // 
            // tlpSelect
            // 
            resources.ApplyResources(this.tlpSelect, "tlpSelect");
            this.tlpSelect.Controls.Add(this.btnRecipeChange, 2, 0);
            this.tlpSelect.Controls.Add(this.cboCurrentRecipeChange, 1, 0);
            this.tlpSelect.Controls.Add(this.lblCurrentRecipe, 0, 0);
            this.tlpSelect.Name = "tlpSelect";
            // 
            // btnRecipeChange
            // 
            resources.ApplyResources(this.btnRecipeChange, "btnRecipeChange");
            this.btnRecipeChange.ForeColor = System.Drawing.Color.Black;
            this.btnRecipeChange.Name = "btnRecipeChange";
            this.btnRecipeChange.UseVisualStyleBackColor = true;
            this.btnRecipeChange.Click += new System.EventHandler(this.btnRecipeChange_Click);
            // 
            // cboCurrentRecipeChange
            // 
            resources.ApplyResources(this.cboCurrentRecipeChange, "cboCurrentRecipeChange");
            this.cboCurrentRecipeChange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrentRecipeChange.FormattingEnabled = true;
            this.cboCurrentRecipeChange.Name = "cboCurrentRecipeChange";
            this.cboCurrentRecipeChange.Tag = "";
            this.cboCurrentRecipeChange.SelectedIndexChanged += new System.EventHandler(this.cboCurrentRecipeChange_SelectedIndexChanged);
            // 
            // lblCurrentRecipe
            // 
            resources.ApplyResources(this.lblCurrentRecipe, "lblCurrentRecipe");
            this.lblCurrentRecipe.Name = "lblCurrentRecipe";
            // 
            // tlpView
            // 
            resources.ApplyResources(this.tlpView, "tlpView");
            this.tlpView.Controls.Add(this.lblRecipeNo, 0, 0);
            this.tlpView.Controls.Add(this.lblRecipeID, 1, 0);
            this.tlpView.Name = "tlpView";
            // 
            // lblRecipeNo
            // 
            resources.ApplyResources(this.lblRecipeNo, "lblRecipeNo");
            this.lblRecipeNo.Name = "lblRecipeNo";
            // 
            // lblRecipeID
            // 
            resources.ApplyResources(this.lblRecipeID, "lblRecipeID");
            this.lblRecipeID.Name = "lblRecipeID";
            // 
            // tlpEdit
            // 
            resources.ApplyResources(this.tlpEdit, "tlpEdit");
            this.tlpEdit.Controls.Add(this.btnRecipeCopy, 0, 0);
            this.tlpEdit.Controls.Add(this.btnRecipeEdit, 1, 0);
            this.tlpEdit.Controls.Add(this.btnRecipeDelete, 2, 0);
            this.tlpEdit.Name = "tlpEdit";
            // 
            // btnRecipeCopy
            // 
            resources.ApplyResources(this.btnRecipeCopy, "btnRecipeCopy");
            this.btnRecipeCopy.ForeColor = System.Drawing.Color.Blue;
            this.btnRecipeCopy.Name = "btnRecipeCopy";
            this.btnRecipeCopy.UseVisualStyleBackColor = true;
            this.btnRecipeCopy.Click += new System.EventHandler(this.btnRecipeCopy_Click);
            // 
            // btnRecipeEdit
            // 
            resources.ApplyResources(this.btnRecipeEdit, "btnRecipeEdit");
            this.btnRecipeEdit.ForeColor = System.Drawing.Color.Maroon;
            this.btnRecipeEdit.Name = "btnRecipeEdit";
            this.btnRecipeEdit.UseVisualStyleBackColor = true;
            this.btnRecipeEdit.Click += new System.EventHandler(this.btnRecipeEdit_Click);
            // 
            // btnRecipeDelete
            // 
            resources.ApplyResources(this.btnRecipeDelete, "btnRecipeDelete");
            this.btnRecipeDelete.ForeColor = System.Drawing.Color.Green;
            this.btnRecipeDelete.Name = "btnRecipeDelete";
            this.btnRecipeDelete.UseVisualStyleBackColor = true;
            this.btnRecipeDelete.Click += new System.EventHandler(this.btnRecipeDelete_Click);
            // 
            // RecipeEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvRecipeList);
            this.Controls.Add(this.tlpEdit);
            this.Controls.Add(this.tlpView);
            this.Controls.Add(this.tlpSelect);
            this.Name = "RecipeEditorControl";
            this.Load += new System.EventHandler(this.RecipeEditorControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipeList)).EndInit();
            this.tlpSelect.ResumeLayout(false);
            this.tlpSelect.PerformLayout();
            this.tlpView.ResumeLayout(false);
            this.tlpEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRecipeList;
        private System.Windows.Forms.TableLayoutPanel tlpSelect;
        private System.Windows.Forms.Button btnRecipeChange;
        private System.Windows.Forms.ComboBox cboCurrentRecipeChange;
        private System.Windows.Forms.Label lblCurrentRecipe;
        private System.Windows.Forms.TableLayoutPanel tlpView;
        private System.Windows.Forms.Label lblRecipeNo;
        private System.Windows.Forms.Label lblRecipeID;
        private System.Windows.Forms.TableLayoutPanel tlpEdit;
        private System.Windows.Forms.Button btnRecipeCopy;
        private System.Windows.Forms.Button btnRecipeEdit;
        private System.Windows.Forms.Button btnRecipeDelete;
    }
}
