using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Recipe
{
    public partial class CopyRecipeForm : Form
    {
        private RecipeInfo _recipeInfo;

        public CopyRecipeForm()
        {
            InitializeComponent();
        }

        public CopyRecipeForm(RecipeInfo srcRecipeInfo, RecipeInfo destRecipeInfo)
        {
            InitializeComponent();

            _recipeInfo = destRecipeInfo;
            this.txtOldRecipeNo.Text = srcRecipeInfo.RecipeNo.ToString();
            this.txtOldRecipeID.Text = srcRecipeInfo.RecipeID;
            this.txtOldDescription.Text = srcRecipeInfo.Description;
            this.txtNewRecipeNo.Text = destRecipeInfo.RecipeNo.ToString();
            this.txtNewRecipeID.Text = srcRecipeInfo.RecipeID + "(Copy)";
            this.txtNewDescription.Text = srcRecipeInfo.Description + "(Copy)";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _recipeInfo.RecipeNo = int.Parse(this.txtNewRecipeNo.Text);
            _recipeInfo.RecipeID = this.txtNewRecipeID.Text;
            _recipeInfo.Description = this.txtNewDescription.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
