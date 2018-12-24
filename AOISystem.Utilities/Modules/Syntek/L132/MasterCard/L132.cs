using System;
using System.Threading;
using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L132.Library;
using AOISystem.Utilities.Resources;
using I16 = System.Int16;
using U16 = System.UInt16;

namespace AOISystem.Utilities.Modules.Syntek.L132.MasterCard
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
        ThreePoint = 2
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

    public enum AdcChannelEnable : byte
    {
        Disable = 0 ,
        Enable = 1
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
        Zero = 0 ,
        One = 1 ,
        Two = 2 ,
        Three = 3
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
        OUT_DIR_OUT_Falling_edge_DIR_high_level ,
        OUT_DIR_OUT_Rising_edge_DIR_high_level ,
        OUT_DIR_OUT_Falling_edge_DIR_low_level ,
        OUT_DIR_OUT_Rising_edge_DIR_low_level ,
        OUT_DIR_OUT_low_active_DIR_high_level ,
        AB_Phase ,
        BA_Phase ,
        CW_CCW
    }

    public abstract class L132 : ModulesBase , IDisposable
    {
        //標記物件是否已被釋放
        private bool disposed = false;

        protected L132(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            masterCardInitialize();
        }

        ~L132()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            //告訴GC此物件的Finalize方法不再需要呼叫
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            // 當物件已經被解構時，不再執行
            if (disposed)
                return;
            if (isDisposing)
            {
                //在這裡釋放託管資源
                //只在使用者呼叫Dispose方法時執行
            }
            //在這裡釋放非託管資源
            //[底層] cant close
            //CPCI_L132.CS_l132_close((I16)Parameter.CardSwitchNo);
            //標記物件已被釋放
            disposed = true;
        }

        private void masterCardInitialize()
        {
            Thread.Sleep(100);
            I16 rc;
            U16 ringStatus = 0;
            int getRingStatusRt = CMNET_L132.CS_mnet_get_ring_status((U16)Parameter.CardSwitchNo , (U16)Parameter.RingNoOfCard , ref ringStatus);
            if (getRingStatusRt != 0)
            {
                I16 exitsCard = 0;
                CPCI_L132.CS_l132_open(ref exitsCard);
                if (exitsCard == 0)
                {
                    throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("DidntFindL132")));
                }

                Thread.Sleep(30);
                rc = CPCI_L132.CS_l132_dsp_pci_boot((U16)Parameter.CardSwitchNo);
                Thread.Sleep(30);
                rc = CMNET_L132.CS_mnet_initial((U16)Parameter.CardSwitchNo , (U16)Parameter.RingNoOfCard);
                if (rc != 0)
                {
                    throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("InitializeErrorL132")));
                }
            }
            Thread.Sleep(30);
            rc = CMNET_L132.CS_mnet_reset_ring((U16)Parameter.CardSwitchNo , (U16)Parameter.RingNoOfCard);
            Thread.Sleep(30);
            rc = CMNET_L132.CS_mnet_start_ring((U16)Parameter.CardSwitchNo , (U16)Parameter.RingNoOfCard);
            if (rc != 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("StartupErrorL132")));
            }
        }
    }
}