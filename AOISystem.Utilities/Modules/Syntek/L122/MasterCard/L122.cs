using System;
using AOISystem.Utilities.Modules.Syntek.L122.Library;
using I16 = System.Int16;
using U16 = System.UInt16;
using U8 = System.Byte;
using System.Diagnostics;
using AOISystem.Utilities.Flow;

namespace AOISystem.Utilities.Modules.Syntek.L122.MasterCard
{
    public enum CardSwitchNo : ushort
    {
        Card0,
        Card1,
        Card2,
        Card3,
        Card4,
        Card5,
        Card6,
        Card7
    }

    public enum RingNoOfCard : ushort
    {
        CN1 ,
        CN2
    }

    public enum AxisNo : ushort
    {
        Axis0,
        Axis1,
        Axis2,
        Axis3
    }

    public enum DACChannel : byte
    {
        CN0 ,
        CN1 ,
        CN2 ,
        CN3
    }

    public enum ADCChannel : ushort
    {
        CN0 ,
        CN1 ,
        CN2 ,
        CN3 ,
        CN4 ,
        CN5 ,
        CN6 ,
        CN7
    }

    public enum ADCGain : byte
    {
        Gain0 ,
        Gain2 ,
        Gain4 ,
        Gain8
    }

    public enum SyntekBaudRate : ushort
    {
        Ring_2_5 ,
        Ring_5 ,
        Ring_10 ,
        Ring_20
    }

    //public enum CmdStatus : ushort
    //{
    //    OFF = 0, ON = 1
    //}

    public enum HomeMode : ushort
    {
        OnePoint = 0 ,
        TwoPoint = 1 ,
        ThreePoint = 2 ,
        ThreePointWithZPhase = 3
    }

    public enum SoftLimitStopType : byte
    {
        INT = 0 ,
        StopImmediately = 1 ,
        SlowStop = 2
    }

    public enum AdcGain : byte
    {
        /// <summary> No Gain </summary>
        No = 0 ,

        /// <summary> Double Gain </summary>
        Double = 1 ,

        /// <summary> Four times Gain </summary>
        Quadruple = 2 ,

        /// <summary> Eight times Gain </summary>
        Octuple = 3
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

    public enum EncDirection : ushort
    {
        NonInverse = 0 ,
        Inverse = 1
    }

    public enum EncMode : ushort
    {
        AB_1X ,
        AB_2X ,
        AB_4X ,
        CW_CCW
    }

    public enum PulseMode : ushort
    {
        OUT_DIR_OUT_Falling_Edge_DIR_High_Level,
        OUT_DIR_OUT_Rising_Edge_DIR_High_Level,
        OUT_DIR_OUT_Falling_Edge_DIR_Low_Level,
        OUT_DIR_OUT_Rising_Edge_DIR_Low_Level,
        CW_CCW_Hight_Active,
        AB_Phase,
        BA_Phase,
        CW_CCW_Low_Active
    }

    public enum MotorMode : ushort
    { 
        Servo,
        Step
    }

    public abstract class L122 : ModulesBase
    {
        public static uint[] DeviceTable;

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        protected U16 RingNoOfMNet;
        public static bool g_bInitialFlag = false;

        protected L122(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            masterCardInitialize();
            if (!g_bInitialFlag)
            {
                FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("SYNTEKMotion");

                g_bInitialFlag = true;
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
            //I16 retErr = CPCI_L122.CS_l122_close((U16)Parameter.CardSwitchNo);
            //if (retErr != 0)
            //{
            //    throw new Exception("PCI_L122 Resource Not Release !!! \n func = [_l122_close]");
            //}
            disposed = true;
        }

        private void masterCardInitialize()
        {
            //沒有實際測試過插上兩張L122或是多張MNet版卡
            U16 ringIdxOfMNet = 8;
            U8 verOfHardware = 0;
            I16 retOfGetHardware = CPCI_L122.CS_l122_get_hardware_info((U16)Parameter.CardSwitchNo , ref verOfHardware);
            if (retOfGetHardware != 0)
            {
                short lExistCards = 0;
                CPCI_L122.CS_l122_open(ref lExistCards);
                if (lExistCards == 0)
                {
                    throw new Exception("Can't find PCI_L122 !!! \n func = [_l122_open, lExistCards=0]");
                }

                I16 retOfSetRingCfg = CPCI_L122.CS_l122_set_ring_config((U16)Parameter.CardSwitchNo , (U16)Parameter.RingNoOfCard , 3);

                if (retOfSetRingCfg != 0)
                {
                    throw new Exception("Error occur when set ring config !!! \n func = [_l122_set_ring_config]");
                }
                else
                {
                    //取得122卡在MotionNet DLL的起始位置
                    I16 retOfStartRingNum = CPCI_L122.CS_l122_get_start_ring_num((U16)Parameter.CardSwitchNo , ref ringIdxOfMNet);
                    if (retOfStartRingNum == 0)
                    {
                        //MotionNet DLL最多支援8個串列埠
                        if ((ringIdxOfMNet + (U16)Parameter.RingNoOfCard) < 8)
                        {
                            RingNoOfMNet = (U16)(ringIdxOfMNet + (U16)Parameter.RingNoOfCard);
                            uint[] lDevTable = new uint[2];
                            if (DeviceTable == null)
                            {
                                CCMNet.CS_mnet_reset_ring(RingNoOfMNet);
                                short rc = CCMNet.CS_mnet_get_ring_active_table(RingNoOfMNet, lDevTable);
                                DeviceTable = lDevTable;
                                //Console.WriteLine("{0}, {1}", lDevTable[0], lDevTable[1]);
                            }
                            I16 rt = CCMNet.CS_mnet_start_ring(RingNoOfMNet);
                        }
                        else
                        {
                            throw new Exception("Wrong ring number of MotionNet !!! \n func = [_l122_get_start_ring_num] ");
                        }
                    }
                    else
                    {
                        throw new Exception("Error occur when get start ring number !!! \n func = [_l122_get_start_ring_num]");
                    }
                }
            }
            else
            {
                //取得122卡在MotionNet DLL的起始位置
                CPCI_L122.CS_l122_get_start_ring_num((U16)Parameter.CardSwitchNo , ref ringIdxOfMNet);

                //MotionNet DLL最多支援8個串列埠
                if ((ringIdxOfMNet + (U16)Parameter.RingNoOfCard) < 8)
                {
                    RingNoOfMNet = (U16)(ringIdxOfMNet + (U16)Parameter.RingNoOfCard);
                    uint[] lDevTable = new uint[2];
                    if (DeviceTable == null)
                    {
                        CCMNet.CS_mnet_reset_ring(RingNoOfMNet);
                        short rc = CCMNet.CS_mnet_get_ring_active_table(RingNoOfMNet, lDevTable);
                        DeviceTable = lDevTable;
                        Console.WriteLine("{0}, {1}", lDevTable[0], lDevTable[1]);
                    }
                    I16 rt = CCMNet.CS_mnet_start_ring(RingNoOfMNet);
                }
                else
                {
                    throw new Exception("Error occur when get hardware info!!! \n func = [_l122_get_hardware_info]");
                }
            }
        }
    }
}