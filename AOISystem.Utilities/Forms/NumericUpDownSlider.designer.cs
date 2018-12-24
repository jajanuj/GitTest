namespace AOISystem.Utilities.Forms
{
    partial class NumericUpDownSlider
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.nudValue = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblLeftLabel = new System.Windows.Forms.Label();
            this.lblRightLabel = new System.Windows.Forms.Label();
            this.trbValue = new AOISystem.Utilities.Forms.DecimalTrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.SystemColors.Control;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblName.Location = new System.Drawing.Point(40, 45);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(81, 30);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Value : ";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudValue
            // 
            this.nudValue.BackColor = System.Drawing.SystemColors.Control;
            this.nudValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudValue.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.nudValue.Location = new System.Drawing.Point(127, 48);
            this.nudValue.Name = "nudValue";
            this.nudValue.Size = new System.Drawing.Size(81, 22);
            this.nudValue.TabIndex = 148;
            this.nudValue.ValueChanged += new System.EventHandler(this.nudValue_ValueChanged);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel.Controls.Add(this.lblLeftLabel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.lblRightLabel, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.trbValue, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.nudValue, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.lblName, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(250, 75);
            this.tableLayoutPanel.TabIndex = 149;
            // 
            // lblLeftLabel
            // 
            this.lblLeftLabel.BackColor = System.Drawing.SystemColors.Control;
            this.lblLeftLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLeftLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLeftLabel.Location = new System.Drawing.Point(3, 45);
            this.lblLeftLabel.Name = "lblLeftLabel";
            this.lblLeftLabel.Size = new System.Drawing.Size(31, 30);
            this.lblLeftLabel.TabIndex = 151;
            this.lblLeftLabel.Text = "Min";
            this.lblLeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRightLabel
            // 
            this.lblRightLabel.BackColor = System.Drawing.SystemColors.Control;
            this.lblRightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRightLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRightLabel.Location = new System.Drawing.Point(214, 45);
            this.lblRightLabel.Name = "lblRightLabel";
            this.lblRightLabel.Size = new System.Drawing.Size(33, 30);
            this.lblRightLabel.TabIndex = 150;
            this.lblRightLabel.Text = "Max";
            this.lblRightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trbValue
            // 
            this.tableLayoutPanel.SetColumnSpan(this.trbValue, 4);
            this.trbValue.DecimalPlaces = 0;
            this.trbValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trbValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.trbValue.Location = new System.Drawing.Point(3, 3);
            this.trbValue.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.trbValue.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.trbValue.Name = "trbValue";
            this.trbValue.Size = new System.Drawing.Size(244, 39);
            this.trbValue.TabIndex = 0;
            this.trbValue.TickFrequency = 10;
            this.trbValue.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trbValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.trbValue.WaitMouseDownUp = false;
            this.trbValue.ValueChanged += new System.EventHandler(this.trbValue_ValueChanged);
            // 
            // NumericUpDownSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Controls.Add(this.tableLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(225, 46);
            this.Name = "NumericUpDownSlider";
            this.Size = new System.Drawing.Size(250, 75);
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AOISystem.Utilities.Forms.DecimalTrackBar trbValue;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.NumericUpDown nudValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblRightLabel;
        private System.Windows.Forms.Label lblLeftLabel;
    }
}
