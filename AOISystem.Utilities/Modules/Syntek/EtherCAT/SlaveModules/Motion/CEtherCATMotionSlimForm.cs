using AOISystem.Utilities.Common;
using AOISystem.Utilities.Forms;
using AOISystem.Utilities.MultiLanguage;
using AOISystem.Utilities.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.Motion
{
    //導程 : 螺桿(馬達)轉一圈的行程(長度) (線馬導程應該都是1)
    //每轉pulse量 : 馬達轉一圈所需的pulse(透過Driver設定 通常1pulse = 1um)
    internal partial class CEtherCATMotionSlimForm : Form
    {
        private Language language;
        private CultureInfo cultureInfo;
        private CEtherCATMotion axis;
        private bool isInitFinished;
        private JogSpeed jogSpeed;
        private ActionTask actionTask;
        private ActionItem itemRepeatMove;

        private CEtherCATMotionSlimForm()
        {
            InitializeComponent();
        }

        public CEtherCATMotionSlimForm(CEtherCATMotion axis, Language language)
        {
            InitializeComponent();
            this.Text = axis.DeviceName;
            this.axis = axis;
            this.language = language;
        }

        public CEtherCATMotionSlimForm(CEtherCATMotion axis)
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
            this.btnServoON.Text = axis.Status.SwitchOn ? "Servo OFF" : "Servo ON";
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
                    axis.AbsolueMove(dest1);
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
                    axis.AbsolueMove(dest2);
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
                        UInt16 val = Convert.ToUInt16(((ComboBox)sender).SelectedIndex);
                        if (p.PropertyType.BaseType.Name == "Enum")
                            p.SetValue(axis.AxisPara , Enum.Parse(p.PropertyType , val.ToString()) , null);
                        if (p.PropertyType.BaseType.Name == "ValueType")
                            p.SetValue(axis.AxisPara , Convert.ChangeType(val , p.PropertyType) , null);
                    }
                }
            }
        }

        private void tmrParaScan_Tick(object sender, EventArgs e)
        {
            ldrReadySwitch.On = axis.Status.ReadySwitch;
            ldrSwitchOn.On = axis.Status.SwitchOn;
            ldrOperationEnable.On = axis.Status.OperationEnable;
            ldrFault.On = axis.Status.Fault;
            ldrVoltage.On = axis.Status.Voltage;
            ldrQuickStop.On = axis.Status.QuickStop;
            ldrSwitchOff.On = axis.Status.SwitchOff;
            ldrWarning.On = axis.Status.Warning;
            ldrIsBusy.On = axis.IsBusy;
            ldrIsBusy.Text = "IsBusy " + axis.MotionDone;
            ldrRemote.On = axis.Status.Remote;
            ldrTargetReached.On = axis.Status.TargetReached;
            ldrIsHome.On = axis.IsHome;
            ldrIntLimitActive.On = axis.Status.IntLimitActive;
            ldrPEL.On = axis.Status.PEL;
            ldrORG.On = axis.Status.Home;
            ldrMEL.On = axis.Status.MEL;

            ntxbCommandCouter.Text = string.Format("{0:F2}", axis.Position);
            ntxbEncoder.Text = string.Format("{0:F2}", axis.Encoder);
            ntxbSpeed.Text = string.Format("{0:F2}", axis.Speed);
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
            axis.ContinuousMove(RotationDirection.CW, jogSpeed);
        }

        private void btnJogP_MouseUp(object sender , MouseEventArgs e)
        {
            axis.Stop(StopType.SlowDown , true);
        }

        private void btnJogN_MouseDown(object sender , MouseEventArgs e)
        {
            axis.ContinuousMove(RotationDirection.CCW, jogSpeed);
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
            axis.AbsolueMove(Convert.ToDouble(ntxbPosition1.Text));
        }

        private void btnRelMove_Click(object sender , EventArgs e)
        {
            axis.RelativeMove(Convert.ToDouble(ntxbPosition2.Text));
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
            if (this.btnServoON.Text == "Servo OFF")
            {
                this.btnServoON.Text = "Servo ON";
                axis.ServoOn(CmdStatus.OFF);
            }
            else
            {
                this.btnServoON.Text = "Servo OFF";
                axis.ServoOn(CmdStatus.ON);
            }
        }

        private void btnResetCounter_Click(object sender , EventArgs e)
        {
            axis.ResetPos();
        }

        private void btnResetAlarm_Click(object sender , EventArgs e)
        {
            axis.ResetAlarm();
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

        private void btnMovePEL_Click(object sender, EventArgs e)
        {
            axis.ContinuousMove(RotationDirection.CW, jogSpeed);
        }

        private void btnMoveMEL_Click(object sender, EventArgs e)
        {
            axis.ContinuousMove(RotationDirection.CCW, jogSpeed);
        }

        private void btnFullSetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (axis.ConfigurationShowDialog() == DialogResult.OK)
            {
                this.Visible = true;
                this.Activate();
            }
        }
    }
}