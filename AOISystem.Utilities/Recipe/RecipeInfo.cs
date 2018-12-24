using System;
using System.Collections;
using System.ComponentModel;
using System.IO;

namespace AOISystem.Utilities.Recipe
{
    [Serializable]
    public class RecipeInfo
    {
        public RecipeInfo()
        {
            this.RecipeNo = 0;
            this.RecipeID = "RecipeID";
            this.Description = "No Description";
            this.ModifyTime = DateTime.Now;

            //if (!File.Exists(GetRecipePath()))
            //{
            //    Directory.CreateDirectory(GetRecipePath());
            //}
        }

        [Browsable(true), Category("Recipe Property"), Description("RecipeNo")]
        public int RecipeNo { get; set; }

        [Browsable(true), Category("Recipe Property"), Description("RecipeID")]
        public string RecipeID { get; set; }

        [Browsable(true), Category("Recipe Property"), Description("Description")]
        public string Description { get; set; }

        [Browsable(true), Category("Recipe Property"), Description("ModifyTime")]
        public DateTime ModifyTime { get; set; }

        //160802.0920 修改System及Recipe檔案存放路徑
        public string GetRecipePath()
        {
            //return string.Format(@"{0}\Recipe\{1:D3}_{2}\", Application.StartupPath, this.RecipeNo, this.RecipeID);
            return string.Format(@"{0}\{1:D3}_{2}\", SystemDefine.RECIPE_DATA_FOLDER_PATH, this.RecipeNo, this.RecipeID);
        }

        public override string ToString()
        {
            return string.Format("{0:D3}_{1}", this.RecipeNo, this.RecipeID);
        }
    }

    [Serializable]
    public class RecipeInfoCollection : CollectionBase
    {
        public RecipeInfo this[int index]
        {
            get { return (RecipeInfo)base.List[index]; }
            set { base.List[index] = value; }
        }

        public int Add(RecipeInfo value)
        {
            return base.InnerList.Add(value);
        }

        public void Insert(int index, RecipeInfo value)
        {
            base.InnerList.Insert(index, value);
        }

        public void Remove(RecipeInfo value)
        {
            base.InnerList.Remove(value);
        }

        public int IndexOf(RecipeInfo value)
        {
            return base.InnerList.IndexOf(value);
        }

        public bool Contains(RecipeInfo value)
        {
            return base.InnerList.Contains(value);
        }

        public void Sort(IComparer comparer)
        {
            base.InnerList.Sort(comparer);
        }

        public RecipeInfo Find(int recipeNo)
        {
            RecipeInfo recipeInfo = null;
            foreach (RecipeInfo item in base.InnerList)
            {
                if (item.RecipeNo == recipeNo)
                {
                    recipeInfo = item;
                    break;
                }
            }
            return recipeInfo;
        }

        public RecipeInfo Find(string recipeID)
        {
            RecipeInfo recipeInfo = null;
            foreach (RecipeInfo item in base.InnerList)
            {
                if (item.RecipeID == recipeID)
                {
                    recipeInfo = item;
                    break;
                }
            }
            return recipeInfo;
        }
    }
}
