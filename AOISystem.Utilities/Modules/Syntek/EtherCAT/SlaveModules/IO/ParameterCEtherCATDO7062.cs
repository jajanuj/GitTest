using AOISystem.Utilities.Common;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.IO
{
    [Serializable]
    public class ParameterCEtherCATDO7062 : ParameterINI
    {
        private ushort _cardNo;
        private ushort _nodeNo;
        private bool isActive;

        /// <summary> CardNo 編號 </summary>
        [Browsable(true), Description("CardNo 編號")]
        public ushort CardNo { get { return _cardNo; } set { _cardNo = value; WriteAndNotify("CardNo"); } }

        /// <summary> NodeID 編號 </summary>
        [Browsable(true), Description("NodeID 編號")]
        public ushort NodeNo { get { return _nodeNo; } set { _nodeNo = value; WriteAndNotify("NodeNo"); } }

        /// <summary> 延伸模組啟用訊號 </summary>
        [DataMember()]
        public bool IsActive { get { return isActive; } set { isActive = value; WriteAndNotify("IsActive"); } }

        public ParameterCEtherCATDO7062(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            _cardNo = 16;
            isActive = true;
        }
    }
}