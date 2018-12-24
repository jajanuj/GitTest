using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Forms;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.Library;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard
{
    class CEtherCAT_CheckStatus : FlowBase
    {
        private string _status = "Wait Initial";

        public CEtherCAT_CheckStatus()
        {
        }

        public override void Flow()
        {
            if (CEtherCAT.g_nESCExistCards > 0)
            {
                for (ushort uCount = 0; uCount < CEtherCAT.g_nESCExistCards; uCount++)
                {
                    ushort uConnectStatus = 0, uInitialDone = 0, uRet = 0, uAbnormal_Flag = 0, uWorking_Slave_Cnt = 0;
                    uRet = CEtherCAT_DLL.CS_ECAT_Master_Get_Connect_Status(CEtherCAT.g_uESCCardNoList[uCount].CardNo, ref uConnectStatus);
                    if (uRet == 0)
                    {
                        if (uConnectStatus == 0)
                        {
                            CEtherCAT.g_uESCCardNoList[uCount].ConnectStatus = "Initial Mode";
                        }
                        else if (uConnectStatus == 1)
                        {
                            CEtherCAT.g_uESCCardNoList[uCount].ConnectStatus = "Pre-OP Mode";
                        }
                        else if (uConnectStatus == 4)
                        {
                            CEtherCAT.g_uESCCardNoList[uCount].ConnectStatus = "Safe-OP Mode";
                        }
                        else if (uConnectStatus == 8)
                        {
                            CEtherCAT.g_uESCCardNoList[uCount].ConnectStatus = "OP Mode";
                        }
                    }
                    uRet = CEtherCAT_DLL.CS_ECAT_Master_Check_Initial_Done(CEtherCAT.g_uESCCardNoList[uCount].CardNo, ref uInitialDone);
                    if (uRet == 0)
                    {
                        if (uInitialDone == 0)
                        {
                            CEtherCAT.g_uESCCardNoList[uCount].InitialStatus = "Initial Done";
                        }
                        else if (uInitialDone == 1)
                        {
                            CEtherCAT.g_uESCCardNoList[uCount].InitialStatus = "Pre Initial";
                        }
                        else if (uInitialDone == 99)
                        {
                            CEtherCAT.g_uESCCardNoList[uCount].InitialStatus = "Initial Error";
                        }
                    }
                    uRet = CEtherCAT_DLL.CS_ECAT_Master_Check_Working_Counter(CEtherCAT.g_uESCCardNoList[uCount].CardNo, ref uAbnormal_Flag, ref uWorking_Slave_Cnt);
                    if (CEtherCAT.IsInitialized)
                    {
                        if (uAbnormal_Flag == 0)
                        {
                            CEtherCAT.g_uESCCardNoList[uCount].ConnectStatus = "Normal";
                        }
                        else
                        {
                            CEtherCAT.g_uESCCardNoList[uCount].ConnectStatus = "Abnormal";
                        }
                        CEtherCAT.g_uESCCardNoList[uCount].ConnectStatus = string.Format("{0} {1} / {2}", CEtherCAT.g_uESCCardNoList[uCount].ConnectStatus, uWorking_Slave_Cnt, CEtherCAT.SlaveInfos.Count);
                    }
                    else
                    {
                        string nowStatus = string.Format("{0}, {1}", CEtherCAT.g_uESCCardNoList[uCount].ConnectStatus, CEtherCAT.g_uESCCardNoList[uCount].InitialStatus);
                        if (nowStatus != _status)
                        {
                            _status = nowStatus;
                            EtherCATInitializationForm.GetInstance().SetStatus(uCount, nowStatus);
                        }
                    }
                }
            }
        }
    }
}
