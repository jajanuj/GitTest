using AOISystem.Utilities.Modules.Syntek.L132.MasterCard;
using AOISystem.Utilities.Forms;
using AOISystem.Utilities.Common;
using System;
using System.Reflection;
using System.Windows.Forms;
using U16 = System.UInt16;

namespace AOISystem.Utilities.Modules.Syntek.L132.SlaveModules.IO
{
    public partial class L132DIOForm : Form
    {
        private L132DIO io;
        private bool isInitFinished;

        public L132DIOForm(L132DIO io)
        {
            InitializeComponent();
            this.io = io;
        }

        private void parameterInitial()
        {
            //cmbCardSwitch.Tag = "CardSwitchNo";
            //cmbRingNo.Tag = "RingNoOfCard";
            //cmbBaudRate.Tag = "BaudRate";
            //cmbSlaveIP.Tag = "SlaveIP";

            //cmbCardSwitch.Items.Clear();
            //cmbCardSwitch.Items.AddRange(Enum.GetNames(typeof(CardSwitchNo)));
            //cmbRingNo.Items.Clear();
            //cmbRingNo.Items.AddRange(Enum.GetNames(typeof(RingNoOfCard)));
            //cmbBaudRate.Items.Clear();
            //cmbBaudRate.Items.AddRange(Enum.GetNames(typeof(SyntekBaudRate)));

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
            ldrIO16.Tag = 16;
            ldrIO17.Tag = 17;
            ldrIO18.Tag = 18;
            ldrIO19.Tag = 19;
            ldrIO20.Tag = 20;
            ldrIO21.Tag = 21;
            ldrIO22.Tag = 22;
            ldrIO23.Tag = 23;
            ldrIO24.Tag = 24;
            ldrIO25.Tag = 25;
            ldrIO26.Tag = 26;
            ldrIO27.Tag = 27;
            ldrIO28.Tag = 28;
            ldrIO29.Tag = 29;
            ldrIO30.Tag = 30;
            ldrIO31.Tag = 31;
        }

        private void L132DIOForm_Load(object sender , EventArgs e)
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
            ldrIO16.On = io.Status1[16];
            ldrIO17.On = io.Status1[17];
            ldrIO18.On = io.Status1[18];
            ldrIO19.On = io.Status1[19];
            ldrIO20.On = io.Status1[20];
            ldrIO21.On = io.Status1[21];
            ldrIO22.On = io.Status1[22];
            ldrIO23.On = io.Status1[23];
            ldrIO24.On = io.Status1[24];
            ldrIO25.On = io.Status1[25];
            ldrIO26.On = io.Status1[26];
            ldrIO27.On = io.Status1[27];
            ldrIO28.On = io.Status1[28];
            ldrIO29.On = io.Status1[29];
            ldrIO30.On = io.Status1[30];
            ldrIO31.On = io.Status1[31];
        }

        private void IO_Click(object sender , MouseEventArgs e)
        {
            byte objIdx = Convert.ToByte(((LedRectangle)sender).Tag);
            byte portNo = (byte)(objIdx / 8);
            byte bitNo = (byte)(objIdx % 8);

            bool ret = io.SetOutput2((PortNo)portNo , (BitNo)bitNo , !((LedRectangle)sender).On);
            if (ret == false)
                MessageBox.Show("It's a Digital Input!!" , "Wrong Access" , MessageBoxButtons.OK , MessageBoxIcon.Warning);
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
                        U16 val = Convert.ToUInt16(((ComboBox)sender).SelectedIndex);
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