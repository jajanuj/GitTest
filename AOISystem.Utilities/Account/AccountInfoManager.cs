using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AOISystem.Utilities.Encryption;
using AOISystem.Utilities.IO;
using AOISystem.Utilities.Logging;

namespace AOISystem.Utilities.Account
{
    public class AccountInfoManager
    {
        private static List<AccountInfo> _accountInfoCollection;

        public static event Action<string, AccountLevel> AccountInfoLogInOutCallback;

        public static string ActiveAccountName { get; set; }

        /// <summary>目前登入權限等級</summary>
        public static AccountLevel ActiveAccountLevel { get; set; }

        public static bool IsTestMode { get; set; }

        public static List<AccountInfo> GetAccountInfoCollection()
        {
            try
            {
                _accountInfoCollection = new List<AccountInfo>();
                IniFile iniFile = new IniFile(SystemDefine.SYSTEM_DATA_FOLDER_PATH, "AccountInfo");
                int accountInfoCount = iniFile.GetInt32("General", "Count");
                if (accountInfoCount > 0)
                {
                    string key = iniFile.GetString("General", "Key");
                    for (int i = 0; i < accountInfoCount; i++)
                    {
                        _accountInfoCollection.Add(new AccountInfo()
                        {
                            Name = AESEncryption.AESDecoder(iniFile.GetString("User" + (i + 1).ToString(), "Name", ""), key),
                            Password = AESEncryption.AESDecoder(iniFile.GetString("User" + (i + 1).ToString(), "Password", "0"), key),
                            Level = (AccountLevel)Enum.Parse(typeof(AccountLevel), AESEncryption.AESDecoder(iniFile.GetString("User" + (i + 1).ToString(), "Level", "0"), key))
                        });
                    }
                }
                else 
                {
                    //v1.0.0.6 新增預設開發者帳號密碼
                    _accountInfoCollection.Add(new AccountInfo()
                    {
                        Name = SystemDefine.DEVELOPER_USER_NAME,
                        Password = SystemDefine.DEVELOPER_USER_PASSWORD,
                        Level = AccountLevel.Developer
                    });
                    
                    AccountInfoManager.SetAccountInfoCollection(_accountInfoCollection);
                    GetAccountInfoCollection();
                }

                return _accountInfoCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                return _accountInfoCollection;
            }
        }

        public static void SetAccountInfoCollection(List<AccountInfo> accountInfoCollection)
        {
            try
            {
                int accountInfoCount = accountInfoCollection.Count;
                IniFile iniFile = new IniFile(SystemDefine.SYSTEM_DATA_FOLDER_PATH, "AccountInfo");
                iniFile.WriteValue("General", "Count", accountInfoCount);
                string key = AESEncryption.GenerateKey();
                iniFile.WriteValue("General", "Key", key);
                for (int i = 0; i < accountInfoCount; i++)
                {
                    iniFile.WriteValue("User" + (i + 1).ToString(), "Name", AESEncryption.AESEncoder(accountInfoCollection[i].Name, key));
                    iniFile.WriteValue("User" + (i + 1).ToString(), "Password", AESEncryption.AESEncoder(accountInfoCollection[i].Password, key));
                    iniFile.WriteValue("User" + (i + 1).ToString(), "Level", AESEncryption.AESEncoder(accountInfoCollection[i].Level.ToString(), key));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static bool LogIn()
        {
            LogInForm logInForm = new LogInForm();
            if (logInForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("登入成功!");
                return true;
            }
            return false;
        }

        public static bool LogIn(string name, string password)
        {
            if (name == SystemDefine.DEVELOPER_USER_NAME)
            {
                if (password == SystemDefine.DEVELOPER_USER_PASSWORD)
                {
                    OnAccountInfoLogInOutCallback(name, AccountLevel.Developer);
                    return true;
                }
                else
                {
                    MessageBox.Show("密碼錯誤!");
                    return false;
                }
            }
            AccountInfo accountInfo = _accountInfoCollection.Find(x => { return x.Name == name && x.Password == password; });
            if (accountInfo != null)
            {
                OnAccountInfoLogInOutCallback(accountInfo.Name, accountInfo.Level);
                return true;
            }
            else
            {
                MessageBox.Show("帳號或密碼錯誤!");
                return false;
            }
        }

        public static void LogOut()
        {
            LogOut(false);
        }

        public static void LogOut(bool isLogOutCheck)
        {
            if (isLogOutCheck)
            {
                //1.0.0.5 新增登出逾時自動登出功能
                LogOutForm logOutForm = new LogOutForm();

                if (logOutForm.ShowDialog() != DialogResult.Yes)
                {
                    OnAccountInfoLogInOutCallback(ActiveAccountName, ActiveAccountLevel);
                    return;
                }
            }

            if (IsTestMode)
            {
                OnAccountInfoLogInOutCallback(string.Empty, AccountLevel.Developer);
            }
            else
            {
                LogHelper.Operation("帳號 :{0},已登出", ActiveAccountName);
                OnAccountInfoLogInOutCallback(string.Empty, AccountLevel.Operator);
            }
        }

        public static void AccountEditor()
        {
            //160802.0920 修改登入帳號編輯功能可由工程師等級以上使用
            if (TestPermission(AccountLevel.Engineer))
            {
                AccountEditorForm accountEditorForm = new AccountEditorForm();
                accountEditorForm.ShowDialog();
            }
        }

        public static bool TestPermission(AccountLevel limitLevel, bool isLogInCheck = true)
        {
            if (IsTestMode)
            {
                return true;
            }
            if (ActiveAccountLevel >= limitLevel)
            {
                return true;
            }
            if (isLogInCheck && MessageBox.Show("此帳號無操作權限, 請切換更高權限帳號", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                return LogIn() && TestPermission(limitLevel);
            }
            return false;
        }

        private static void OnAccountInfoLogInOutCallback(string name, AccountLevel level)
        {
            //新增記錄登入/出帳號資料
            if (name != string.Empty)
            {
                if (ActiveAccountName != string.Empty && ActiveAccountName != null)
                {
                    LogHelper.Operation("帳號 :{0},已登出", ActiveAccountName);
                    LogHelper.Operation("帳號 :{0},登入成功", name);
                }
                else
                    LogHelper.Operation("帳號 :{0},登入成功", name);
            }

            ActiveAccountName = name;
            ActiveAccountLevel = level;
            if (AccountInfoLogInOutCallback != null)
            {
                AccountInfoLogInOutCallback(name, level);
            }
        }
    }
}
