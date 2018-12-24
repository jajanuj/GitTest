using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class DirectoryTreeView : TreeView
    {
        public DirectoryTreeView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            InitializeDirectory();
        }

        public DirectoryTreeView()
        {
            InitializeComponent();

            InitializeDirectory();
        }

        public event Action<string> DirectiorySelected;

        private void OnDirectiorySelected(string path)
        {
            if (DirectiorySelected != null)
            {
                DirectiorySelected(path);
            }
        }

        private void InitializeDirectory()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            if (processName == "devenv" || processName == "VCSExpress" || processName == "WDExpress")
            {
                return;
            }

            this.ImageList = directoryIcons;

            this.Nodes.Clear();

            //實例化TreeNode類 TreeNode(string text,int imageIndex,int selectImageIndex)            
            //TreeNode rootNode = new TreeNode("我的電腦",
            //    IconIndexes.MyComputer, IconIndexes.MyComputer);  //載入顯示 選擇顯示
            //rootNode.Tag = "我的電腦";                            //樹節點數據
            //rootNode.Text = "我的電腦";                           //樹節點標籤內容
            //this.Nodes.Add(rootNode);               //樹中添加根目錄

            //顯示Desktop(桌面)結點
            var myDesktop = Environment.GetFolderPath           //獲取桌面文檔文件夾
                (Environment.SpecialFolder.Desktop);
            TreeNode desktopNode = new TreeNode(myDesktop);
            desktopNode.Tag = "桌面";                            //設置結點名稱
            desktopNode.Text = "桌面";
            desktopNode.ImageIndex = IconIndexes.Desktop;         //設置獲取結點顯示圖片
            desktopNode.SelectedImageIndex = IconIndexes.Desktop; //設置選擇顯示圖片
            this.Nodes.Add(desktopNode);                          //rootNode目錄下加載節點
            desktopNode.Nodes.Add("");

            //顯示MyDocuments(我的文檔)結點
            var myDocuments = Environment.GetFolderPath           //獲取計算機我的文檔文件夾
                (Environment.SpecialFolder.MyDocuments);
            TreeNode DocNode = new TreeNode(myDocuments);
            DocNode.Tag = "我的文檔";                            //設置結點名稱
            DocNode.Text = "我的文檔";
            DocNode.ImageIndex = IconIndexes.MyDocuments;         //設置獲取結點顯示圖片
            DocNode.SelectedImageIndex = IconIndexes.MyDocuments; //設置選擇顯示圖片
            this.Nodes.Add(DocNode);                          //rootNode目錄下加載節點
            DocNode.Nodes.Add("");

            //循環遍歷計算機所有邏輯驅動器名稱(盤符)
            foreach (string drive in Environment.GetLogicalDrives())
            {
                //實例化DriveInfo對像 命名空間System.IO
                var dir = new DriveInfo(drive);
                switch (dir.DriveType)           //判斷驅動器類型
                {
                    case DriveType.Fixed:        //僅取固定磁盤盤符 Removable-U盤 
                        {
                            //Split僅獲取盤符字母
                            TreeNode tNode = new TreeNode(dir.Name.Split(':')[0]);
                            tNode.Name = dir.Name;
                            tNode.Tag = tNode.Name;
                            tNode.ImageIndex = IconIndexes.FixedDrive;         //設置獲取結點顯示圖片
                            tNode.SelectedImageIndex = IconIndexes.FixedDrive; //設置選擇顯示圖片
                            this.Nodes.Add(tNode);                    //加載驅動節點
                            tNode.Nodes.Add("");
                        }
                        break;
                }
            }
            //rootNode.Expand();                  //展開樹狀視圖
        }

        /// <summary>
        /// 在結點展開後發生 展開子結點
        /// </summary>
        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            base.OnAfterExpand(e);

            TreeViewItems.Add(e.Node);
            e.Node.Expand();
        }

        /// <summary>
        /// 更改選定內容後發生 後去當前節點名字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            try
            {
                e.Node.Expand();

                //定義變量
                string path;                        //文件路徑
                TreeNode clickedNode = e.Node;      //獲取當前選中結點

                //獲取路徑賦值path              
                if (clickedNode.Tag.ToString() == "桌面")
                {
                    //獲取計算機我的文檔文件夾
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                else if (clickedNode.Tag.ToString() == "我的文檔")
                {
                    //獲取計算機我的文檔文件夾
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                else
                {
                    //通過自定義函數GetPathFromNode獲取結點路徑
                    path = GetPathFromNode(clickedNode);
                }

                //由於"我的電腦"為空結點,無需處理,否則會出現路徑獲取錯誤或沒有找到"我的電腦"路徑
                if (clickedNode.Tag.ToString() != "我的電腦")
                {
                    OnDirectiorySelected(path);
                }
            }
            catch (Exception msg)  //異常處理
            {
                MessageBox.Show(msg.Message);
            }
        }

        /// <summary>
        /// 獲取節點的路徑:遞歸調用產生節點對應文件夾的路徑
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string GetPathFromNode(TreeNode node)
        {
            //注意:樹形控件中我只賦值Tag\Name,使用Text時賦值即可使用
            if (node.Parent == null)
            {
                return node.Name;
            }
            //Path.Combine組合產生路徑 如 Path.Combine("A","B")則生成"A\\B"
            return Path.Combine(GetPathFromNode(node.Parent), node.Name);
        }
    }

    /// <summary>
    /// IconIndexs類 對應ImageList中5張圖片的序列
    /// </summary>
    public class IconIndexes
    {
        public const int Desktop = 0;      //桌面
        public const int MyComputer = 1;      //我的電腦
        public const int ClosedFolder = 2;    //文件夾關閉
        public const int OpenFolder = 3;      //文件夾打開
        public const int FixedDrive = 4;      //磁盤盤符
        public const int MyDocuments = 5;     //我的文檔
    }

    /// <summary>
    /// 自定義類TreeViewItems 調用其Add(TreeNode e)方法加載子目錄
    /// </summary>
    public static class TreeViewItems
    {
        public static void Add(TreeNode e)
        {
            //try..catch異常處理
            try
            {
                //判斷"我的電腦"Tag 上面加載的該結點沒指定其路徑
                if (e.Tag.ToString() != "我的電腦")
                {
                    e.Nodes.Clear();                               //清除空節點再加載子節點
                    TreeNode tNode = e;                            //獲取選中\展開\折疊結點
                    string path = tNode.Name;                      //路徑  

                    //獲取"我的文檔"路徑
                    if (e.Tag.ToString() == "桌面")
                    {
                        path = Environment.GetFolderPath           //獲取桌面文件夾
                            (Environment.SpecialFolder.Desktop);
                    }
                    else if (e.Tag.ToString() == "我的文檔")
                    {
                        path = Environment.GetFolderPath           //獲取計算機我的文檔文件夾
                            (Environment.SpecialFolder.MyDocuments);
                    }

                    //獲取指定目錄中的子目錄名稱並加載結點
                    string[] dics = Directory.GetDirectories(path);
                    foreach (string dic in dics)
                    {
                        TreeNode subNode = new TreeNode(new DirectoryInfo(dic).Name); //實例化
                        subNode.Name = new DirectoryInfo(dic).FullName;               //完整目錄
                        subNode.Tag = subNode.Name;
                        subNode.ImageIndex = IconIndexes.ClosedFolder;       //設置獲取節點顯示圖片
                        subNode.SelectedImageIndex = IconIndexes.OpenFolder; //設置選擇節點顯示圖片
                        tNode.Nodes.Add(subNode);
                        subNode.Nodes.Add("");                               //加載空節點 實現+號
                    }
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);                   //異常處理
            }
        }
    }
}
