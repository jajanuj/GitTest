using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AOISystem.Utilities.Forms;
using AOISystem.Utilities.MultiLanguage;
using AOISystem.Utilities.Threading;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.Motion
{
    //此Form是從FrmL132M2X4來的，許多M314才有的功能並未在此Form上實做。
    //導程 : 螺桿(馬達)轉一圈的行程(長度) (線馬導程應該都是1)
    //每轉pulse量 : 馬達轉一圈所需的pulse(透過Driver設定 通常1pulse = 1um)
    internal partial class L122M2X4SlimForm : Form
    {
        private Language language;
        private CultureInfo cultureInfo;
        private L122M2X4 axis;
        private bool isInitFinished;
        private JogSpeed jogSpeed;
        private ActionTask actionTask;
        private ActionItem itemRepeatMove;

        private L122M2X4SlimForm()
        {
            InitializeComponent();
        }

        public L122M2X4SlimForm(L122M2X4 axis , Language language)
        {
            InitializeComponent();
            this.Text = axis.DeviceName;
            this.axis = axis;
            this.language = language;
        }

        public L122M2X4SlimForm(L122M2X4 axis)
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

            jogSpeed = JogSpeed.Micro;
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

        private void L122M2X4SlimForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnEMG_Click(sender, e);
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

            ntxbCommandCouter.Text = axis.Position.ToString();
            ntxbFeedBackCounter.Text = axis.Encoder.ToString();

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
            axis.AbsolueMove(Convert.ToDouble(ntxbPosition1.Text) , SpeedMode.Manual);
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

        private void btnFullSetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (axis.ConfigurationShowDialog() == DialogResult.OK)
            {
                this.Visible = true;
                this.Activate();
            }
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