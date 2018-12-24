using AOISystem.Utilities.Common;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.ADC
{
    [Serializable]
    public class ParameterCEtherCATADC8124 : ParameterINI
    {
        private ushort _cardNo;
        private ushort _nodeNo;
        private bool isActive;
        private bool _enableCH0;
        private ADCRangeMode _adcRangeModeCH0;
        private ConvstFreqMode _convstFreqModeCH0;
        private ushort _averageTimesCH0;
        private bool _enableCH1;
        private ADCRangeMode _adcRangeModeCH1;
        private ConvstFreqMode _convstFreqModeCH1;
        private ushort _averageTimesCH1;
        private bool _enableCH2;
        private ADCRangeMode _adcRangeModeCH2;
        private ConvstFreqMode _convstFreqModeCH2;
        private ushort _averageTimesCH2;
        private bool _enableCH3;
        private ADCRangeMode _adcRangeModeCH3;
        private ConvstFreqMode _convstFreqModeCH3;
        private ushort _averageTimesCH3;

        /// <summary> CardNo 編號 </summary>
        [Browsable(true), Description("CardNo 編號")]
        public ushort CardNo { get { return _cardNo; } set { _cardNo = value; WriteAndNotify("CardNo"); } }

        /// <summary> NodeID 編號 </summary>
        [Browsable(true), Description("NodeID 編號")]
        public ushort NodeNo { get { return _nodeNo; } set { _nodeNo = value; WriteAndNotify("NodeNo"); } }

        /// <summary> 延伸模組啟用訊號 </summary>
        [DataMember()]
        public bool IsActive { get { return isActive; } set { isActive = value; WriteAndNotify("IsActive"); } }

        /// <summary> 是否啟用 CH0 </summary>
        [Browsable(true), Description("是否啟用 CH0")]
        public bool EnableCH0 { get { return _enableCH0; } set { _enableCH0 = value; WriteAndNotify("EnableCH0"); } }

        /// <summary> 輸出範圍 CH0 </summary>
        [Browsable(true), Description("輸出範圍 CH0")]
        public ADCRangeMode ADCRangeModeCH0 { get { return _adcRangeModeCH0; } set { _adcRangeModeCH0 = value; WriteAndNotify("ADCRangeModeCH0"); } }

        /// <summary> 轉換頻率 CH0 </summary>
        [Browsable(true), Description("轉換頻率 CH0")]
        public ConvstFreqMode ConvstFreqModeCH0 { get { return _convstFreqModeCH0; } set { _convstFreqModeCH0 = value; WriteAndNotify("ConvstFreqModeCH0"); } }

        /// <summary> 平均次數 CH0 </summary>
        [Browsable(true), Description("平均次數 CH0")]
        public ushort AverageTimesCH0 
        { 
            get { return _averageTimesCH0; } 
            set 
            {
                if (value <1)
                    _averageTimesCH0 = 1;
                else if (value > 127)
                    _averageTimesCH0 = 127;
                else
                    _averageTimesCH0 = value; 
                WriteAndNotify("AverageTimesCH0"); 
            } 
        }

        /// <summary> 是否啟用 CH1 </summary>
        [Browsable(true), Description("是否啟用 CH1")]
        public bool EnableCH1 { get { return _enableCH1; } set { _enableCH1 = value; WriteAndNotify("EnableCH1"); } }

        /// <summary> 輸出範圍 CH1 </summary>
        [Browsable(true), Description("輸出範圍 CH1")]
        public ADCRangeMode ADCRangeModeCH1 { get { return _adcRangeModeCH1; } set { _adcRangeModeCH1 = value; WriteAndNotify("ADCRangeModeCH1"); } }

        /// <summary> 轉換頻率 CH1 </summary>
        [Browsable(true), Description("轉換頻率 CH1")]
        public ConvstFreqMode ConvstFreqModeCH1 { get { return _convstFreqModeCH1; } set { _convstFreqModeCH1 = value; WriteAndNotify("ConvstFreqModeCH1"); } }

        /// <summary> 平均次數 CH1 </summary>
        [Browsable(true), Description("平均次數 CH1")]
        public ushort AverageTimesCH1
        {
            get { return _averageTimesCH1; }
            set
            {
                if (value < 1)
                    _averageTimesCH1 = 1;
                else if (value > 127)
                    _averageTimesCH1 = 127;
                else
                    _averageTimesCH1 = value;
                WriteAndNotify("AverageTimesCH1");
            }
        }

        /// <summary> 是否啟用 CH2 </summary>
        [Browsable(true), Description("是否啟用 CH2")]
        public bool EnableCH2 { get { return _enableCH2; } set { _enableCH2 = value; WriteAndNotify("EnableCH2"); } }

        /// <summary> 輸出範圍 CH2 </summary>
        [Browsable(true), Description("輸出範圍 CH2")]
        public ADCRangeMode ADCRangeModeCH2 { get { return _adcRangeModeCH2; } set { _adcRangeModeCH2 = value; WriteAndNotify("ADCRangeModeCH2"); } }

        /// <summary> 轉換頻率 CH2 </summary>
        [Browsable(true), Description("轉換頻率 CH2")]
        public ConvstFreqMode ConvstFreqModeCH2 { get { return _convstFreqModeCH2; } set { _convstFreqModeCH2 = value; WriteAndNotify("ConvstFreqModeCH2"); } }

        /// <summary> 平均次數 CH2 </summary>
        [Browsable(true), Description("平均次數 CH2")]
        public ushort AverageTimesCH2
        {
            get { return _averageTimesCH2; }
            set
            {
                if (value < 1)
                    _averageTimesCH2 = 1;
                else if (value > 127)
                    _averageTimesCH2 = 127;
                else
                    _averageTimesCH2 = value;
                WriteAndNotify("AverageTimesCH2");
            }
        }

        /// <summary> 是否啟用 CH3 </summary>
        [Browsable(true), Description("是否啟用 CH3")]
        public bool EnableCH3 { get { return _enableCH3; } set { _enableCH3 = value; WriteAndNotify("EnableCH3"); } }

        /// <summary> 輸出範圍 CH3 </summary>
        [Browsable(true), Description("輸出範圍 CH3")]
        public ADCRangeMode ADCRangeModeCH3 { get { return _adcRangeModeCH3; } set { _adcRangeModeCH3 = value; WriteAndNotify("ADCRangeModeCH3"); } }

        /// <summary> 轉換頻率 CH3 </summary>
        [Browsable(true), Description("轉換頻率 CH3")]
        public ConvstFreqMode ConvstFreqModeCH3 { get { return _convstFreqModeCH3; } set { _convstFreqModeCH3 = value; WriteAndNotify("ConvstFreqModeCH3"); } }

        /// <summary> 平均次數 CH3 </summary>
        [Browsable(true), Description("平均次數 CH3")]
        public ushort AverageTimesCH3
        {
            get { return _averageTimesCH3; }
            set
            {
                if (value < 1)
                    _averageTimesCH3 = 1;
                else if (value > 127)
                    _averageTimesCH3 = 127;
                else
                    _averageTimesCH3 = value;
                WriteAndNotify("AverageTimesCH3");
            }
        }

        public ParameterCEtherCATADC8124(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            _cardNo = 16;
            isActive = true;
            _enableCH0 = true;
            _adcRangeModeCH0 = ADCRangeMode.Positive_Negative_10V;
            _convstFreqModeCH0 = ConvstFreqMode.Hz_200000; 
            _averageTimesCH0 = 1;
            _enableCH1 = true;
            _adcRangeModeCH1 = ADCRangeMode.Positive_Negative_10V;
            _convstFreqModeCH1 = ConvstFreqMode.Hz_200000;
            _averageTimesCH1 = 1;
            _enableCH2 = true;
            _adcRangeModeCH2 = ADCRangeMode.Positive_Negative_10V;
            _convstFreqModeCH2 = ConvstFreqMode.Hz_200000;
            _averageTimesCH2 = 1;
            _enableCH3 = true;
            _adcRangeModeCH3 = ADCRangeMode.Positive_Negative_10V;
            _convstFreqModeCH3 = ConvstFreqMode.Hz_200000;
            _averageTimesCH3 = 1;
        }
    }
}