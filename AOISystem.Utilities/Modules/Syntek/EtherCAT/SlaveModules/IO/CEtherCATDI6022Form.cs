using AOISystem.Utilities.Forms;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.IO
{
    public partial class CEtherCATDI6022Form : Form
    {
        private CEtherCATDI6022 io;
        private bool isInitFinished;

        public CEtherCATDI6022Form(CEtherCATDI6022 io)
        {
            InitializeComponent();
            this.io = io;
        }

        private void parameterInitial()
        {
            ldrIO0.Tag = 0;
            ldrIO1.Tag = 1;
            ldrIO2.Tag = 2;
            ldrIO3.Tag = 3;
            ldrIO4.Tag = 4;
            ldrIO5.Tag = 5;
            ldrIO6.Tag = 6;
            ldrIO7.Tag = 7;
            ldrIO8.Tag = 8;
            ldrIO9.Tag = 9;
            ldrIO10.Tag = 10;
            ldrIO11.Tag = 11;
            ldrIO12.Tag = 12;
            ldrIO13.Tag = 13;
            ldrIO14.Tag = 14;
            ldrIO15.Tag = 15;
        }

        private void FrmSlaveDIO_Load(object sender , EventArgs e)
        {
            parameterInitial();
            isInitFinished = true;
        }

        private void timer1_Tick(object sender , EventArgs e)
        {
            ldrIO0.On = io.Status1[0];
            ldrIO1.On = io.Status1[1];
            ldrIO2.On = io.Status1[2];
            ldrIO3.On = io.Status1[3];
            ldrIO4.On = io.Status1[4];
            ldrIO5.On = io.Status1[5];
            ldrIO6.On = io.Status1[6];
            ldrIO7.On = io.Status1[7];
            ldrIO8.On = io.Status1[8];
            ldrIO9.On = io.Status1[9];
            ldrIO10.On = io.Status1[10];
            ldrIO11.On = io.Status1[11];
            ldrIO12.On = io.Status1[12];
            ldrIO13.On = io.Status1[13];
            ldrIO14.On = io.Status1[14];
            ldrIO15.On = io.Status1[15];
        }

        private void para_SelectedIndexChanged(object sender , EventArgs e)
        {
            if (isInitFinished)
            {
                PropertyInfo[] pi = io.DIOPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo p in pi)
                {
                    if (((ComboBox)sender).Tag.ToString() == p.Name)
                    {
                        UInt16 val = Convert.ToUInt16(((ComboBox)sender).SelectedIndex);
                        if (p.PropertyType.BaseType.Name == "Enum")
                            p.SetValue(io.DIOPara , Enum.Parse(p.PropertyType , val.ToString()) , null);
                        if (p.PropertyType.BaseType.Name == "ValueType")
                            p.SetValue(io.DIOPara , Convert.ChangeType(val , p.PropertyType) , null);
                    }
                }
            }
        }

        private void btnSystemSetting_Click(object sender , EventArgs e)
        {
            ParameterINIForm dialog = new ParameterINIForm(io.DIOPara, io.DeviceName);
            dialog.ShowDialog();
        }
    }
}