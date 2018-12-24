using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipeEditorForm : Form
    {
        public RecipeEditorForm()
        {
            InitializeComponent();
        }

        private void RecipeEditorForm_Load(object sender, EventArgs e)
        {
            RecipeInfoManager.GetInstance().RecipeInfoSelectedIndexChanged += new RecipeInfoManager.RecipeInfoSelectedIndexChangedEventHandler(RecipeEditorForm_RecipeInfoSelectedIndexChangedEvent);
        }

        private void RecipeEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void RecipeEditorForm_RecipeInfoSelectedIndexChangedEvent(RecipeInfo recipeInfo)
        { 
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
