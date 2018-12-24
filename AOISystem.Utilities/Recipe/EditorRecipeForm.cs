using System;
using System.Linq;
using System.Windows.Forms;

namespace AOISystem.Utilities.Recipe
{
    public partial class EditorRecipeForm : Form
    {
        private RecipeInfo _srcRecipeInfo;
        private RecipeInfo _destRecipeInfo;

        public EditorRecipeForm()
        {
            InitializeComponent();
        }

        public EditorRecipeForm(RecipeInfo srcRecipeInfo, RecipeInfo destRecipeInfo)
        {
            InitializeComponent();

            _srcRecipeInfo = srcRecipeInfo;
            _destRecipeInfo = destRecipeInfo;
            this.txtRecipeNo.Text = srcRecipeInfo.RecipeNo.ToString();
            this.txtRecipeID.Text = srcRecipeInfo.RecipeID;
            this.txtDescription.Text = srcRecipeInfo.Description;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _destRecipeInfo.RecipeNo = int.Parse(this.txtRecipeNo.Text);
            _destRecipeInfo.RecipeID = this.txtRecipeID.Text;
            _destRecipeInfo.Description = this.txtDescription.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbx_RecipeID_TextChanged(object sender, EventArgs e)
        {
            if (this.txtRecipeID.Text == string.Empty)
            {
                return;
            }
            string recipeID = this.txtRecipeID.Text.Substring(0, this.txtRecipeID.Text.Count()-1);
            if (this.txtDescription.Text == string.Empty || recipeID == this.txtDescription.Text)
            {
                this.txtDescription.Text = this.txtRecipeID.Text;
            }
        }

        private void frmEditorRecipe_Load(object sender, EventArgs e)
        {
            this.txtRecipeNo.ReadOnly = this.Text == "Recipe Create" ? false : true;
            tbx_RecipeID_TextChanged(this, EventArgs.Empty);
        }
    }
}
