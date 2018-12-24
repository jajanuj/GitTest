using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AOISystem.Utilities.Properties;
using System.IO;

namespace AOISystem.Utilities.Forms
{
    // DirectoryTreeView 類別衍生自 TreeView 類別並加以擴充，使之能夠滿足本範例的需求。
    public partial class DirectoryTreeView : TreeView
    {
        public DirectoryTreeView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        // 這是類別的建構函式。
        public DirectoryTreeView()
        {
            InitializeComponent();

            // 騰出更大的空間以便容納長目錄名稱。
            this.Width *= 2;

            // 呼叫 RefreshTree 方法來建構樹狀。
            RefreshTree();
        }

        // 覆寫 OnBeforeExpand 方法。
        protected override void OnBeforeExpand(TreeViewCancelEventArgs tvcea)
        {
            // 在衍生類別中覆寫 OnBeforeExpand 時，請確定呼叫基底類別的 OnBeforeExpand 方法，
            // 使已註冊的委派能接收到事件。
            base.OnBeforeExpand(tvcea);

            // 基於效能的考量，以及為了避免在大量的節點更新時發生閃爍的狀況，
            // 最好將節點更新的程式碼內含在 BeginUpdate 與 EndUpdate 之間。
            this.BeginUpdate();

            // 替使用者所按下之節點中的每一個子節點新增各個子節點。
            // 基於效能的考量，DirectoryTreeView 中的每一個節點只會
            // 內含下一層的各個子節點，以便顯示出 + 號來讓使用者知道
            // 是否能夠繼續展開該節點。因此，每當使用者展開一個節點
            // 時，為了使該節點之下一層之各個子節點的 + 號能夠正確
            // 顯示出來，我們還是必須替這些子節點新增它們的子節點。
            foreach (TreeNode tn in tvcea.Node.Nodes)
            {
                AddDirectories(tn);
            }

            this.EndUpdate();
        }

        // 此程序的主要用途，是在每一個父節點之下，新增代表每一個目錄的子節點。
        // 父節點會被當作引數傳遞給此程序。
        private void AddDirectories(TreeNode tn)
        {
            tn.Nodes.Clear();

            string strPath = tn.FullPath;
            DirectoryInfo diDirectory = new DirectoryInfo(strPath);
            DirectoryInfo[] adiDirectories;

            try
            {
                // 取得內含所有子目錄的 DirectoryInfo 物件陣列。
                adiDirectories = diDirectory.GetDirectories();
            }
            catch (Exception)
            {
                return;
            }

            foreach (DirectoryInfo di in adiDirectories)
            {
                // 使用指定的目錄名稱和當樹狀節點處於選取或未選取狀態時所顯示的影像，
                // 來初始化 TreeNode 類別的新執行個體，以便替每一個子目錄建立一個子節點。
                TreeNode tnDir = new TreeNode(di.Name, 1, 2);

                // 將此一新的子節點新增至父節點中。
                tn.Nodes.Add(tnDir);

                // 如果要填滿整個樹狀，只需繼續遞迴呼叫 AddDirectories() 即可。
                // 不過這樣做的話將非常耗時，所以我們僅止於此。
                //
                // AddDirectories():
                //
                //   AddDirectories(tnDir);
                //
            }
        }

        // 此程序會清除既存的 TreeNode 物件並重新建立 DirectoryTreeView 以便顯示出邏輯磁碟。
        public void RefreshTree()
        {
            // 基於效能的考量，以及為了避免在大量的節點更新時發生 TreeView 閃爍的狀況，
            // 最好將節點更新的程式碼內含在 BeginUpdate 與 EndUpdate 之間。
            BeginUpdate();

            Nodes.Clear();

            //取得電腦上邏輯磁碟的名稱。
            string[] astrDrives = Directory.GetLogicalDrives();

            //string desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            //string myComputer = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyComputer);
            //string myDocuments = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //string myPictures = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            //string favorites = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Favorites);

            //ArrayList initRoot = new ArrayList();
            //initRoot.Add(myDocuments);

            //for (int i = 0; i < astrDrives.Length; i++)
            //{
            //    initRoot.Add(astrDrives[i]);
            //}

            // 使邏輯磁碟成為根節點。
            foreach (string strDrive in astrDrives)
            //foreach (string strDrive in initRoot)
            {
                TreeNode tnDrive = new TreeNode(strDrive, 0, 0);

                Nodes.Add(tnDrive);
                AddDirectories(tnDrive);

                //預設選取 C 磁碟。
                //if (strDrive == @"C:\")
                //{
                //    this.SelectedNode = tnDrive;
                //}
            }

            EndUpdate();
        }
    }
}
