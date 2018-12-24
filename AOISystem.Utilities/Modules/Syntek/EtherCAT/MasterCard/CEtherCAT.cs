using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Linq;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard
{
    public enum HomeMode : ushort
    {
        OnePoint = 0 ,
        TwoPoint = 1 ,
        ThreePoint = 2 ,
        ThreePointWithZPhase = 3
    }

    public enum BitNo : byte
    {
        Bit0 = 0,
        Bit1 = 1,
        Bit2 = 2,
        Bit3 = 3,
        Bit4 = 4,
        Bit5 = 5,
        Bit6 = 6,
        Bit7 = 7
    }

    public enum PortNo : byte
    {
        Port0 = 0,
        Port1 = 1,
        Port2 = 2,
        Port3 = 3
    }

    public enum PulseMode : ushort
    {
        AB_Phase,
        CW_CCW,
        PLS_DIR
    }

    public enum EncMode : ushort
    {
        AB_Phase,
        CW_CCW,
        Command_Pulse
    }

    public enum MotorMode : ushort
    { 
        Servo,
        Step
    }

    public abstract class CEtherCAT : ModulesBase
    {
        public static ushort g_nESCExistCards = 0;
        public static ushort g_uESCSlotID = 0;
        public static Dictionary<ushort, CardInfo> g_uESCCardNoList = new Dictionary<ushort, CardInfo>();

        public static bool IsInitialized = false;
        public static bool g_bInitialFlag = false;
        public static Dictionary<string, SlaveInfo> SlaveInfos = new Dictionary<string, SlaveInfo>();
        private static Dictionary<ushort, string> errorCodes = new Dictionary<ushort, string>();

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        ushort g_uRet = 0;

        protected CEtherCAT(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            if (!g_bInitialFlag)
            {
                PrepareErrorCodes();

                FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("SYNTEKMotion");
                CEtherCAT_CheckStatus etherCAT_CheckStatus = new CEtherCAT_CheckStatus();
                flowControl.AddFlowBase(etherCAT_CheckStatus);
                etherCAT_CheckStatus.Start();

                EtherCATInitializationForm.GetInstance().ShowForm();
                EtherCATInitializationForm.GetInstance().SetStatus("Wait Initial");
                masterCardInitialize();
                EtherCATInitializationForm.GetInstance().CloseForm();

                etherCAT_CheckStatus.Stop();
            }
        }

        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            if (g_nESCExistCards > 0)
            {
                foreach (var item in g_uESCCardNoList)
                {
                    CEtherCAT_DLL.CS_ECAT_Master_Reset(item.Value.CardNo);
                }
                CEtherCAT_DLL.CS_ECAT_Master_Close();
                g_nESCExistCards = 0;
            }

            disposed = true;
        }

        private void masterCardInitialize()
        {
            //目前只設定卡片只有一張的情況
            InitialCard();

            if (!SpinWait.SpinUntil(() => g_uESCCardNoList.Where(x => x.Value.InitialStatus == "Initial Done").Count() == g_nESCExistCards, 30000))
            {
                throw new Exception("ECAT Initial Error");
            }

            FindSlave();
        }

        private void InitialCard()
        {
            try
            {
            g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Open(ref g_nESCExistCards);
            }
            catch (Exception ex)
            {
                throw new Exception("No EtherCat dll can be found!");
            }
            g_bInitialFlag = false;
            if (g_nESCExistCards == 0)
            {
                throw new Exception("No EtherCat can be found!");
            }
            else
            {
                for (ushort uCount = 0; uCount < g_nESCExistCards; uCount++)
                {
                    ushort uCardNo = 0;
                    g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Get_CardSeq(uCount, ref uCardNo);
                    if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                    {
                        throw new Exception("CS_ECAT_Master_Get_CardSeq, " + GetEtherCATErrorCode(g_uRet));
                    }
                    else
                    {
                        g_uESCCardNoList.Add(uCount, new CardInfo() { CardNo = uCardNo });
                    }
                }

                EtherCATInitializationForm.GetInstance().SetCardList(g_uESCCardNoList);

                foreach (var item in g_uESCCardNoList)
                    {
                    g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Initial(item.Value.CardNo);
                    if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                    {
                        throw new Exception("CS_ECAT_Master_Initial, " + GetEtherCATErrorCode(g_uRet));
                    }
                }
                g_bInitialFlag = true;
            }
        }

        private void FindSlave()
        {
            if (g_bInitialFlag)
            {
                SlaveInfos.Clear();
                foreach (var item in g_uESCCardNoList)
                {
                    ushort uNID = 0, uSlaveNum = 0, uReMapNodeID = 0;
                    uint uVendorID = 0, uProductCode = 0, uRevisionNo = 0, uSlaveDCTime = 0;

                    g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Get_SlaveNum(item.Value.CardNo, ref uSlaveNum);

                    if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                    {
                        throw new Exception("CS_ECAT_Master_Get_SlaveNum, " + GetEtherCATErrorCode(g_uRet));
                    }
                    else
                    {
                        if (uSlaveNum == 0)
                        {
                            throw new Exception("Card NO: " + item.Value.CardNo.ToString() + " No slave found!");
                        }
                        else
                        {
                            for (uNID = 0; uNID < uSlaveNum; uNID++)
                            {
                                g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Get_Slave_Info(item.Value.CardNo, uNID, ref uReMapNodeID, ref uVendorID, ref uProductCode, ref uRevisionNo, ref uSlaveDCTime);
                                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                                {
                                    throw new Exception("CS_ECAT_Master_Get_Slave_Info, " + GetEtherCATErrorCode(g_uRet));
                                }
                                Console.WriteLine(string.Format("{0}_{1}", item.Value.CardNo, uReMapNodeID));
                                SlaveInfos.Add(string.Format("{0}_{1}", item.Value.CardNo, uReMapNodeID), new SlaveInfo()
                                {
                                    CardNo = item.Value.CardNo,
                                    SeqID = uNID,
                                    NodeID = uReMapNodeID,
                                    VendorID = uVendorID,
                                    ProductCode = uProductCode,
                                    RevisionNo = uRevisionNo,
                                    DCTime = uSlaveDCTime
                                });

                                //Console.WriteLine("NID : {0}, NodeID : {1}, VendorID : {2}, ProductCode : {3}, RevisionNo : {4}", uNID, uReMapNodeID, uVendorID, uProductCode, uRevisionNo);

                                if (uVendorID == 0x1A05 && uProductCode == 0x6022) //R1-EC6022 16-ch Sink/Source Type Digital Input Module with 2ms Filter Function
                                {
                                }

                                if (uVendorID == 0x1A05 && uProductCode == 0x7062) //R1-EC7062 16-ch 24VDC/0.5A/Sink Type Digital Output Module
                                {
                                }

                                if (uVendorID == 0x1A05 && uProductCode == 0x8124) //R1-EC8124 4-ch 16-bit Single-ended/10kHz/Voltage or Current Mode A/D Control Module
                                {
                                }

                                if (uVendorID == 0x1A05 && uProductCode == 0x9144) //R1-EC9144 4-ch 16-bit Single-ended/Voltage or Current Mode D/A Control Module
                                {
                                }

                                if (uVendorID == 0x1A05 && uProductCode == 0x5621) //R1-EC5621 1-Axis Pulse Output Motion Control Module
                                {
                                }

                                if (uVendorID == 0x66F)
                                {
                                }
                            }
                        }
                    }
                }
                IsInitialized = true;
            }
        }

        private void PrepareErrorCodes()
        {
            if (errorCodes.Count == 0)
            {
                Type errorCodeType = typeof(CEtherCAT_DLL_Err);

                FieldInfo[] infoArray = errorCodeType.GetFields();

                foreach (FieldInfo info in infoArray)
                {
                    if (info.FieldType == typeof(ushort))
                    {
                        errorCodes.Add((ushort)info.GetValue(info.Name), info.Name);
                    }
                }   
            }
        }

        protected string GetEtherCATErrorCode(ushort errorCode)
        {
            string errorDescription = string.Format("[{0}] ErrorCode : {1}", this.DeviceName, IntToHexString(errorCode));
            if (errorCodes.ContainsKey(errorCode))
            {
                errorDescription = string.Format("{0}, {1}", errorDescription, errorCodes[errorCode]);
            }
            return errorDescription;
        }

        private string IntToHexString(ushort value)
        {
            return "0x" + string.Format("{0:X}", value);
        }

        private ushort HexStringToInt(string value)
        {
            if (value.ToUpper().StartsWith("0X"))
                value = value.Substring(2);
            return ushort.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }
    }
}