using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace AOISystem.Utilities.Forms
{
    //[ToolboxBitmap(typeof(NumTextBox), "NumTextBox.png")]
    [Description("可定義型態的數值輸入框")]
    public class NumTextBox : System.Windows.Forms.TextBox
    {

        private const int WM_CONTEXTMENU = 0x007b;//右键菜单消息
        private const int WM_CHAR = 0x0102;       //输入字符消息（键盘输入的，输入法输入的好像不是这个消息）
        private const int WM_CUT = 0x0300;        //程序发送此消息给一个编辑框或combobox来删除当前选择的文本
        private const int WM_COPY = 0x0301;       //程序发送此消息给一个编辑框或combobox来复制当前选择的文本到剪贴板
        private const int WM_PASTE = 0x0302;      //程序发送此消息给editcontrol或combobox从剪贴板中得到数据
        private const int WM_CLEAR = 0x0303;      //程序发送此消息给editcontrol或combobox清除当前选择的内容;
        private const int WM_UNDO = 0x0304;        //程序发送此消息给editcontrol或combobox撤消最后一次操作

        /// <summary>
        /// 定義文字框接受的數字類型
        /// </summary>
        [Browsable(true)]
        [Description("文字框內所接受的數字類型")]
        public FilterType FilterFlag
        { get { return filterFlag; } set { filterFlag = value; } }

        private FilterType filterFlag = FilterType.IntegralPos;

        public NumTextBox()
        {
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
        }

        protected override void WndProc(ref Message m)
        {
            switch (filterFlag)
            {
                // 負浮點數到正浮點數
                case FilterType.Numeric:
                    switch (m.Msg)
                    {
                        case WM_CHAR:
                            //System.Console.WriteLine(m.WParam);
                            bool isSign = ((int)m.WParam == 45);
                            bool isNum = ((int)m.WParam >= 48) && ((int)m.WParam <= 57);
                            bool isBack = (int)m.WParam == (int)Keys.Back;
                            bool isDelete = (int)m.WParam == (int)Keys.Delete;//实际上这是一个"."键
                            bool isCtr = ((int)m.WParam == 24) || ((int)m.WParam == 22) || ((int)m.WParam == 26) || ((int)m.WParam == 3);

                            if (isNum || isBack || isCtr)
                            {
                                base.WndProc(ref m);
                            }
                            if (isSign)
                            {
                                if (this.SelectionStart != 0)
                                {
                                    break;
                                }
                                base.WndProc(ref m);
                                break;
                            }
                            if (isDelete)
                            {
                                if (this.Text.IndexOf(".") < 0)
                                {
                                    base.WndProc(ref m);
                                }
                            }
                            if ((int)m.WParam == 1)
                            {
                                this.SelectAll();
                            }
                            break;

                        default:
                            base.WndProc(ref m);
                            break;
                    }
                    break;
                //負整數到正整數
                case FilterType.Integral:
                    switch (m.Msg)
                    {
                        case WM_CHAR:
                            //System.Console.WriteLine(m.WParam);
                            bool isSign = ((int)m.WParam == 45);
                            bool isNum = ((int)m.WParam >= 48) && ((int)m.WParam <= 57);
                            bool isBack = (int)m.WParam == (int)Keys.Back;
                            //bool isDelete = (int)m.WParam == (int)Keys.Delete;//实际上这是一个"."键
                            bool isCtr = ((int)m.WParam == 24) || ((int)m.WParam == 22) || ((int)m.WParam == 26) || ((int)m.WParam == 3);

                            if (isNum || isBack || isCtr)
                            {
                                base.WndProc(ref m);
                            }
                            if (isSign)
                            {
                                if (this.SelectionStart != 0)
                                {
                                    break;
                                }
                                base.WndProc(ref m);
                                break;
                            }
                            //if (isDelete)
                            //{
                            //    if (this.Text.IndexOf(".") < 0)
                            //    {
                            //        base.WndProc(ref m);
                            //    }
                            //}
                            if ((int)m.WParam == 1)
                            {
                                this.SelectAll();
                            }
                            break;

                        default:
                            base.WndProc(ref m);
                            break;
                    }
                    break;

                //負小數到負整數
                case FilterType.NumericNeg:
                    switch (m.Msg)
                    {
                        case WM_CHAR:
                            //System.Console.WriteLine(m.WParam);
                            bool isSign = ((int)m.WParam == 45);
                            bool isNum = ((int)m.WParam >= 48) && ((int)m.WParam <= 57);
                            bool isBack = (int)m.WParam == (int)Keys.Back;
                            bool isDelete = (int)m.WParam == (int)Keys.Delete;//实际上这是一个"."键
                            bool isCtr = ((int)m.WParam == 24) || ((int)m.WParam == 22) || ((int)m.WParam == 26) || ((int)m.WParam == 3);

                            if (isNum || isBack || isCtr)
                            {
                                base.WndProc(ref m);
                            }
                            if (isSign)
                            {
                                if (this.SelectionStart != 0)
                                {
                                    break;
                                }
                                base.WndProc(ref m);
                                break;
                            }
                            if ((int)m.WParam == 1)
                            {
                                this.SelectAll();
                            }
                            break;

                        default:
                            base.WndProc(ref m);
                            break;
                    }
                    break;

                //正小數到正整數
                case FilterType.NumericPos:
                    switch (m.Msg)
                    {
                        case WM_CHAR:
                            //System.Console.WriteLine(m.WParam);
                            //bool isSign = ((int)m.WParam == 45);
                            bool isNum = ((int)m.WParam >= 48) && ((int)m.WParam <= 57);
                            bool isBack = (int)m.WParam == (int)Keys.Back;
                            bool isDelete = (int)m.WParam == (int)Keys.Delete;//实际上这是一个"."键
                            bool isCtr = ((int)m.WParam == 24) || ((int)m.WParam == 22) || ((int)m.WParam == 26) || ((int)m.WParam == 3);

                            if (isNum || isBack || isCtr)
                            {
                                base.WndProc(ref m);
                            }
                            //if (isSign)
                            //{
                            //    if (this.SelectionStart != 0)
                            //    {
                            //        break;
                            //    }
                            //    base.WndProc(ref m);
                            //    break;
                            //}
                            if (isDelete)
                            {
                                if (this.Text.IndexOf(".") < 0)
                                {
                                    base.WndProc(ref m);
                                }
                            }
                            if ((int)m.WParam == 1)
                            {
                                this.SelectAll();
                            }
                            break;

                        default:
                            base.WndProc(ref m);
                            break;
                    }
                    break;

                case FilterType.IntegralNeg:
                    switch (m.Msg)
                    {
                        case WM_CHAR:
                            //System.Console.WriteLine(m.WParam);
                            bool isSign = ((int)m.WParam == 45);
                            bool isNum = ((int)m.WParam >= 48) && ((int)m.WParam <= 57);
                            bool isBack = (int)m.WParam == (int)Keys.Back;
                            //bool isDelete = (int)m.WParam == (int)Keys.Delete;//实际上这是一个"."键
                            bool isCtr = ((int)m.WParam == 24) || ((int)m.WParam == 22) || ((int)m.WParam == 26) || ((int)m.WParam == 3);

                            if (isNum || isBack || isCtr)
                            {
                                base.WndProc(ref m);
                            }
                            if (isSign)
                            {
                                if (this.SelectionStart != 0)
                                {
                                    break;
                                }
                                base.WndProc(ref m);
                                break;
                            }
                            if ((int)m.WParam == 1)
                            {
                                this.SelectAll();
                            }
                            break;

                        default:
                            base.WndProc(ref m);
                            break;
                    }
                    break;

                case FilterType.IntegralPos:
                    switch (m.Msg)
                    {
                        case WM_CHAR:
                            //System.Console.WriteLine(m.WParam);
                            //bool isSign = ((int)m.WParam == 45);
                            bool isNum = ((int)m.WParam >= 48) && ((int)m.WParam <= 57);
                            bool isBack = (int)m.WParam == (int)Keys.Back;
                            //bool isDelete = (int)m.WParam == (int)Keys.Delete;//实际上这是一个"."键
                            bool isCtr = ((int)m.WParam == 24) || ((int)m.WParam == 22) || ((int)m.WParam == 26) || ((int)m.WParam == 3);

                            if (isNum || isBack || isCtr)
                            {
                                base.WndProc(ref m);
                            }
                            //if (isSign)
                            //{
                            //    if (this.SelectionStart != 0)
                            //    {
                            //        break;
                            //    }
                            //    base.WndProc(ref m);
                            //    break;
                            //}
                            if ((int)m.WParam == 1)
                            {
                                this.SelectAll();
                            }
                            break;

                        default:
                            base.WndProc(ref m);
                            break;
                    }
                    break;
            }
        }

        private bool matchNumber(string ClipboardText)
        {
            int index = 0;
            string strNum = "-0.123456789";

            //檢查第一個字元以外是否為負號
            index = ClipboardText.IndexOf(strNum[0]);
            if (index >= 0)
            {
                if (index > 0)
                {
                    return false;
                }
                //檢查文字起始點是否在開始點
                index = this.SelectionStart;
                if (index > 0)
                {
                    return false;
                }
            }

            //檢查小數點是否出現兩次
            index = ClipboardText.IndexOf(strNum[2]);
            if (index != -1)
            {
                index = this.Text.IndexOf(strNum[2]);
                if (index != -1)
                {
                    return false;
                }
            }

            //將剪貼簿的文字和strNum一一比對
            for (int i = 0; i < ClipboardText.Length; i++)
            {
                index = strNum.IndexOf(ClipboardText[i]);
                if (index < 0)
                {
                    return false;
                }
            }
            return true;
        }

        //設定為只有負數時，可以手動輸入負號，也可以不輸入，只要檢查數值大於0，會把原數值乘上-1。
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (filterFlag == FilterType.NumericNeg || filterFlag == FilterType.IntegralNeg)
            {
                try
                {
                    if (Convert.ToInt32(this.Text) > 0)
                    {
                        this.Text = (Convert.ToInt32(this.Text) * -1).ToString();
                    }
                }
                catch (FormatException)
                {
                    this.Text = "";
                }
            }
        }
    }

    public enum FilterType
    {
        // 包括 [負小數] [正小數] [負整數] [正整數]
        Numeric,

        // 包括 [負小數] [負整數]
        NumericNeg,

        // 包括 [正小數] [正整數]
        NumericPos,

        // 包括 [負整數] [正整數]
        Integral,

        // 包括 [負整數]
        IntegralNeg,

        // 包括 [正整數]
        IntegralPos,
    }
}

