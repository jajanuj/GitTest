using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipeListSelectForm : Form
    {
        public RecipeListSelectForm()
        {
            InitializeComponent();

            this.SelectedRecipeInfoCollection = new RecipeInfoCollection();
        }

        private void RecipeListSelectForm_Load(object sender, EventArgs e)
        {
            this.dataGridView.DataSource = RecipeInfoManager.GetInstance().RecipeInfoCollection;
        }

        public RecipeInfoCollection SelectedRecipeInfoCollection { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRowCollection = this.dataGridView.SelectedRows;
            foreach (DataGridViewRow selectedRow in selectedRowCollection)
            {
                if (selectedRow.Cells[0].Value != null)
                {
                    RecipeInfo recipeInfo = selectedRow.DataBoundItem as RecipeInfo;
                    this.SelectedRecipeInfoCollection.Add(recipeInfo);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
