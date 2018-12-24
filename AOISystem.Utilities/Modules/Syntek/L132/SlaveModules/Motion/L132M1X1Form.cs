using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using AOISystem.Utilities.Forms;
using AOISystem.Utilities.MultiLanguage;
using U16 = System.UInt16;

namespace AOISystem.Utilities.Modules.Syntek.L132.SlaveModules.Motion
{
    //導程 : 螺桿(馬達)轉一圈的行程(長度) (線馬導程應該都是1)
    //每轉pulse量 : 馬達轉一圈所需的pulse(透過Driver設定 通常1pulse = 1um)
    internal partial class L132M1X1Form : Form
    {
        private Language language;
        private CultureInfo cultureInfo;
        private L132M1X1 axis;
        private bool isInitFinished;
        private JogSpeed jogSpeed;

        private L132M1X1Form()
        {
            InitializeComponent();
        }

        public L132M1X1Form(L132M1X1 axis , Language language)
        {
            InitializeComponent();
            this.Text = axis.DeviceName;
            this.axis = axis;
            this.language = language;
        }

        private void setLanguage(Language lang)
        {
            if (lang == Language.English)
            {
                this.cultureInfo = new CultureInfo("en");
                //更新表單
                LanguageManager.Apply(this, "en");
            }
            if (lang == Language.TraditionalChinese)
            {
                this.cultureInfo = new CultureInfo("zh-TW");
                //更新表單
                LanguageManager.Apply(this, "zh-TW");
            }

            // UI 的語系(MessageBox 屬於Form的UI)
            Thread.CurrentThread.CurrentUICulture = this.cultureInfo;
            // 非 UI 的語系(Exception 不屬於Form的UI)，因為有些錯誤資訊是擷取Exception的data，所以exception的語系也要改掉。
            Thread.CurrentThread.CurrentCulture = this.cultureInfo;
        }

        private void parameterInitial()
        {
            ntxbStrVelM.Tag = "StrVelM";
            ntxbDevVelM.Tag = "DevVelM";
            ntxbAccVelM.Tag = "AccVelM";
            ntxbDecVelM.Tag = "DecVelM";
            ntxbDistPerRole.Tag = "DistPerRole";
            ntxbPulsePerRole.Tag = "PulsePerRole";
            ntxbDecVelA.Tag = "DecVelA";
            ntxbAccVelA.Tag = "AccVelA";
            ntxbDevVelA.Tag = "DevVelA";
            ntxbStrVelA.Tag = "StrVelA";
            ntxbOffsetH.Tag = "OffsetH";
            ntxbCreepDecVelH.Tag = "CreepDecVelH";
            ntxbCreepAccVelH.Tag = "CreepAccVelH";
            ntxbCreepDevVelH.Tag = "CreepDevVelH";
            ntxbCreepStrVelH.Tag = "CreepStrVelH";
            ntxbOPRDevVelH.Tag = "OPRDevVelH";
            ntxbClearDelay.Tag = "ClearDelay";
            ntxbMaxSpeed.Tag = "JogMaxSpeed";
            ntxbHighSpeed.Tag = "JogHighSpeed";
            ntxbMidSpeed.Tag = "JogMidSpeed";
            ntxbLowSpeed.Tag = "JogLowSpeed";
            ntxbMicroSpeed.Tag = "JogMicroSpeed";

            cmbLTC.Tag = "LogicLTC";
            cmbSD.Tag = "LogicSD";
            cmbZPhase.Tag = "LogicZ";
            cmbORG.Tag = "LogicORG";
            cmbERC.Tag = "LogicERC";
            cmbINP.Tag = "LogicINP";
            cmbEncMode1.Tag = "EncMode";
            cmbPulseMode.Tag = "PulseMode";
            cmbALM.Tag = "LogicALM";
            cmbEncDir.Tag = "EncDir";
            cmbHomeMode.Tag = "HomeMode";

            ldrReady.On = axis.Status.RDY;
            ldrAlarm.On = axis.Status.ALM;
            ldrLimitP.On = axis.Status.LimitP;
            ldrLimitN.On = axis.Status.LimitN;
            ldrORG.On = axis.Status.ORG;
            ldrHome.On = axis.Status.Home;
            ldrEMG.On = axis.Status.EMG;
            ldrRALM.On = axis.Status.RALM;
            ldrSVON.On = axis.Status.SVON;
            ldrINP.On = axis.Status.INP;
            ldrSD.On = axis.Status.SD;
            ldrLatch.On = axis.Status.Latch;
            ldrZPhase.On = axis.Status.ZPhase;
            ldrERC.On = axis.Status.ERC;

            ntxbStrVelM.Text = axis.AxisPara.StrVelM.ToString();
            ntxbDevVelM.Text = axis.AxisPara.DevVelM.ToString();
            ntxbAccVelM.Text = axis.AxisPara.AccVelM.ToString();
            ntxbDecVelM.Text = axis.AxisPara.DecVelM.ToString();
            ntxbDistPerRole.Text = axis.AxisPara.DistPerRole.ToString();
            ntxbPulsePerRole.Text = axis.AxisPara.PulsePerRole.ToString();
            ntxbDecVelA.Text = axis.AxisPara.DecVelA.ToString();
            ntxbAccVelA.Text = axis.AxisPara.AccVelA.ToString();
            ntxbDevVelA.Text = axis.AxisPara.DevVelA.ToString();
            ntxbStrVelA.Text = axis.AxisPara.StrVelA.ToString();
            ntxbOffsetH.Text = axis.AxisPara.OffsetH.ToString();
            ntxbCreepDecVelH.Text = axis.AxisPara.CreepDecVelH.ToString();
            ntxbCreepAccVelH.Text = axis.AxisPara.CreepAccVelH.ToString();
            ntxbCreepDevVelH.Text = axis.AxisPara.CreepDevVelH.ToString();
            ntxbCreepStrVelH.Text = axis.AxisPara.CreepStrVelH.ToString();
            ntxbOPRDevVelH.Text = axis.AxisPara.OPRDevVelH.ToString();
            ntxbClearDelay.Text = axis.AxisPara.ClearDelay.ToString();
            ntxbMaxSpeed.Text = axis.AxisPara.JogMaxSpeed.ToString();
            ntxbHighSpeed.Text = axis.AxisPara.JogHighSpeed.ToString();
            ntxbMidSpeed.Text = axis.AxisPara.JogMidSpeed.ToString();
            ntxbLowSpeed.Text = axis.AxisPara.JogLowSpeed.ToString();
            ntxbMicroSpeed.Text = axis.AxisPara.JogMicroSpeed.ToString();

            cmbLTC.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicLTC);
            cmbSD.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicSD);
            cmbZPhase.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicZ);
            cmbORG.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicORG);
            cmbERC.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicERC);
            cmbINP.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicINP);
            cmbEncMode1.SelectedIndex = Convert.ToInt16(axis.AxisPara.EncMode);
            cmbPulseMode.SelectedIndex = Convert.ToInt16(axis.AxisPara.PulseMode);
            cmbALM.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicALM);
            cmbEncDir.SelectedIndex = Convert.ToInt16(axis.AxisPara.EncDir);
            cmbHomeMode.SelectedIndex = Convert.ToInt16(axis.AxisPara.HomeMode);

            jogSpeed = JogSpeed.Micro;

            lbCardNumber.Text = axis.AxisPara.CardSwitchNo.ToString();
            lbRingNumber.Text = axis.AxisPara.RingNoOfCard.ToString();
            lbSlaveIP.Text = axis.AxisPara.SlaveIP.ToString();
        }

        #region controls event

        private void L132M1X1Form_Load(object sender , EventArgs e)
        {
            parameterInitial();
            setLanguage(language);
            isInitFinished = true;
        }

        private void para_TextChanged(object sender , EventArgs e)
        {
            if (isInitFinished)
            {
                PropertyInfo[] pi = axis.AxisPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo p in pi)
                {
                    if (((NumTextBox)sender).Tag.ToString() == p.Name)
                    {
                        string val;
                        if (((NumTextBox)sender).Text.Length > 0 && ((NumTextBox)sender).Text != "-")
                        {
                            val = ((NumTextBox)sender).Text;
                        }
                        else
                        {
                            val = "0";
                        }
                        if (p.PropertyType.BaseType.Name == "ValueType")
                            p.SetValue(axis.AxisPara , Convert.ChangeType(val , p.PropertyType) , null);
                    }
                }
            }
        }

        private void para_SelectedIndexChanged(object sender , EventArgs e)
        {
            if (isInitFinished)
            {
                PropertyInfo[] pi = axis.AxisPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo p in pi)
                {
                    if (((ComboBox)sender).Tag.ToString() == p.Name)
                    {
                        U16 val = Convert.ToUInt16(((ComboBox)sender).SelectedIndex);
                        if (p.PropertyType.BaseType.Name == "Enum")
                            p.SetValue(axis.AxisPara , Enum.Parse(p.PropertyType , val.ToString()) , null);
                        if (p.PropertyType.BaseType.Name == "ValueType")
                            p.SetValue(axis.AxisPara , Convert.ChangeType(val , p.PropertyType) , null);
                    }
                }
            }
        }

        private void tmrParaScan_Tick(object sender , EventArgs e)
        {
            ldrReady.On = axis.Status.RDY;
            ldrAlarm.On = axis.Status.ALM;
            ldrLimitP.On = axis.Status.LimitP;
            ldrLimitN.On = axis.Status.LimitN;
            ldrORG.On = axis.Status.ORG;
            ldrHome.On = axis.Status.Home;
            ldrEMG.On = axis.Status.EMG;
            ldrRALM.On = axis.Status.RALM;
            ldrSVON.On = axis.Status.SVON;
            ldrINP.On = axis.Status.INP;
            ldrSD.On = axis.Status.SD;
            ldrLatch.On = axis.Status.Latch;
            ldrZPhase.On = axis.Status.ZPhase;
            ldrERC.On = axis.Status.ERC;

            ntxbCommandCouter.Text = axis.Position.ToString();
            //ntxbFeedBackCounter.Text = axis.Encoder.ToString();

            ldrIsBusy.On = axis.IsBusy;
            ldrIsReached.On = axis.IsReached;
        }

        private void jogSpeed_Click(object sender , MouseEventArgs e)
        {
            List<LedButton> allLedBtn = new List<LedButton>();
            allLedBtn.Add(lbtnMicroSpeed);
            allLedBtn.Add(lbtnLowSpeed);
            allLedBtn.Add(lbtnMidSpeed);
            allLedBtn.Add(lbtnHighSpeed);
            allLedBtn.Add(lbtnMaxSpeed);
            foreach (LedButton eachLedBtn in allLedBtn)
            {
                if (((LedButton)sender).Name == eachLedBtn.Name)
                {
                    eachLedBtn.Active = true;
                    jogSpeed = (JogSpeed)allLedBtn.IndexOf(eachLedBtn);
                }
                else
                {
                    eachLedBtn.Active = false;
                }
            }
        }

        private void btnJogP_MouseDown(object sender , MouseEventArgs e)
        {
            axis.Jog(jogSpeed , RotationDirection.CW);
        }

        private void btnJogP_MouseUp(object sender , MouseEventArgs e)
        {
            axis.Stop(StopType.Emergency , true);
        }

        private void btnJogN_MouseDown(object sender , MouseEventArgs e)
        {
            axis.Jog(jogSpeed , RotationDirection.CCW);
        }

        private void btnJogN_MouseUp(object sender , MouseEventArgs e)
        {
            axis.Stop(StopType.Emergency , true);
        }

        private void btnEMG_Click(object sender , EventArgs e)
        {
            axis.Stop(StopType.Emergency , true);
        }

        #endregion controls event

        private void btnToPosition1_Click(object sender , EventArgs e)
        {
            axis.Move(Convert.ToDouble(ntxbPosition1.Text) , SpeedMode.Manual);
        }

        private void btnP2PStart_Click(object sender , EventArgs e)
        {
            //[底層] [待處理] 完成點到點動作
        }

        private void btnHome_Click(object sender , EventArgs e)
        {
            axis.Home();
        }

        private void btnServoON_Click(object sender , EventArgs e)
        {
            if (axis.Status.SVON == true)
                axis.ServoOn(CmdStatus.OFF);
            if (axis.Status.SVON == false)
                axis.ServoOn(CmdStatus.ON);
        }

        private void btnResetCounter_Click(object sender , EventArgs e)
        {
            axis.ResetPos();
        }

        private void btnSystemSetting_Click(object sender , EventArgs e)
        {
            ParameterINIForm dialog = new ParameterINIForm(axis.AxisPara, axis.DeviceName);
            dialog.ShowDialog();
        }
    }
}