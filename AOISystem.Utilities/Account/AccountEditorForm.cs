using AOISystem.Utilities.Resources;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AOISystem.Utilities.Account
{
    public partial class AccountEditorForm : Form
    {
        private List<AccountInfo> _accountInfoCollection;
        private AccountInfo _selectedAccountInfo;

        public AccountEditorForm()
        {
            InitializeComponent();
            Initialize();
            SetEditLoginLevel();                                            //v1.0.0.6 新增設定編輯頁面權限等級內容
            SetText();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                AccountInfoManager.SetAccountInfoCollection(_accountInfoCollection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtAccount.Text == "" || this.txtPassword.Text == "")
                {
                    MessageBox.Show(ResourceHelper.Language.GetString("AccountEmptyMsg"));
                    return;
                }
                if (_accountInfoCollection.Find(x => { return x.Name == this.txtAccount.Text; }) != null)
                {
                    MessageBox.Show(ResourceHelper.Language.GetString("AccountExistMsg"));
                    return;
                }
                _accountInfoCollection.Add(new AccountInfo() 
                {
                    Name = this.txtAccount.Text,
                    Password = this.txtPassword.Text,
                    Level = (AccountLevel)Enum.Parse(typeof(AccountLevel), this.cboAccountLevel.Text)
                });
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lvwAccount.SelectedItems.Count > 0)
                {
                    if (this.txtAccount.Text == "" || this.txtPassword.Text == "")
                    {
                        MessageBox.Show(ResourceHelper.Language.GetString("AccountEmptyMsg"));
                        return;
                    }
                    AccountInfo accountInfo = _accountInfoCollection.Find(x => { return x.Name == _selectedAccountInfo.Name; });
                    if (accountInfo != null)
                    {
                        accountInfo.Name = this.txtAccount.Text;
                        accountInfo.Password = this.txtPassword.Text;
                        accountInfo.Level = (AccountLevel)Enum.Parse(typeof(AccountLevel), this.cboAccountLevel.Text);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0}[{1}]", ResourceHelper.Language.GetString("AccountNoExistMsg"), this.txtAccount.Text));
                        return;
                    }
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void lvwAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwAccount.SelectedItems.Count > 0)
            {
                this.txtAccount.Text = this.lvwAccount.SelectedItems[0].SubItems[0].Text;
                this.txtPassword.Text = this.lvwAccount.SelectedItems[0].SubItems[2].Text;
                this.cboAccountLevel.Text = this.lvwAccount.SelectedItems[0].SubItems[1].Text;
                _selectedAccountInfo = _accountInfoCollection.Find(x => { return x.Name == this.txtAccount.Text; });
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            AccountInfo accountInfoFount = _accountInfoCollection.Find(x => { return x.Name == this.txtAccount.Text; });
            if (accountInfoFount != null)
            {
                _accountInfoCollection.Remove(accountInfoFount);
            }
            else
            {
                MessageBox.Show(string.Format("{0}[{1}]",ResourceHelper.Language.GetString("AccountNoExistMsg"), this.txtAccount.Text));
            }
            RefreshData();
        }

        private void SetText()
        {
            this.grpAccountEdit.Text = ResourceHelper.Language.GetString("AccountgrpAccountEdit");
            this.lblAccount.Text = ResourceHelper.Language.GetString("AccountlblAccount");
            this.lblPassword.Text = ResourceHelper.Language.GetString("AccountlblPassword");
            this.lblAccountLevel.Text = ResourceHelper.Language.GetString("AccountlblAccountLevel");
            this.btnAdd.Text = ResourceHelper.Language.GetString("AccountbtnAdd");
            this.btnDelete.Text = ResourceHelper.Language.GetString("AccountbtnDelete");
            this.btnOK.Text = ResourceHelper.Language.GetString("AccountbtnOK");
            this.btnExit.Text = ResourceHelper.Language.GetString("AccountbtnExit");
            this.btnModify.Text = ResourceHelper.Language.GetString("AccountbtnModify");
            this.lvwAccount.Columns[0].Text = ResourceHelper.Language.GetString("AccountlblAccount");
            this.lvwAccount.Columns[1].Text = ResourceHelper.Language.GetString("AccountlblPassword");
            this.lvwAccount.Columns[2].Text = ResourceHelper.Language.GetString("AccountlblAccountLevel"); 
        }

        private void Initialize()
        {
            try
            {
                //1.0.160811.0800 新增使用者權限不得高於當前使用者
                foreach (string accountLevelName in Enum.GetNames(typeof(AccountLevel)))
                {
                    AccountLevel accountLevel = (AccountLevel)Enum.Parse(typeof(AccountLevel), accountLevelName);
                    if ((AccountInfoManager.ActiveAccountLevel) >= accountLevel && (accountLevel != AccountLevel.Developer))
                    {
                        this.cboAccountLevel.Items.Add(accountLevelName);
                    }
                }
                _accountInfoCollection = AccountInfoManager.GetAccountInfoCollection();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void RefreshData()
        {
            //if (_accountInfoCollection.Count <= 0)
            //    return;

            _accountInfoCollection.Sort((x, y) => { return x.Level.CompareTo(y.Level); });
            this.lvwAccount.Items.Clear();
            foreach (AccountInfo accountInfo in _accountInfoCollection)
            {

                //160802.0920 修改登入帳號編輯功能僅顯示目前權限的帳號密碼
                if (accountInfo.Level == AccountLevel.Developer)
                {
                    if (AccountInfoManager.ActiveAccountLevel != AccountLevel.Developer)
                        break;
                }
                else if (accountInfo.Level == AccountLevel.Administrator)
                {
                    if (AccountInfoManager.ActiveAccountLevel < AccountLevel.Administrator)
                        break;
                }
                else if (accountInfo.Level == AccountLevel.Engineer)
                {
                    if (AccountInfoManager.ActiveAccountLevel < AccountLevel.Engineer)
                        break;
                }
                
                ListViewItem list = new ListViewItem();
                list.Text = accountInfo.Name;
                list.SubItems.Add(accountInfo.Level.ToString());
                list.SubItems.Add(accountInfo.Password);

                this.lvwAccount.Items.Add(list);
            }

            this.txtPassword.Text = "";
            this.txtAccount.Text = "";
            this.cboAccountLevel.SelectedIndex = 0;
        }

        //v1.0.0.6 新增設定編輯頁面權限等級內容
        private void SetEditLoginLevel()
        {
            cboAccountLevel.Items.Clear();
            if (AccountInfoManager.ActiveAccountLevel == AccountLevel.Engineer)
            {
                cboAccountLevel.Items.AddRange(new[] { "Operator", "Engineer" });
            }
            else if (AccountInfoManager.ActiveAccountLevel == AccountLevel.Administrator)
            {
                cboAccountLevel.Items.AddRange(new[] { "Operator", "Engineer", "Administrator" });
            }
            else if (AccountInfoManager.ActiveAccountLevel == AccountLevel.Developer)
            {
                cboAccountLevel.Items.AddRange(new[] { "Operator", "Engineer", "Administrator", "Developer"});
            }
        }
    }
}
