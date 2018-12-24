namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.Motion
{
    partial class CEtherCATMotionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblAccVel = new System.Windows.Forms.Label();
            this.lblConstVel = new System.Windows.Forms.Label();
            this.lblEndVel = new System.Windows.Forms.Label();
            this.lblStrVel = new System.Windows.Forms.Label();
            this.lblDistPerRole = new System.Windows.Forms.Label();
            this.lblPulsePerRole = new System.Windows.Forms.Label();
            this.lblHomeSwitchVel = new System.Windows.Forms.Label();
            this.lblHomeZeroVel = new System.Windows.Forms.Label();
            this.lblHomeAccVel = new System.Windows.Forms.Label();
            this.lblHomeOffset = new System.Windows.Forms.Label();
            this.lblHomeMode = new System.Windows.Forms.Label();
            this.lblHighSpeed = new System.Windows.Forms.Label();
            this.lblMidSpeed = new System.Windows.Forms.Label();
            this.lblMicroSpeed = new System.Windows.Forms.Label();
            this.lblLowSpeed = new System.Windows.Forms.Label();
            this.lblMaxSpeed = new System.Windows.Forms.Label();
            this.lblPulseMode = new System.Windows.Forms.Label();
            this.lblEncMode = new System.Windows.Forms.Label();
            this.lblLogicEL = new System.Windows.Forms.Label();
            this.lblLogicORG = new System.Windows.Forms.Label();
            this.lblLogicSvon = new System.Windows.Forms.Label();
            this.lblEncoder = new System.Windows.Forms.Label();
            this.lblCommand = new System.Windows.Forms.Label();
            this.cmbLogicORG = new System.Windows.Forms.ComboBox();
            this.cmbLogicSvon = new System.Windows.Forms.ComboBox();
            this.cmbLogicZ = new System.Windows.Forms.ComboBox();
            this.cmbEncMode = new System.Windows.Forms.ComboBox();
            this.cmbLogicEL = new System.Windows.Forms.ComboBox();
            this.tmrParaScan = new System.Windows.Forms.Timer(this.components);
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.ntxbTDec = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbTAcc = new AOISystem.Utilities.Forms.NumTextBox();
            this.lblTDec = new System.Windows.Forms.Label();
            this.lblTAcc = new System.Windows.Forms.Label();
            this.ntxbHomeMode = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbDecVel = new AOISystem.Utilities.Forms.NumTextBox();
            this.lblDecVel = new System.Windows.Forms.Label();
            this.ntxbStopCmdWaitSeconds = new AOISystem.Utilities.Forms.NumTextBox();
            this.lblStopCmdWaitSeconds = new System.Windows.Forms.Label();
            this.ntxbInPositionPrecise = new AOISystem.Utilities.Forms.NumTextBox();
            this.lblInPositionPrecise = new System.Windows.Forms.Label();
            this.ntxbMicroSpeed = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbLowSpeed = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbPulsePerRole = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbDistPerRole = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbMidSpeed = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbHighSpeed = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbMaxSpeed = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbHomeSwitchVel = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbAccVel = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbEndVel = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbHomeZeroVel = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbConstVel = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbStrVel = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbHomeAccVel = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbHomeOffset = new AOISystem.Utilities.Forms.NumTextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSlotID = new System.Windows.Forms.Label();
            this.lblNodeID = new System.Windows.Forms.Label();
            this.lblCardID = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ntxbSpeed = new AOISystem.Utilities.Forms.NumTextBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.ldrOperationEnable = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrVoltage = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrWarning = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrORG = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrSwitchOff = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrSwitchOn = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrIntLimitActive = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrTargetReached = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrFault = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrPEL = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrIsHome = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrQuickStop = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrMEL = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrRemote = new AOISystem.Utilities.Forms.LedRectangle();
            this.ntxbCommandCouter = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbEncoder = new AOISystem.Utilities.Forms.NumTextBox();
            this.ldrReadySwitch = new AOISystem.Utilities.Forms.LedRectangle();
            this.ldrIsBusy = new AOISystem.Utilities.Forms.LedRectangle();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.btnConfiguration = new System.Windows.Forms.Button();
            this.btnMoveMEL = new System.Windows.Forms.Button();
            this.btnMovePEL = new System.Windows.Forms.Button();
            this.btnResetAlarm = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnEMG = new System.Windows.Forms.Button();
            this.btnResetCounter = new System.Windows.Forms.Button();
            this.btnServoON = new System.Windows.Forms.Button();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRelMove = new System.Windows.Forms.Button();
            this.btnP2PStart = new System.Windows.Forms.Button();
            this.btnAbsMove = new System.Windows.Forms.Button();
            this.ntxbDelayTime = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbPosition2 = new AOISystem.Utilities.Forms.NumTextBox();
            this.ntxbPosition1 = new AOISystem.Utilities.Forms.NumTextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.btnJogP = new System.Windows.Forms.Button();
            this.btnJogN = new System.Windows.Forms.Button();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.lbtnMaxSpeed = new AOISystem.Utilities.Forms.LedButton();
            this.lbtnHighSpeed = new AOISystem.Utilities.Forms.LedButton();
            this.lbtnMicroSpeed = new AOISystem.Utilities.Forms.LedButton();
            this.lbtnMidSpeed = new AOISystem.Utilities.Forms.LedButton();
            this.lbtnLowSpeed = new AOISystem.Utilities.Forms.LedButton();
            this.grpR1EC5621 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbMotorMode = new System.Windows.Forms.ComboBox();
            this.lblMotorMode = new System.Windows.Forms.Label();
            this.tableLayoutPanel31 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel27 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel25 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel23 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogicZ = new System.Windows.Forms.Label();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbPulseMode = new System.Windows.Forms.ComboBox();
            this.groupBox9.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.grpR1EC5621.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tableLayoutPanel31.SuspendLayout();
            this.tableLayoutPanel27.SuspendLayout();
            this.tableLayoutPanel25.SuspendLayout();
            this.tableLayoutPanel23.SuspendLayout();
            this.tableLayoutPanel21.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAccVel
            // 
            this.lblAccVel.AutoSize = true;
            this.lblAccVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblAccVel.Location = new System.Drawing.Point(3, 170);
            this.lblAccVel.Name = "lblAccVel";
            this.lblAccVel.Size = new System.Drawing.Size(77, 32);
            this.lblAccVel.TabIndex = 9;
            this.lblAccVel.Text = "加速度(mm/sec^2)";
            // 
            // lblConstVel
            // 
            this.lblConstVel.AutoSize = true;
            this.lblConstVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblConstVel.Location = new System.Drawing.Point(3, 34);
            this.lblConstVel.Name = "lblConstVel";
            this.lblConstVel.Size = new System.Drawing.Size(80, 32);
            this.lblConstVel.TabIndex = 8;
            this.lblConstVel.Text = "運動常態速度(mm/sec)";
            // 
            // lblEndVel
            // 
            this.lblEndVel.AutoSize = true;
            this.lblEndVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEndVel.Location = new System.Drawing.Point(3, 68);
            this.lblEndVel.Name = "lblEndVel";
            this.lblEndVel.Size = new System.Drawing.Size(80, 32);
            this.lblEndVel.TabIndex = 7;
            this.lblEndVel.Text = "運動結束速度(mm/sec)";
            // 
            // lblStrVel
            // 
            this.lblStrVel.AutoSize = true;
            this.lblStrVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblStrVel.Location = new System.Drawing.Point(3, 0);
            this.lblStrVel.Name = "lblStrVel";
            this.lblStrVel.Size = new System.Drawing.Size(80, 32);
            this.lblStrVel.TabIndex = 6;
            this.lblStrVel.Tag = "StrVel";
            this.lblStrVel.Text = "運動初始速度(mm/sec)";
            // 
            // lblDistPerRole
            // 
            this.lblDistPerRole.AutoSize = true;
            this.lblDistPerRole.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDistPerRole.Location = new System.Drawing.Point(219, 170);
            this.lblDistPerRole.Name = "lblDistPerRole";
            this.lblDistPerRole.Size = new System.Drawing.Size(32, 16);
            this.lblDistPerRole.TabIndex = 10;
            this.lblDistPerRole.Text = "導程";
            // 
            // lblPulsePerRole
            // 
            this.lblPulsePerRole.AutoSize = true;
            this.lblPulsePerRole.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPulsePerRole.Location = new System.Drawing.Point(219, 204);
            this.lblPulsePerRole.Name = "lblPulsePerRole";
            this.lblPulsePerRole.Size = new System.Drawing.Size(73, 16);
            this.lblPulsePerRole.TabIndex = 11;
            this.lblPulsePerRole.Text = "每轉Pulse量";
            // 
            // lblHomeSwitchVel
            // 
            this.lblHomeSwitchVel.AutoSize = true;
            this.lblHomeSwitchVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHomeSwitchVel.Location = new System.Drawing.Point(219, 68);
            this.lblHomeSwitchVel.Name = "lblHomeSwitchVel";
            this.lblHomeSwitchVel.Size = new System.Drawing.Size(92, 32);
            this.lblHomeSwitchVel.TabIndex = 12;
            this.lblHomeSwitchVel.Text = "復歸Switch速度(mm/sec";
            // 
            // lblHomeZeroVel
            // 
            this.lblHomeZeroVel.AutoSize = true;
            this.lblHomeZeroVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHomeZeroVel.Location = new System.Drawing.Point(219, 102);
            this.lblHomeZeroVel.Name = "lblHomeZeroVel";
            this.lblHomeZeroVel.Size = new System.Drawing.Size(82, 32);
            this.lblHomeZeroVel.TabIndex = 14;
            this.lblHomeZeroVel.Tag = "";
            this.lblHomeZeroVel.Text = "復歸Zero速度(mm/sec)";
            // 
            // lblHomeAccVel
            // 
            this.lblHomeAccVel.AutoSize = true;
            this.lblHomeAccVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHomeAccVel.Location = new System.Drawing.Point(219, 136);
            this.lblHomeAccVel.Name = "lblHomeAccVel";
            this.lblHomeAccVel.Size = new System.Drawing.Size(77, 32);
            this.lblHomeAccVel.TabIndex = 15;
            this.lblHomeAccVel.Text = "復歸加減速(mm/sec^2)";
            // 
            // lblHomeOffset
            // 
            this.lblHomeOffset.AutoSize = true;
            this.lblHomeOffset.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHomeOffset.Location = new System.Drawing.Point(219, 34);
            this.lblHomeOffset.Name = "lblHomeOffset";
            this.lblHomeOffset.Size = new System.Drawing.Size(80, 32);
            this.lblHomeOffset.TabIndex = 17;
            this.lblHomeOffset.Text = "復歸原點位移(mm)";
            // 
            // lblHomeMode
            // 
            this.lblHomeMode.AutoSize = true;
            this.lblHomeMode.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHomeMode.Location = new System.Drawing.Point(219, 0);
            this.lblHomeMode.Name = "lblHomeMode";
            this.lblHomeMode.Size = new System.Drawing.Size(56, 16);
            this.lblHomeMode.TabIndex = 19;
            this.lblHomeMode.Text = "復歸模式";
            // 
            // lblHighSpeed
            // 
            this.lblHighSpeed.AutoSize = true;
            this.lblHighSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblHighSpeed.Location = new System.Drawing.Point(435, 102);
            this.lblHighSpeed.Name = "lblHighSpeed";
            this.lblHighSpeed.Size = new System.Drawing.Size(59, 16);
            this.lblHighSpeed.TabIndex = 24;
            this.lblHighSpeed.Text = "JOG 高速";
            // 
            // lblMidSpeed
            // 
            this.lblMidSpeed.AutoSize = true;
            this.lblMidSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMidSpeed.Location = new System.Drawing.Point(435, 68);
            this.lblMidSpeed.Name = "lblMidSpeed";
            this.lblMidSpeed.Size = new System.Drawing.Size(59, 16);
            this.lblMidSpeed.TabIndex = 23;
            this.lblMidSpeed.Text = "JOG 中速";
            // 
            // lblMicroSpeed
            // 
            this.lblMicroSpeed.AutoSize = true;
            this.lblMicroSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMicroSpeed.Location = new System.Drawing.Point(435, 0);
            this.lblMicroSpeed.Name = "lblMicroSpeed";
            this.lblMicroSpeed.Size = new System.Drawing.Size(59, 16);
            this.lblMicroSpeed.TabIndex = 22;
            this.lblMicroSpeed.Text = "JOG 微速";
            // 
            // lblLowSpeed
            // 
            this.lblLowSpeed.AutoSize = true;
            this.lblLowSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLowSpeed.Location = new System.Drawing.Point(435, 34);
            this.lblLowSpeed.Name = "lblLowSpeed";
            this.lblLowSpeed.Size = new System.Drawing.Size(59, 16);
            this.lblLowSpeed.TabIndex = 21;
            this.lblLowSpeed.Text = "JOG 低速";
            // 
            // lblMaxSpeed
            // 
            this.lblMaxSpeed.AutoSize = true;
            this.lblMaxSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMaxSpeed.Location = new System.Drawing.Point(435, 136);
            this.lblMaxSpeed.Name = "lblMaxSpeed";
            this.lblMaxSpeed.Size = new System.Drawing.Size(59, 16);
            this.lblMaxSpeed.TabIndex = 20;
            this.lblMaxSpeed.Text = "JOG 極速";
            // 
            // lblPulseMode
            // 
            this.lblPulseMode.AutoSize = true;
            this.lblPulseMode.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPulseMode.Location = new System.Drawing.Point(3, 0);
            this.lblPulseMode.Name = "lblPulseMode";
            this.lblPulseMode.Size = new System.Drawing.Size(61, 16);
            this.lblPulseMode.TabIndex = 46;
            this.lblPulseMode.Text = "Pulse模式";
            // 
            // lblEncMode
            // 
            this.lblEncMode.AutoSize = true;
            this.lblEncMode.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEncMode.Location = new System.Drawing.Point(3, 0);
            this.lblEncMode.Name = "lblEncMode";
            this.lblEncMode.Size = new System.Drawing.Size(79, 16);
            this.lblEncMode.TabIndex = 45;
            this.lblEncMode.Text = "Encoder模式";
            // 
            // lblLogicEL
            // 
            this.lblLogicEL.AutoSize = true;
            this.lblLogicEL.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLogicEL.Location = new System.Drawing.Point(3, 0);
            this.lblLogicEL.Name = "lblLogicEL";
            this.lblLogicEL.Size = new System.Drawing.Size(45, 16);
            this.lblLogicEL.TabIndex = 44;
            this.lblLogicEL.Text = "EL邏輯";
            // 
            // lblLogicORG
            // 
            this.lblLogicORG.AutoSize = true;
            this.lblLogicORG.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLogicORG.Location = new System.Drawing.Point(3, 0);
            this.lblLogicORG.Name = "lblLogicORG";
            this.lblLogicORG.Size = new System.Drawing.Size(59, 16);
            this.lblLogicORG.TabIndex = 43;
            this.lblLogicORG.Text = "ORG邏輯";
            // 
            // lblLogicSvon
            // 
            this.lblLogicSvon.AutoSize = true;
            this.lblLogicSvon.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLogicSvon.Location = new System.Drawing.Point(3, 0);
            this.lblLogicSvon.Name = "lblLogicSvon";
            this.lblLogicSvon.Size = new System.Drawing.Size(60, 16);
            this.lblLogicSvon.TabIndex = 42;
            this.lblLogicSvon.Text = "Svon邏輯";
            // 
            // lblEncoder
            // 
            this.lblEncoder.AutoSize = true;
            this.lblEncoder.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEncoder.Location = new System.Drawing.Point(3, 25);
            this.lblEncoder.Name = "lblEncoder";
            this.lblEncoder.Size = new System.Drawing.Size(55, 16);
            this.lblEncoder.TabIndex = 95;
            this.lblEncoder.Text = "Encoder";
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCommand.Location = new System.Drawing.Point(3, 0);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(68, 16);
            this.lblCommand.TabIndex = 94;
            this.lblCommand.Text = "Command";
            // 
            // cmbLogicORG
            // 
            this.cmbLogicORG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbLogicORG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogicORG.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbLogicORG.FormattingEnabled = true;
            this.cmbLogicORG.Items.AddRange(new object[] {
            "Low",
            "High"});
            this.cmbLogicORG.Location = new System.Drawing.Point(106, 3);
            this.cmbLogicORG.Name = "cmbLogicORG";
            this.cmbLogicORG.Size = new System.Drawing.Size(98, 24);
            this.cmbLogicORG.TabIndex = 65;
            this.cmbLogicORG.Tag = "LogicORG";
            this.cmbLogicORG.SelectedIndexChanged += new System.EventHandler(this.para_SelectedIndexChanged);
            // 
            // cmbLogicSvon
            // 
            this.cmbLogicSvon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbLogicSvon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogicSvon.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbLogicSvon.FormattingEnabled = true;
            this.cmbLogicSvon.Items.AddRange(new object[] {
            "Low",
            "High"});
            this.cmbLogicSvon.Location = new System.Drawing.Point(106, 3);
            this.cmbLogicSvon.Name = "cmbLogicSvon";
            this.cmbLogicSvon.Size = new System.Drawing.Size(98, 24);
            this.cmbLogicSvon.TabIndex = 64;
            this.cmbLogicSvon.Tag = "LogicSvon";
            this.cmbLogicSvon.SelectedIndexChanged += new System.EventHandler(this.para_SelectedIndexChanged);
            // 
            // cmbLogicZ
            // 
            this.cmbLogicZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbLogicZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogicZ.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbLogicZ.FormattingEnabled = true;
            this.cmbLogicZ.Items.AddRange(new object[] {
            "Low",
            "High"});
            this.cmbLogicZ.Location = new System.Drawing.Point(106, 3);
            this.cmbLogicZ.Name = "cmbLogicZ";
            this.cmbLogicZ.Size = new System.Drawing.Size(98, 24);
            this.cmbLogicZ.TabIndex = 63;
            this.cmbLogicZ.Tag = "LogicZ";
            this.cmbLogicZ.SelectedIndexChanged += new System.EventHandler(this.para_SelectedIndexChanged);
            // 
            // cmbEncMode
            // 
            this.cmbEncMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEncMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncMode.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbEncMode.FormattingEnabled = true;
            this.cmbEncMode.Items.AddRange(new object[] {
            "A/B Phase",
            "CW/CCW",
            "Command_Pulse"});
            this.cmbEncMode.Location = new System.Drawing.Point(106, 3);
            this.cmbEncMode.Name = "cmbEncMode";
            this.cmbEncMode.Size = new System.Drawing.Size(98, 24);
            this.cmbEncMode.TabIndex = 57;
            this.cmbEncMode.Tag = "EncMode";
            this.cmbEncMode.SelectedIndexChanged += new System.EventHandler(this.para_SelectedIndexChanged);
            // 
            // cmbLogicEL
            // 
            this.cmbLogicEL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbLogicEL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogicEL.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbLogicEL.FormattingEnabled = true;
            this.cmbLogicEL.Items.AddRange(new object[] {
            "Low",
            "High"});
            this.cmbLogicEL.Location = new System.Drawing.Point(106, 3);
            this.cmbLogicEL.Name = "cmbLogicEL";
            this.cmbLogicEL.Size = new System.Drawing.Size(98, 24);
            this.cmbLogicEL.TabIndex = 55;
            this.cmbLogicEL.Tag = "LogicEL";
            this.cmbLogicEL.SelectedIndexChanged += new System.EventHandler(this.para_SelectedIndexChanged);
            // 
            // tmrParaScan
            // 
            this.tmrParaScan.Enabled = true;
            this.tmrParaScan.Interval = 50;
            this.tmrParaScan.Tick += new System.EventHandler(this.tmrParaScan_Tick);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.tableLayoutPanel6);
            this.groupBox9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox9.Location = new System.Drawing.Point(9, 77);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox9.Size = new System.Drawing.Size(650, 300);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Motion Configuration";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 6;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.ntxbTDec, 1, 4);
            this.tableLayoutPanel6.Controls.Add(this.ntxbTAcc, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.lblTDec, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.lblTAcc, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.ntxbHomeMode, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.ntxbDecVel, 1, 6);
            this.tableLayoutPanel6.Controls.Add(this.lblDecVel, 0, 6);
            this.tableLayoutPanel6.Controls.Add(this.ntxbStopCmdWaitSeconds, 5, 6);
            this.tableLayoutPanel6.Controls.Add(this.lblStopCmdWaitSeconds, 4, 6);
            this.tableLayoutPanel6.Controls.Add(this.ntxbInPositionPrecise, 3, 7);
            this.tableLayoutPanel6.Controls.Add(this.lblInPositionPrecise, 2, 7);
            this.tableLayoutPanel6.Controls.Add(this.ntxbMicroSpeed, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.ntxbLowSpeed, 5, 1);
            this.tableLayoutPanel6.Controls.Add(this.ntxbPulsePerRole, 3, 6);
            this.tableLayoutPanel6.Controls.Add(this.lblPulsePerRole, 2, 6);
            this.tableLayoutPanel6.Controls.Add(this.lblDistPerRole, 2, 5);
            this.tableLayoutPanel6.Controls.Add(this.ntxbDistPerRole, 3, 5);
            this.tableLayoutPanel6.Controls.Add(this.ntxbMidSpeed, 5, 2);
            this.tableLayoutPanel6.Controls.Add(this.ntxbHighSpeed, 5, 3);
            this.tableLayoutPanel6.Controls.Add(this.ntxbMaxSpeed, 5, 4);
            this.tableLayoutPanel6.Controls.Add(this.lblMicroSpeed, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblStrVel, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblLowSpeed, 4, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblConstVel, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblEndVel, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.lblMidSpeed, 4, 2);
            this.tableLayoutPanel6.Controls.Add(this.lblHighSpeed, 4, 3);
            this.tableLayoutPanel6.Controls.Add(this.lblAccVel, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.lblMaxSpeed, 4, 4);
            this.tableLayoutPanel6.Controls.Add(this.ntxbHomeSwitchVel, 3, 2);
            this.tableLayoutPanel6.Controls.Add(this.ntxbAccVel, 1, 5);
            this.tableLayoutPanel6.Controls.Add(this.lblHomeSwitchVel, 2, 2);
            this.tableLayoutPanel6.Controls.Add(this.ntxbEndVel, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.ntxbHomeZeroVel, 3, 3);
            this.tableLayoutPanel6.Controls.Add(this.ntxbConstVel, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.ntxbStrVel, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.ntxbHomeAccVel, 3, 4);
            this.tableLayoutPanel6.Controls.Add(this.lblHomeAccVel, 2, 4);
            this.tableLayoutPanel6.Controls.Add(this.ntxbHomeOffset, 3, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblHomeOffset, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblHomeZeroVel, 2, 3);
            this.tableLayoutPanel6.Controls.Add(this.lblHomeMode, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnTest, 4, 5);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 22);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 8;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(650, 278);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // ntxbTDec
            // 
            this.ntxbTDec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbTDec.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbTDec.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbTDec.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbTDec.Location = new System.Drawing.Point(111, 139);
            this.ntxbTDec.Name = "ntxbTDec";
            this.ntxbTDec.Size = new System.Drawing.Size(102, 23);
            this.ntxbTDec.TabIndex = 92;
            this.ntxbTDec.TabStop = false;
            this.ntxbTDec.Tag = "AccVel";
            this.ntxbTDec.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbTAcc
            // 
            this.ntxbTAcc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbTAcc.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbTAcc.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbTAcc.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbTAcc.Location = new System.Drawing.Point(111, 105);
            this.ntxbTAcc.Name = "ntxbTAcc";
            this.ntxbTAcc.Size = new System.Drawing.Size(102, 23);
            this.ntxbTAcc.TabIndex = 91;
            this.ntxbTAcc.TabStop = false;
            this.ntxbTAcc.Tag = "TAcc";
            this.ntxbTAcc.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // lblTDec
            // 
            this.lblTDec.AutoSize = true;
            this.lblTDec.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTDec.Location = new System.Drawing.Point(3, 136);
            this.lblTDec.Name = "lblTDec";
            this.lblTDec.Size = new System.Drawing.Size(80, 32);
            this.lblTDec.TabIndex = 90;
            this.lblTDec.Text = "運動減速時間(sec)";
            // 
            // lblTAcc
            // 
            this.lblTAcc.AutoSize = true;
            this.lblTAcc.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTAcc.Location = new System.Drawing.Point(3, 102);
            this.lblTAcc.Name = "lblTAcc";
            this.lblTAcc.Size = new System.Drawing.Size(80, 32);
            this.lblTAcc.TabIndex = 89;
            this.lblTAcc.Text = "運動加速時間(sec)";
            // 
            // ntxbHomeMode
            // 
            this.ntxbHomeMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbHomeMode.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbHomeMode.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbHomeMode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbHomeMode.Location = new System.Drawing.Point(327, 3);
            this.ntxbHomeMode.Name = "ntxbHomeMode";
            this.ntxbHomeMode.Size = new System.Drawing.Size(102, 23);
            this.ntxbHomeMode.TabIndex = 88;
            this.ntxbHomeMode.TabStop = false;
            this.ntxbHomeMode.Tag = "HomeMode";
            this.ntxbHomeMode.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbDecVel
            // 
            this.ntxbDecVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbDecVel.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbDecVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbDecVel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbDecVel.Location = new System.Drawing.Point(111, 207);
            this.ntxbDecVel.Name = "ntxbDecVel";
            this.ntxbDecVel.Size = new System.Drawing.Size(102, 23);
            this.ntxbDecVel.TabIndex = 87;
            this.ntxbDecVel.TabStop = false;
            this.ntxbDecVel.Tag = "DecVel";
            this.ntxbDecVel.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // lblDecVel
            // 
            this.lblDecVel.AutoSize = true;
            this.lblDecVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDecVel.Location = new System.Drawing.Point(3, 204);
            this.lblDecVel.Name = "lblDecVel";
            this.lblDecVel.Size = new System.Drawing.Size(77, 32);
            this.lblDecVel.TabIndex = 86;
            this.lblDecVel.Text = "減速度(mm/sec^2)";
            // 
            // ntxbStopCmdWaitSeconds
            // 
            this.ntxbStopCmdWaitSeconds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbStopCmdWaitSeconds.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbStopCmdWaitSeconds.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbStopCmdWaitSeconds.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbStopCmdWaitSeconds.Location = new System.Drawing.Point(543, 207);
            this.ntxbStopCmdWaitSeconds.Name = "ntxbStopCmdWaitSeconds";
            this.ntxbStopCmdWaitSeconds.Size = new System.Drawing.Size(104, 23);
            this.ntxbStopCmdWaitSeconds.TabIndex = 85;
            this.ntxbStopCmdWaitSeconds.TabStop = false;
            this.ntxbStopCmdWaitSeconds.Tag = "StopCmdWaitSeconds";
            this.ntxbStopCmdWaitSeconds.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // lblStopCmdWaitSeconds
            // 
            this.lblStopCmdWaitSeconds.AutoSize = true;
            this.lblStopCmdWaitSeconds.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblStopCmdWaitSeconds.Location = new System.Drawing.Point(435, 204);
            this.lblStopCmdWaitSeconds.Name = "lblStopCmdWaitSeconds";
            this.lblStopCmdWaitSeconds.Size = new System.Drawing.Size(92, 32);
            this.lblStopCmdWaitSeconds.TabIndex = 84;
            this.lblStopCmdWaitSeconds.Text = "停止指令等待時間(sec)";
            // 
            // ntxbInPositionPrecise
            // 
            this.ntxbInPositionPrecise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbInPositionPrecise.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbInPositionPrecise.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbInPositionPrecise.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbInPositionPrecise.Location = new System.Drawing.Point(327, 241);
            this.ntxbInPositionPrecise.Name = "ntxbInPositionPrecise";
            this.ntxbInPositionPrecise.Size = new System.Drawing.Size(102, 23);
            this.ntxbInPositionPrecise.TabIndex = 83;
            this.ntxbInPositionPrecise.TabStop = false;
            this.ntxbInPositionPrecise.Tag = "InPositionPrecise";
            this.ntxbInPositionPrecise.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // lblInPositionPrecise
            // 
            this.lblInPositionPrecise.AutoSize = true;
            this.lblInPositionPrecise.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblInPositionPrecise.Location = new System.Drawing.Point(219, 238);
            this.lblInPositionPrecise.Name = "lblInPositionPrecise";
            this.lblInPositionPrecise.Size = new System.Drawing.Size(80, 16);
            this.lblInPositionPrecise.TabIndex = 82;
            this.lblInPositionPrecise.Text = "到位訊號精度";
            // 
            // ntxbMicroSpeed
            // 
            this.ntxbMicroSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbMicroSpeed.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbMicroSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbMicroSpeed.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbMicroSpeed.Location = new System.Drawing.Point(543, 3);
            this.ntxbMicroSpeed.Name = "ntxbMicroSpeed";
            this.ntxbMicroSpeed.Size = new System.Drawing.Size(104, 23);
            this.ntxbMicroSpeed.TabIndex = 68;
            this.ntxbMicroSpeed.TabStop = false;
            this.ntxbMicroSpeed.Tag = "JogMicroSpeed";
            this.ntxbMicroSpeed.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbLowSpeed
            // 
            this.ntxbLowSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbLowSpeed.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbLowSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbLowSpeed.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbLowSpeed.Location = new System.Drawing.Point(543, 37);
            this.ntxbLowSpeed.Name = "ntxbLowSpeed";
            this.ntxbLowSpeed.Size = new System.Drawing.Size(104, 23);
            this.ntxbLowSpeed.TabIndex = 69;
            this.ntxbLowSpeed.TabStop = false;
            this.ntxbLowSpeed.Tag = "JogLowSpeed";
            this.ntxbLowSpeed.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbPulsePerRole
            // 
            this.ntxbPulsePerRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbPulsePerRole.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbPulsePerRole.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbPulsePerRole.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbPulsePerRole.Location = new System.Drawing.Point(327, 207);
            this.ntxbPulsePerRole.Name = "ntxbPulsePerRole";
            this.ntxbPulsePerRole.Size = new System.Drawing.Size(102, 23);
            this.ntxbPulsePerRole.TabIndex = 55;
            this.ntxbPulsePerRole.TabStop = false;
            this.ntxbPulsePerRole.Tag = "PulsePerRole";
            this.ntxbPulsePerRole.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbDistPerRole
            // 
            this.ntxbDistPerRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbDistPerRole.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbDistPerRole.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbDistPerRole.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbDistPerRole.Location = new System.Drawing.Point(327, 173);
            this.ntxbDistPerRole.Name = "ntxbDistPerRole";
            this.ntxbDistPerRole.Size = new System.Drawing.Size(102, 23);
            this.ntxbDistPerRole.TabIndex = 54;
            this.ntxbDistPerRole.TabStop = false;
            this.ntxbDistPerRole.Tag = "DistPerRole";
            this.ntxbDistPerRole.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbMidSpeed
            // 
            this.ntxbMidSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbMidSpeed.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbMidSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbMidSpeed.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbMidSpeed.Location = new System.Drawing.Point(543, 71);
            this.ntxbMidSpeed.Name = "ntxbMidSpeed";
            this.ntxbMidSpeed.Size = new System.Drawing.Size(104, 23);
            this.ntxbMidSpeed.TabIndex = 70;
            this.ntxbMidSpeed.TabStop = false;
            this.ntxbMidSpeed.Tag = "JogMidSpeed";
            this.ntxbMidSpeed.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbHighSpeed
            // 
            this.ntxbHighSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbHighSpeed.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbHighSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbHighSpeed.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbHighSpeed.Location = new System.Drawing.Point(543, 105);
            this.ntxbHighSpeed.Name = "ntxbHighSpeed";
            this.ntxbHighSpeed.Size = new System.Drawing.Size(104, 23);
            this.ntxbHighSpeed.TabIndex = 71;
            this.ntxbHighSpeed.TabStop = false;
            this.ntxbHighSpeed.Tag = "JogHighSpeed";
            this.ntxbHighSpeed.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbMaxSpeed
            // 
            this.ntxbMaxSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbMaxSpeed.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbMaxSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbMaxSpeed.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbMaxSpeed.Location = new System.Drawing.Point(543, 139);
            this.ntxbMaxSpeed.Name = "ntxbMaxSpeed";
            this.ntxbMaxSpeed.Size = new System.Drawing.Size(104, 23);
            this.ntxbMaxSpeed.TabIndex = 72;
            this.ntxbMaxSpeed.TabStop = false;
            this.ntxbMaxSpeed.Tag = "JogMaxSpeed";
            this.ntxbMaxSpeed.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbHomeSwitchVel
            // 
            this.ntxbHomeSwitchVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbHomeSwitchVel.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbHomeSwitchVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbHomeSwitchVel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbHomeSwitchVel.Location = new System.Drawing.Point(327, 71);
            this.ntxbHomeSwitchVel.Name = "ntxbHomeSwitchVel";
            this.ntxbHomeSwitchVel.Size = new System.Drawing.Size(102, 23);
            this.ntxbHomeSwitchVel.TabIndex = 60;
            this.ntxbHomeSwitchVel.TabStop = false;
            this.ntxbHomeSwitchVel.Tag = "HomeSwitchVel";
            this.ntxbHomeSwitchVel.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbAccVel
            // 
            this.ntxbAccVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbAccVel.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbAccVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbAccVel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbAccVel.Location = new System.Drawing.Point(111, 173);
            this.ntxbAccVel.Name = "ntxbAccVel";
            this.ntxbAccVel.Size = new System.Drawing.Size(102, 23);
            this.ntxbAccVel.TabIndex = 59;
            this.ntxbAccVel.TabStop = false;
            this.ntxbAccVel.Tag = "AccVel";
            this.ntxbAccVel.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbEndVel
            // 
            this.ntxbEndVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbEndVel.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbEndVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbEndVel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbEndVel.Location = new System.Drawing.Point(111, 71);
            this.ntxbEndVel.Name = "ntxbEndVel";
            this.ntxbEndVel.Size = new System.Drawing.Size(102, 23);
            this.ntxbEndVel.TabIndex = 58;
            this.ntxbEndVel.TabStop = false;
            this.ntxbEndVel.Tag = "EndVel";
            this.ntxbEndVel.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbHomeZeroVel
            // 
            this.ntxbHomeZeroVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbHomeZeroVel.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbHomeZeroVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbHomeZeroVel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbHomeZeroVel.Location = new System.Drawing.Point(327, 105);
            this.ntxbHomeZeroVel.Name = "ntxbHomeZeroVel";
            this.ntxbHomeZeroVel.Size = new System.Drawing.Size(102, 23);
            this.ntxbHomeZeroVel.TabIndex = 62;
            this.ntxbHomeZeroVel.TabStop = false;
            this.ntxbHomeZeroVel.Tag = "HomeZeroVel";
            this.ntxbHomeZeroVel.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbConstVel
            // 
            this.ntxbConstVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbConstVel.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbConstVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbConstVel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbConstVel.Location = new System.Drawing.Point(111, 37);
            this.ntxbConstVel.Name = "ntxbConstVel";
            this.ntxbConstVel.Size = new System.Drawing.Size(102, 23);
            this.ntxbConstVel.TabIndex = 57;
            this.ntxbConstVel.TabStop = false;
            this.ntxbConstVel.Tag = "ConstVel";
            this.ntxbConstVel.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbStrVel
            // 
            this.ntxbStrVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbStrVel.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbStrVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbStrVel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbStrVel.Location = new System.Drawing.Point(111, 3);
            this.ntxbStrVel.Name = "ntxbStrVel";
            this.ntxbStrVel.Size = new System.Drawing.Size(102, 23);
            this.ntxbStrVel.TabIndex = 56;
            this.ntxbStrVel.TabStop = false;
            this.ntxbStrVel.Tag = "StrVel";
            this.ntxbStrVel.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbHomeAccVel
            // 
            this.ntxbHomeAccVel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbHomeAccVel.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbHomeAccVel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbHomeAccVel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbHomeAccVel.Location = new System.Drawing.Point(327, 139);
            this.ntxbHomeAccVel.Name = "ntxbHomeAccVel";
            this.ntxbHomeAccVel.Size = new System.Drawing.Size(102, 23);
            this.ntxbHomeAccVel.TabIndex = 63;
            this.ntxbHomeAccVel.TabStop = false;
            this.ntxbHomeAccVel.Tag = "HomeAccVel";
            this.ntxbHomeAccVel.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // ntxbHomeOffset
            // 
            this.ntxbHomeOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbHomeOffset.FilterFlag = AOISystem.Utilities.Forms.FilterType.Numeric;
            this.ntxbHomeOffset.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbHomeOffset.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbHomeOffset.Location = new System.Drawing.Point(327, 37);
            this.ntxbHomeOffset.Name = "ntxbHomeOffset";
            this.ntxbHomeOffset.Size = new System.Drawing.Size(102, 23);
            this.ntxbHomeOffset.TabIndex = 65;
            this.ntxbHomeOffset.TabStop = false;
            this.ntxbHomeOffset.Tag = "HomeOffset";
            this.ntxbHomeOffset.TextChanged += new System.EventHandler(this.para_TextChanged);
            // 
            // btnTest
            // 
            this.btnTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTest.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTest.Location = new System.Drawing.Point(435, 173);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(102, 28);
            this.btnTest.TabIndex = 95;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tableLayoutPanel5);
            this.groupBox8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox8.Location = new System.Drawing.Point(10, 10);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox8.Size = new System.Drawing.Size(861, 63);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Information";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.lblSlotID, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblNodeID, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblCardID, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(1, 23);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(859, 39);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // lblSlotID
            // 
            this.lblSlotID.AutoSize = true;
            this.lblSlotID.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSlotID.Location = new System.Drawing.Point(575, 0);
            this.lblSlotID.Name = "lblSlotID";
            this.lblSlotID.Size = new System.Drawing.Size(62, 16);
            this.lblSlotID.TabIndex = 25;
            this.lblSlotID.Text = "Slot ID : #";
            // 
            // lblNodeID
            // 
            this.lblNodeID.AutoSize = true;
            this.lblNodeID.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblNodeID.Location = new System.Drawing.Point(289, 0);
            this.lblNodeID.Name = "lblNodeID";
            this.lblNodeID.Size = new System.Drawing.Size(73, 16);
            this.lblNodeID.TabIndex = 24;
            this.lblNodeID.Text = "Node ID : #";
            // 
            // lblCardID
            // 
            this.lblCardID.AutoSize = true;
            this.lblCardID.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCardID.Location = new System.Drawing.Point(3, 0);
            this.lblCardID.Name = "lblCardID";
            this.lblCardID.Size = new System.Drawing.Size(67, 16);
            this.lblCardID.TabIndex = 23;
            this.lblCardID.Text = "Card ID : #";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel4);
            this.groupBox7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox7.Location = new System.Drawing.Point(587, 383);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox7.Size = new System.Drawing.Size(282, 300);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Status";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.ntxbSpeed, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.lblSpeed, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.ldrOperationEnable, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.ldrVoltage, 0, 7);
            this.tableLayoutPanel4.Controls.Add(this.ldrWarning, 0, 10);
            this.tableLayoutPanel4.Controls.Add(this.ldrORG, 1, 9);
            this.tableLayoutPanel4.Controls.Add(this.ldrSwitchOff, 0, 9);
            this.tableLayoutPanel4.Controls.Add(this.ldrSwitchOn, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.ldrIntLimitActive, 1, 7);
            this.tableLayoutPanel4.Controls.Add(this.ldrTargetReached, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this.lblCommand, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.ldrFault, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.ldrPEL, 1, 8);
            this.tableLayoutPanel4.Controls.Add(this.lblEncoder, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.ldrIsHome, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.ldrQuickStop, 0, 8);
            this.tableLayoutPanel4.Controls.Add(this.ldrMEL, 1, 10);
            this.tableLayoutPanel4.Controls.Add(this.ldrRemote, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.ntxbCommandCouter, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.ntxbEncoder, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.ldrReadySwitch, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.ldrIsBusy, 1, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1, 23);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 11;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(280, 276);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // ntxbSpeed
            // 
            this.ntxbSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbSpeed.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbSpeed.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbSpeed.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbSpeed.Location = new System.Drawing.Point(143, 53);
            this.ntxbSpeed.Name = "ntxbSpeed";
            this.ntxbSpeed.ReadOnly = true;
            this.ntxbSpeed.Size = new System.Drawing.Size(134, 22);
            this.ntxbSpeed.TabIndex = 229;
            this.ntxbSpeed.TabStop = false;
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSpeed.Location = new System.Drawing.Point(3, 50);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(45, 16);
            this.lblSpeed.TabIndex = 228;
            this.lblSpeed.Text = "Speed";
            // 
            // ldrOperationEnable
            // 
            this.ldrOperationEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrOperationEnable.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrOperationEnable.Location = new System.Drawing.Point(2, 127);
            this.ldrOperationEnable.Margin = new System.Windows.Forms.Padding(2);
            this.ldrOperationEnable.Name = "ldrOperationEnable";
            this.ldrOperationEnable.On = false;
            this.ldrOperationEnable.Padding = new System.Windows.Forms.Padding(1);
            this.ldrOperationEnable.Size = new System.Drawing.Size(136, 21);
            this.ldrOperationEnable.TabIndex = 88;
            this.ldrOperationEnable.Text = "Operation Enable";
            this.ldrOperationEnable.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrOperationEnable.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrOperationEnable.TextVisible = true;
            // 
            // ldrVoltage
            // 
            this.ldrVoltage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrVoltage.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrVoltage.Location = new System.Drawing.Point(2, 177);
            this.ldrVoltage.Margin = new System.Windows.Forms.Padding(2);
            this.ldrVoltage.Name = "ldrVoltage";
            this.ldrVoltage.On = false;
            this.ldrVoltage.Padding = new System.Windows.Forms.Padding(1);
            this.ldrVoltage.Size = new System.Drawing.Size(136, 21);
            this.ldrVoltage.TabIndex = 89;
            this.ldrVoltage.Text = "Voltage";
            this.ldrVoltage.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrVoltage.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrVoltage.TextVisible = true;
            // 
            // ldrWarning
            // 
            this.ldrWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrWarning.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrWarning.Location = new System.Drawing.Point(2, 252);
            this.ldrWarning.Margin = new System.Windows.Forms.Padding(2);
            this.ldrWarning.Name = "ldrWarning";
            this.ldrWarning.On = false;
            this.ldrWarning.Padding = new System.Windows.Forms.Padding(1);
            this.ldrWarning.Size = new System.Drawing.Size(136, 22);
            this.ldrWarning.TabIndex = 227;
            this.ldrWarning.Text = "Warning";
            this.ldrWarning.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrWarning.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrWarning.TextVisible = true;
            // 
            // ldrORG
            // 
            this.ldrORG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrORG.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrORG.Location = new System.Drawing.Point(142, 227);
            this.ldrORG.Margin = new System.Windows.Forms.Padding(2);
            this.ldrORG.Name = "ldrORG";
            this.ldrORG.On = false;
            this.ldrORG.Padding = new System.Windows.Forms.Padding(1);
            this.ldrORG.Size = new System.Drawing.Size(136, 21);
            this.ldrORG.TabIndex = 90;
            this.ldrORG.Text = "ORG";
            this.ldrORG.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrORG.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrORG.TextVisible = true;
            // 
            // ldrSwitchOff
            // 
            this.ldrSwitchOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrSwitchOff.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrSwitchOff.Location = new System.Drawing.Point(2, 227);
            this.ldrSwitchOff.Margin = new System.Windows.Forms.Padding(2);
            this.ldrSwitchOff.Name = "ldrSwitchOff";
            this.ldrSwitchOff.On = false;
            this.ldrSwitchOff.Padding = new System.Windows.Forms.Padding(1);
            this.ldrSwitchOff.Size = new System.Drawing.Size(136, 21);
            this.ldrSwitchOff.TabIndex = 80;
            this.ldrSwitchOff.Text = "Switch Off";
            this.ldrSwitchOff.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrSwitchOff.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrSwitchOff.TextVisible = true;
            // 
            // ldrSwitchOn
            // 
            this.ldrSwitchOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrSwitchOn.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrSwitchOn.Location = new System.Drawing.Point(2, 102);
            this.ldrSwitchOn.Margin = new System.Windows.Forms.Padding(2);
            this.ldrSwitchOn.Name = "ldrSwitchOn";
            this.ldrSwitchOn.On = false;
            this.ldrSwitchOn.Padding = new System.Windows.Forms.Padding(1);
            this.ldrSwitchOn.Size = new System.Drawing.Size(136, 21);
            this.ldrSwitchOn.TabIndex = 91;
            this.ldrSwitchOn.Text = "Switch On";
            this.ldrSwitchOn.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrSwitchOn.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrSwitchOn.TextVisible = true;
            // 
            // ldrIntLimitActive
            // 
            this.ldrIntLimitActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrIntLimitActive.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrIntLimitActive.Location = new System.Drawing.Point(142, 177);
            this.ldrIntLimitActive.Margin = new System.Windows.Forms.Padding(2);
            this.ldrIntLimitActive.Name = "ldrIntLimitActive";
            this.ldrIntLimitActive.On = false;
            this.ldrIntLimitActive.Padding = new System.Windows.Forms.Padding(1);
            this.ldrIntLimitActive.Size = new System.Drawing.Size(136, 21);
            this.ldrIntLimitActive.TabIndex = 92;
            this.ldrIntLimitActive.Text = "Int Limit Active";
            this.ldrIntLimitActive.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrIntLimitActive.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrIntLimitActive.TextVisible = true;
            // 
            // ldrTargetReached
            // 
            this.ldrTargetReached.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrTargetReached.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrTargetReached.Location = new System.Drawing.Point(142, 127);
            this.ldrTargetReached.Margin = new System.Windows.Forms.Padding(2);
            this.ldrTargetReached.Name = "ldrTargetReached";
            this.ldrTargetReached.On = false;
            this.ldrTargetReached.Padding = new System.Windows.Forms.Padding(1);
            this.ldrTargetReached.Size = new System.Drawing.Size(136, 21);
            this.ldrTargetReached.TabIndex = 226;
            this.ldrTargetReached.Text = "Target Reached";
            this.ldrTargetReached.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrTargetReached.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrTargetReached.TextVisible = true;
            // 
            // ldrFault
            // 
            this.ldrFault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrFault.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrFault.Location = new System.Drawing.Point(2, 152);
            this.ldrFault.Margin = new System.Windows.Forms.Padding(2);
            this.ldrFault.Name = "ldrFault";
            this.ldrFault.On = false;
            this.ldrFault.Padding = new System.Windows.Forms.Padding(1);
            this.ldrFault.Size = new System.Drawing.Size(136, 21);
            this.ldrFault.TabIndex = 85;
            this.ldrFault.Text = "Fault";
            this.ldrFault.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrFault.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrFault.TextVisible = true;
            // 
            // ldrPEL
            // 
            this.ldrPEL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrPEL.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrPEL.Location = new System.Drawing.Point(142, 202);
            this.ldrPEL.Margin = new System.Windows.Forms.Padding(2);
            this.ldrPEL.Name = "ldrPEL";
            this.ldrPEL.On = false;
            this.ldrPEL.Padding = new System.Windows.Forms.Padding(1);
            this.ldrPEL.Size = new System.Drawing.Size(136, 21);
            this.ldrPEL.TabIndex = 81;
            this.ldrPEL.Text = "PEL";
            this.ldrPEL.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrPEL.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrPEL.TextVisible = true;
            // 
            // ldrIsHome
            // 
            this.ldrIsHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrIsHome.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrIsHome.Location = new System.Drawing.Point(142, 152);
            this.ldrIsHome.Margin = new System.Windows.Forms.Padding(2);
            this.ldrIsHome.Name = "ldrIsHome";
            this.ldrIsHome.On = false;
            this.ldrIsHome.Padding = new System.Windows.Forms.Padding(1);
            this.ldrIsHome.Size = new System.Drawing.Size(136, 21);
            this.ldrIsHome.TabIndex = 84;
            this.ldrIsHome.Text = "IsHome";
            this.ldrIsHome.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrIsHome.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrIsHome.TextVisible = true;
            // 
            // ldrQuickStop
            // 
            this.ldrQuickStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrQuickStop.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrQuickStop.Location = new System.Drawing.Point(2, 202);
            this.ldrQuickStop.Margin = new System.Windows.Forms.Padding(2);
            this.ldrQuickStop.Name = "ldrQuickStop";
            this.ldrQuickStop.On = false;
            this.ldrQuickStop.Padding = new System.Windows.Forms.Padding(1);
            this.ldrQuickStop.Size = new System.Drawing.Size(136, 21);
            this.ldrQuickStop.TabIndex = 83;
            this.ldrQuickStop.Text = "Quick Stop";
            this.ldrQuickStop.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrQuickStop.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrQuickStop.TextVisible = true;
            // 
            // ldrMEL
            // 
            this.ldrMEL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrMEL.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrMEL.Location = new System.Drawing.Point(142, 252);
            this.ldrMEL.Margin = new System.Windows.Forms.Padding(2);
            this.ldrMEL.Name = "ldrMEL";
            this.ldrMEL.On = false;
            this.ldrMEL.Padding = new System.Windows.Forms.Padding(1);
            this.ldrMEL.Size = new System.Drawing.Size(136, 22);
            this.ldrMEL.TabIndex = 82;
            this.ldrMEL.Text = "MEL";
            this.ldrMEL.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrMEL.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrMEL.TextVisible = true;
            // 
            // ldrRemote
            // 
            this.ldrRemote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrRemote.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrRemote.Location = new System.Drawing.Point(142, 102);
            this.ldrRemote.Margin = new System.Windows.Forms.Padding(2);
            this.ldrRemote.Name = "ldrRemote";
            this.ldrRemote.On = false;
            this.ldrRemote.Padding = new System.Windows.Forms.Padding(1);
            this.ldrRemote.Size = new System.Drawing.Size(136, 21);
            this.ldrRemote.TabIndex = 87;
            this.ldrRemote.Text = "Remote";
            this.ldrRemote.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrRemote.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrRemote.TextVisible = true;
            // 
            // ntxbCommandCouter
            // 
            this.ntxbCommandCouter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbCommandCouter.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbCommandCouter.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbCommandCouter.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbCommandCouter.Location = new System.Drawing.Point(143, 3);
            this.ntxbCommandCouter.Name = "ntxbCommandCouter";
            this.ntxbCommandCouter.ReadOnly = true;
            this.ntxbCommandCouter.Size = new System.Drawing.Size(134, 22);
            this.ntxbCommandCouter.TabIndex = 73;
            this.ntxbCommandCouter.TabStop = false;
            // 
            // ntxbEncoder
            // 
            this.ntxbEncoder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbEncoder.FilterFlag = AOISystem.Utilities.Forms.FilterType.NumericPos;
            this.ntxbEncoder.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbEncoder.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbEncoder.Location = new System.Drawing.Point(143, 28);
            this.ntxbEncoder.Name = "ntxbEncoder";
            this.ntxbEncoder.ReadOnly = true;
            this.ntxbEncoder.Size = new System.Drawing.Size(134, 22);
            this.ntxbEncoder.TabIndex = 74;
            this.ntxbEncoder.TabStop = false;
            // 
            // ldrReadySwitch
            // 
            this.ldrReadySwitch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrReadySwitch.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrReadySwitch.Location = new System.Drawing.Point(2, 77);
            this.ldrReadySwitch.Margin = new System.Windows.Forms.Padding(2);
            this.ldrReadySwitch.Name = "ldrReadySwitch";
            this.ldrReadySwitch.On = false;
            this.ldrReadySwitch.Padding = new System.Windows.Forms.Padding(1);
            this.ldrReadySwitch.Size = new System.Drawing.Size(136, 21);
            this.ldrReadySwitch.TabIndex = 79;
            this.ldrReadySwitch.Text = "Ready Switch";
            this.ldrReadySwitch.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrReadySwitch.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrReadySwitch.TextVisible = true;
            // 
            // ldrIsBusy
            // 
            this.ldrIsBusy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ldrIsBusy.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrIsBusy.Location = new System.Drawing.Point(142, 77);
            this.ldrIsBusy.Margin = new System.Windows.Forms.Padding(2);
            this.ldrIsBusy.Name = "ldrIsBusy";
            this.ldrIsBusy.On = false;
            this.ldrIsBusy.Padding = new System.Windows.Forms.Padding(1);
            this.ldrIsBusy.Size = new System.Drawing.Size(136, 21);
            this.ldrIsBusy.TabIndex = 86;
            this.ldrIsBusy.Text = "IsBusy";
            this.ldrIsBusy.TextColor = System.Drawing.Color.MediumVioletRed;
            this.ldrIsBusy.TextFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ldrIsBusy.TextVisible = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.tableLayoutPanel8);
            this.groupBox11.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox11.Location = new System.Drawing.Point(9, 383);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox11.Size = new System.Drawing.Size(576, 300);
            this.groupBox11.TabIndex = 2;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Operation";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.groupBox14, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(1, 23);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(574, 276);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.tableLayoutPanel15);
            this.groupBox14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox14.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox14.Location = new System.Drawing.Point(288, 1);
            this.groupBox14.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox14.Size = new System.Drawing.Size(285, 274);
            this.groupBox14.TabIndex = 3;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Main";
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Controls.Add(this.btnConfiguration, 1, 2);
            this.tableLayoutPanel15.Controls.Add(this.btnMoveMEL, 1, 3);
            this.tableLayoutPanel15.Controls.Add(this.btnMovePEL, 0, 3);
            this.tableLayoutPanel15.Controls.Add(this.btnResetAlarm, 1, 1);
            this.tableLayoutPanel15.Controls.Add(this.btnHome, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.btnEMG, 1, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnResetCounter, 0, 2);
            this.tableLayoutPanel15.Controls.Add(this.btnServoON, 0, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(1, 17);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 4;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(283, 256);
            this.tableLayoutPanel15.TabIndex = 0;
            // 
            // btnConfiguration
            // 
            this.btnConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConfiguration.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConfiguration.Location = new System.Drawing.Point(144, 131);
            this.btnConfiguration.Name = "btnConfiguration";
            this.btnConfiguration.Size = new System.Drawing.Size(136, 58);
            this.btnConfiguration.TabIndex = 234;
            this.btnConfiguration.Text = "Configuration";
            this.btnConfiguration.UseVisualStyleBackColor = true;
            this.btnConfiguration.Click += new System.EventHandler(this.btnConfiguration_Click);
            // 
            // btnMoveMEL
            // 
            this.btnMoveMEL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveMEL.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnMoveMEL.Location = new System.Drawing.Point(144, 195);
            this.btnMoveMEL.Name = "btnMoveMEL";
            this.btnMoveMEL.Size = new System.Drawing.Size(136, 58);
            this.btnMoveMEL.TabIndex = 233;
            this.btnMoveMEL.Text = "Move MEL";
            this.btnMoveMEL.UseVisualStyleBackColor = true;
            this.btnMoveMEL.Click += new System.EventHandler(this.btnMoveMEL_Click);
            // 
            // btnMovePEL
            // 
            this.btnMovePEL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMovePEL.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnMovePEL.Location = new System.Drawing.Point(3, 195);
            this.btnMovePEL.Name = "btnMovePEL";
            this.btnMovePEL.Size = new System.Drawing.Size(135, 58);
            this.btnMovePEL.TabIndex = 232;
            this.btnMovePEL.Text = "Move PEL";
            this.btnMovePEL.UseVisualStyleBackColor = true;
            this.btnMovePEL.Click += new System.EventHandler(this.btnMovePEL_Click);
            // 
            // btnResetAlarm
            // 
            this.btnResetAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResetAlarm.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnResetAlarm.Location = new System.Drawing.Point(144, 67);
            this.btnResetAlarm.Name = "btnResetAlarm";
            this.btnResetAlarm.Size = new System.Drawing.Size(136, 58);
            this.btnResetAlarm.TabIndex = 229;
            this.btnResetAlarm.Text = "Reset Alarm";
            this.btnResetAlarm.UseVisualStyleBackColor = true;
            this.btnResetAlarm.Click += new System.EventHandler(this.btnResetAlarm_Click);
            // 
            // btnHome
            // 
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHome.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnHome.Location = new System.Drawing.Point(3, 67);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(135, 58);
            this.btnHome.TabIndex = 226;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnEMG
            // 
            this.btnEMG.BackColor = System.Drawing.Color.Red;
            this.btnEMG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEMG.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEMG.Location = new System.Drawing.Point(144, 3);
            this.btnEMG.Name = "btnEMG";
            this.btnEMG.Size = new System.Drawing.Size(136, 58);
            this.btnEMG.TabIndex = 225;
            this.btnEMG.Text = "Emergency Stop";
            this.btnEMG.UseVisualStyleBackColor = false;
            this.btnEMG.Click += new System.EventHandler(this.btnEMG_Click);
            // 
            // btnResetCounter
            // 
            this.btnResetCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResetCounter.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnResetCounter.Location = new System.Drawing.Point(3, 131);
            this.btnResetCounter.Name = "btnResetCounter";
            this.btnResetCounter.Size = new System.Drawing.Size(135, 58);
            this.btnResetCounter.TabIndex = 228;
            this.btnResetCounter.Text = "Reset Pos/Enc";
            this.btnResetCounter.UseVisualStyleBackColor = true;
            this.btnResetCounter.Click += new System.EventHandler(this.btnResetCounter_Click);
            // 
            // btnServoON
            // 
            this.btnServoON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnServoON.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnServoON.Location = new System.Drawing.Point(3, 3);
            this.btnServoON.Name = "btnServoON";
            this.btnServoON.Size = new System.Drawing.Size(135, 58);
            this.btnServoON.TabIndex = 227;
            this.btnServoON.Text = "Servo ON/OFF";
            this.btnServoON.UseVisualStyleBackColor = true;
            this.btnServoON.Click += new System.EventHandler(this.btnServoON_Click);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.groupBox12, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.groupBox13, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.57143F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.42857F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(281, 270);
            this.tableLayoutPanel9.TabIndex = 3;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.tableLayoutPanel11);
            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox12.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox12.Location = new System.Drawing.Point(1, 1);
            this.groupBox12.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox12.Size = new System.Drawing.Size(279, 129);
            this.groupBox12.TabIndex = 1;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Position Move";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 3;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tableLayoutPanel11.Controls.Add(this.btnRelMove, 2, 1);
            this.tableLayoutPanel11.Controls.Add(this.btnP2PStart, 2, 2);
            this.tableLayoutPanel11.Controls.Add(this.btnAbsMove, 2, 0);
            this.tableLayoutPanel11.Controls.Add(this.ntxbDelayTime, 1, 2);
            this.tableLayoutPanel11.Controls.Add(this.ntxbPosition2, 1, 1);
            this.tableLayoutPanel11.Controls.Add(this.ntxbPosition1, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.label27, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.label28, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.label24, 0, 1);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(1, 17);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 3;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(277, 111);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // btnRelMove
            // 
            this.btnRelMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRelMove.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnRelMove.Location = new System.Drawing.Point(146, 39);
            this.btnRelMove.Name = "btnRelMove";
            this.btnRelMove.Size = new System.Drawing.Size(128, 31);
            this.btnRelMove.TabIndex = 231;
            this.btnRelMove.Text = "Rel Move  (Pos2)";
            this.btnRelMove.UseVisualStyleBackColor = true;
            this.btnRelMove.Click += new System.EventHandler(this.btnRelMove_Click);
            // 
            // btnP2PStart
            // 
            this.btnP2PStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnP2PStart.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnP2PStart.Location = new System.Drawing.Point(146, 76);
            this.btnP2PStart.Name = "btnP2PStart";
            this.btnP2PStart.Size = new System.Drawing.Size(128, 32);
            this.btnP2PStart.TabIndex = 223;
            this.btnP2PStart.Text = "Pos1 ~ Pos2 Move";
            this.btnP2PStart.UseVisualStyleBackColor = true;
            this.btnP2PStart.Click += new System.EventHandler(this.btnP2PStart_Click);
            // 
            // btnAbsMove
            // 
            this.btnAbsMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAbsMove.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAbsMove.Location = new System.Drawing.Point(146, 3);
            this.btnAbsMove.Name = "btnAbsMove";
            this.btnAbsMove.Size = new System.Drawing.Size(128, 30);
            this.btnAbsMove.TabIndex = 230;
            this.btnAbsMove.Text = "Abs Move (Pos1)";
            this.btnAbsMove.UseVisualStyleBackColor = true;
            this.btnAbsMove.Click += new System.EventHandler(this.btnAbsMove_Click);
            // 
            // ntxbDelayTime
            // 
            this.ntxbDelayTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbDelayTime.FilterFlag = AOISystem.Utilities.Forms.FilterType.Integral;
            this.ntxbDelayTime.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbDelayTime.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbDelayTime.Location = new System.Drawing.Point(50, 76);
            this.ntxbDelayTime.Name = "ntxbDelayTime";
            this.ntxbDelayTime.Size = new System.Drawing.Size(90, 25);
            this.ntxbDelayTime.TabIndex = 229;
            this.ntxbDelayTime.TabStop = false;
            this.ntxbDelayTime.Text = "1000";
            // 
            // ntxbPosition2
            // 
            this.ntxbPosition2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbPosition2.FilterFlag = AOISystem.Utilities.Forms.FilterType.Numeric;
            this.ntxbPosition2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbPosition2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbPosition2.Location = new System.Drawing.Point(50, 39);
            this.ntxbPosition2.Name = "ntxbPosition2";
            this.ntxbPosition2.Size = new System.Drawing.Size(90, 25);
            this.ntxbPosition2.TabIndex = 228;
            this.ntxbPosition2.TabStop = false;
            this.ntxbPosition2.Text = "0";
            // 
            // ntxbPosition1
            // 
            this.ntxbPosition1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ntxbPosition1.FilterFlag = AOISystem.Utilities.Forms.FilterType.Numeric;
            this.ntxbPosition1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ntxbPosition1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ntxbPosition1.Location = new System.Drawing.Point(50, 3);
            this.ntxbPosition1.Name = "ntxbPosition1";
            this.ntxbPosition1.Size = new System.Drawing.Size(90, 25);
            this.ntxbPosition1.TabIndex = 227;
            this.ntxbPosition1.TabStop = false;
            this.ntxbPosition1.Text = "0";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label27.Location = new System.Drawing.Point(3, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(35, 16);
            this.label27.TabIndex = 225;
            this.label27.Text = "Pos1";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label28.Location = new System.Drawing.Point(3, 73);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(36, 16);
            this.label28.TabIndex = 226;
            this.label28.Text = "Time";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label24.Location = new System.Drawing.Point(3, 36);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(35, 16);
            this.label24.TabIndex = 224;
            this.label24.Text = "Pos2";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.tableLayoutPanel12);
            this.groupBox13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox13.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox13.Location = new System.Drawing.Point(1, 132);
            this.groupBox13.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox13.Size = new System.Drawing.Size(279, 137);
            this.groupBox13.TabIndex = 2;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "JOG";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 1;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel14, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel13, 0, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(1, 17);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(277, 119);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel14.Controls.Add(this.btnJogP, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.btnJogN, 1, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 59);
            this.tableLayoutPanel14.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(277, 60);
            this.tableLayoutPanel14.TabIndex = 1;
            // 
            // btnJogP
            // 
            this.btnJogP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnJogP.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnJogP.Location = new System.Drawing.Point(3, 3);
            this.btnJogP.Name = "btnJogP";
            this.btnJogP.Size = new System.Drawing.Size(132, 54);
            this.btnJogP.TabIndex = 222;
            this.btnJogP.Text = "Jog+";
            this.btnJogP.UseVisualStyleBackColor = true;
            this.btnJogP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogP_MouseDown);
            this.btnJogP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogP_MouseUp);
            // 
            // btnJogN
            // 
            this.btnJogN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnJogN.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnJogN.Location = new System.Drawing.Point(141, 3);
            this.btnJogN.Name = "btnJogN";
            this.btnJogN.Size = new System.Drawing.Size(133, 54);
            this.btnJogN.TabIndex = 223;
            this.btnJogN.Text = "Jog-";
            this.btnJogN.UseVisualStyleBackColor = true;
            this.btnJogN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogN_MouseDown);
            this.btnJogN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogN_MouseUp);
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 5;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel13.Controls.Add(this.lbtnMaxSpeed, 4, 0);
            this.tableLayoutPanel13.Controls.Add(this.lbtnHighSpeed, 3, 0);
            this.tableLayoutPanel13.Controls.Add(this.lbtnMicroSpeed, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.lbtnMidSpeed, 2, 0);
            this.tableLayoutPanel13.Controls.Add(this.lbtnLowSpeed, 1, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel13.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(277, 59);
            this.tableLayoutPanel13.TabIndex = 0;
            // 
            // lbtnMaxSpeed
            // 
            this.lbtnMaxSpeed.Active = false;
            this.lbtnMaxSpeed.ActiveColor = System.Drawing.Color.Lime;
            this.lbtnMaxSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbtnMaxSpeed.InActiveColor = System.Drawing.Color.Red;
            this.lbtnMaxSpeed.Location = new System.Drawing.Point(223, 4);
            this.lbtnMaxSpeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbtnMaxSpeed.Name = "lbtnMaxSpeed";
            this.lbtnMaxSpeed.Size = new System.Drawing.Size(51, 51);
            this.lbtnMaxSpeed.TabIndex = 221;
            this.lbtnMaxSpeed.Text = "極速";
            this.lbtnMaxSpeed.UseVisualStyleBackColor = true;
            this.lbtnMaxSpeed.MouseClick += new System.Windows.Forms.MouseEventHandler(this.jogSpeed_Click);
            // 
            // lbtnHighSpeed
            // 
            this.lbtnHighSpeed.Active = false;
            this.lbtnHighSpeed.ActiveColor = System.Drawing.Color.Lime;
            this.lbtnHighSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbtnHighSpeed.InActiveColor = System.Drawing.Color.Red;
            this.lbtnHighSpeed.Location = new System.Drawing.Point(168, 4);
            this.lbtnHighSpeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbtnHighSpeed.Name = "lbtnHighSpeed";
            this.lbtnHighSpeed.Size = new System.Drawing.Size(49, 51);
            this.lbtnHighSpeed.TabIndex = 220;
            this.lbtnHighSpeed.Text = "高速";
            this.lbtnHighSpeed.UseVisualStyleBackColor = true;
            this.lbtnHighSpeed.MouseClick += new System.Windows.Forms.MouseEventHandler(this.jogSpeed_Click);
            // 
            // lbtnMicroSpeed
            // 
            this.lbtnMicroSpeed.Active = true;
            this.lbtnMicroSpeed.ActiveColor = System.Drawing.Color.Lime;
            this.lbtnMicroSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbtnMicroSpeed.InActiveColor = System.Drawing.Color.Red;
            this.lbtnMicroSpeed.Location = new System.Drawing.Point(3, 4);
            this.lbtnMicroSpeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbtnMicroSpeed.Name = "lbtnMicroSpeed";
            this.lbtnMicroSpeed.Size = new System.Drawing.Size(49, 51);
            this.lbtnMicroSpeed.TabIndex = 217;
            this.lbtnMicroSpeed.Text = "微速";
            this.lbtnMicroSpeed.UseVisualStyleBackColor = true;
            this.lbtnMicroSpeed.MouseClick += new System.Windows.Forms.MouseEventHandler(this.jogSpeed_Click);
            // 
            // lbtnMidSpeed
            // 
            this.lbtnMidSpeed.Active = false;
            this.lbtnMidSpeed.ActiveColor = System.Drawing.Color.Lime;
            this.lbtnMidSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbtnMidSpeed.InActiveColor = System.Drawing.Color.Red;
            this.lbtnMidSpeed.Location = new System.Drawing.Point(113, 4);
            this.lbtnMidSpeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbtnMidSpeed.Name = "lbtnMidSpeed";
            this.lbtnMidSpeed.Size = new System.Drawing.Size(49, 51);
            this.lbtnMidSpeed.TabIndex = 219;
            this.lbtnMidSpeed.Text = "中速";
            this.lbtnMidSpeed.UseVisualStyleBackColor = true;
            this.lbtnMidSpeed.MouseClick += new System.Windows.Forms.MouseEventHandler(this.jogSpeed_Click);
            // 
            // lbtnLowSpeed
            // 
            this.lbtnLowSpeed.Active = false;
            this.lbtnLowSpeed.ActiveColor = System.Drawing.Color.Lime;
            this.lbtnLowSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbtnLowSpeed.InActiveColor = System.Drawing.Color.Red;
            this.lbtnLowSpeed.Location = new System.Drawing.Point(58, 4);
            this.lbtnLowSpeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbtnLowSpeed.Name = "lbtnLowSpeed";
            this.lbtnLowSpeed.Size = new System.Drawing.Size(49, 51);
            this.lbtnLowSpeed.TabIndex = 218;
            this.lbtnLowSpeed.Text = "低速";
            this.lbtnLowSpeed.UseVisualStyleBackColor = true;
            this.lbtnLowSpeed.MouseClick += new System.Windows.Forms.MouseEventHandler(this.jogSpeed_Click);
            // 
            // grpR1EC5621
            // 
            this.grpR1EC5621.Controls.Add(this.tableLayoutPanel10);
            this.grpR1EC5621.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpR1EC5621.Location = new System.Drawing.Point(661, 77);
            this.grpR1EC5621.Margin = new System.Windows.Forms.Padding(1);
            this.grpR1EC5621.Name = "grpR1EC5621";
            this.grpR1EC5621.Padding = new System.Windows.Forms.Padding(1);
            this.grpR1EC5621.Size = new System.Drawing.Size(209, 300);
            this.grpR1EC5621.TabIndex = 0;
            this.grpR1EC5621.TabStop = false;
            this.grpR1EC5621.Text = "R1-EC5621 Configuration";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel16, 0, 4);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel31, 0, 6);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel27, 0, 3);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel25, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel23, 0, 2);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel21, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel19, 0, 5);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(1, 23);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 7;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(207, 276);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel16.Controls.Add(this.cmbMotorMode, 1, 0);
            this.tableLayoutPanel16.Controls.Add(this.lblMotorMode, 0, 0);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(0, 156);
            this.tableLayoutPanel16.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(207, 39);
            this.tableLayoutPanel16.TabIndex = 70;
            // 
            // cmbMotorMode
            // 
            this.cmbMotorMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMotorMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMotorMode.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbMotorMode.FormattingEnabled = true;
            this.cmbMotorMode.Items.AddRange(new object[] {
            "Servo",
            "Step"});
            this.cmbMotorMode.Location = new System.Drawing.Point(106, 3);
            this.cmbMotorMode.Name = "cmbMotorMode";
            this.cmbMotorMode.Size = new System.Drawing.Size(98, 24);
            this.cmbMotorMode.TabIndex = 62;
            this.cmbMotorMode.Tag = "MotorMode";
            this.cmbMotorMode.SelectedIndexChanged += new System.EventHandler(this.para_SelectedIndexChanged);
            // 
            // lblMotorMode
            // 
            this.lblMotorMode.AutoSize = true;
            this.lblMotorMode.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMotorMode.Location = new System.Drawing.Point(3, 0);
            this.lblMotorMode.Name = "lblMotorMode";
            this.lblMotorMode.Size = new System.Drawing.Size(56, 16);
            this.lblMotorMode.TabIndex = 50;
            this.lblMotorMode.Text = "馬達型態";
            // 
            // tableLayoutPanel31
            // 
            this.tableLayoutPanel31.ColumnCount = 2;
            this.tableLayoutPanel31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel31.Controls.Add(this.lblEncMode, 0, 0);
            this.tableLayoutPanel31.Controls.Add(this.cmbEncMode, 1, 0);
            this.tableLayoutPanel31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel31.Location = new System.Drawing.Point(0, 234);
            this.tableLayoutPanel31.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel31.Name = "tableLayoutPanel31";
            this.tableLayoutPanel31.RowCount = 1;
            this.tableLayoutPanel31.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel31.Size = new System.Drawing.Size(207, 42);
            this.tableLayoutPanel31.TabIndex = 12;
            // 
            // tableLayoutPanel27
            // 
            this.tableLayoutPanel27.ColumnCount = 2;
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel27.Controls.Add(this.cmbLogicEL, 1, 0);
            this.tableLayoutPanel27.Controls.Add(this.lblLogicEL, 0, 0);
            this.tableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel27.Location = new System.Drawing.Point(0, 117);
            this.tableLayoutPanel27.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel27.Name = "tableLayoutPanel27";
            this.tableLayoutPanel27.RowCount = 1;
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel27.Size = new System.Drawing.Size(207, 39);
            this.tableLayoutPanel27.TabIndex = 8;
            // 
            // tableLayoutPanel25
            // 
            this.tableLayoutPanel25.ColumnCount = 2;
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel25.Controls.Add(this.cmbLogicORG, 1, 0);
            this.tableLayoutPanel25.Controls.Add(this.lblLogicORG, 0, 0);
            this.tableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel25.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel25.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel25.Name = "tableLayoutPanel25";
            this.tableLayoutPanel25.RowCount = 1;
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel25.Size = new System.Drawing.Size(207, 39);
            this.tableLayoutPanel25.TabIndex = 6;
            // 
            // tableLayoutPanel23
            // 
            this.tableLayoutPanel23.ColumnCount = 2;
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.Controls.Add(this.cmbLogicSvon, 1, 0);
            this.tableLayoutPanel23.Controls.Add(this.lblLogicSvon, 0, 0);
            this.tableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel23.Location = new System.Drawing.Point(0, 78);
            this.tableLayoutPanel23.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            this.tableLayoutPanel23.RowCount = 1;
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.Size = new System.Drawing.Size(207, 39);
            this.tableLayoutPanel23.TabIndex = 4;
            // 
            // tableLayoutPanel21
            // 
            this.tableLayoutPanel21.ColumnCount = 2;
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel21.Controls.Add(this.cmbLogicZ, 1, 0);
            this.tableLayoutPanel21.Controls.Add(this.lblLogicZ, 0, 0);
            this.tableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel21.Location = new System.Drawing.Point(0, 39);
            this.tableLayoutPanel21.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 1;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(207, 39);
            this.tableLayoutPanel21.TabIndex = 2;
            // 
            // lblLogicZ
            // 
            this.lblLogicZ.AutoSize = true;
            this.lblLogicZ.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLogicZ.Location = new System.Drawing.Point(3, 0);
            this.lblLogicZ.Name = "lblLogicZ";
            this.lblLogicZ.Size = new System.Drawing.Size(51, 16);
            this.lblLogicZ.TabIndex = 41;
            this.lblLogicZ.Text = "Z相邏輯";
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 2;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.Controls.Add(this.cmbPulseMode, 1, 0);
            this.tableLayoutPanel19.Controls.Add(this.lblPulseMode, 0, 0);
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(0, 195);
            this.tableLayoutPanel19.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 1;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(207, 39);
            this.tableLayoutPanel19.TabIndex = 0;
            // 
            // cmbPulseMode
            // 
            this.cmbPulseMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPulseMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPulseMode.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbPulseMode.FormattingEnabled = true;
            this.cmbPulseMode.Items.AddRange(new object[] {
            "A/B Phase",
            "CW/CCW",
            "PLS/DIR"});
            this.cmbPulseMode.Location = new System.Drawing.Point(106, 3);
            this.cmbPulseMode.Name = "cmbPulseMode";
            this.cmbPulseMode.Size = new System.Drawing.Size(98, 24);
            this.cmbPulseMode.TabIndex = 57;
            this.cmbPulseMode.Tag = "PulseMode";
            this.cmbPulseMode.SelectedIndexChanged += new System.EventHandler(this.para_SelectedIndexChanged);
            // 
            // CEtherCATMotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 693);
            this.Controls.Add(this.grpR1EC5621);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CEtherCATMotionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Motion Teach";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.L122M2X4Form_FormClosing);
            this.Load += new System.EventHandler(this.L122M2X4Form_Load);
            this.groupBox9.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.grpR1EC5621.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel16.PerformLayout();
            this.tableLayoutPanel31.ResumeLayout(false);
            this.tableLayoutPanel31.PerformLayout();
            this.tableLayoutPanel27.ResumeLayout(false);
            this.tableLayoutPanel27.PerformLayout();
            this.tableLayoutPanel25.ResumeLayout(false);
            this.tableLayoutPanel25.PerformLayout();
            this.tableLayoutPanel23.ResumeLayout(false);
            this.tableLayoutPanel23.PerformLayout();
            this.tableLayoutPanel21.ResumeLayout(false);
            this.tableLayoutPanel21.PerformLayout();
            this.tableLayoutPanel19.ResumeLayout(false);
            this.tableLayoutPanel19.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAccVel;
        private System.Windows.Forms.Label lblConstVel;
        private System.Windows.Forms.Label lblEndVel;
        private System.Windows.Forms.Label lblStrVel;
        private System.Windows.Forms.Label lblDistPerRole;
        private System.Windows.Forms.Label lblPulsePerRole;
        private System.Windows.Forms.Label lblHomeSwitchVel;
        private System.Windows.Forms.Label lblHomeZeroVel;
        private System.Windows.Forms.Label lblHomeAccVel;
        private System.Windows.Forms.Label lblHomeOffset;
        private System.Windows.Forms.Label lblHomeMode;
        private System.Windows.Forms.Label lblHighSpeed;
        private System.Windows.Forms.Label lblMidSpeed;
        private System.Windows.Forms.Label lblMicroSpeed;
        private System.Windows.Forms.Label lblLowSpeed;
        private System.Windows.Forms.Label lblMaxSpeed;
        private System.Windows.Forms.Label lblPulseMode;
        private System.Windows.Forms.Label lblEncMode;
        private System.Windows.Forms.Label lblLogicEL;
        private System.Windows.Forms.Label lblLogicORG;
        private System.Windows.Forms.Label lblLogicSvon;
        private AOISystem.Utilities.Forms.NumTextBox ntxbHomeOffset;
        private AOISystem.Utilities.Forms.NumTextBox ntxbHomeAccVel;
        private AOISystem.Utilities.Forms.NumTextBox ntxbHomeZeroVel;
        private AOISystem.Utilities.Forms.NumTextBox ntxbHomeSwitchVel;
        private AOISystem.Utilities.Forms.NumTextBox ntxbMaxSpeed;
        private AOISystem.Utilities.Forms.NumTextBox ntxbHighSpeed;
        private AOISystem.Utilities.Forms.NumTextBox ntxbMidSpeed;
        private AOISystem.Utilities.Forms.NumTextBox ntxbLowSpeed;
        private AOISystem.Utilities.Forms.NumTextBox ntxbMicroSpeed;
        private AOISystem.Utilities.Forms.NumTextBox ntxbCommandCouter;
        private AOISystem.Utilities.Forms.NumTextBox ntxbEncoder;
        private AOISystem.Utilities.Forms.LedRectangle ldrReadySwitch;
        private AOISystem.Utilities.Forms.LedRectangle ldrSwitchOff;
        private AOISystem.Utilities.Forms.LedRectangle ldrPEL;
        private AOISystem.Utilities.Forms.LedRectangle ldrMEL;
        private AOISystem.Utilities.Forms.LedRectangle ldrQuickStop;
        private AOISystem.Utilities.Forms.LedRectangle ldrIsHome;
        private AOISystem.Utilities.Forms.LedRectangle ldrFault;
        private AOISystem.Utilities.Forms.LedRectangle ldrIntLimitActive;
        private AOISystem.Utilities.Forms.LedRectangle ldrSwitchOn;
        private AOISystem.Utilities.Forms.LedRectangle ldrORG;
        private AOISystem.Utilities.Forms.LedRectangle ldrVoltage;
        private AOISystem.Utilities.Forms.LedRectangle ldrOperationEnable;
        private AOISystem.Utilities.Forms.LedRectangle ldrRemote;
        private AOISystem.Utilities.Forms.LedRectangle ldrIsBusy;
        private System.Windows.Forms.Label lblEncoder;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.ComboBox cmbLogicORG;
        private System.Windows.Forms.ComboBox cmbLogicSvon;
        private System.Windows.Forms.ComboBox cmbLogicZ;
        private System.Windows.Forms.ComboBox cmbEncMode;
        private System.Windows.Forms.ComboBox cmbLogicEL;
        private System.Windows.Forms.Timer tmrParaScan;
        private AOISystem.Utilities.Forms.LedRectangle ldrTargetReached;
        private AOISystem.Utilities.Forms.LedRectangle ldrWarning;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox grpR1EC5621;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel31;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel27;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel25;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel23;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel21;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.Button btnMoveMEL;
        private System.Windows.Forms.Button btnMovePEL;
        private System.Windows.Forms.Button btnResetAlarm;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnEMG;
        private System.Windows.Forms.Button btnResetCounter;
        private System.Windows.Forms.Button btnServoON;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Button btnRelMove;
        private System.Windows.Forms.Button btnP2PStart;
        private System.Windows.Forms.Button btnAbsMove;
        private Forms.NumTextBox ntxbDelayTime;
        private Forms.NumTextBox ntxbPosition2;
        private Forms.NumTextBox ntxbPosition1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.Button btnJogP;
        private System.Windows.Forms.Button btnJogN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private Forms.LedButton lbtnMaxSpeed;
        private Forms.LedButton lbtnHighSpeed;
        private Forms.LedButton lbtnMicroSpeed;
        private Forms.LedButton lbtnMidSpeed;
        private Forms.LedButton lbtnLowSpeed;
        private System.Windows.Forms.Button btnConfiguration;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.ComboBox cmbMotorMode;
        private System.Windows.Forms.Label lblMotorMode;
        private System.Windows.Forms.Label lblInPositionPrecise;
        private Forms.NumTextBox ntxbStopCmdWaitSeconds;
        private System.Windows.Forms.Label lblStopCmdWaitSeconds;
        private System.Windows.Forms.Label lblDecVel;
        private System.Windows.Forms.ComboBox cmbPulseMode;
        private System.Windows.Forms.Label lblLogicZ;
        private Forms.NumTextBox ntxbDecVel;
        private Forms.NumTextBox ntxbInPositionPrecise;
        private Forms.NumTextBox ntxbPulsePerRole;
        private Forms.NumTextBox ntxbDistPerRole;
        private Forms.NumTextBox ntxbAccVel;
        private Forms.NumTextBox ntxbEndVel;
        private Forms.NumTextBox ntxbConstVel;
        private Forms.NumTextBox ntxbStrVel;
        private System.Windows.Forms.Label lblSlotID;
        private System.Windows.Forms.Label lblNodeID;
        private System.Windows.Forms.Label lblCardID;
        private Forms.NumTextBox ntxbSpeed;
        private System.Windows.Forms.Label lblSpeed;
        private Forms.NumTextBox ntxbHomeMode;
        private Forms.NumTextBox ntxbTDec;
        private Forms.NumTextBox ntxbTAcc;
        private System.Windows.Forms.Label lblTDec;
        private System.Windows.Forms.Label lblTAcc;
        private System.Windows.Forms.Button btnTest;
    }
}