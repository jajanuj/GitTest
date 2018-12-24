namespace AOISystem.Utilities.Recipe
{
    partial class CopyRecipeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyRecipeForm));
            this.txtOldRecipeID = new System.Windows.Forms.TextBox();
            this.txtOldRecipeNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCopy = new System.Windows.Forms.Label();
            this.txtNewRecipeNo = new System.Windows.Forms.TextBox();
            this.lblNewNo = new System.Windows.Forms.Label();
            this.txtNewRecipeID = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblNewId = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtNewDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtOldDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOldRecipeID
            // 
            this.txtOldRecipeID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldRecipeID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtOldRecipeID.Location = new System.Drawing.Point(78, 91);
            this.txtOldRecipeID.Name = "txtOldRecipeID";
            this.txtOldRecipeID.ReadOnly = true;
            this.txtOldRecipeID.Size = new System.Drawing.Size(193, 22);
            this.txtOldRecipeID.TabIndex = 47;
            // 
            // txtOldRecipeNo
            // 
            this.txtOldRecipeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldRecipeNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtOldRecipeNo.Location = new System.Drawing.Point(78, 63);
            this.txtOldRecipeNo.Name = "txtOldRecipeNo";
            this.txtOldRecipeNo.ReadOnly = true;
            this.txtOldRecipeNo.Size = new System.Drawing.Size(193, 22);
            this.txtOldRecipeNo.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 44;
            this.label1.Text = "No：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 45;
            this.label2.Text = "ID：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCopy
            // 
            this.lblCopy.BackColor = System.Drawing.Color.Navy;
            this.lblCopy.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCopy.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCopy.ForeColor = System.Drawing.Color.LightBlue;
            this.lblCopy.Location = new System.Drawing.Point(0, 0);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(384, 32);
            this.lblCopy.TabIndex = 43;
            this.lblCopy.Text = "Recipe Copy";
            this.lblCopy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNewRecipeNo
            // 
            this.txtNewRecipeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewRecipeNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtNewRecipeNo.Location = new System.Drawing.Point(78, 166);
            this.txtNewRecipeNo.Name = "txtNewRecipeNo";
            this.txtNewRecipeNo.Size = new System.Drawing.Size(193, 22);
            this.txtNewRecipeNo.TabIndex = 42;
            // 
            // lblNewNo
            // 
            this.lblNewNo.Location = new System.Drawing.Point(15, 169);
            this.lblNewNo.Name = "lblNewNo";
            this.lblNewNo.Size = new System.Drawing.Size(64, 16);
            this.lblNewNo.TabIndex = 41;
            this.lblNewNo.Text = "New No：";
            this.lblNewNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNewRecipeID
            // 
            this.txtNewRecipeID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewRecipeID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtNewRecipeID.Location = new System.Drawing.Point(78, 194);
            this.txtNewRecipeID.Name = "txtNewRecipeID";
            this.txtNewRecipeID.Size = new System.Drawing.Size(193, 22);
            this.txtNewRecipeID.TabIndex = 38;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCancel.ForeColor = System.Drawing.Color.Red;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(277, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 40);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblNewId
            // 
            this.lblNewId.Location = new System.Drawing.Point(15, 197);
            this.lblNewId.Name = "lblNewId";
            this.lblNewId.Size = new System.Drawing.Size(64, 16);
            this.lblNewId.TabIndex = 37;
            this.lblNewId.Text = "New ID：";
            this.lblNewId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOK.ForeColor = System.Drawing.Color.Blue;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(277, 63);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(95, 40);
            this.btnOK.TabIndex = 39;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtNewDescription
            // 
            this.txtNewDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewDescription.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtNewDescription.Location = new System.Drawing.Point(78, 227);
            this.txtNewDescription.Name = "txtNewDescription";
            this.txtNewDescription.Size = new System.Drawing.Size(193, 22);
            this.txtNewDescription.TabIndex = 49;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(8, 229);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(71, 16);
            this.lblDescription.TabIndex = 48;
            this.lblDescription.Text = "Description：";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOldDescription
            // 
            this.txtOldDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldDescription.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtOldDescription.Location = new System.Drawing.Point(78, 120);
            this.txtOldDescription.Name = "txtOldDescription";
            this.txtOldDescription.ReadOnly = true;
            this.txtOldDescription.Size = new System.Drawing.Size(193, 22);
            this.txtOldDescription.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 50;
            this.label3.Text = "Description：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CopyRecipeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.txtOldDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNewDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtOldRecipeID);
            this.Controls.Add(this.txtOldRecipeNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.txtNewRecipeNo);
            this.Controls.Add(this.lblNewNo);
            this.Controls.Add(this.txtNewRecipeID);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblNewId);
            this.Controls.Add(this.btnOK);
            this.Name = "CopyRecipeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy Recipe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.TextBox txtNewRecipeNo;
        private System.Windows.Forms.Label lblNewNo;
        private System.Windows.Forms.TextBox txtNewRecipeID;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNewId;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOldRecipeID;
        private System.Windows.Forms.TextBox txtOldRecipeNo;
        private System.Windows.Forms.TextBox txtNewDescription;
        private System.Windows.Forms.TextBox txtOldDescription;
    }
}