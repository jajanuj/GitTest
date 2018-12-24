namespace ChartTools
{
    partial class LineProfileChart
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox1_Acquisition = new System.Windows.Forms.GroupBox();
            this.lab_AverageValue = new System.Windows.Forms.Label();
            this.lab_MinimumValue = new System.Windows.Forms.Label();
            this.lab_MaximumValue = new System.Windows.Forms.Label();
            this.chb_YAxisZoomEnable = new System.Windows.Forms.CheckBox();
            this.chb_XAxisZoomEnable = new System.Windows.Forms.CheckBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1_Acquisition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1_Acquisition
            // 
            this.groupBox1_Acquisition.Controls.Add(this.lab_AverageValue);
            this.groupBox1_Acquisition.Controls.Add(this.lab_MinimumValue);
            this.groupBox1_Acquisition.Controls.Add(this.lab_MaximumValue);
            this.groupBox1_Acquisition.Controls.Add(this.chb_YAxisZoomEnable);
            this.groupBox1_Acquisition.Controls.Add(this.chb_XAxisZoomEnable);
            this.groupBox1_Acquisition.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1_Acquisition.Location = new System.Drawing.Point(0, 0);
            this.groupBox1_Acquisition.Name = "groupBox1_Acquisition";
            this.groupBox1_Acquisition.Size = new System.Drawing.Size(677, 47);
            this.groupBox1_Acquisition.TabIndex = 22;
            this.groupBox1_Acquisition.TabStop = false;
            this.groupBox1_Acquisition.Text = "Configuration";
            // 
            // lab_AverageValue
            // 
            this.lab_AverageValue.AutoSize = true;
            this.lab_AverageValue.Location = new System.Drawing.Point(494, 22);
            this.lab_AverageValue.Name = "lab_AverageValue";
            this.lab_AverageValue.Size = new System.Drawing.Size(101, 12);
            this.lab_AverageValue.TabIndex = 2;
            this.lab_AverageValue.Text = "Average Value : ###";
            // 
            // lab_MinimumValue
            // 
            this.lab_MinimumValue.AutoSize = true;
            this.lab_MinimumValue.Location = new System.Drawing.Point(380, 22);
            this.lab_MinimumValue.Name = "lab_MinimumValue";
            this.lab_MinimumValue.Size = new System.Drawing.Size(108, 12);
            this.lab_MinimumValue.TabIndex = 4;
            this.lab_MinimumValue.Text = "Minimum Value : ###";
            // 
            // lab_MaximumValue
            // 
            this.lab_MaximumValue.AutoSize = true;
            this.lab_MaximumValue.Location = new System.Drawing.Point(264, 22);
            this.lab_MaximumValue.Name = "lab_MaximumValue";
            this.lab_MaximumValue.Size = new System.Drawing.Size(110, 12);
            this.lab_MaximumValue.TabIndex = 3;
            this.lab_MaximumValue.Text = "Maximum Value : ###";
            // 
            // chb_YAxisZoomEnable
            // 
            this.chb_YAxisZoomEnable.AutoSize = true;
            this.chb_YAxisZoomEnable.Location = new System.Drawing.Point(135, 21);
            this.chb_YAxisZoomEnable.Name = "chb_YAxisZoomEnable";
            this.chb_YAxisZoomEnable.Size = new System.Drawing.Size(123, 16);
            this.chb_YAxisZoomEnable.TabIndex = 1;
            this.chb_YAxisZoomEnable.Text = "Y-Axis Zoom Enable";
            this.chb_YAxisZoomEnable.UseVisualStyleBackColor = true;
            this.chb_YAxisZoomEnable.CheckedChanged += new System.EventHandler(this.chb_XYAxisZoomEnable_CheckedChanged);
            // 
            // chb_XAxisZoomEnable
            // 
            this.chb_XAxisZoomEnable.AutoSize = true;
            this.chb_XAxisZoomEnable.Location = new System.Drawing.Point(6, 21);
            this.chb_XAxisZoomEnable.Name = "chb_XAxisZoomEnable";
            this.chb_XAxisZoomEnable.Size = new System.Drawing.Size(123, 16);
            this.chb_XAxisZoomEnable.TabIndex = 0;
            this.chb_XAxisZoomEnable.Text = "X-Axis Zoom Enable";
            this.chb_XAxisZoomEnable.UseVisualStyleBackColor = true;
            this.chb_XAxisZoomEnable.CheckedChanged += new System.EventHandler(this.chb_XYAxisZoomEnable_CheckedChanged);
            // 
            // chart1
            // 
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "Default";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 47);
            this.chart1.Name = "chart1";
            series1.ChartArea = "Default";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Default";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(677, 459);
            this.chart1.TabIndex = 23;
            this.chart1.Text = "chart1";
            // 
            // LineProfileChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.groupBox1_Acquisition);
            this.Name = "LineProfileChart";
            this.Size = new System.Drawing.Size(677, 506);
            this.groupBox1_Acquisition.ResumeLayout(false);
            this.groupBox1_Acquisition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1_Acquisition;
        private System.Windows.Forms.Label lab_AverageValue;
        private System.Windows.Forms.Label lab_MinimumValue;
        private System.Windows.Forms.Label lab_MaximumValue;
        private System.Windows.Forms.CheckBox chb_YAxisZoomEnable;
        private System.Windows.Forms.CheckBox chb_XAxisZoomEnable;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;

    }
}
