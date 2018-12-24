namespace AOISystem.Utilities.Recipe
{
    partial class RecipeEditorForm
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
            this.recipeEditorControl = new AOISystem.Utilities.Recipe.RecipeEditorControl();
            this.SuspendLayout();
            // 
            // recipeEditorControl
            // 
            this.recipeEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recipeEditorControl.IsShowChangeRecipeButton = true;
            this.recipeEditorControl.Location = new System.Drawing.Point(0, 0);
            this.recipeEditorControl.Name = "recipeEditorControl";
            this.recipeEditorControl.SelectedRecipeInfo = null;
            this.recipeEditorControl.Size = new System.Drawing.Size(453, 605);
            this.recipeEditorControl.TabIndex = 0;
            // 
            // RecipeEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 605);
            this.Controls.Add(this.recipeEditorControl);
            this.Name = "RecipeEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RecipEditorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecipeEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.RecipeEditorForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RecipeEditorControl recipeEditorControl;


    }
}