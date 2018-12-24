using AOISystem.Utilities.MultiLanguage;
using AOISystem.Utilities.Resources;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipeEditorControl : UserControl
    {
        #region - Private Methods -

        private RecipeInfoCollection _recipeInfoCollection;

        private void ShowControl(bool show)
        {
            //1.0.2.19 修改顯示更換Recipe確定按鈕
            this.btnRecipeChange.Visible = show;
        }

        private static DateTime _lastRecipeChangeTime = DateTime.Now.AddSeconds(-10);

        #endregion - Private Methods -

        #region - Constructor -

        public RecipeEditorControl()
        {
            InitializeComponent();

            ShowControl(_isShowChangeRecipeButton);

            //v1.0.2.19 修改無法選擇DataGridView中Recipe列表
            this.dgvRecipeList.Enabled = false;

            this.ChangeRecipeTimeInterval = 10;
        }

        private void RecipeEditorControl_Load(object sender, EventArgs e)
        {
            if (ProcessInfo.IsDesignMode())
            {
                return;
            }
            _recipeInfoCollection = new RecipeInfoCollection();
            this.SelectedRecipeInfo = new RecipeInfo();

            RecipeInfoManager.GetInstance().RecipeInfoSelectedIndexChanged += new RecipeInfoManager.RecipeInfoSelectedIndexChangedEventHandler(RecipeEditorControl_RecipeInfoSelectedIndexChangedEvent);
            RecipeInfoManager.GetInstance().RecipeInfoCollectionChanged += new RecipeInfoManager.RecipeInfoCollectionChangedEventHandler(RecipeEditorControl_RecipeInfoCollectionChangedEvent);
            RecipeInfoManager.GetInstance().RecipeInfoCopyChanged += new RecipeInfoManager.RecipeInfoCopyChangedEventHandler(RecipeEditorControl_RecipeInfoCopyChangedEvent);
            RefreshRecipeInfoCollection();

            RecipeInfo actRecipeInfo = RecipeInfoManager.GetInstance().ActiveRecipe;
            if (actRecipeInfo != null)
            {
                RefreshRecipeStatus(actRecipeInfo.RecipeNo.ToString("D3"), actRecipeInfo.RecipeID);
            }

            LanguageManager.Apply(this);
        }

        #endregion - Constructor -

        #region - Event Methods -

        public delegate void SelectedRecipeInfoEventHandler(RecipeInfo recipeInfo);
        public event SelectedRecipeInfoEventHandler SelectedRecipeInfoEvent;

        //v1.0.2.19 新增刪除Recipe觸發事件
        public delegate void DeleteRecipeInfoEventHandler();
        public event DeleteRecipeInfoEventHandler DeleteRecipeInfoEvent;

        private void OnSelectedRecipeInfoEvent(RecipeInfo recipeInfo)
        {
            this.SelectedRecipeInfo = recipeInfo;
            if (SelectedRecipeInfoEvent != null)
                SelectedRecipeInfoEvent(recipeInfo);
        }

        private void RecipeEditorControl_RecipeInfoSelectedIndexChangedEvent(RecipeInfo recipeInfo)
        {
            RefreshRecipeStatus(recipeInfo.RecipeNo.ToString("D3"), recipeInfo.RecipeID);
        }

        private void RecipeEditorControl_RecipeInfoCollectionChangedEvent(RecipeInfoCollection recipeInfoCollection)
        {
            RefreshRecipeInfoCollection();
        }

        private void RecipeEditorControl_RecipeInfoCopyChangedEvent(RecipeInfo oldRecipeInfo, RecipeInfo newRecipeInfo)
        {
        }

        private void cboCurrentRecipeChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboCurrentRecipeChange.SelectedItem != null)
            {
                OnSelectedRecipeInfoEvent((RecipeInfo)this.cboCurrentRecipeChange.SelectedItem);

                if (this.dgvRecipeList.Rows.Count > 0)
                {
                    //v1.0.2.19 修改無法選擇DataGridView中Recipe列表
                    this.dgvRecipeList.CurrentCell = this.dgvRecipeList.Rows[this.cboCurrentRecipeChange.SelectedIndex].Cells[0];   
                }
            }
        }

        private void dgvRecipeList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            string recipeNo = this.dgvRecipeList.Rows[e.RowIndex].Cells["RecipeNo"].Value.ToString();
            string recipeID = this.dgvRecipeList.Rows[e.RowIndex].Cells["RecipeID"].Value.ToString();
            RefreshRecipeStatus(recipeNo, recipeID);
        }

        private void btnRecipeChange_Click(object sender, EventArgs e)
        {
            //v1.0.2.31 避免短時間內重複切換Recipe
            if (this.ChangeRecipeTimeInterval > 0 && (DateTime.Now - _lastRecipeChangeTime).TotalSeconds < this.ChangeRecipeTimeInterval)
            {
                MessageBox.Show(ResourceHelper.Language.GetString("ChagneRecipeRepeatMsg"));
                return;
            }
            _lastRecipeChangeTime = DateTime.Now;
            if (this.cboCurrentRecipeChange.SelectedItem == null)
            {
                MessageBox.Show(ResourceHelper.Language.GetString("SelectRecipeMsg"));
                return;
            }
            RecipeInfo recipeInfo = (RecipeInfo)this.cboCurrentRecipeChange.SelectedItem;
            if (MessageBox.Show(string.Format("{0} {1:000} {2}", ResourceHelper.Language.GetString("ChangeRecipeMsg1"), recipeInfo, ResourceHelper.Language.GetString("ChangeRecipeMsg2")), "",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (RecipeInfoManager.GetInstance().SetRecipeNo(recipeInfo.RecipeNo))
                {
                }
            }
        }

        private void btnRecipeCopy_Click(object sender, EventArgs e)
        {
            if (this.SelectedRecipeInfo.RecipeNo == 0 || this.SelectedRecipeInfo.RecipeID == "RecipeID")
            {
                MessageBox.Show(ResourceHelper.Language.GetString("SelectRecipeMsg"));
                return;
            }

            RecipeInfo recipeInfo = new RecipeInfo();
            recipeInfo.RecipeNo = RecipeNoGenerator();

            CopyRecipeForm copyRecipeForm = new CopyRecipeForm(this.SelectedRecipeInfo, recipeInfo);

            if (copyRecipeForm.ShowDialog() == DialogResult.OK)
            {
                if (RecipeInfoManager.GetInstance().CopyRecipeInfo(this.SelectedRecipeInfo, recipeInfo))
                {
                    //RefreshRecipeInfoCollection();
                }
            }
        }

        private void btnRecipeEdit_Click(object sender, EventArgs e)
        {
            if (this.SelectedRecipeInfo.RecipeNo == 0 || this.SelectedRecipeInfo.RecipeID == "RecipeID")
            {
                MessageBox.Show(ResourceHelper.Language.GetString("SelectRecipeMsg"));
                return;
            }

            RecipeInfo recipeInfo = new RecipeInfo();

            EditorRecipeForm editorRecipeForm = new EditorRecipeForm(this.SelectedRecipeInfo, recipeInfo);
            editorRecipeForm.Text = ( (Button)sender ).Text == "Create" ? "Recipe Create" : "Recipe Edit";

            if (editorRecipeForm.ShowDialog() == DialogResult.OK)
            {
                if (RecipeInfoManager.GetInstance().EditRecipeInfo(this.SelectedRecipeInfo, recipeInfo))
                {
                    //RefreshRecipeInfoCollection();
                }
            }
        }

        private void btnRecipeDelete_Click(object sender, EventArgs e)
        {
            if (this.SelectedRecipeInfo.RecipeNo == 0 || this.SelectedRecipeInfo.RecipeID == "RecipeID")
            {
                MessageBox.Show(ResourceHelper.Language.GetString("SelectRecipeMsg"));
                return;
            }
            RecipeInfo activeRecipe = RecipeInfoManager.GetInstance().ActiveRecipe;
            if (this.SelectedRecipeInfo.RecipeNo == activeRecipe.RecipeNo || this.SelectedRecipeInfo.RecipeID == activeRecipe.RecipeID)
            {
                MessageBox.Show(ResourceHelper.Language.GetString("DeleteRecipeMsg"));
                return;
            }
            if (RecipeInfoManager.GetInstance().DeleteRecipeInfo(this.SelectedRecipeInfo))
            {
                if (DeleteRecipeInfoEvent != null)
                    DeleteRecipeInfoEvent();
                //RefreshRecipeInfoCollection();
            }
        }

        #endregion - Event Methods -

        #region - Public Properties -

        //v1.0.6.20 新增是否顯示更換Recipe按鈕
        private bool _isShowChangeRecipeButton = true;
        public bool IsShowChangeRecipeButton
        {
            get { return _isShowChangeRecipeButton; }
            set
            {
                _isShowChangeRecipeButton = value;

                ShowControl(_isShowChangeRecipeButton);
            }
        }

        public RecipeInfo SelectedRecipeInfo { get; set; }

        //v1.0.2.31 切換Recipe間隔時間設定
        public int ChangeRecipeTimeInterval { get; set; }

        #endregion - Public Properties -

        #region - Private Methods -

        private void RefreshRecipeInfoCollection()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(RefreshRecipeInfoCollection));
                return;
            }
            try
            {
                _recipeInfoCollection = RecipeInfoManager.GetInstance().GetRecipeInfoCollection();
                this.dgvRecipeList.Rows.Clear();
                this.dgvRecipeList.Columns.Clear();
                this.dgvRecipeList.Columns.AddRange(new DataGridViewColumn[] {
                    new DataGridViewTextBoxColumn() { HeaderText = "RecipeNo", Name = "RecipeNo", ReadOnly = true, Width = 55 },
                    new DataGridViewTextBoxColumn() { HeaderText = "RecipeID", Name = "RecipeID", ReadOnly = true, Width = 120 },
                    new DataGridViewTextBoxColumn() { HeaderText = "Description", Name = "Description", ReadOnly = true, Width = 160 },
                    new DataGridViewTextBoxColumn() { HeaderText = "ModifyTime", Name = "ModifyTime", ReadOnly = true, Width = 120 }});
                foreach (RecipeInfo recipeInfo in _recipeInfoCollection)
                {
                    this.dgvRecipeList.Rows.Add(
                        string.Format("{0:D3}", recipeInfo.RecipeNo),
                        recipeInfo.RecipeID,
                        recipeInfo.Description,
                        recipeInfo.ModifyTime.ToString("yyyy.MM.dd HH:mm:ss"));
                }
                this.cboCurrentRecipeChange.DataSource = null;
                this.cboCurrentRecipeChange.DataSource = _recipeInfoCollection;

                if (this.cboCurrentRecipeChange.SelectedIndex == -1)
                {
                    this.SelectedRecipeInfo = null;
                    this.lblRecipeNo.Text = "";
                    this.lblRecipeID.Text = "";
                    RefreshRecipeStatus("", "");
                }
                else
                {
                    this.SelectedRecipeInfo = (RecipeInfo)this.cboCurrentRecipeChange.SelectedItem;
                    this.lblRecipeNo.Text = string.Format("Recipe No：{0:D3}", this.SelectedRecipeInfo.RecipeNo);
                    this.lblRecipeID.Text = string.Format("Recipe ID：{0}", this.SelectedRecipeInfo.RecipeID);
                    RefreshRecipeStatus(this.SelectedRecipeInfo.RecipeNo.ToString("D3"), this.SelectedRecipeInfo.RecipeID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private int RecipeNoGenerator()
        {
            int recipeNo = 1;
            foreach (RecipeInfo recipeInfo in _recipeInfoCollection)
            {
                if (recipeNo == recipeInfo.RecipeNo)
                {
                    recipeNo++;
                }
                else
                {
                    break;
                }
            }
            return recipeNo;
        }

        private void RefreshRecipeStatus(string recipeNo, string recipeID)
        {
            this.lblRecipeNo.Text = string.Format("Recipe No：{0}", recipeNo);
            this.lblRecipeID.Text = string.Format("Recipe ID：{0}", recipeID);
            if (recipeNo == RecipeInfoManager.GetInstance().ActiveRecipe.RecipeNo.ToString("D3") &&
                recipeID == RecipeInfoManager.GetInstance().ActiveRecipe.RecipeID)
            {
                this.lblRecipeNo.ForeColor = Color.FromKnownColor(KnownColor.Red);
                this.lblRecipeID.ForeColor = Color.FromKnownColor(KnownColor.Red);
            }
            else
            {
                this.lblRecipeNo.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                this.lblRecipeID.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            }
            for (int i = 0; i < _recipeInfoCollection.Count; i++)
            {
                if (recipeID == _recipeInfoCollection[i].RecipeID)
                {
                    this.cboCurrentRecipeChange.SelectedItem = _recipeInfoCollection[i];
                }
            }
        }

        #endregion - Private Methods -
    }
}
