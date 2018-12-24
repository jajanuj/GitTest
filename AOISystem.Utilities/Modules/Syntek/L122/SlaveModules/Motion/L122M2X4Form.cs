using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using AOISystem.Utilities.Common;
using AOISystem.Utilities.Forms;
using AOISystem.Utilities.MultiLanguage;
using AOISystem.Utilities.Threading;
using U16 = System.UInt16;
using System.Diagnostics;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.Motion
{
    //此Form是從FrmL132M2X4來的，許多M314才有的功能並未在此Form上實做。
    //導程 : 螺桿(馬達)轉一圈的行程(長度) (線馬導程應該都是1)
    //每轉pulse量 : 馬達轉一圈所需的pulse(透過Driver設定 通常1pulse = 1um)
    internal partial class L122M2X4Form : Form
    {
        private Language language;
        private CultureInfo cultureInfo;
        private L122M2X4 axis;
        private bool isInitFinished;
        private JogSpeed jogSpeed;
        private ActionTask actionTask;
        private ActionItem itemRepeatMove;

        private L122M2X4Form()
        {
            InitializeComponent();
        }

        public L122M2X4Form(L122M2X4 axis , Language language)
        {
            InitializeComponent();
            this.Text = axis.DeviceName;
            this.axis = axis;
            this.language = language;
        }

        public L122M2X4Form(L122M2X4 axis)
        {
            InitializeComponent();
            this.Text = axis.DeviceName;
            this.axis = axis;
            this.language = Language.TraditionalChinese;
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
            ntxbInPositionPrecise.Tag = "InPositionPrecise";
            ntxbStopCmdWaitSeconds.Tag = "StopCmdWaitSeconds";
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
            cbSoftLimitEnable.Tag = "SoftLimitEnabled";
            ntxbSoftLimitP.Tag = "SoftLimitP";
            ntxbSoftLimitN.Tag = "SoftLimitN";

            cmbLTC.Tag = "LogicLTC";
            cmbSD.Tag = "LogicSD";
            cmbZPhase.Tag = "LogicZ";
            cmbORG.Tag = "LogicORG";
            cmbEL.Tag = "LogicEL";
            cmbERC.Tag = "LogicERC";
            cmbINP.Tag = "LogicINP";
            cmbEncMode1.Tag = "EncMode";
            cmbLTCSignalSource.Tag = "LTCSignalSource";
            cmbPulseMode.Tag = "PulseMode";
            cmbALM.Tag = "LogicALM";
            cmbEncDir.Tag = "EncDir";
            cmbHomeMode.Tag = "HomeMode";
            cmbMotorMode.Tag = "MotorMode";

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
            ntxbInPositionPrecise.Text = axis.axisPara.InPositionPrecise.ToString();
            ntxbStopCmdWaitSeconds.Text = axis.axisPara.StopCmdWaitSeconds.ToString();
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
            //cmbLTCSignalSource.SelectedIndex = Convert.ToInt16(axis.AxisPara.LTCSignalSource);
            cmbORG.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicORG);
            cmbERC.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicERC);
            cmbINP.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicINP);
            cmbEncMode1.SelectedIndex = Convert.ToInt16(axis.AxisPara.EncMode);
            cmbPulseMode.SelectedIndex = Convert.ToInt16(axis.AxisPara.PulseMode);
            cmbALM.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicALM);
            cmbEncDir.SelectedIndex = Convert.ToInt16(axis.AxisPara.EncDir);
            cmbHomeMode.SelectedIndex = Convert.ToInt16(axis.AxisPara.HomeMode);
            cmbEL.SelectedIndex = Convert.ToInt16(axis.AxisPara.LogicEL);
            cmbMotorMode.SelectedIndex = Convert.ToInt16(axis.AxisPara.MotorMode);
            jogSpeed = JogSpeed.Micro;

            lbCardNumber.Text = axis.AxisPara.CardSwitchNo.ToString();
            lbAxisNumber.Text = axis.AxisPara.AxisNo.ToString();
            lbSlaveIP.Text = axis.AxisPara.SlaveIP.ToString();
            ntxbSoftLimitN.Text = axis.AxisPara.SoftLimitN.ToString();
            ntxbSoftLimitP.Text = axis.AxisPara.SoftLimitP.ToString();
            cbSoftLimitEnable.Checked = axis.AxisPara.SoftLimitEnabled;
        }

        #region controls event

        private void L122M2X4Form_Load(object sender , EventArgs e)
        {
            parameterInitial();
            setLanguage(language);
            actionTask = new ActionTask(TaskRunType.Cycle , 50);
            itemRepeatMove = new ActionItem("RepeatMove", false, this, flowRepeatMove);
            actionTask.Add(itemRepeatMove);
            isInitFinished = true;
        }

        private void L122M2X4Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnEMG_Click(sender, e);
            this.DialogResult = DialogResult.OK;
        }

        private void flowRepeatMove(TProcVar S)
        {
            switch (S.StrStep1)
            {
                case "0":
                    if (!axis.IsBusy)
                    {
                        S.StrStep1 = "1";
                    }
                    break;

                case "1":
                    double dest1;
                    if (!Double.TryParse(ntxbPosition1.Text , out dest1))
                    {
                        dest1 = 0;
                    }
                    axis.AbsolueMove(dest1 , SpeedMode.Manual);
                    S.StrStep1 = "2";
                    break;

                case "2":
                    if (axis.IsReached && !axis.IsBusy)
                    {
                        S.TM1.Restart();
                        S.StrStep1 = "3";
                    }
                    break;

                case "3":
                    double delayTime1;
                    if (!Double.TryParse(ntxbDelayTime.Text , out delayTime1))
                    {
                        delayTime1 = 0;
                    }
                    if (S.TM1.ElapsedMilliseconds > delayTime1)
                    {
                        S.StrStep1 = "4";
                    }
                    break;

                case "4":
                    double dest2;
                    if (!Double.TryParse(ntxbPosition2.Text , out dest2))
                    {
                        dest2 = 0;
                    }
                    axis.AbsolueMove(dest2 , SpeedMode.Manual);
                    S.StrStep1 = "5";
                    break;

                case "5":
                    if (axis.IsReached && !axis.IsBusy)
                    {
                        S.TM1.Restart();
                        S.StrStep1 = "6";
                    }
                    break;

                case "6":
                    double delayTime2;
                    if (!Double.TryParse(ntxbDelayTime.Text , out delayTime2))
                    {
                        delayTime1 = 0;
                    }
                    if (S.TM1.ElapsedMilliseconds > delayTime2)
                    {
                        S.StrStep1 = "0";
                    }
                    break;
            }
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

        Stopwatch sw = new Stopwatch();
        double position = 0;
        double maxSpeed = double.MinValue;
        double minSpeed = double.MaxValue;
        int sampleCount = 0;
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
            //ldrZPhase.On = axis.Status.ZPhase;
            ldrERC.On = axis.Status.ERC;

            double speed = 0;
            if (sw.ElapsedMilliseconds != 0)
            {
                speed = (axis.Position - position) / sw.ElapsedMilliseconds * 1000;
            }
            position = axis.Position;
            sw.Restart();

            maxSpeed = Math.Max(maxSpeed, speed);
            minSpeed = Math.Min(minSpeed, speed);

            sampleCount++;
            sampleCount %= 20;
            if (sampleCount == 0)
            {
                this.lbSlaveIP.Text = string.Format("{0:F2}, {1:F2}", maxSpeed, minSpeed);
                maxSpeed = double.MinValue;
                minSpeed = double.MaxValue;
            }

            ntxbCommandCouter.Text = string.Format("{0:F2}, {1:F2}", axis.Position, axis.Speed);
            ntxbFeedBackCounter.Text = string.Format("{0:F2}, {1:F2}", axis.Encoder, speed);

            this.lbAxisNumber.Text = string.Format("{0:F2}", axis.ErrorCount);

            ldrIsBusy.On = axis.IsBusy;
            ldrIsReached.On = axis.IsReached;
            ldrEnabled.On = axis.Enabled && axis.IsActive;
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
            axis.JogConsiderSoftLimit(jogSpeed , RotationDirection.CW);
        }

        private void btnJogP_MouseUp(object sender , MouseEventArgs e)
        {
            axis.Stop(StopType.SlowDown , true);
        }

        private void btnJogN_MouseDown(object sender , MouseEventArgs e)
        {
            axis.JogConsiderSoftLimit(jogSpeed , RotationDirection.CCW);
        }

        private void btnJogN_MouseUp(object sender , MouseEventArgs e)
        {
            axis.Stop(StopType.SlowDown , true);
        }

        private void btnEMG_Click(object sender , EventArgs e)
        {
            actionTask.TurnOffAll();
            axis.Stop(StopType.Emergency , true);
        }

        #endregion controls event

        private void btnAbsMove_Click(object sender , EventArgs e)
        {
            axis.AbsolueMove(Convert.ToDouble(ntxbPosition1.Text), SpeedMode.Manual);
        }

        private void btnRelMove_Click(object sender , EventArgs e)
        {
            axis.RelativeMove(Convert.ToDouble(ntxbPosition2.Text) , SpeedMode.Manual);
        }

        private void btnP2PStart_Click(object sender , EventArgs e)
        {
            actionTask.TurnOn(itemRepeatMove);
        }

        private void btnHome_Click(object sender , EventArgs e)
        {
            axis.Home();
        }

        private void btnServoON_Click(object sender , EventArgs e)
        {
            axis.ServoOn(axis.Status.SVON ? CmdStatus.OFF : CmdStatus.ON);
        }

        private void btnResetCounter_Click(object sender , EventArgs e)
        {
            axis.ResetPos();
        }

        private void btnSlowDown_Click(object sender , EventArgs e)
        {
            actionTask.TurnOffAll();
            axis.Stop(StopType.SlowDown , true);
        }

        private void cbSoftLimitEnable_CheckedChanged(object sender , EventArgs e)
        {
            if (isInitFinished)
            {
                PropertyInfo[] pi = axis.AxisPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo p in pi)
                {
                    if (((CheckBox)sender).Tag.ToString() == p.Name)
                    {
                        bool b = Convert.ToBoolean(((CheckBox)sender).Checked);
                        if (p.PropertyType.BaseType.Name == "ValueType")
                        {
                            p.SetValue(axis.AxisPara , Convert.ChangeType(b , p.PropertyType) , null);
                        }
                        axis.SetSoftLimit(b , axis.AxisPara.SoftLimitN , axis.AxisPara.SoftLimitP);
                    }
                }
            }
        }

        private void ntxbSoftLimitN_TextChanged(object sender , EventArgs e)
        {
            if (isInitFinished)
            {
                PropertyInfo[] pi = axis.AxisPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo p in pi)
                {
                    if (((NumTextBox)sender).Tag.ToString() == p.Name)
                    {
                        string val = "0";
                        if (((NumTextBox)sender).Text.Length > 0 && ((NumTextBox)sender).Text != "-")
                        {
                            val = ((NumTextBox)sender).Text;
                        }
                        if (p.PropertyType.BaseType.Name == "ValueType")
                            p.SetValue(axis.AxisPara , Convert.ChangeType(val , p.PropertyType) , null);
                    }
                }
            }
        }

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            ParameterINIForm propertyGridForm = new ParameterINIForm(axis.AxisPara, axis.DeviceName);
            propertyGridForm.ShowDialog();
            ((ParameterINI)axis.AxisPara).Save();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            axis.SetHomeAutoResetCounter();
        }

        private void btnMoveLimitP_Click(object sender, EventArgs e)
        {
            axis.JogConsiderSoftLimit(jogSpeed, RotationDirection.CW);
        }

        private void btnMoveLimitN_Click(object sender, EventArgs e)
        {
            axis.JogConsiderSoftLimit(jogSpeed, RotationDirection.CCW);
        }

        private void ldrRALM_Click(object sender, EventArgs e)
        {
            axis.ResetAlarm();
        }
    }
}