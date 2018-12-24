using AOISystem.Utilities.Common;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.DAC
{
    [Serializable]
    public class ParameterCEtherCATDAC9144 : ParameterINI
    {
        private ushort _cardNo;
        private ushort _nodeNo;
        private bool isActive;
        private bool _enableCH0;
        private RangeMode _rangeModeCH0;
        private bool _enableCH1;
        private RangeMode _rangeModeCH1;
        private bool _enableCH2;
        private RangeMode _rangeModeCH2;
        private bool _enableCH3;
        private RangeMode _rangeModeCH3;

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
        [Browsable(true), Description("轉換頻率 CH0")]
        public RangeMode RangeModeCH0 { get { return _rangeModeCH0; } set { _rangeModeCH0 = value; WriteAndNotify("RangeModeCH0"); } }

        /// <summary> 是否啟用 CH1 </summary>
        [Browsable(true), Description("是否啟用 CH1")]
        public bool EnableCH1 { get { return _enableCH1; } set { _enableCH1 = value; WriteAndNotify("EnableCH1"); } }

        /// <summary> 輸出範圍 CH1 </summary>
        [Browsable(true), Description("轉換頻率 CH1")]
        public RangeMode RangeModeCH1 { get { return _rangeModeCH1; } set { _rangeModeCH1 = value; WriteAndNotify("RangeModeCH1"); } }

        /// <summary> 是否啟用 CH2 </summary>
        [Browsable(true), Description("是否啟用 CH2")]
        public bool EnableCH2 { get { return _enableCH2; } set { _enableCH2 = value; WriteAndNotify("EnableCH2"); } }

        /// <summary> 輸出範圍 CH2 </summary>
        [Browsable(true), Description("轉換頻率 CH2")]
        public RangeMode RangeModeCH2 { get { return _rangeModeCH2; } set { _rangeModeCH2 = value; WriteAndNotify("RangeModeCH2"); } }

        /// <summary> 是否啟用 CH3 </summary>
        [Browsable(true), Description("是否啟用 CH3")]
        public bool EnableCH3 { get { return _enableCH3; } set { _enableCH3 = value; WriteAndNotify("EnableCH3"); } }

        /// <summary> 輸出範圍 CH3 </summary>
        [Browsable(true), Description("轉換頻率 CH3")]
        public RangeMode RangeModeCH3 { get { return _rangeModeCH3; } set { _rangeModeCH3 = value; WriteAndNotify("RangeModeCH3"); } }

        public ParameterCEtherCATDAC9144(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            _cardNo = 16;
            isActive = true;
            _enableCH0 = true;
            _rangeModeCH0 = RangeMode.Positive_Negative_10V; 
            _enableCH1 = true;
            _rangeModeCH1 = RangeMode.Positive_Negative_10V; 
            _enableCH2 = true;
            _rangeModeCH2 = RangeMode.Positive_Negative_10V; 
            _enableCH3 = true;
            _rangeModeCH3 = RangeMode.Positive_Negative_10V; 
        }
    }
}