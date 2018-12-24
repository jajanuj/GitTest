namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.Motion
{
    partial class CEtherCATMotionSlimForm
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
            this.lblEncoder = new System.Windows.Forms.Label();
            this.lblCommand = new System.Windows.Forms.Label();
            this.tmrParaScan = new System.Windows.Forms.Timer(this.components);
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
            this.btnFullSetting = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
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
            // tmrParaScan
            // 
            this.tmrParaScan.Enabled = true;
            this.tmrParaScan.Interval = 50;
            this.tmrParaScan.Tick += new System.EventHandler(this.tmrParaScan_Tick);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tableLayoutPanel4);
            this.groupBox7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox7.Location = new System.Drawing.Point(589, 10);
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
            this.groupBox11.Location = new System.Drawing.Point(10, 10);
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
            this.tableLayoutPanel15.Controls.Add(this.btnFullSetting, 1, 2);
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
            // btnFullSetting
            // 
            this.btnFullSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFullSetting.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnFullSetting.Location = new System.Drawing.Point(144, 131);
            this.btnFullSetting.Name = "btnFullSetting";
            this.btnFullSetting.Size = new System.Drawing.Size(136, 58);
            this.btnFullSetting.TabIndex = 234;
            this.btnFullSetting.Text = "Full Setting";
            this.btnFullSetting.UseVisualStyleBackColor = true;
            this.btnFullSetting.Click += new System.EventHandler(this.btnFullSetting_Click);
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
            // CEtherCATMotionSlimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 315);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox7);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CEtherCATMotionSlimForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Motion Teach";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.L122M2X4Form_FormClosing);
            this.Load += new System.EventHandler(this.L122M2X4Form_Load);
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
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.Timer tmrParaScan;
        private AOISystem.Utilities.Forms.LedRectangle ldrTargetReached;
        private AOISystem.Utilities.Forms.LedRectangle ldrWarning;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
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
        private System.Windows.Forms.Button btnFullSetting;
        private Forms.NumTextBox ntxbSpeed;
        private System.Windows.Forms.Label lblSpeed;
    }
}