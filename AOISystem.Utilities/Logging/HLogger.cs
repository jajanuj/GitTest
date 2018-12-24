using System;
using System.Windows.Forms;
using System.Threading;

namespace AOISystem.Utilities.Logging
{
    public partial class HLogger : UserControl, ILoggerTarget
    {
        private SynchronizationContext _context;

        public HLogger()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
        }

        public void  ClearCommand()
        {
            lstCommand.Items.Clear();
        }

        // ILogTarget 
        public void SetCommandLine(string obj)
        {
            try
            {
                if (_context != null)
                {
                    _context.Post(InsertCommandLine, obj);
                }
                else
                {
                    this.BeginInvoke(new Action<string>(InsertCommandLine), obj);
                }
            }
            catch (InvalidOperationException e)
            {
            }
            catch(StackOverflowException e)
            {
            }
        }

        private void InsertCommandLine(object obj)
        {
            if (lstCommand.Items.Count > 500)
            {
                lstCommand.Items.RemoveAt(lstCommand.Items.Count - 1);
            }
            lstCommand.Items.Insert(0, obj);
            lstCommand.SelectedIndex = 0;
        }

        private void lbCommand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lstCommand.SelectedItem != null)
            {
                MessageBox.Show(this.lstCommand.SelectedItem.ToString());
            }
        }
    }
}
