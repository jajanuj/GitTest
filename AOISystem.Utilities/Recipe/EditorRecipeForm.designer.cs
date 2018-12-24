namespace AOISystem.Utilities.Recipe
{
    partial class EditorRecipeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorRecipeForm));
            this.lblRecipe = new System.Windows.Forms.Label();
            this.txtRecipeID = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtRecipeNo = new System.Windows.Forms.TextBox();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRecipe
            // 
            this.lblRecipe.BackColor = System.Drawing.Color.Navy;
            this.lblRecipe.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRecipe.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRecipe.ForeColor = System.Drawing.Color.LightBlue;
            this.lblRecipe.Location = new System.Drawing.Point(0, 0);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(384, 32);
            this.lblRecipe.TabIndex = 27;
            this.lblRecipe.Text = "Recipe";
            this.lblRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRecipeID
            // 
            this.txtRecipeID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRecipeID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtRecipeID.Location = new System.Drawing.Point(78, 108);
            this.txtRecipeID.Name = "txtRecipeID";
            this.txtRecipeID.Size = new System.Drawing.Size(193, 22);
            this.txtRecipeID.TabIndex = 24;
            this.txtRecipeID.TextChanged += new System.EventHandler(this.tbx_RecipeID_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCancel.ForeColor = System.Drawing.Color.Red;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(277, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 40);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtRecipeNo
            // 
            this.txtRecipeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRecipeNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtRecipeNo.Location = new System.Drawing.Point(78, 62);
            this.txtRecipeNo.Name = "txtRecipeNo";
            this.txtRecipeNo.Size = new System.Drawing.Size(193, 22);
            this.txtRecipeNo.TabIndex = 23;
            // 
            // lblNo
            // 
            this.lblNo.Location = new System.Drawing.Point(15, 65);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(64, 16);
            this.lblNo.TabIndex = 21;
            this.lblNo.Text = "No：";
            this.lblNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(15, 111);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(64, 16);
            this.lblId.TabIndex = 22;
            this.lblId.Text = "ID：";
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOK.ForeColor = System.Drawing.Color.Blue;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(277, 65);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 40);
            this.btnOK.TabIndex = 25;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtDescription.Location = new System.Drawing.Point(78, 155);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(193, 22);
            this.txtDescription.TabIndex = 29;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(8, 157);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(71, 16);
            this.lblDescription.TabIndex = 28;
            this.lblDescription.Text = "Description：";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EditorRecipeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 209);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblRecipe);
            this.Controls.Add(this.txtRecipeID);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtRecipeNo);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.btnOK);
            this.Name = "EditorRecipeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor Recipe";
            this.Load += new System.EventHandler(this.frmEditorRecipe_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecipe;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtRecipeID;
        private System.Windows.Forms.TextBox txtRecipeNo;
        private System.Windows.Forms.TextBox txtDescription;

    }
}