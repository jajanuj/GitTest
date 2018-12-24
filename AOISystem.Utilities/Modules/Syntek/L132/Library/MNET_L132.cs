using System.Runtime.InteropServices;

namespace AOISystem.Utilities.Modules.Syntek.L132.Library
{
    public class CMNET_L132
    {
        /////////////////////system/////////////////////////////////////////////////////
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_initial")]
        public static extern short CS_mnet_initial(ushort CardNo , ushort RingNo);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_close")]
        public static extern short CS_mnet_close(ushort CardNo);

        /////////////////////ring operation//////////////////////////////////////////////
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_set_ring_config")]
        public static extern short CS_mnet_set_ring_config(ushort CardNo , ushort RingNO , ushort BaudRate);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_reset_ring")]
        public static extern short CS_mnet_reset_ring(ushort CardNo , ushort RingNo);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_get_ring_active_table")]
        public static extern short CS_mnet_get_ring_active_table(ushort CardNo , ushort RingNo , ref uint DevTable);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_get_slave_info")]
        public static extern short CS_mnet_get_slave_info(ushort CardNo , ushort RingNo , ushort SlaveIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_start_ring")]
        public static extern short CS_mnet_start_ring(ushort CardNo , ushort RingNo);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_stop_ring")]
        public static extern short CS_mnet_stop_ring(ushort CardNo , ushort RingNo);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_get_ring_status")]
        public static extern short CS_mnet_get_ring_status(ushort CardNo , ushort RingNo , ref ushort Status);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_get_slave_type")]
        public static extern short CS_mnet_get_slave_type(ushort CardNo , ushort RingNo , ushort DeviceIP , ref byte Type);

        /////////////////////io slave operation//////////////////////////////////////////
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_io_output")]
        public static extern short CS_mnet_io_output(ushort CardNo , ushort RingNo , ushort DeviceIP , byte PortNo , byte Val);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_io_input")]
        public static extern short CS_mnet_io_input(ushort CardNo , ushort RingNo , ushort DeviceIP , byte PortNo , ref byte Val);

        /////////////////////Axis slave operation//////////////////////////////////////////
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_initial")]
        public static extern short CS_mnet_m1_initial(ushort CardNo , ushort RingNo , ushort DeviceIP);

        //Pulse Input/Output Configuration
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_pls_outmode")]
        public static extern short CS_mnet_m1_set_pls_outmode(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort pls_outmode);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_pls_iptmode")]
        public static extern short CS_mnet_m1_set_pls_iptmode(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort pls_iptmode , ushort pls_logic);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_feedback_src")]
        public static extern short CS_mnet_m1_set_feedback_src(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort Src);

        //Motion Interface I/O
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_alm")]
        public static extern short CS_mnet_m1_set_alm(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort alm_logic , ushort alm_mode);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_inp")]
        public static extern short CS_mnet_m1_set_inp(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort inp_enable , ushort inp_logic);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_erc")]
        public static extern short CS_mnet_m1_set_erc(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort erc_logic , ushort erc_on_time , ushort erc_off_time);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_ralm")]
        public static extern short CS_mnet_m1_set_ralm(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort ON_OFF);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_sd")]
        public static extern short CS_mnet_m1_set_sd(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort enable , ushort sd_logic , ushort sd_latch , ushort sd_mode);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_pcs")]
        public static extern short CS_mnet_m1_set_pcs(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort PCS_logic);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_svon")]
        public static extern short CS_mnet_m1_set_svon(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort ON_OFF);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_dio_output")]
        public static extern short CS_mnet_m1_dio_output(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort DoNO , ushort ON_OFF);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_dio_input")]
        public static extern short CS_mnet_m1_dio_input(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort DiNO);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_sd_stop")]
        public static extern short CS_mnet_m1_sd_stop(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_emg_stop")]
        public static extern short CS_mnet_m1_emg_stop(ushort CardNo , ushort RingNo , ushort DeviceIP);

        /////////////////IO Monitor
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_io_status")]
        public static extern short CS_mnet_m1_get_io_status(ushort CardNo , ushort RingNo , ushort DeviceIP , ref uint IO_status);

        //Motion Status
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_motion_done")]
        public static extern short CS_mnet_m1_motion_done(ushort CardNo , ushort RingNo , ushort DeviceIP , ref ushort MoSt);

        // Load CFG
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_load_motion_file")]
        public static extern short CS_mnet_m1_load_motion_file(ushort CardNo , ushort RingNo , ushort SlaveIP , string FilePath);

        //Single Axis Speed
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_tmove_speed")]
        public static extern short CS_mnet_m1_set_tmove_speed(ushort CardNo , ushort RingNo , ushort DeviceIP , double StrVel , double MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_smove_speed")]
        public static extern short CS_mnet_m1_set_smove_speed(ushort CardNo , ushort RingNo , ushort DeviceIP , double StrVel , double MaxVel , double Tacc , double Tdec , double SVacc , double SVdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_v_change")]
        public static extern short CS_mnet_m1_v_change(ushort CardNo , ushort RingNo , ushort DeviceIP , double NewVel , double Time);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_fix_speed_range")]
        public static extern short CS_mnet_m1_fix_speed_range(ushort CardNo , ushort RingNo , ushort DeviceIP , double MaxVel);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_unfix_speed_range")]
        public static extern short CS_mnet_m1_unfix_speed_range(ushort CardNo , ushort RingNo , ushort DeviceIP);

        //Single Axis Motion
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_v_move")]
        public static extern short CS_mnet_m1_v_move(ushort CardNo , ushort RingNo , ushort DeviceIP , byte Dir);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_r_move")]
        public static extern short CS_mnet_m1_start_r_move(ushort CardNo , ushort RingNo , ushort DeviceIP , int Distance);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_a_move")]
        public static extern short CS_mnet_m1_start_a_move(ushort CardNo , ushort RingNo , ushort DeviceIP , int Pos);

        //Multi Axis Motion
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_tr_line")]
        public static extern short CS_mnet_m1_start_tr_line(ushort CardNo , ushort RingNo , ref ushort DeviceIP , int DistX , int DistY , double StrVel , double MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_ta_line")]
        public static extern short CS_mnet_m1_start_ta_line(ushort CardNo , ushort RingNo , ref ushort DeviceIP , int PosX , int PosY , double StrVel , double MaxVel , double Tacc , double Tdec);

        //Multi Axis Motion
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_tr_line2")]
        public static extern short CS_mnet_m1_start_tr_line2(ushort CardNo , ref ushort RingNo , ref ushort DeviceIP , int DistX , int DistY , double StrVel , double MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_ta_line2")]
        public static extern short CS_mnet_m1_start_ta_line2(ushort CardNo , ref ushort RingNo , ref ushort DeviceIP , int PosX , int PosY , double StrVel , double MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_sync_tr_line")]
        public static extern short CS_mnet_m1_start_sync_tr_line(ushort CardNo , ref ushort RingNo , ref ushort DeviceIP , int DistX , int DistY , double StrVel , double MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_sync_ta_line")]
        public static extern short CS_mnet_m1_start_sync_ta_line(ushort CardNo , ref ushort RingNo , ref ushort DeviceIP , int PosX , int PosY , double StrVel , double MaxVel , double Tacc , double Tdec);

        //Multi Axis Motion
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_sr_line")]
        public static extern short CS_mnet_m1_start_sr_line(ushort CardNo , ushort RingNo , ref ushort DeviceIP , int DistX , int DistY , double StrVel , double MaxVel , double Tacc , double Tdec , double SVacc , double SVdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_sa_line")]
        public static extern short CS_mnet_m1_start_sa_line(ushort CardNo , ushort RingNo , ref ushort DeviceIP , int PosX , int PosY , double StrVel , double MaxVel , double Tacc , double Tdec , double SVacc , double SVdec);

        //Multi Axis Motion
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_sr_line2")]
        public static extern short CS_mnet_m1_start_sr_line2(ushort CardNo , ref ushort RingNo , ref ushort DeviceIP , int DistX , int DistY , double StrVel , double MaxVel , double Tacc , double Tdec , double SVacc , double SVdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_sa_line2")]
        public static extern short CS_mnet_m1_start_sa_line2(ushort CardNo , ref ushort RingNo , ref ushort DeviceIP , int PosX , int PosY , double StrVel , double MaxVel , double Tacc , double Tdec , double SVacc , double SVdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_sync_sr_line")]
        public static extern short CS_mnet_m1_start_sync_sr_line(ushort CardNo , ref ushort RingNo , ref ushort DeviceIP , int DistX , int DistY , double StrVel , double MaxVel , double Tacc , double Tdec , double SVacc , double SVdec);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_sync_sa_line")]
        public static extern short CS_mnet_m1_start_sync_sa_line(ushort CardNo , ref ushort RingNo , ref ushort DeviceIP , int PosX , int PosY , double StrVel , double MaxVel , double Tacc , double Tdec , double SVacc , double SVdec);

        //Position Compare and Latch
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_comparator_mode")]
        public static extern short CS_mnet_m1_set_comparator_mode(ushort CardNo , ushort RingNo , ushort DeviceIP , short CompNo , short CmpSrc , short CmpMethod , short CmpAction);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_comparator_data")]
        public static extern short CS_mnet_m1_set_comparator_data(ushort CardNo , ushort RingNo , ushort DeviceIP , short CompNo , double Pos);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_trigger_comparator")]
        public static extern short CS_mnet_m1_set_trigger_comparator(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort CmpSrc , ushort CmpMethod);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_trigger_comparator_data")]
        public static extern short CS_mnet_m1_set_trigger_comparator_data(ushort CardNo , ushort RingNo , ushort DeviceIP , double Data);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_comparator_data")]
        public static extern short CS_mnet_m1_get_comparator_data(ushort CardNo , ushort RingNo , ushort DeviceIP , short CompNo , ref double Pos);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_soft_limit")]
        public static extern short CS_mnet_m1_set_soft_limit(ushort CardNo , ushort RingNo , ushort DeviceIP , int PLimit , int MLimit);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_enable_soft_limit")]
        public static extern short CS_mnet_m1_enable_soft_limit(ushort CardNo , ushort RingNo , ushort DeviceIP , byte Action);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_disable_soft_limit")]
        public static extern short CS_mnet_m1_disable_soft_limit(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_steplose_check")]
        public static extern short CS_mnet_m1_steplose_check(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort Tolerance , ushort CmpAction , ushort Enable);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_ltc_logic")]
        public static extern short CS_mnet_m1_set_ltc_logic(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort ltc_logic);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_latch_data")]
        public static extern short CS_mnet_m1_get_latch_data(ushort CardNo , ushort RingNo , ushort DeviceIP , short LatchNo , ref int Pos);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_soft_ltc")]
        public static extern short CS_mnet_m1_start_soft_ltc(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_auto_trigger")]
        public static extern short CS_mnet_m1_set_auto_trigger(ushort CardNo , ushort RingNo , ushort SlaveIP , ushort CmpSrc , ushort CmpMethod , ushort Interval , ushort On_off);

        //===== Compensation =====================
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_backlash")]
        public static extern short CS_mnet_m1_set_backlash(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort Value , ushort Enable , ushort CntSrc);

        /////////////////Counter Operating
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_command")]
        public static extern short CS_mnet_m1_set_command(ushort CardNo , ushort RingNo , ushort DeviceIP , int Cmd);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_command")]
        public static extern short CS_mnet_m1_get_command(ushort CardNo , ushort RingNo , ushort DeviceIP , ref int Cmd);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_reset_command")]
        public static extern short CS_mnet_m1_reset_command(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_position")]
        public static extern short CS_mnet_m1_get_position(ushort CardNo , ushort RingNo , ushort DeviceIP , ref int Pos);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_position")]
        public static extern short CS_mnet_m1_set_position(ushort CardNo , ushort RingNo , ushort DeviceIP , int Pos);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_reset_position")]
        public static extern short CS_mnet_m1_reset_position(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_error_counter")]
        public static extern short CS_mnet_m1_get_error_counter(ushort CardNo , ushort RingNo , ushort DeviceIP , ref int ErrCnt);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_reset_error_counter")]
        public static extern short CS_mnet_m1_reset_error_counter(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_current_speed")]
        public static extern short CS_mnet_m1_get_current_speed(ushort CardNo , ushort RingNo , ushort DeviceIP , ref double speed);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_error_counter")]
        public static extern short CS_mnet_m1_set_error_counter(ushort RingNo , ushort SlaveIP , int ErrCnt);

        ////////////////Home
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_start_home_move")]
        public static extern short CS_mnet_m1_start_home_move(ushort CardNo , ushort RingNo , ushort DeviceIP , byte Dir);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_home_config")]
        public static extern short CS_mnet_m1_set_home_config(ushort CardNo , ushort RingNo , ushort SlaveIP , ushort home_mode , ushort org_logic , ushort ez_logic , ushort ez_count , ushort erc_out);

        //new
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_move_ratio")]
        public static extern short CS_mnet_m1_set_move_ratio(ushort CardNo , ushort RingNo , ushort SlaveIP , double move_ratio);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_cmp_v_change_config")]
        public static extern short CS_mnet_m1_set_cmp_v_change_config(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort CmpMethod , double NewVel , double Time , double Pos);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_set_cmp_v_change")]
        public static extern short CS_mnet_m1_set_cmp_v_change(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort ON_OFF);

        //////////////////Engineering////////////////////////////////////
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_FH")]
        public static extern short CS_mnet_m1_get_FH(ushort CardNo , ushort RingNo , ushort DeviceIP , ref double FH , ref double SoftFH);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_rmv")]
        public static extern short CS_mnet_m1_get_rmv(ushort CardNo , ushort RingNo , ushort DeviceIP , ref double RMV , ref double SoftRMV);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_m1_get_feedback_source")]
        public static extern short CS_mnet_m1_get_feedback_source(ushort CardNo , ushort RingNo , ushort DeviceIP , ref ushort Src);
    }
}