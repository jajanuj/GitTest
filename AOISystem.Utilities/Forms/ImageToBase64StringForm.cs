using System;
using System.Drawing;
using System.Windows.Forms;
using AOISystem.Utilities.Common;

namespace AOISystem.Utilities.Forms
{
    public partial class ImageToBase64StringForm : Form
    {
        public ImageToBase64StringForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = ofd.FileName;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                Image image = Image.FromFile(this.txtPath.Text);
                this.txtBase64String.Text = ImageHelper.ImageToBase64String(image);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.txtBase64String.SelectionStart = 0;
            this.txtBase64String.SelectionLength = this.txtBase64String.Text.Length;
            Clipboard.SetDataObject(txtBase64String.SelectedText);
        }
    }
}
