using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AOISystem.Utilities.BarcodeScanner
{
    public partial class BarcodeText : UserControl
    {

        private AIDAReaderController _aidaReaderController = null;

        public BarcodeText()
        {
            InitializeComponent();
            
            _aidaReaderController = new AIDAReaderController();
            _aidaReaderController.ReaderEventChanged += new Action<string>(_aidaReaderController_ReaderEventChanged);
        }

        void _aidaReaderController_ReaderEventChanged(string codeName)
        {
            this.txtBarCode.Text = codeName;
        }

        [Browsable(true), Description("Text")]
        private string _text;
        public string Text
        {
            get
            {
                return this.txtBarCode.Text;
            }
            set
            {
                _text = value;
                txtBarCode.Text = _text;
            }
        }
    }
}
