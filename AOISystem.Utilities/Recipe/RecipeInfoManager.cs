using AOISystem.Utilities.IO;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using AOISystem.Utilities.Resources;

namespace AOISystem.Utilities.Recipe
{
    public class RecipeInfoManager
    {
        #region - Private Fields -
        private static readonly RecipeInfoManager instance = new RecipeInfoManager();
        #endregion - Private Fields -

        #region - Constructor -
        public RecipeInfoManager()
        {
            this.ActiveRecipe = new RecipeInfo();

            this.RecipeInfoCollection = new RecipeInfoCollection();
        }

        public static RecipeInfoManager GetInstance()
        {
            return instance;
        }
        #endregion - Constructor -

        #region - Public Properties -
        public string RecipeRootPath
        {
            get
            {
                string recipeRootPath = SystemDefine.RECIPE_DATA_FOLDER_PATH;
                if (!Directory.Exists(recipeRootPath))
                {
                    Directory.CreateDirectory(recipeRootPath);
                }
                return recipeRootPath;
            }
        }

        public RecipeInfo ActiveRecipe { get; set; }

        public RecipeInfoCollection RecipeInfoCollection { get; set; }
        #endregion - Public Properties -

        # region - Event Methods -
        public delegate void RecipeInfoCollectionChangedEventHandler(RecipeInfoCollection recipeInfoCollection);
        public event RecipeInfoCollectionChangedEventHandler RecipeInfoCollectionChanged;

        public delegate void RecipeInfoSelectedIndexChangedEventHandler(RecipeInfo recipeInfo);
        public event RecipeInfoSelectedIndexChangedEventHandler RecipeInfoSelectedIndexChanged;

        public delegate void RecipeInfoCopyChangedEventHandler(RecipeInfo oldRecipeInfo, RecipeInfo newRecipeInfo);
        public event RecipeInfoCopyChangedEventHandler RecipeInfoCopyChanged;

        public delegate void ErrorRaisedEventHandler(object sender, int errorCode, string errorMsg);
        public event ErrorRaisedEventHandler ErrorRaised;

        public void OnRecipeInfoCollectionChanged(RecipeInfoCollection recipeInfoCollection)
        {
            if (RecipeInfoCollectionChanged != null)
                RecipeInfoCollectionChanged(recipeInfoCollection);
        }

        public void OnRecipeInfoSelectedIndexChanged(RecipeInfo recipeInfo)
        {
            if (RecipeInfoSelectedIndexChanged != null)
                RecipeInfoSelectedIndexChanged(recipeInfo);
        }

        public void OnRecipeInfoCopyChanged(RecipeInfo oldRecipeInfo, RecipeInfo rewRecipeInfo)
        {
            if (RecipeInfoCopyChanged != null)
                RecipeInfoCopyChanged(oldRecipeInfo, rewRecipeInfo);
        }

        public void OnErrorRaised(int errorCode, string errorMsg)
        {
            if (ErrorRaised != null)
                ErrorRaised(this, errorCode, errorMsg);
        }

        # endregion - Event Methods -

        #region - Public Methods -
        public RecipeInfoCollection GetRecipeInfoCollection()
        {
            try
            {
                if (!Directory.Exists(RecipeRootPath))
                {
                    Directory.CreateDirectory(RecipeRootPath);
                }
                RecipeInfoCollection recipeInfoCollection = AnalyzeDirectoryToRecieInfoCollection();
                this.RecipeInfoCollection = recipeInfoCollection;
                return recipeInfoCollection;
            }
            catch (Exception ex)
            {
                OnErrorRaised(-1, ex.Message + "\r\n" + ex.StackTrace);
                return new RecipeInfoCollection();
            }
        }

        public void SetRecipeInfoCollection(RecipeInfoCollection recipeInfoCollection)
        {
            foreach (RecipeInfo recipeInfo in recipeInfoCollection)
            {
                string infoPath = string.Format(@"{0}\{1:D3}_{2}\RecipeInfo.ini", RecipeRootPath, recipeInfo.RecipeNo, recipeInfo.RecipeID);
                IniFile iniFile = new IniFile(infoPath);
                iniFile.WriteValue("INFO", "ModifyTime", recipeInfo.ModifyTime.ToString("yyyyMMddHHmmss"));
                iniFile.WriteValue("INFO", "Description", recipeInfo.Description);
            }
            OnRecipeInfoCollectionChanged(recipeInfoCollection);
        }

        public bool SetRecipeNo(int recipeNo)
        {
            for (int i = 0; i < this.RecipeInfoCollection.Count; i++)
            {
                if (recipeNo == this.RecipeInfoCollection[i].RecipeNo)
                {
                    SetRecipeInfo(this.RecipeInfoCollection[i]);
                    return true;
                }
            }
            OnErrorRaised(-1, string.Format("{0} : {1}", ResourceHelper.Language.GetString("NoRecipeNoMsg"), recipeNo));
            return false;
        }

        public bool SetRecipeID(string recipeID)
        {
            for (int i = 0; i < this.RecipeInfoCollection.Count; i++)
            {
                if (recipeID == this.RecipeInfoCollection[i].RecipeID)
                {
                    SetRecipeInfo(this.RecipeInfoCollection[i]);
                    return true;
                }
            }
            OnErrorRaised(-1, string.Format("{0} : {1}", ResourceHelper.Language.GetString("NoRecipeIdMsg"), recipeID));
            return false;
        }

        public void SetRecipeInfo(RecipeInfo recipeInfo)
        {
            this.ActiveRecipe = recipeInfo;
            OnRecipeInfoSelectedIndexChanged(recipeInfo);
        }

        public RecipeInfo GetRecipeInfo(int recipeNo)
        {
            RecipeInfo recipeInfo = null;
            for (int i = 0; i < this.RecipeInfoCollection.Count; i++)
            {
                if (recipeNo == this.RecipeInfoCollection[i].RecipeNo)
                {
                    recipeInfo =  this.RecipeInfoCollection[i];
                }
            }
            return recipeInfo;
        }

        public RecipeInfo GetRecipeInfo(string recipeID)
        {
            RecipeInfo recipeInfo = null;
            for (int i = 0; i < this.RecipeInfoCollection.Count; i++)
            {
                if (recipeID == this.RecipeInfoCollection[i].RecipeID)
                {
                    recipeInfo = this.RecipeInfoCollection[i];
                }
            }
            return recipeInfo;
        }

        public bool CopyRecipeInfo(RecipeInfo srcRecipeInfo, RecipeInfo destRecipeInfo)
        {
            if (destRecipeInfo.RecipeNo == 0 || destRecipeInfo.RecipeID == "")
            {
                MessageBox.Show(ResourceHelper.Language.GetString("SelectRecipeMsg"));
                return false;
            }
            for (int i = 0; i < this.RecipeInfoCollection.Count; i++)
            {
                if (this.RecipeInfoCollection[i].RecipeNo == destRecipeInfo.RecipeNo)
                {
                    MessageBox.Show(string.Format("{0} : {1}", ResourceHelper.Language.GetString("ExistRecipeMsg1"), destRecipeInfo.RecipeNo));
                    return false;
                }
                if (this.RecipeInfoCollection[i].RecipeID == destRecipeInfo.RecipeID)
                {
                    MessageBox.Show(string.Format("{0} : {1}", ResourceHelper.Language.GetString("ExistRecipeMsg1"), destRecipeInfo.RecipeID));
                    return false;
                }
            }
            string sourcePath = string.Format(@"{0}\{1:D3}_{2}\", this.RecipeRootPath, srcRecipeInfo.RecipeNo, srcRecipeInfo.RecipeID);
            string targetPath = string.Format(@"{0}\{1:D3}_{2}\", this.RecipeRootPath, destRecipeInfo.RecipeNo, destRecipeInfo.RecipeID);

            RecursiveCopy(sourcePath, targetPath);

            destRecipeInfo.ModifyTime = DateTime.Now;
            this.RecipeInfoCollection.Add(destRecipeInfo);
            SaveRecipeInfoCollectionXML();

            OnRecipeInfoCollectionChanged(this.RecipeInfoCollection);
            OnRecipeInfoCopyChanged(srcRecipeInfo, destRecipeInfo);
            return true;
        }

        public bool EditRecipeInfo(RecipeInfo srcRecipeInfo, RecipeInfo destRecipeInfo)
        {
            for (int i = 0; i < this.RecipeInfoCollection.Count; i++)
            {
                if (this.RecipeInfoCollection[i].RecipeID == destRecipeInfo.RecipeID &&
                    this.RecipeInfoCollection[i].Description == destRecipeInfo.Description)
                {
                    MessageBox.Show(string.Format("{0} : {1}", ResourceHelper.Language.GetString("ExistRecipeMsg1"), destRecipeInfo.RecipeID));
                    return false;
                }
            }
            this.RecipeInfoCollection.Remove(srcRecipeInfo);
            this.RecipeInfoCollection.Add(destRecipeInfo);

            string editOldNo = string.Format(@"{0}\{1:D3}_{2}\", this.RecipeRootPath, srcRecipeInfo.RecipeNo, srcRecipeInfo.RecipeID);
            string editNewNo = string.Format(@"{0}\{1:D3}_{2}\", this.RecipeRootPath, destRecipeInfo.RecipeNo, destRecipeInfo.RecipeID);

            int compareInt = editOldNo.CompareTo(editNewNo);
            if (compareInt != 0)
            {
                string interpath = Path.GetRandomFileName();
                Directory.Move(editOldNo, interpath);
                Directory.Move(interpath, editNewNo);
                if (destRecipeInfo.RecipeNo == this.ActiveRecipe.RecipeNo)
                {
                    SetRecipeNo(destRecipeInfo.RecipeNo);   
                }
                SaveRecipeInfoCollectionXML();
            }
            return true;
        }

        public bool DeleteRecipeInfo(RecipeInfo recipeInfo)
        {
            if (recipeInfo == this.ActiveRecipe)
            {
                MessageBox.Show(ResourceHelper.Language.GetString("DeleteRecipeMsg"));
                return false;
            }
            DialogResult result;
            result = MessageBox.Show(string.Format("{0} : {1} ?", ResourceHelper.Language.GetString("DeleteRecipeMsg1"), recipeInfo), "" ,MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return false;
            }
            this.RecipeInfoCollection.Remove(recipeInfo);
            DeleteDirectoryFiles(string.Format(@"{0}\{1:D3}_{2}\", this.RecipeRootPath, recipeInfo.RecipeNo, recipeInfo.RecipeID));
            SaveRecipeInfoCollectionXML();
            return true;
        }
        #endregion - Public Methods -

        #region - Configuration Methods -
        /// <summary>
        /// 初始化並切換Recipe
        /// </summary>
        /// <param name="recipeNo">Recipe No</param>
        public bool InitializeConfiguration(int recipeNo)
        {
            InitializeRecipeInfoCollection(recipeNo, "ID");
            return SetRecipeNo(recipeNo);
        }
        /// <summary>
        /// 初始化並切換Recipe
        /// </summary>
        /// <param name="recipeID">Recipe ID</param>
        public bool InitializeConfiguration(string recipeID)
        {
            InitializeRecipeInfoCollection(0, recipeID);
            return SetRecipeID(recipeID);
        }
        /// <summary>
        /// 初始化RecipeInfoCollection
        /// </summary>
        private void InitializeRecipeInfoCollection(int recipeNo = 0, string recipeID = "RecipeID")
        {
            LoadRecipeInfoCollectionXML(recipeNo, recipeID);
        }
        /// <summary>
        /// 讀取Recipe Info Collection從XML
        /// </summary>
        private bool LoadRecipeInfoCollectionXML(int recipeNo = 0, string recipeID = "RecipeID")
        {
            try
            {
                this.RecipeInfoCollection = GetRecipeInfoCollection();
                if (this.RecipeInfoCollection.Count == 0)
                {
                    RecipeInfo recipInfo = new RecipeInfo()
                    {
                        RecipeNo = recipeNo == 0 ? 1 : recipeNo,
                        RecipeID = recipeID == "RecipeID" ? "RecipeID" : recipeID,
                        ModifyTime = DateTime.Now,
                        Description = "Auto Create Recipe"
                    };
                    this.RecipeInfoCollection.Add(recipInfo);
                    string recipePath = string.Format(@"{0}\{1:D3}_{2}", this.RecipeRootPath, recipInfo.RecipeNo, recipInfo.RecipeID);
                    if (!Directory.Exists(recipePath))
                    {
                        Directory.CreateDirectory(recipePath);
                    }
                }
                SetRecipeInfoCollection(this.RecipeInfoCollection);
                return true;
            }
            catch (Exception ex)
            {
                OnErrorRaised(-1, ex.Message + "\r\n" + ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 儲存Recipe Info Collection到XML
        /// </summary>
        private void SaveRecipeInfoCollectionXML()
        {
            this.RecipeInfoCollection.Sort(new ComparerByRecipeNo());

            SetRecipeInfoCollection(this.RecipeInfoCollection);
        }
        #endregion - Configuration Methods -

        #region - Private Methods -
        private RecipeInfoCollection AnalyzeDirectoryToRecieInfoCollection()
        {
            RecipeInfoCollection recipeInfoCollection = new RecipeInfoCollection();
            string[] recipeDirs = Directory.GetDirectories(RecipeRootPath);
            foreach (string recipeDir in recipeDirs)
            {
                string[] recipeSplited = recipeDir.Split('\\');
                string folder = recipeSplited[recipeSplited.Length - 1];
                int splitIndex = folder.IndexOf('_');
                //if (folderSplited.Length != 2)
                //{
                //    throw new ArgumentException("Recipe folder's name error.");
                //}
                RecipeInfo recipeInfo = new RecipeInfo()
                {
                    RecipeNo = int.Parse(folder.Substring(0, splitIndex)),
                    RecipeID = folder.Substring(splitIndex + 1, folder.Length - splitIndex - 1)
                };
                AnalyzeRecipeDetailInfo(recipeDir, recipeInfo);
                recipeInfoCollection.Add(recipeInfo);
            }
            return recipeInfoCollection;
        }

        private void AnalyzeRecipeDetailInfo(string recipeDir, RecipeInfo recipeInfo)
        {
            string infoPath = string.Format(@"{0}\RecipeInfo.ini", recipeDir);
            if (File.Exists(infoPath))
            {
                IniFile iniFile = new IniFile(infoPath);
                recipeInfo.ModifyTime = DateTime.ParseExact(iniFile.GetString("INFO", "ModifyTime"),
                                  "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces); 
                recipeInfo.Description = iniFile.GetString("INFO", "Description");
            }
        }

        /// <summary>
        /// 目錄與目錄下檔案複製(原目錄, 目標目錄)
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        private void RecursiveCopy(string sourcePath, string targetPath)
        {
            if (Directory.Exists(sourcePath))
            {
                //copy directories
                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);
                string[] dirs = Directory.GetDirectories(sourcePath);
                foreach (string d in dirs)
                {
                    string nextSource = d + "\\";
                    int index = d.LastIndexOf('\\');
                    int index2 = nextSource.LastIndexOf('\\');
                    string s = nextSource.Substring(index + 1, index2 - index - 1);
                    string nextTarget = targetPath + s + "\\";

                    RecursiveCopy(nextSource, nextTarget);
                }
                //copy files
                string[] files = Directory.GetFiles(sourcePath);
                foreach (string f in files)
                {
                    string fileName = Path.GetFileName(f);
                    string destFile = Path.Combine(targetPath, fileName);
                    File.Copy(f, destFile, true);
                }
            }
            else
            {
                if (!Directory.Exists(sourcePath))
                    Directory.CreateDirectory(sourcePath);
                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);
            }
        }

        /// <summary>
        /// 刪除資料夾檔案
        /// </summary>
        /// <param name="rOIPath">指定要刪除資料夾檔案的完整路徑</param>
        public void DeleteDirectoryFiles(string path)
        {
            try
            {
                string[] tempDirectorys, tempSubDirectorys;
                // 檢查指定的資料夾是否存在
                if (Directory.Exists(path))
                {
                    // 檢查指定的資料夾內是否有子資料夾
                    tempDirectorys = Directory.GetDirectories(path);
                    // 刪除指定的資料夾內的所有子資料夾
                    for (int i = 0; i < tempDirectorys.Length; i++)
                    {
                        // 檢查指定的子資料夾內是否還有資料夾
                        tempSubDirectorys = Directory.GetDirectories(tempDirectorys[i]);
                        if (tempSubDirectorys.Length > 0)
                        {
                            // 刪除指定的子資料夾內還有的資料夾
                            for (int j = 0; j < tempSubDirectorys.Length; j++)
                                DeleteDirectoryFiles(tempSubDirectorys[j]);
                        }
                        // 刪除指定的子資料夾內的所有檔案
                        DeleteFiles(tempDirectorys[i]);
                        // 刪除指定的子資料夾
                        Directory.Delete(tempDirectorys[i]);
                    }
                    // 刪除指定的資料夾內的所有檔案
                    DeleteFiles(path);
                    // 刪除指定的資料夾
                    Directory.Delete(path);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="rOIPath">指定要刪除檔案的完整路徑</param>
        public void DeleteFiles(string path)
        {
            try
            {
                string[] tempFileNames;
                // 檢查指定的資料夾是否存在
                if (Directory.Exists(path))
                {
                    // 檢查指定的資料夾內是否有檔案
                    tempFileNames = Directory.GetFiles(path);
                    // 刪除指定的資料夾內的所有檔案
                    for (int i = 0; i < tempFileNames.Length; i++)
                    {
                        File.Delete(tempFileNames[i]);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion - Private Methods -
    }
}
