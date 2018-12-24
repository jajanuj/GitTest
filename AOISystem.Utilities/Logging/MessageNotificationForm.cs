using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Logging
{
    public partial class MessageNotificationForm : Form
    {
        private static MessageNotificationForm instance = null;
        private static readonly object synObject = new object();

        public MessageNotificationForm()
        {
            InitializeComponent();
        }

        public static MessageNotificationForm GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                lock (synObject)
                {
                    if (instance == null || instance.IsDisposed)
                    {
                        instance = new MessageNotificationForm();
                    }
                }
            }
            return instance;
        }

        public void Post(string message)
        {
            this.hLogger.SetCommandLine(message);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
