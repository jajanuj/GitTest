using System;
using System.IO;
using System.Runtime.InteropServices;

/*
 Syn-tek reply
 陳先生您好

X86 與 X64 的安裝檔主要有幾個檔案不同需要特別注意

驅動程式 : PCI_M314.sys, PCI_M314_64.sys
DLL : PCI_M314.dll, PCI_M314_X64.dll

X86 的安裝檔會幫您安裝 PCI_M314.sys, PCI_M314.dll
X64 的安裝檔會幫您安裝 PCI_M314_64.sys, PCI_M314_X64.dll

假設使用的作業系統是Win7 64bit
必須使用”PCI_M314_64.sys”這個檔案才能將卡片的驅動程式掛載起來

假設您所編寫的機台程式選擇用x64的方式來編譯
就必須使用PCI_M314_X64.dll

如果您的使用環境同上述的假設
那就安裝X64 安裝檔即可

有些客戶雖然作業系統是64位元的
但機台程式會選擇用x86的方式來編譯
這部分就會比較麻煩
要用到PCI_M314_64.sys 與PCI_M314.dll
而且我們可能要再確認一下是否可以正常使用

不知貴公司是否會有這樣的使用需求

關於安裝路徑的問題
這部分跟安裝檔製作時的設定有關
我會在我們部門會議時提出看是否需要修改

我可以跟您確定
在目錄”C:\program files(86)\SYN-TEK\PCI_M314_X64\”內的檔案都可以直接複製出來使用
因此該路徑的位置並不會造成使用上的問題
 */

namespace AOISystem.Utilities.Modules.Syntek.M314.Library
{
    //INTERRUPT
    public delegate void PM314UserCbk(UInt16 CardNo , UInt16 AxisNo);

    public interface IM314
    {
        short CS_m314_open(ref ushort existcard);

        short CS_m314_close(ushort CardNo);

        short CS_m314_initial_card(ushort CardNo);

        short CS_m314_reset_card(ushort CardNo);

        short CS_m314_check_dsp_running(ushort CardNo , ref ushort running);

        short CS_m314_buffer_enable(ushort CardNo , ushort Enable);

        short CS_m314_get_cardno(ushort seq , ref ushort CardNo);

        short CS_m314_get_version(ushort CardNo , ref ushort ver);

        short CS_m314_config_from_file(ushort CardNo , string file_name);

        short CS_m314_get_buffer_length(ushort CardNo , ushort AxisNo , ref ushort bufferLength);

        short CS_m314_get_cardVer(ushort CardNo , ref ushort ver);

        short CS_m314_get_firmwarever(ushort CardNo , ref ushort ver);

        short CS_m314_set_motion_disable(ushort CardNo , ushort on_off);

        short CS_m314_buffer_clear(ushort CardNo , ushort AxisNo);

        short CS_m314_set_position_cycle(ushort CardNo , ushort value);

        short CS_m314_set_alm(ushort CardNo , ushort AxisNo , short alm_logic , short alm_mode);

        short CS_m314_set_inp(ushort CardNo , ushort AxisNo , short inp_enable , short inp_logic);

        short CS_m314_set_erc(ushort CardNo , ushort AxisNo , short erc_on_time);

        short CS_m314_set_servo(ushort CardNo , ushort AxisNo , short on_off);

        short CS_m314_set_sd(ushort CardNo , ushort AxisNo , short enable , short sd_logic , short sd_mode);

        short CS_m314_set_ralm(ushort CardNo , ushort AxisNo , short on_off);

        short CS_m314_set_erc_on(ushort CardNo , ushort AxisNo , short on_off);

        short CS_m314_set_ell(ushort CardNo , ushort AxisNo , short ell_logic);

        short CS_m314_set_org(ushort CardNo , ushort AxisNo , short org_logic);

        short CS_m314_set_emg(ushort CardNo , ushort AxisNo , short emg_logic);

        short CS_m314_set_ez(ushort CardNo , ushort AxisNo , short ez_logic);

        short CS_m314_set_ltc_logic(ushort CardNo , ushort AxisNo , short ltc_logic);

        short CS_m314_set_ltc_src(ushort CardNo , ushort AxisNo , short ltc_src);

        short CS_m314_get_ltc_src(ushort CardNo , ushort AxisNo , ref short ltc_src);

        short CS_m314_motion_done(ushort CardNo , ushort AxisNo , ref ushort MC_status);

        short CS_m314_get_io_status(ushort CardNo , ushort AxisNo , ref ushort io_sts);

        short CS_m314_start_tr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_ta_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sa_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_tr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_ta_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sa_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_tr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_ta_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sa_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_tr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_ta_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sa_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_tr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_ta_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sa_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_tr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_ta_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sa_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_tr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_ta_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_start_sa_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        short CS_m314_emg_stop(ushort CardNo , ushort AxisNo);

        short CS_m314_emg_stop_erc(ushort CardNo , ushort AxisNo);

        short CS_m314_sd_stop(ushort CardNo , ushort AxisNo , double Tdec);

        short CS_m314_set_home_config(ushort CardNo , ushort AxisNo , short home_mode , short org_logic , short ez_logic , short ez_count);

        short CS_m314_home_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir);

        short CS_m314_disable_home_move(ushort CardNo , ushort AxisNo , double Tdec);

        short CS_m314_set_home_offset_position(ushort CardNo , ushort AxisNo , int pos);

        short CS_m314_set_home_finish_reset(ushort CardNo , ushort AxisNo , ushort enable);

        short CS_m314_get_ltc_position(ushort CardNo , ushort AxisNo , ref double ltc_pos);

        short CS_m314_get_ltc_position_manual_clr(ushort CardNo , ushort AxisNo , ref double ltc_pos , ushort clr);

        short CS_m314_get_current_speed(ushort CardNo , ushort AxisNo , ref double speed);

        short CS_m314_get_position(ushort CardNo , ushort AxisNo , ref double pos);

        short CS_m314_set_position(ushort CardNo , ushort AxisNo , double pos);

        short CS_m314_get_command(ushort CardNo , ushort AxisNo , ref int cmd);

        short CS_m314_set_command(ushort CardNo , ushort AxisNo , int cmd);

        short CS_m314_set_move_ratio(ushort CardNo , ushort AxisNo , double move_ratio);

        short CS_m314_set_electronic_cam(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable);

        short CS_m314_set_gear(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable);

        short CS_m314_get_error_counter(ushort CardNo , ushort AxisNo , ref short error);

        short CS_m314_reset_error_counter(ushort CardNo , ushort AxisNo);

        short CS_m314_get_target_pos(ushort CardNo , ushort AxisNo , ref double pos);

        short CS_m314_p_change(ushort CardNo , ushort AxisNo , int NewPos);

        short CS_m314_position_cmp(ushort CardNo , ushort Comparechannel , int start , int end , uint interval);

        short CS_m314_position_cmp_table(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset);

        short CS_m314_position_cmp_level(ushort CardNo , ushort Comparechannel , int start , int end , uint interval , ushort first_level_on_off);

        short CS_m314_position_cmp_table_level(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset , ushort first_level_on_off);

        short CS_m314_set_trigger_enable(ushort CardNo , ushort Comparechannel , ushort enable);

        short CS_m314_set_trigger_src(ushort CardNo , ushort Comparechannel , ushort CompareSrc , ushort OutputSrc , ushort OupPulseWidth);

        short CS_m314_get_trigger_cnt(ushort CardNo , ushort Comparechannel , ref int trigger_cnt);

        short CS_m314_cmp_oneshut(ushort CardNo , ushort Comparechannel);

        short CS_m314_cmp_gpio(ushort CardNo , ushort Comparechannel , ushort on_off);

        short CS_m314_position_cmp_high_speed(ushort CardNo , ushort Comparechannel , int start , ushort dir , ushort interval , uint trigger_cnt);

        short CS_m314_tv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir);

        short CS_m314_sv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir);

        short CS_m314_v_change(ushort CardNo , ushort AxisNo , int NewVel , double Time);

        short CS_m314_set_pls_outmode(ushort CardNo , ushort AxisNo , short pls_outmode);

        short CS_m314_set_pls_outfastmode(ushort CardNo , ushort AxisNo , short enable);

        short CS_m314_set_pls_outwidth(ushort CardNo , ushort AxisNo , short pls_outwidth);

        short CS_m314_set_pls_iptmode(ushort CardNo , ushort AxisNo , short inputMode , short pls_logic);

        short CS_m314_set_feedback_src(ushort CardNo , ushort AxisNo , short Src);

        short CS_m314_set_a_move_feedback_src(ushort CardNo , ushort AxisNo , short Src);

        short CS_m314_link_interrupt(ushort CardNo , PM314UserCbk callback);

        short CS_m314_set_int_factor(ushort CardNo , ushort AxisNo , ushort int_factor);

        short CS_m314_int_enable(ushort CardNo , ushort AxisNo);

        short CS_m314_int_disable(ushort CardNo , ushort AxisNo);

        short CS_m314_get_int_status(ushort CardNo , ushort AxisNo , ref ushort event_int_status);

        short CS_m314_get_int_count(ushort CardNo , ushort AxisNo , ref ushort count);

        short CS_m314_set_soft_limit(ushort CardNo , ushort AxisNo , int PLimit , int NLimit);

        short CS_m314_enable_soft_limit(ushort CardNo , ushort AxisNo , short Action);

        short CS_m314_disable_soft_limit(ushort CardNo , ushort AxisNo);

        short CS_m314_get_soft_limit_status(ushort CardNo , ushort AxisNo , ref ushort PLimit_sts , ref ushort NLimit_sts);

        short CS_m314_set_dio_output(ushort CardNo , ushort AxisNo , ushort output);

        short CS_m314_set_dio_input(ushort CardNo , ushort AxisNo , ref ushort input);

        short CS_m314_get_dio_input(ushort CardNo , ushort AxisNo , ref ushort input);

        short CS_m314_get_dio_output(ushort CardNo , ushort AxisNo , ref ushort output);

        short CS_m314_dwell_buffer(ushort CardNo , ushort AxisNo , uint TimeCnt);

        short CS_m314_set_interrupt_buffer(ushort CardNo , ushort AxisNo);

        short CS_m314_set_compare_int(ushort CardNo , ushort AxisNo , ref int table , ushort total_cnt , ushort compare_dir);

        short CS_m314_get_compare_count(ushort CardNo , ushort AxisNo , ref ushort index);

        short CS_misc_app_get_circle_endpoint(int Start_X , int Start_Y , int Center_X , int Center_Y , double Angle , ref int end_x , ref int end_y);

        short CS_m314_enable_tracing_axis(ushort CardNo , ref ushort AxisNo , ushort enable);

        short CS_m314_monitor_tracing_axis(ushort CardNo , ushort AxisNo , ushort mon_enable , ushort mon_type , int pos_err);

        short CS_m314_get_tracing_axis_lag(ushort CardNo , ushort AxisNo , ref int pos_lag);

        short CS_m314_set_tracing_passing_timer(ushort CardNo , ushort AxisNo , ushort timer);

        short CS_m314_set_trace_rate(ushort CardNo , ushort AxisNo , ushort numerator , ushort denominator);

        short CS_m314_sync_move(short CardNo);

        short CS_m314_sync_move_config(short CardNo , short AxisNo , short enable);

        short CS_m314_set_motion_dio_sync_start(short CardNo , short AxisNo , ushort enable);

        short CS_m314_set_premove_config(short CardNo , short AxisNo , short enable , short src_axis , short dir , int pos);

        short CS_m314_dda_table(ushort CardNo , ushort AxisNo , ref short dda_table , int count , int offset);

        short CS_m314_dda_enable(ushort CardNo , ref ushort Axis_dda_active , ushort enable);

        short CS_m314_dio_output(ushort CardNo , ushort AxisNo , ushort output);

        short CS_m314_dio_input(ushort CardNo , ushort AxisNo , ref ushort input);

        short CS_m314_dda_from_file(ushort CardNo , ushort AxisNo , string file_name);

        short CS_m314_set_cam_param(ushort CardNo , int xOriginalPos , int xPitch , ref int camTable , int camTableLen);

        short CS_m314_enable_cam_func(ushort CardNo , ushort AxisNo , ushort enable);

        short CS_m314_feedhold_stop(ushort CardNo , ushort AxisNo , double Tdec , ushort On_off);

        short CS_m314_feedhold_enable(ushort CardNo , ushort enable);

        short CS_m314_set_io_record_position_cfg(ushort CardNo , ushort DIchannel , ushort axis_pos , ushort polarity , ushort filter_time , ushort enable);

        short CS_m314_get_io_record_position_cnt(ushort CardNo , ushort DIchannel , ref ushort cnt);

        short CS_m314_get_io_record_position(ushort CardNo , ushort DIchannel , ushort Index , ref int pos);

        short CS_m314_start_multi_axes_move(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_move_xy(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_arc_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_arc2_xy(ushort CardNo , ref ushort AxisNo , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_arc3_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int End_x , int End_y , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_move_xyz(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_heli_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_multi_axes(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_start_v3_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        short CS_m314_set_scurve_rate(ushort CardNo , ushort AxisNo , ushort scurve_rate);

        short CS_m314_set_feedrate_overwrite(ushort CardNo , ushort AxisNo , ushort Mode , int New_Speed , double sec);

        short CS_m314_set_sd_mode(ushort CardNo , ushort AxisNo , ushort mode);

        short CS_m314_set_sd_time(ushort CardNo , ushort AxisNo , double sd_dec);

        short CS_m314_get_DLL_path(byte[] lpFilePath , uint nSize , ref uint nLength);

        short CS_m314_get_DLL_version(byte[] lpBuf , uint nSize , ref uint nLength);

        short CS_m314_ini_from_file(ushort CardNo , string file_name);

        short CS_m314_Monitor_Counter(ushort CardNo , ushort AxisNo , ref ushort DSP_Cnt , ref ushort PC_Cnt);

        short CS_m314_set_backlash(short CardNo , ushort AxisNo , short enable);

        short CS_m314_set_backlash_info(short CardNo , ushort AxisNo , short backlash , ushort accstep , ushort contstep , ushort decstep);

        short CS_m314_set_pitcherr_pitch(ushort CardNo , ushort AxisNo , int pitch);

        short CS_m314_set_pitcherr_org(short CardNo , ushort AxisNo , short Dir , int orgpos);

        short CS_m314_set_pitcherr_enable(short CardNo , ushort AxisNo , ushort on_off);

        short CS_m314_set_pitcherr_mode(short CardNo , ushort AxisNo , ushort Mode);

        short CS_m314_set_pitcherr_table(short CardNo , ushort AxisNo , short Dir , ref int table);

        short CS_m314_set_pitcherr_table2(short CardNo , ushort AxisNo , short Dir , ref int table , int Num);

        short CS_m314_enable_electcam(short CardNo , ushort AxisNo , ushort enable , ushort axisbit , ushort mode);

        short CS_m314_set_monitor_tracing_lag_offset(ushort CardNo , ushort AxisNo , int lag_offset);

        short CS_m314_get_gear(ushort CardNo , ushort AxisNo , ref short numerator , ref short denominator , ref ushort Enable);

        short CS_m314_get_servo_command(ushort CardNo , ushort AxisNo , ref int cmd);
    }

    public sealed class PCI_M314
    {
        private readonly static PCI_M314 instance = new PCI_M314();

        private IM314 library;

        public IM314 Library { get { return library; } }

        // 1. 在Win64 裝 M314_x64 找System32\PCI_M314_X64.dll ->專案平台目標 = any cpu
        // 2. 在Win32 裝 M314_x86 找System32\PCI_M314.dll ->專案平台目標 = any cpu
        // 3. 在Win64 裝 M314_x86 找SysWOW64\PCI_M314.dll ->專案平台目標 = x86
        // 採用 PCI_M314.dll On Win64時，若未設定平台目標為x86 會產生此錯誤-> HRESULT: 0x8007000B
        private PCI_M314()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                //M314 as a 32bit software, even I install x64 version.
                //in platform target x64, system will search  directory "SysWOW64"
                //in platform target x86, system will search directory "System32"
                //but the real path is "C:\Windows\System32\PCI_M314_X64.dll"
                //the "sysnative" alias will redirect SysWOW64 to System32
                if (File.Exists(@"C:\Windows\Sysnative\PCI_M314_X64.dll"))
                {
                    library = new CPCI_M314_64(); // 64 bit
                }
                if (File.Exists(@"C:\Windows\System32\PCI_M314_X64.dll"))
                {
                    library = new CPCI_M314_64(); // 64 bit
                }
            }
            else
            {
                if (File.Exists(@"C:\Windows\System32\PCI_M314.dll"))
                {
                    library = new CPCI_M314_32(); // 32 bit
                }
            }
        }

        public static PCI_M314 GetInstance()
        {
            if (PCI_M314.instance.Library != null)
            {
                //throw new Exception("Please make sure Syntek M314 setup already");
                return instance;
            }
            else
            {
                //return instance;
                throw new Exception("Please make sure program \"Syntek M314\" install already");
            }
        }
    }

    public class CPCI_M314_64 : IM314
    {
        //Initial
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_open")]
        public static extern short _CS_m314_open(ref ushort existcard);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_close")]
        public static extern short _CS_m314_close(ushort CardNo);

        //Card Operate
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_initial_card")]
        public static extern short _CS_m314_initial_card(ushort CardNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_reset_card")]
        public static extern short _CS_m314_reset_card(ushort CardNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_check_dsp_running")]
        public static extern short _CS_m314_check_dsp_running(ushort CardNo , ref ushort running);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_buffer_enable")]
        public static extern short _CS_m314_buffer_enable(ushort CardNo , ushort Enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_cardno")]
        public static extern short _CS_m314_get_cardno(ushort seq , ref ushort CardNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_version")]
        public static extern short _CS_m314_get_version(ushort CardNo , ref ushort ver);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_config_from_file")]
        public static extern short _CS_m314_config_from_file(ushort CardNo , string file_name);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_buffer_length")]
        public static extern short _CS_m314_get_buffer_length(ushort CardNo , ushort AxisNo , ref ushort bufferLength);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_cardVer")]
        public static extern short _CS_m314_get_cardVer(ushort CardNo , ref ushort ver);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_firmwarever")]
        public static extern short _CS_m314_get_firmwarever(ushort CardNo , ref ushort ver);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_motion_disable")]
        public static extern short _CS_m314_set_motion_disable(ushort CardNo , ushort on_off);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_buffer_clear")]
        public static extern short _CS_m314_buffer_clear(ushort CardNo , ushort AxisNo);

        //0:1.25ms,1:2.5ms,2:5ms,3:10ms
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_position_cycle")]
        public static extern short _CS_m314_set_position_cycle(ushort CardNo , ushort value);

        //Motion Interface I/O
        ///////SERVO IO
        //CONFIGURATION
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_alm")]
        public static extern short _CS_m314_set_alm(ushort CardNo , ushort AxisNo , short alm_logic , short alm_mode);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_inp")]
        public static extern short _CS_m314_set_inp(ushort CardNo , ushort AxisNo , short inp_enable , short inp_logic);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_erc")]
        public static extern short _CS_m314_set_erc(ushort CardNo , ushort AxisNo , short erc_on_time);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_servo")]
        public static extern short _CS_m314_set_servo(ushort CardNo , ushort AxisNo , short on_off);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_sd")]
        public static extern short _CS_m314_set_sd(ushort CardNo , ushort AxisNo , short enable , short sd_logic , short sd_mode);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_ralm")]
        public static extern short _CS_m314_set_ralm(ushort CardNo , ushort AxisNo , short on_off);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_erc_on")]
        public static extern short _CS_m314_set_erc_on(ushort CardNo , ushort AxisNo , short on_off);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_ell")]
        public static extern short _CS_m314_set_ell(ushort CardNo , ushort AxisNo , short ell_logic);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_org")]
        public static extern short _CS_m314_set_org(ushort CardNo , ushort AxisNo , short org_logic);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_emg")]
        public static extern short _CS_m314_set_emg(ushort CardNo , ushort AxisNo , short emg_logic);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_ez")]
        public static extern short _CS_m314_set_ez(ushort CardNo , ushort AxisNo , short ez_logic);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_ltc_logic")]
        public static extern short _CS_m314_set_ltc_logic(ushort CardNo , ushort AxisNo , short ltc_logic);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_ltc_src")]
        public static extern short _CS_m314_set_ltc_src(ushort CardNo , ushort AxisNo , short ltc_src);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_ltc_src")]
        public static extern short _CS_m314_get_ltc_src(ushort CardNo , ushort AxisNo , ref short ltc_src);

        //Motion status
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_motion_done")]
        public static extern short _CS_m314_motion_done(ushort CardNo , ushort AxisNo , ref ushort MC_status);

        //Motion IO Monitor
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_io_status")]
        public static extern short _CS_m314_get_io_status(ushort CardNo , ushort AxisNo , ref ushort io_sts);

        //Motion P Command Control
        //{
        //Motion Velocity mode
        //Motion Single Axis
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_tr_move")]
        public static extern short _CS_m314_start_tr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_ta_move")]
        public static extern short _CS_m314_start_ta_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sr_move")]
        public static extern short _CS_m314_start_sr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sa_move")]
        public static extern short _CS_m314_start_sa_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        //Motion Multi Axes
        //2 Axes Linear move
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_tr_move_xy")]
        public static extern short _CS_m314_start_tr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sr_move_xy")]
        public static extern short _CS_m314_start_sr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_ta_move_xy")]
        public static extern short _CS_m314_start_ta_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sa_move_xy")]
        public static extern short _CS_m314_start_sa_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        //2 Axes Circle Move
        //Angle>0:CW..Angle<0:CCW   //Center Point ,Angle
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_tr_arc_xy")]
        public static extern short _CS_m314_start_tr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sr_arc_xy")]
        public static extern short _CS_m314_start_sr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_ta_arc_xy")]
        public static extern short _CS_m314_start_ta_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sa_arc_xy")]
        public static extern short _CS_m314_start_sa_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        //Angle>0:CW..Angle<0:CCW   //End Point ,Angle
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_tr_arc2_xy")]
        public static extern short _CS_m314_start_tr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sr_arc2_xy")]
        public static extern short _CS_m314_start_sr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_ta_arc2_xy")]
        public static extern short _CS_m314_start_ta_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sa_arc2_xy")]
        public static extern short _CS_m314_start_sa_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        //Dir=1:CW..Dir=0:CCW   //End Point ,Center Point
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_tr_arc3_xy")]
        public static extern short _CS_m314_start_tr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sr_arc3_xy")]
        public static extern short _CS_m314_start_sr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_ta_arc3_xy")]
        public static extern short _CS_m314_start_ta_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sa_arc3_xy")]
        public static extern short _CS_m314_start_sa_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        //3 Axes Linear move
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_tr_move_xyz")]
        public static extern short _CS_m314_start_tr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sr_move_xyz")]
        public static extern short _CS_m314_start_sr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_ta_move_xyz")]
        public static extern short _CS_m314_start_ta_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sa_move_xyz")]
        public static extern short _CS_m314_start_sa_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        //3 Axes Heli move Dir=1:CW..Dir=0:CCW
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_tr_heli_xy")]
        public static extern short _CS_m314_start_tr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sr_heli_xy")]
        public static extern short _CS_m314_start_sr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_ta_heli_xy")]
        public static extern short _CS_m314_start_ta_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_sa_heli_xy")]
        public static extern short _CS_m314_start_sa_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        //}	//Motion Multi Axes

        //Motion Stop
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_emg_stop")]
        public static extern short _CS_m314_emg_stop(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_emg_stop_erc")]
        public static extern short _CS_m314_emg_stop_erc(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_sd_stop")]
        public static extern short _CS_m314_sd_stop(ushort CardNo , ushort AxisNo , double Tdec);

        ///////HOMING
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_home_config")]
        public static extern short _CS_m314_set_home_config(ushort CardNo , ushort AxisNo , short home_mode , short org_logic , short ez_logic , short ez_count);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_home_move")]
        public static extern short _CS_m314_home_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_disable_home_move")]
        public static extern short _CS_m314_disable_home_move(ushort CardNo , ushort AxisNo , double Tdec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_home_offset_position")]
        public static extern short _CS_m314_set_home_offset_position(ushort CardNo , ushort AxisNo , int pos);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_home_finish_reset")]
        public static extern short _CS_m314_set_home_finish_reset(ushort CardNo , ushort AxisNo , ushort enable);

        //Motion Counter Operating
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_ltc_position")]
        public static extern short _CS_m314_get_ltc_position(ushort CardNo , ushort AxisNo , ref double ltc_pos);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_ltc_position_manual_clr")]
        public static extern short _CS_m314_get_ltc_position_manual_clr(ushort CardNo , ushort AxisNo , ref double ltc_pos , ushort clr);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_current_speed")]
        public static extern short _CS_m314_get_current_speed(ushort CardNo , ushort AxisNo , ref double speed);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_position")]
        public static extern short _CS_m314_get_position(ushort CardNo , ushort AxisNo , ref double pos);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_position")]
        public static extern short _CS_m314_set_position(ushort CardNo , ushort AxisNo , double pos);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_command")]
        public static extern short _CS_m314_get_command(ushort CardNo , ushort AxisNo , ref int cmd);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_command")]
        public static extern short _CS_m314_set_command(ushort CardNo , ushort AxisNo , int cmd);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_move_ratio")]
        public static extern short _CS_m314_set_move_ratio(ushort CardNo , ushort AxisNo , double move_ratio);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_electronic_cam")]
        public static extern short _CS_m314_set_electronic_cam(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_gear")]
        public static extern short _CS_m314_set_gear(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_error_counter")]
        public static extern short _CS_m314_get_error_counter(ushort CardNo , ushort AxisNo , ref short error);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_reset_error_counter")]
        public static extern short _CS_m314_reset_error_counter(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_target_pos")]
        public static extern short _CS_m314_get_target_pos(ushort CardNo , ushort AxisNo , ref double pos);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_p_change")]
        public static extern short _CS_m314_p_change(ushort CardNo , ushort AxisNo , int NewPos);

        //Position Compare
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_position_cmp")]
        public static extern short _CS_m314_position_cmp(ushort CardNo , ushort Comparechannel , int start , int end , uint interval);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_position_cmp_table")]
        public static extern short _CS_m314_position_cmp_table(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_position_cmp_level")]
        public static extern short _CS_m314_position_cmp_level(ushort CardNo , ushort Comparechannel , int start , int end , uint interval , ushort first_level_on_off);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_position_cmp_table_level")]
        public static extern short _CS_m314_position_cmp_table_level(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset , ushort first_level_on_off);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_trigger_enable")]
        public static extern short _CS_m314_set_trigger_enable(ushort CardNo , ushort Comparechannel , ushort enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_trigger_src")]
        public static extern short _CS_m314_set_trigger_src(ushort CardNo , ushort Comparechannel , ushort CompareSrc , ushort OutputSrc , ushort OupPulseWidth);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_trigger_cnt")]
        public static extern short _CS_m314_get_trigger_cnt(ushort CardNo , ushort Comparechannel , ref int trigger_cnt);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_cmp_oneshut")]
        public static extern short _CS_m314_cmp_oneshut(ushort CardNo , ushort Comparechannel);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_cmp_gpio")]
        public static extern short _CS_m314_cmp_gpio(ushort CardNo , ushort Comparechannel , ushort on_off);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_position_cmp_high_speed")]
        public static extern short _CS_m314_position_cmp_high_speed(ushort CardNo , ushort Comparechannel , int start , ushort dir , ushort interval , uint trigger_cnt);

        ///VELOCITY MOVE
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_tv_move")]
        public static extern short _CS_m314_tv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_sv_move")]
        public static extern short _CS_m314_sv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_v_change")]
        public static extern short _CS_m314_v_change(ushort CardNo , ushort AxisNo , int NewVel , double Time);

        //Pulse Input/Output Configuration
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pls_outmode")]
        public static extern short _CS_m314_set_pls_outmode(ushort CardNo , ushort AxisNo , short pls_outmode);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pls_outfastmode")]
        public static extern short _CS_m314_set_pls_outfastmode(ushort CardNo , ushort AxisNo , short enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pls_outwidth")]
        public static extern short _CS_m314_set_pls_outwidth(ushort CardNo , ushort AxisNo , short pls_outwidth);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pls_iptmode")]
        public static extern short _CS_m314_set_pls_iptmode(ushort CardNo , ushort AxisNo , short inputMode , short pls_logic);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_feedback_src")]
        public static extern short _CS_m314_set_feedback_src(ushort CardNo , ushort AxisNo , short Src);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_a_move_feedback_src")]
        public static extern short _CS_m314_set_a_move_feedback_src(ushort CardNo , ushort AxisNo , short Src);

        ////INTERRUPT
        //public delegate void PM314UserCbk(UInt16 CardNo, UInt16 AxisNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_link_interrupt")]
        public static extern short _CS_m314_link_interrupt(ushort CardNo , PM314UserCbk callback);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_int_factor")]
        public static extern short _CS_m314_set_int_factor(ushort CardNo , ushort AxisNo , ushort int_factor);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_int_enable")]
        public static extern short _CS_m314_int_enable(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_int_disable")]
        public static extern short _CS_m314_int_disable(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_int_status")]
        public static extern short _CS_m314_get_int_status(ushort CardNo , ushort AxisNo , ref ushort event_int_status);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_int_count")]
        public static extern short _CS_m314_get_int_count(ushort CardNo , ushort AxisNo , ref ushort count);

        ////////SOFT LIMIT
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_soft_limit")]
        public static extern short _CS_m314_set_soft_limit(ushort CardNo , ushort AxisNo , int PLimit , int NLimit);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_enable_soft_limit")]
        public static extern short _CS_m314_enable_soft_limit(ushort CardNo , ushort AxisNo , short Action);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_disable_soft_limit")]
        public static extern short _CS_m314_disable_soft_limit(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_soft_limit_status")]
        public static extern short _CS_m314_get_soft_limit_status(ushort CardNo , ushort AxisNo , ref ushort PLimit_sts , ref ushort NLimit_sts);

        ////////DIO
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_dio_output")]
        public static extern short _CS_m314_set_dio_output(ushort CardNo , ushort AxisNo , ushort output);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_dio_input")]
        public static extern short _CS_m314_set_dio_input(ushort CardNo , ushort AxisNo , ref ushort input);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_dio_input")]
        public static extern short _CS_m314_get_dio_input(ushort CardNo , ushort AxisNo , ref ushort input);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_dio_output")]
        public static extern short _CS_m314_get_dio_output(ushort CardNo , ushort AxisNo , ref ushort output);

        //MISC
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_dwell_buffer")]
        public static extern short _CS_m314_dwell_buffer(ushort CardNo , ushort AxisNo , uint TimeCnt);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_interrupt_buffer")]
        public static extern short _CS_m314_set_interrupt_buffer(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_compare_int")]
        public static extern short _CS_m314_set_compare_int(ushort CardNo , ushort AxisNo , ref int table , ushort total_cnt , ushort compare_dir);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_compare_count")]
        public static extern short _CS_m314_get_compare_count(ushort CardNo , ushort AxisNo , ref ushort index);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_misc_app_get_circle_endpoint")]
        public static extern short _CS_misc_app_get_circle_endpoint(int Start_X , int Start_Y , int Center_X , int Center_Y , double Angle , ref int end_x , ref int end_y);

        //Trace
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_enable_tracing_axis")]
        public static extern short _CS_m314_enable_tracing_axis(ushort CardNo , ref ushort AxisNo , ushort enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_monitor_tracing_axis")]
        public static extern short _CS_m314_monitor_tracing_axis(ushort CardNo , ushort AxisNo , ushort mon_enable , ushort mon_type , int pos_err);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_tracing_axis_lag")]
        public static extern short _CS_m314_get_tracing_axis_lag(ushort CardNo , ushort AxisNo , ref int pos_lag);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_tracing_passing_timer")]
        public static extern short _CS_m314_set_tracing_passing_timer(ushort CardNo , ushort AxisNo , ushort timer);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_trace_rate")]
        public static extern short _CS_m314_set_trace_rate(ushort CardNo , ushort AxisNo , ushort numerator , ushort denominator);

        //SYNC Move
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_sync_move")]
        public static extern short _CS_m314_sync_move(short CardNo);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_sync_move_config")]
        public static extern short _CS_m314_sync_move_config(short CardNo , short AxisNo , short enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_motion_dio_sync_start")]
        public static extern short _CS_m314_set_motion_dio_sync_start(short CardNo , short AxisNo , ushort enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_premove_config")]
        public static extern short _CS_m314_set_premove_config(short CardNo , short AxisNo , short enable , short src_axis , short dir , int pos);

        //dda go
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_dda_table")]
        public static extern short _CS_m314_dda_table(ushort CardNo , ushort AxisNo , ref short dda_table , int count , int offset);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_dda_enable")]
        public static extern short _CS_m314_dda_enable(ushort CardNo , ref ushort Axis_dda_active , ushort enable);

        ////////Special DIO for COMWEB
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_dio_output")]
        public static extern short _CS_m314_dio_output(ushort CardNo , ushort AxisNo , ushort output);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_dio_input")]
        public static extern short _CS_m314_dio_input(ushort CardNo , ushort AxisNo , ref ushort input);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_dda_from_file")]
        public static extern short _CS_m314_dda_from_file(ushort CardNo , ushort AxisNo , string file_name);

        //20080918
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_cam_param")]
        public static extern short _CS_m314_set_cam_param(ushort CardNo , int xOriginalPos , int xPitch , ref int camTable , int camTableLen);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_enable_cam_func")]
        public static extern short _CS_m314_enable_cam_func(ushort CardNo , ushort AxisNo , ushort enable);

        //Feed Hold
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_feedhold_stop")]
        public static extern short _CS_m314_feedhold_stop(ushort CardNo , ushort AxisNo , double Tdec , ushort On_off);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_feedhold_enable")]
        public static extern short _CS_m314_feedhold_enable(ushort CardNo , ushort enable);

        //Software IO set record position
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_io_record_position_cfg")]
        public static extern short _CS_m314_set_io_record_position_cfg(ushort CardNo , ushort DIchannel , ushort axis_pos , ushort polarity , ushort filter_time , ushort enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_io_record_position_cnt")]
        public static extern short _CS_m314_get_io_record_position_cnt(ushort CardNo , ushort DIchannel , ref ushort cnt);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_io_record_position")]
        public static extern short _CS_m314_get_io_record_position(ushort CardNo , ushort DIchannel , ushort Index , ref int pos);

        //Muliti Axis Function
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_multi_axes_move")]
        public static extern short _CS_m314_start_multi_axes_move(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_spiral_xy")]
        public static extern short _CS_m314_start_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_spiral2_xy")]
        public static extern short _CS_m314_start_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_move")]
        public static extern short _CS_m314_start_v3_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_move_xy")]
        public static extern short _CS_m314_start_v3_move_xy(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_arc_xy")]
        public static extern short _CS_m314_start_v3_arc_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_arc2_xy")]
        public static extern short _CS_m314_start_v3_arc2_xy(ushort CardNo , ref ushort AxisNo , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_arc3_xy")]
        public static extern short _CS_m314_start_v3_arc3_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int End_x , int End_y , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_move_xyz")]
        public static extern short _CS_m314_start_v3_move_xyz(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_heli_xy")]
        public static extern short _CS_m314_start_v3_heli_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_multi_axes")]
        public static extern short _CS_m314_start_v3_multi_axes(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_spiral_xy")]
        public static extern short _CS_m314_start_v3_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_start_v3_spiral2_xy")]
        public static extern short _CS_m314_start_v3_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_scurve_rate")]
        public static extern short _CS_m314_set_scurve_rate(ushort CardNo , ushort AxisNo , ushort scurve_rate);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_feedrate_overwrite")]
        public static extern short _CS_m314_set_feedrate_overwrite(ushort CardNo , ushort AxisNo , ushort Mode , int New_Speed , double sec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_sd_mode")]
        public static extern short _CS_m314_set_sd_mode(ushort CardNo , ushort AxisNo , ushort mode);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_sd_time")]
        public static extern short _CS_m314_set_sd_time(ushort CardNo , ushort AxisNo , double sd_dec);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_DLL_path")]
        public static extern short _CS_m314_get_DLL_path(byte[] lpFilePath , uint nSize , ref uint nLength);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_DLL_version")]
        public static extern short _CS_m314_get_DLL_version(byte[] lpBuf , uint nSize , ref uint nLength);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_ini_from_file")]
        public static extern short _CS_m314_ini_from_file(ushort CardNo , string file_name);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_Monitor_Counter")]
        public static extern short _CS_m314_Monitor_Counter(ushort CardNo , ushort AxisNo , ref ushort DSP_Cnt , ref ushort PC_Cnt);

        //20130204
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_backlash")]
        public static extern short _CS_m314_set_backlash(short CardNo , ushort AxisNo , short enable);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_backlash_info")]
        public static extern short _CS_m314_set_backlash_info(short CardNo , ushort AxisNo , short backlash , ushort accstep , ushort contstep , ushort decstep);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pitcherr_pitch")]
        public static extern short _CS_m314_set_pitcherr_pitch(ushort CardNo , ushort AxisNo , int pitch);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pitcherr_org")]
        public static extern short _CS_m314_set_pitcherr_org(short CardNo , ushort AxisNo , short Dir , int orgpos);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pitcherr_enable")]
        public static extern short _CS_m314_set_pitcherr_enable(short CardNo , ushort AxisNo , ushort on_off);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pitcherr_mode")]
        public static extern short _CS_m314_set_pitcherr_mode(short CardNo , ushort AxisNo , ushort Mode);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pitcherr_table")]
        public static extern short _CS_m314_set_pitcherr_table(short CardNo , ushort AxisNo , short Dir , ref int table);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_pitcherr_table2")]
        public static extern short _CS_m314_set_pitcherr_table2(short CardNo , ushort AxisNo , short Dir , ref int table , int Num);

        //20130205
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_enable_electcam")]
        public static extern short _CS_m314_enable_electcam(short CardNo , ushort AxisNo , ushort enable , ushort axisbit , ushort mode);

        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_set_monitor_tracing_lag_offset")]
        public static extern short _CS_m314_set_monitor_tracing_lag_offset(ushort CardNo , ushort AxisNo , int lag_offset);

        //20130325
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_gear")]
        public static extern short _CS_m314_get_gear(ushort CardNo , ushort AxisNo , ref short numerator , ref short denominator , ref ushort Enable);

        //20130411
        [DllImport("PCI_M314_X64.dll" , EntryPoint = "_m314_get_servo_command")]
        public static extern short _CS_m314_get_servo_command(ushort CardNo , ushort AxisNo , ref int cmd);

        public short CS_m314_open(ref ushort existcard)
        {
            return _CS_m314_open(ref existcard);
        }

        public short CS_m314_close(ushort CardNo)
        {
            return _CS_m314_close(CardNo);
        }

        public short CS_m314_initial_card(ushort CardNo)
        {
            return _CS_m314_initial_card(CardNo);
        }

        public short CS_m314_reset_card(ushort CardNo)
        {
            return _CS_m314_reset_card(CardNo);
        }

        public short CS_m314_check_dsp_running(ushort CardNo , ref ushort running)
        {
            return _CS_m314_check_dsp_running(CardNo , ref  running);
        }

        public short CS_m314_buffer_enable(ushort CardNo , ushort Enable)
        {
            return _CS_m314_buffer_enable(CardNo , Enable);
        }

        public short CS_m314_get_cardno(ushort seq , ref ushort CardNo)
        {
            return _CS_m314_get_cardno(seq , ref  CardNo);
        }

        public short CS_m314_get_version(ushort CardNo , ref ushort ver)
        {
            return _CS_m314_get_version(CardNo , ref ver);
        }

        public short CS_m314_config_from_file(ushort CardNo , string file_name)
        {
            return _CS_m314_config_from_file(CardNo , file_name);
        }

        public short CS_m314_get_buffer_length(ushort CardNo , ushort AxisNo , ref ushort bufferLength)
        {
            return _CS_m314_get_buffer_length(CardNo , AxisNo , ref  bufferLength);
        }

        public short CS_m314_get_cardVer(ushort CardNo , ref ushort ver)
        {
            return _CS_m314_get_cardVer(CardNo , ref  ver);
        }

        public short CS_m314_get_firmwarever(ushort CardNo , ref ushort ver)
        {
            return _CS_m314_get_firmwarever(CardNo , ref  ver);
        }

        public short CS_m314_set_motion_disable(ushort CardNo , ushort on_off)
        {
            return _CS_m314_set_motion_disable(CardNo , on_off);
        }

        public short CS_m314_buffer_clear(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_buffer_clear(CardNo , AxisNo);
        }

        public short CS_m314_set_position_cycle(ushort CardNo , ushort value)
        {
            return _CS_m314_set_position_cycle(CardNo , value);
        }

        public short CS_m314_set_alm(ushort CardNo , ushort AxisNo , short alm_logic , short alm_mode)
        {
            return _CS_m314_set_alm(CardNo , AxisNo , alm_logic , alm_mode);
        }

        public short CS_m314_set_inp(ushort CardNo , ushort AxisNo , short inp_enable , short inp_logic)
        {
            return _CS_m314_set_inp(CardNo , AxisNo , inp_enable , inp_logic);
        }

        public short CS_m314_set_erc(ushort CardNo , ushort AxisNo , short erc_on_time)
        {
            return _CS_m314_set_erc(CardNo , AxisNo , erc_on_time);
        }

        public short CS_m314_set_servo(ushort CardNo , ushort AxisNo , short on_off)
        {
            return _CS_m314_set_servo(CardNo , AxisNo , on_off);
        }

        public short CS_m314_set_sd(ushort CardNo , ushort AxisNo , short enable , short sd_logic , short sd_mode)
        {
            return _CS_m314_set_sd(CardNo , AxisNo , enable , sd_logic , sd_mode);
        }

        public short CS_m314_set_ralm(ushort CardNo , ushort AxisNo , short on_off)
        {
            return _CS_m314_set_ralm(CardNo , AxisNo , on_off);
        }

        public short CS_m314_set_erc_on(ushort CardNo , ushort AxisNo , short on_off)
        {
            return _CS_m314_set_erc_on(CardNo , AxisNo , on_off);
        }

        public short CS_m314_set_ell(ushort CardNo , ushort AxisNo , short ell_logic)
        {
            return _CS_m314_set_ell(CardNo , AxisNo , ell_logic);
        }

        public short CS_m314_set_org(ushort CardNo , ushort AxisNo , short org_logic)
        {
            return _CS_m314_set_org(CardNo , AxisNo , org_logic);
        }

        public short CS_m314_set_emg(ushort CardNo , ushort AxisNo , short emg_logic)
        {
            return _CS_m314_set_emg(CardNo , AxisNo , emg_logic);
        }

        public short CS_m314_set_ez(ushort CardNo , ushort AxisNo , short ez_logic)
        {
            return _CS_m314_set_ez(CardNo , AxisNo , ez_logic);
        }

        public short CS_m314_set_ltc_logic(ushort CardNo , ushort AxisNo , short ltc_logic)
        {
            return _CS_m314_set_ltc_logic(CardNo , AxisNo , ltc_logic);
        }

        public short CS_m314_set_ltc_src(ushort CardNo , ushort AxisNo , short ltc_src)
        {
            return _CS_m314_set_ltc_src(CardNo , AxisNo , ltc_src);
        }

        public short CS_m314_get_ltc_src(ushort CardNo , ushort AxisNo , ref short ltc_src)
        {
            return _CS_m314_get_ltc_src(CardNo , AxisNo , ref  ltc_src);
        }

        public short CS_m314_motion_done(ushort CardNo , ushort AxisNo , ref ushort MC_status)
        {
            return _CS_m314_motion_done(CardNo , AxisNo , ref  MC_status);
        }

        public short CS_m314_get_io_status(ushort CardNo , ushort AxisNo , ref ushort io_sts)
        {
            return _CS_m314_get_io_status(CardNo , AxisNo , ref  io_sts);
        }

        public short CS_m314_start_tr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_move(CardNo , AxisNo , Dist , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_move(CardNo , AxisNo , Dist , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_move(CardNo , AxisNo , Dist , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_move(CardNo , AxisNo , Dist , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_move_xy(CardNo , ref  AxisArray , DisX , DisY , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_move_xy(CardNo , ref  AxisArray , DisX , DisY , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_move_xy(CardNo , ref  AxisArray , DisX , DisY , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_move_xy(CardNo , ref  AxisArray , DisX , DisY , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_arc_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_arc_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_arc_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_arc_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_arc2_xy(CardNo , ref  AxisArray , End_X , End_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_arc2_xy(CardNo , ref  AxisArray , End_X , End_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_arc2_xy(CardNo , ref  AxisArray , End_X , End_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_arc2_xy(CardNo , ref  AxisArray , End_X , End_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_arc3_xy(CardNo , ref  AxisArray , Center_X , Center_Y , End_X , End_Y , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_arc3_xy(CardNo , ref  AxisArray , Center_X , Center_Y , End_X , End_Y , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_arc3_xy(CardNo , ref  AxisArray , Center_X , Center_Y , End_X , End_Y , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_arc3_xy(CardNo , ref  AxisArray , Center_X , Center_Y , End_X , End_Y , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_move_xyz(CardNo , ref  AxisArray , DisX , DisY , DisZ , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_move_xyz(CardNo , ref  AxisArray , DisX , DisY , DisZ , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_move_xyz(CardNo , ref  AxisArray , DisX , DisY , DisZ , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_move_xyz(CardNo , ref  AxisArray , DisX , DisY , DisZ , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_heli_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_heli_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_heli_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_heli_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_emg_stop(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_emg_stop(CardNo , AxisNo);
        }

        public short CS_m314_emg_stop_erc(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_emg_stop_erc(CardNo , AxisNo);
        }

        public short CS_m314_sd_stop(ushort CardNo , ushort AxisNo , double Tdec)
        {
            return _CS_m314_sd_stop(CardNo , AxisNo , Tdec);
        }

        public short CS_m314_set_home_config(ushort CardNo , ushort AxisNo , short home_mode , short org_logic , short ez_logic , short ez_count)
        {
            return _CS_m314_set_home_config(CardNo , AxisNo , home_mode , org_logic , ez_logic , ez_count);
        }

        public short CS_m314_home_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir)
        {
            return _CS_m314_home_move(CardNo , AxisNo , StrVel , MaxVel , Tacc , Dir);
        }

        public short CS_m314_disable_home_move(ushort CardNo , ushort AxisNo , double Tdec)
        {
            return _CS_m314_disable_home_move(CardNo , AxisNo , Tdec);
        }

        public short CS_m314_set_home_offset_position(ushort CardNo , ushort AxisNo , int pos)
        {
            return _CS_m314_set_home_offset_position(CardNo , AxisNo , pos);
        }

        public short CS_m314_set_home_finish_reset(ushort CardNo , ushort AxisNo , ushort enable)
        {
            return _CS_m314_set_home_finish_reset(CardNo , AxisNo , enable);
        }

        public short CS_m314_get_ltc_position(ushort CardNo , ushort AxisNo , ref double ltc_pos)
        {
            return _CS_m314_get_ltc_position(CardNo , AxisNo , ref  ltc_pos);
        }

        public short CS_m314_get_ltc_position_manual_clr(ushort CardNo , ushort AxisNo , ref double ltc_pos , ushort clr)
        {
            return _CS_m314_get_ltc_position_manual_clr(CardNo , AxisNo , ref  ltc_pos , clr);
        }

        public short CS_m314_get_current_speed(ushort CardNo , ushort AxisNo , ref double speed)
        {
            return _CS_m314_get_current_speed(CardNo , AxisNo , ref  speed);
        }

        public short CS_m314_get_position(ushort CardNo , ushort AxisNo , ref double pos)
        {
            return _CS_m314_get_position(CardNo , AxisNo , ref  pos);
        }

        public short CS_m314_set_position(ushort CardNo , ushort AxisNo , double pos)
        {
            return _CS_m314_set_position(CardNo , AxisNo , pos);
        }

        public short CS_m314_get_command(ushort CardNo , ushort AxisNo , ref int cmd)
        {
            return _CS_m314_get_command(CardNo , AxisNo , ref  cmd);
        }

        public short CS_m314_set_command(ushort CardNo , ushort AxisNo , int cmd)
        {
            return _CS_m314_set_command(CardNo , AxisNo , cmd);
        }

        public short CS_m314_set_move_ratio(ushort CardNo , ushort AxisNo , double move_ratio)
        {
            return _CS_m314_set_move_ratio(CardNo , AxisNo , move_ratio);
        }

        public short CS_m314_set_electronic_cam(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable)
        {
            return _CS_m314_set_electronic_cam(CardNo , AxisNo , numerator , denominator , Enable);
        }

        public short CS_m314_set_gear(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable)
        {
            return _CS_m314_set_gear(CardNo , AxisNo , numerator , denominator , Enable);
        }

        public short CS_m314_get_error_counter(ushort CardNo , ushort AxisNo , ref short error)
        {
            return _CS_m314_get_error_counter(CardNo , AxisNo , ref  error);
        }

        public short CS_m314_reset_error_counter(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_reset_error_counter(CardNo , AxisNo);
        }

        public short CS_m314_get_target_pos(ushort CardNo , ushort AxisNo , ref double pos)
        {
            return _CS_m314_get_target_pos(CardNo , AxisNo , ref  pos);
        }

        public short CS_m314_p_change(ushort CardNo , ushort AxisNo , int NewPos)
        {
            return _CS_m314_p_change(CardNo , AxisNo , NewPos);
        }

        public short CS_m314_position_cmp(ushort CardNo , ushort Comparechannel , int start , int end , uint interval)
        {
            return _CS_m314_position_cmp(CardNo , Comparechannel , start , end , interval);
        }

        public short CS_m314_position_cmp_table(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset)
        {
            return _CS_m314_position_cmp_table(CardNo , Comparechannel , TriggerTable , count , offset);
        }

        public short CS_m314_position_cmp_level(ushort CardNo , ushort Comparechannel , int start , int end , uint interval , ushort first_level_on_off)
        {
            return _CS_m314_position_cmp_level(CardNo , Comparechannel , start , end , interval , first_level_on_off);
        }

        public short CS_m314_position_cmp_table_level(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset , ushort first_level_on_off)
        {
            return _CS_m314_position_cmp_table_level(CardNo , Comparechannel , TriggerTable , count , offset , first_level_on_off);
        }

        public short CS_m314_set_trigger_enable(ushort CardNo , ushort Comparechannel , ushort enable)
        {
            return _CS_m314_set_trigger_enable(CardNo , Comparechannel , enable);
        }

        public short CS_m314_set_trigger_src(ushort CardNo , ushort Comparechannel , ushort CompareSrc , ushort OutputSrc , ushort OupPulseWidth)
        {
            return _CS_m314_set_trigger_src(CardNo , Comparechannel , CompareSrc , OutputSrc , OupPulseWidth);
        }

        public short CS_m314_get_trigger_cnt(ushort CardNo , ushort Comparechannel , ref int trigger_cnt)
        {
            return _CS_m314_get_trigger_cnt(CardNo , Comparechannel , ref  trigger_cnt);
        }

        public short CS_m314_cmp_oneshut(ushort CardNo , ushort Comparechannel)
        {
            return _CS_m314_cmp_oneshut(CardNo , Comparechannel);
        }

        public short CS_m314_cmp_gpio(ushort CardNo , ushort Comparechannel , ushort on_off)
        {
            return _CS_m314_cmp_gpio(CardNo , Comparechannel , on_off);
        }

        public short CS_m314_position_cmp_high_speed(ushort CardNo , ushort Comparechannel , int start , ushort dir , ushort interval , uint trigger_cnt)
        {
            return _CS_m314_position_cmp_high_speed(CardNo , Comparechannel , start , dir , interval , trigger_cnt);
        }

        public short CS_m314_tv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir)
        {
            return _CS_m314_tv_move(CardNo , AxisNo , StrVel , MaxVel , Tacc , Dir);
        }

        public short CS_m314_sv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir)
        {
            return _CS_m314_sv_move(CardNo , AxisNo , StrVel , MaxVel , Tacc , Dir);
        }

        public short CS_m314_v_change(ushort CardNo , ushort AxisNo , int NewVel , double Time)
        {
            return _CS_m314_v_change(CardNo , AxisNo , NewVel , Time);
        }

        public short CS_m314_set_pls_outmode(ushort CardNo , ushort AxisNo , short pls_outmode)
        {
            return _CS_m314_set_pls_outmode(CardNo , AxisNo , pls_outmode);
        }

        public short CS_m314_set_pls_outfastmode(ushort CardNo , ushort AxisNo , short enable)
        {
            return _CS_m314_set_pls_outfastmode(CardNo , AxisNo , enable);
        }

        public short CS_m314_set_pls_outwidth(ushort CardNo , ushort AxisNo , short pls_outwidth)
        {
            return _CS_m314_set_pls_outwidth(CardNo , AxisNo , pls_outwidth);
        }

        public short CS_m314_set_pls_iptmode(ushort CardNo , ushort AxisNo , short inputMode , short pls_logic)
        {
            return _CS_m314_set_pls_iptmode(CardNo , AxisNo , inputMode , pls_logic);
        }

        public short CS_m314_set_feedback_src(ushort CardNo , ushort AxisNo , short Src)
        {
            return _CS_m314_set_feedback_src(CardNo , AxisNo , Src);
        }

        public short CS_m314_set_a_move_feedback_src(ushort CardNo , ushort AxisNo , short Src)
        {
            return _CS_m314_set_a_move_feedback_src(CardNo , AxisNo , Src);
        }

        public short CS_m314_link_interrupt(ushort CardNo , PM314UserCbk callback)
        {
            return _CS_m314_link_interrupt(CardNo , callback);
        }

        public short CS_m314_set_int_factor(ushort CardNo , ushort AxisNo , ushort int_factor)
        {
            return _CS_m314_set_int_factor(CardNo , AxisNo , int_factor);
        }

        public short CS_m314_int_enable(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_int_enable(CardNo , AxisNo);
        }

        public short CS_m314_int_disable(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_int_disable(CardNo , AxisNo);
        }

        public short CS_m314_get_int_status(ushort CardNo , ushort AxisNo , ref ushort event_int_status)
        {
            return _CS_m314_get_int_status(CardNo , AxisNo , ref  event_int_status);
        }

        public short CS_m314_get_int_count(ushort CardNo , ushort AxisNo , ref ushort count)
        {
            return _CS_m314_get_int_count(CardNo , AxisNo , ref  count);
        }

        public short CS_m314_set_soft_limit(ushort CardNo , ushort AxisNo , int PLimit , int NLimit)
        {
            return _CS_m314_set_soft_limit(CardNo , AxisNo , PLimit , NLimit);
        }

        public short CS_m314_enable_soft_limit(ushort CardNo , ushort AxisNo , short Action)
        {
            return _CS_m314_enable_soft_limit(CardNo , AxisNo , Action);
        }

        public short CS_m314_disable_soft_limit(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_disable_soft_limit(CardNo , AxisNo);
        }

        public short CS_m314_get_soft_limit_status(ushort CardNo , ushort AxisNo , ref ushort PLimit_sts , ref ushort NLimit_sts)
        {
            return _CS_m314_get_soft_limit_status(CardNo , AxisNo , ref  PLimit_sts , ref  NLimit_sts);
        }

        public short CS_m314_set_dio_output(ushort CardNo , ushort AxisNo , ushort output)
        {
            return _CS_m314_set_dio_output(CardNo , AxisNo , output);
        }

        public short CS_m314_set_dio_input(ushort CardNo , ushort AxisNo , ref ushort input)
        {
            return _CS_m314_set_dio_input(CardNo , AxisNo , ref  input);
        }

        public short CS_m314_get_dio_input(ushort CardNo , ushort AxisNo , ref ushort input)
        {
            return _CS_m314_get_dio_input(CardNo , AxisNo , ref  input);
        }

        public short CS_m314_get_dio_output(ushort CardNo , ushort AxisNo , ref ushort output)
        {
            return _CS_m314_get_dio_output(CardNo , AxisNo , ref  output);
        }

        public short CS_m314_dwell_buffer(ushort CardNo , ushort AxisNo , uint TimeCnt)
        {
            return _CS_m314_dwell_buffer(CardNo , AxisNo , TimeCnt);
        }

        public short CS_m314_set_interrupt_buffer(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_set_interrupt_buffer(CardNo , AxisNo);
        }

        public short CS_m314_set_compare_int(ushort CardNo , ushort AxisNo , ref int table , ushort total_cnt , ushort compare_dir)
        {
            return _CS_m314_set_compare_int(CardNo , AxisNo , ref  table , total_cnt , compare_dir);
        }

        public short CS_m314_get_compare_count(ushort CardNo , ushort AxisNo , ref ushort index)
        {
            return _CS_m314_get_compare_count(CardNo , AxisNo , ref  index);
        }

        public short CS_misc_app_get_circle_endpoint(int Start_X , int Start_Y , int Center_X , int Center_Y , double Angle , ref int end_x , ref int end_y)
        {
            return _CS_misc_app_get_circle_endpoint(Start_X , Start_Y , Center_X , Center_Y , Angle , ref  end_x , ref  end_y);
        }

        public short CS_m314_enable_tracing_axis(ushort CardNo , ref ushort AxisNo , ushort enable)
        {
            return _CS_m314_enable_tracing_axis(CardNo , ref  AxisNo , enable);
        }

        public short CS_m314_monitor_tracing_axis(ushort CardNo , ushort AxisNo , ushort mon_enable , ushort mon_type , int pos_err)
        {
            return _CS_m314_monitor_tracing_axis(CardNo , AxisNo , mon_enable , mon_type , pos_err);
        }

        public short CS_m314_get_tracing_axis_lag(ushort CardNo , ushort AxisNo , ref int pos_lag)
        {
            return _CS_m314_get_tracing_axis_lag(CardNo , AxisNo , ref  pos_lag);
        }

        public short CS_m314_set_tracing_passing_timer(ushort CardNo , ushort AxisNo , ushort timer)
        {
            return _CS_m314_set_tracing_passing_timer(CardNo , AxisNo , timer);
        }

        public short CS_m314_set_trace_rate(ushort CardNo , ushort AxisNo , ushort numerator , ushort denominator)
        {
            return _CS_m314_set_trace_rate(CardNo , AxisNo , numerator , denominator);
        }

        public short CS_m314_sync_move(short CardNo)
        {
            return _CS_m314_sync_move(CardNo);
        }

        public short CS_m314_sync_move_config(short CardNo , short AxisNo , short enable)
        {
            return _CS_m314_sync_move_config(CardNo , AxisNo , enable);
        }

        public short CS_m314_set_motion_dio_sync_start(short CardNo , short AxisNo , ushort enable)
        {
            return _CS_m314_set_motion_dio_sync_start(CardNo , AxisNo , enable);
        }

        public short CS_m314_set_premove_config(short CardNo , short AxisNo , short enable , short src_axis , short dir , int pos)
        {
            return _CS_m314_set_premove_config(CardNo , AxisNo , enable , src_axis , dir , pos);
        }

        public short CS_m314_dda_table(ushort CardNo , ushort AxisNo , ref short dda_table , int count , int offset)
        {
            return _CS_m314_dda_table(CardNo , AxisNo , ref  dda_table , count , offset);
        }

        public short CS_m314_dda_enable(ushort CardNo , ref ushort Axis_dda_active , ushort enable)
        {
            return _CS_m314_dda_enable(CardNo , ref  Axis_dda_active , enable);
        }

        public short CS_m314_dio_output(ushort CardNo , ushort AxisNo , ushort output)
        {
            return _CS_m314_dio_output(CardNo , AxisNo , output);
        }

        public short CS_m314_dio_input(ushort CardNo , ushort AxisNo , ref ushort input)
        {
            return _CS_m314_dio_input(CardNo , AxisNo , ref  input);
        }

        public short CS_m314_dda_from_file(ushort CardNo , ushort AxisNo , string file_name)
        {
            return _CS_m314_dda_from_file(CardNo , AxisNo , file_name);
        }

        public short CS_m314_set_cam_param(ushort CardNo , int xOriginalPos , int xPitch , ref int camTable , int camTableLen)
        {
            return _CS_m314_set_cam_param(CardNo , xOriginalPos , xPitch , ref  camTable , camTableLen);
        }

        public short CS_m314_enable_cam_func(ushort CardNo , ushort AxisNo , ushort enable)
        {
            return _CS_m314_enable_cam_func(CardNo , AxisNo , enable);
        }

        public short CS_m314_feedhold_stop(ushort CardNo , ushort AxisNo , double Tdec , ushort On_off)
        {
            return _CS_m314_feedhold_stop(CardNo , AxisNo , Tdec , On_off);
        }

        public short CS_m314_feedhold_enable(ushort CardNo , ushort enable)
        {
            return _CS_m314_feedhold_enable(CardNo , enable);
        }

        public short CS_m314_set_io_record_position_cfg(ushort CardNo , ushort DIchannel , ushort axis_pos , ushort polarity , ushort filter_time , ushort enable)
        {
            return _CS_m314_set_io_record_position_cfg(CardNo , DIchannel , axis_pos , polarity , filter_time , enable);
        }

        public short CS_m314_get_io_record_position_cnt(ushort CardNo , ushort DIchannel , ref ushort cnt)
        {
            return _CS_m314_get_io_record_position_cnt(CardNo , DIchannel , ref  cnt);
        }

        public short CS_m314_get_io_record_position(ushort CardNo , ushort DIchannel , ushort Index , ref int pos)
        {
            return _CS_m314_get_io_record_position(CardNo , DIchannel , Index , ref  pos);
        }

        public short CS_m314_start_multi_axes_move(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_multi_axes_move(CardNo , AxisNum , ref  AxisNo , ref  DistArrary , StrVel , MaxVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_spiral_xy(CardNo , ref  AxisNo , Center_X , Center_Y , spiral_interval , spiral_angle , StrVel , MaxVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_spiral2_xy(CardNo , ref  AxisNo , center_x , center_y , end_x , end_y , dir , circlenum , StrVel , MaxVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_move(CardNo , AxisNo , Dist , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_move_xy(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_move_xy(CardNo , ref  AxisNo , DisX , DisY , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_arc_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_arc_xy(CardNo , ref  AxisNo , Center_X , Center_Y , Angle , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_arc2_xy(ushort CardNo , ref ushort AxisNo , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_arc2_xy(CardNo , ref  AxisNo , End_X , End_Y , Angle , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_arc3_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int End_x , int End_y , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_arc3_xy(CardNo , ref  AxisNo , Center_X , Center_Y , End_x , End_y , Dir , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_move_xyz(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_move_xyz(CardNo , ref  AxisNo , DisX , DisY , DisZ , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_heli_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_heli_xy(CardNo , ref  AxisNo , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_multi_axes(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_multi_axes(CardNo , AxisNum , ref  AxisNo , ref  DistArrary , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_spiral_xy(CardNo , ref  AxisNo , Center_X , Center_Y , spiral_interval , spiral_angle , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_spiral2_xy(CardNo , ref  AxisNo , center_x , center_y , end_x , end_y , dir , circlenum , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_set_scurve_rate(ushort CardNo , ushort AxisNo , ushort scurve_rate)
        {
            return _CS_m314_set_scurve_rate(CardNo , AxisNo , scurve_rate);
        }

        public short CS_m314_set_feedrate_overwrite(ushort CardNo , ushort AxisNo , ushort Mode , int New_Speed , double sec)
        {
            return _CS_m314_set_feedrate_overwrite(CardNo , AxisNo , Mode , New_Speed , sec);
        }

        public short CS_m314_set_sd_mode(ushort CardNo , ushort AxisNo , ushort mode)
        {
            return _CS_m314_set_sd_mode(CardNo , AxisNo , mode);
        }

        public short CS_m314_set_sd_time(ushort CardNo , ushort AxisNo , double sd_dec)
        {
            return _CS_m314_set_sd_time(CardNo , AxisNo , sd_dec);
        }

        public short CS_m314_get_DLL_path(byte[] lpFilePath , uint nSize , ref uint nLength)
        {
            return _CS_m314_get_DLL_path(lpFilePath , nSize , ref  nLength);
        }

        public short CS_m314_get_DLL_version(byte[] lpBuf , uint nSize , ref uint nLength)
        {
            return _CS_m314_get_DLL_version(lpBuf , nSize , ref  nLength);
        }

        public short CS_m314_ini_from_file(ushort CardNo , string file_name)
        {
            return _CS_m314_ini_from_file(CardNo , file_name);
        }

        public short CS_m314_Monitor_Counter(ushort CardNo , ushort AxisNo , ref ushort DSP_Cnt , ref ushort PC_Cnt)
        {
            return _CS_m314_Monitor_Counter(CardNo , AxisNo , ref  DSP_Cnt , ref  PC_Cnt);
        }

        public short CS_m314_set_backlash(short CardNo , ushort AxisNo , short enable)
        {
            return _CS_m314_set_backlash(CardNo , AxisNo , enable);
        }

        public short CS_m314_set_backlash_info(short CardNo , ushort AxisNo , short backlash , ushort accstep , ushort contstep , ushort decstep)
        {
            return _CS_m314_set_backlash_info(CardNo , AxisNo , backlash , accstep , contstep , decstep);
        }

        public short CS_m314_set_pitcherr_pitch(ushort CardNo , ushort AxisNo , int pitch)
        {
            return _CS_m314_set_pitcherr_pitch(CardNo , AxisNo , pitch);
        }

        public short CS_m314_set_pitcherr_org(short CardNo , ushort AxisNo , short Dir , int orgpos)
        {
            return _CS_m314_set_pitcherr_org(CardNo , AxisNo , Dir , orgpos);
        }

        public short CS_m314_set_pitcherr_enable(short CardNo , ushort AxisNo , ushort on_off)
        {
            return _CS_m314_set_pitcherr_enable(CardNo , AxisNo , on_off);
        }

        public short CS_m314_set_pitcherr_mode(short CardNo , ushort AxisNo , ushort Mode)
        {
            return _CS_m314_set_pitcherr_mode(CardNo , AxisNo , Mode);
        }

        public short CS_m314_set_pitcherr_table(short CardNo , ushort AxisNo , short Dir , ref int table)
        {
            return _CS_m314_set_pitcherr_table(CardNo , AxisNo , Dir , ref  table);
        }

        public short CS_m314_set_pitcherr_table2(short CardNo , ushort AxisNo , short Dir , ref int table , int Num)
        {
            return _CS_m314_set_pitcherr_table2(CardNo , AxisNo , Dir , ref  table , Num);
        }

        public short CS_m314_enable_electcam(short CardNo , ushort AxisNo , ushort enable , ushort axisbit , ushort mode)
        {
            return _CS_m314_enable_electcam(CardNo , AxisNo , enable , axisbit , mode);
        }

        public short CS_m314_set_monitor_tracing_lag_offset(ushort CardNo , ushort AxisNo , int lag_offset)
        {
            return _CS_m314_set_monitor_tracing_lag_offset(CardNo , AxisNo , lag_offset);
        }

        public short CS_m314_get_gear(ushort CardNo , ushort AxisNo , ref short numerator , ref short denominator , ref ushort Enable)
        {
            return _CS_m314_get_gear(CardNo , AxisNo , ref  numerator , ref  denominator , ref  Enable);
        }

        public short CS_m314_get_servo_command(ushort CardNo , ushort AxisNo , ref int cmd)
        {
            return _CS_m314_get_servo_command(CardNo , AxisNo , ref  cmd);
        }
    }

    public class CPCI_M314_32 : IM314
    {
        //Initial
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_open")]
        public static extern short _CS_m314_open(ref ushort existcard);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_close")]
        public static extern short _CS_m314_close(ushort CardNo);

        //Card Operate
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_initial_card")]
        public static extern short _CS_m314_initial_card(ushort CardNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_reset_card")]
        public static extern short _CS_m314_reset_card(ushort CardNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_check_dsp_running")]
        public static extern short _CS_m314_check_dsp_running(ushort CardNo , ref ushort running);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_buffer_enable")]
        public static extern short _CS_m314_buffer_enable(ushort CardNo , ushort Enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_cardno")]
        public static extern short _CS_m314_get_cardno(ushort seq , ref ushort CardNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_version")]
        public static extern short _CS_m314_get_version(ushort CardNo , ref ushort ver);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_config_from_file")]
        public static extern short _CS_m314_config_from_file(ushort CardNo , string file_name);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_buffer_length")]
        public static extern short _CS_m314_get_buffer_length(ushort CardNo , ushort AxisNo , ref ushort bufferLength);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_cardVer")]
        public static extern short _CS_m314_get_cardVer(ushort CardNo , ref ushort ver);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_firmwarever")]
        public static extern short _CS_m314_get_firmwarever(ushort CardNo , ref ushort ver);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_motion_disable")]
        public static extern short _CS_m314_set_motion_disable(ushort CardNo , ushort on_off);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_buffer_clear")]
        public static extern short _CS_m314_buffer_clear(ushort CardNo , ushort AxisNo);

        //0:1.25ms,1:2.5ms,2:5ms,3:10ms
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_position_cycle")]
        public static extern short _CS_m314_set_position_cycle(ushort CardNo , ushort value);

        //Motion Interface I/O
        ///////SERVO IO
        //CONFIGURATION
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_alm")]
        public static extern short _CS_m314_set_alm(ushort CardNo , ushort AxisNo , short alm_logic , short alm_mode);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_inp")]
        public static extern short _CS_m314_set_inp(ushort CardNo , ushort AxisNo , short inp_enable , short inp_logic);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_erc")]
        public static extern short _CS_m314_set_erc(ushort CardNo , ushort AxisNo , short erc_on_time);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_servo")]
        public static extern short _CS_m314_set_servo(ushort CardNo , ushort AxisNo , short on_off);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_sd")]
        public static extern short _CS_m314_set_sd(ushort CardNo , ushort AxisNo , short enable , short sd_logic , short sd_mode);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_ralm")]
        public static extern short _CS_m314_set_ralm(ushort CardNo , ushort AxisNo , short on_off);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_erc_on")]
        public static extern short _CS_m314_set_erc_on(ushort CardNo , ushort AxisNo , short on_off);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_ell")]
        public static extern short _CS_m314_set_ell(ushort CardNo , ushort AxisNo , short ell_logic);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_org")]
        public static extern short _CS_m314_set_org(ushort CardNo , ushort AxisNo , short org_logic);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_emg")]
        public static extern short _CS_m314_set_emg(ushort CardNo , ushort AxisNo , short emg_logic);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_ez")]
        public static extern short _CS_m314_set_ez(ushort CardNo , ushort AxisNo , short ez_logic);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_ltc_logic")]
        public static extern short _CS_m314_set_ltc_logic(ushort CardNo , ushort AxisNo , short ltc_logic);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_ltc_src")]
        public static extern short _CS_m314_set_ltc_src(ushort CardNo , ushort AxisNo , short ltc_src);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_ltc_src")]
        public static extern short _CS_m314_get_ltc_src(ushort CardNo , ushort AxisNo , ref short ltc_src);

        //Motion status
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_motion_done")]
        public static extern short _CS_m314_motion_done(ushort CardNo , ushort AxisNo , ref ushort MC_status);

        //Motion IO Monitor
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_io_status")]
        public static extern short _CS_m314_get_io_status(ushort CardNo , ushort AxisNo , ref ushort io_sts);

        //Motion P Command Control
        //{
        //Motion Velocity mode
        //Motion Single Axis
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_tr_move")]
        public static extern short _CS_m314_start_tr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_ta_move")]
        public static extern short _CS_m314_start_ta_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sr_move")]
        public static extern short _CS_m314_start_sr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sa_move")]
        public static extern short _CS_m314_start_sa_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec);

        //Motion Multi Axes
        //2 Axes Linear move
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_tr_move_xy")]
        public static extern short _CS_m314_start_tr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sr_move_xy")]
        public static extern short _CS_m314_start_sr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_ta_move_xy")]
        public static extern short _CS_m314_start_ta_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sa_move_xy")]
        public static extern short _CS_m314_start_sa_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec);

        //2 Axes Circle Move
        //Angle>0:CW..Angle<0:CCW   //Center Point ,Angle
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_tr_arc_xy")]
        public static extern short _CS_m314_start_tr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sr_arc_xy")]
        public static extern short _CS_m314_start_sr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_ta_arc_xy")]
        public static extern short _CS_m314_start_ta_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sa_arc_xy")]
        public static extern short _CS_m314_start_sa_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        //Angle>0:CW..Angle<0:CCW   //End Point ,Angle
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_tr_arc2_xy")]
        public static extern short _CS_m314_start_tr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sr_arc2_xy")]
        public static extern short _CS_m314_start_sr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_ta_arc2_xy")]
        public static extern short _CS_m314_start_ta_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sa_arc2_xy")]
        public static extern short _CS_m314_start_sa_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec);

        //Dir=1:CW..Dir=0:CCW   //End Point ,Center Point
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_tr_arc3_xy")]
        public static extern short _CS_m314_start_tr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sr_arc3_xy")]
        public static extern short _CS_m314_start_sr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_ta_arc3_xy")]
        public static extern short _CS_m314_start_ta_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sa_arc3_xy")]
        public static extern short _CS_m314_start_sa_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        //3 Axes Linear move
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_tr_move_xyz")]
        public static extern short _CS_m314_start_tr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sr_move_xyz")]
        public static extern short _CS_m314_start_sr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_ta_move_xyz")]
        public static extern short _CS_m314_start_ta_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sa_move_xyz")]
        public static extern short _CS_m314_start_sa_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec);

        //3 Axes Heli move Dir=1:CW..Dir=0:CCW
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_tr_heli_xy")]
        public static extern short _CS_m314_start_tr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sr_heli_xy")]
        public static extern short _CS_m314_start_sr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_ta_heli_xy")]
        public static extern short _CS_m314_start_ta_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_sa_heli_xy")]
        public static extern short _CS_m314_start_sa_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec);

        //}	//Motion Multi Axes

        //Motion Stop
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_emg_stop")]
        public static extern short _CS_m314_emg_stop(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_emg_stop_erc")]
        public static extern short _CS_m314_emg_stop_erc(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_sd_stop")]
        public static extern short _CS_m314_sd_stop(ushort CardNo , ushort AxisNo , double Tdec);

        ///////HOMING
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_home_config")]
        public static extern short _CS_m314_set_home_config(ushort CardNo , ushort AxisNo , short home_mode , short org_logic , short ez_logic , short ez_count);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_home_move")]
        public static extern short _CS_m314_home_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_disable_home_move")]
        public static extern short _CS_m314_disable_home_move(ushort CardNo , ushort AxisNo , double Tdec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_home_offset_position")]
        public static extern short _CS_m314_set_home_offset_position(ushort CardNo , ushort AxisNo , int pos);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_home_finish_reset")]
        public static extern short _CS_m314_set_home_finish_reset(ushort CardNo , ushort AxisNo , ushort enable);

        //Motion Counter Operating
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_ltc_position")]
        public static extern short _CS_m314_get_ltc_position(ushort CardNo , ushort AxisNo , ref double ltc_pos);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_ltc_position_manual_clr")]
        public static extern short _CS_m314_get_ltc_position_manual_clr(ushort CardNo , ushort AxisNo , ref double ltc_pos , ushort clr);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_current_speed")]
        public static extern short _CS_m314_get_current_speed(ushort CardNo , ushort AxisNo , ref double speed);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_position")]
        public static extern short _CS_m314_get_position(ushort CardNo , ushort AxisNo , ref double pos);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_position")]
        public static extern short _CS_m314_set_position(ushort CardNo , ushort AxisNo , double pos);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_command")]
        public static extern short _CS_m314_get_command(ushort CardNo , ushort AxisNo , ref int cmd);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_command")]
        public static extern short _CS_m314_set_command(ushort CardNo , ushort AxisNo , int cmd);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_move_ratio")]
        public static extern short _CS_m314_set_move_ratio(ushort CardNo , ushort AxisNo , double move_ratio);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_electronic_cam")]
        public static extern short _CS_m314_set_electronic_cam(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_gear")]
        public static extern short _CS_m314_set_gear(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_error_counter")]
        public static extern short _CS_m314_get_error_counter(ushort CardNo , ushort AxisNo , ref short error);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_reset_error_counter")]
        public static extern short _CS_m314_reset_error_counter(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_target_pos")]
        public static extern short _CS_m314_get_target_pos(ushort CardNo , ushort AxisNo , ref double pos);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_p_change")]
        public static extern short _CS_m314_p_change(ushort CardNo , ushort AxisNo , int NewPos);

        //Position Compare
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_position_cmp")]
        public static extern short _CS_m314_position_cmp(ushort CardNo , ushort Comparechannel , int start , int end , uint interval);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_position_cmp_table")]
        public static extern short _CS_m314_position_cmp_table(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_position_cmp_level")]
        public static extern short _CS_m314_position_cmp_level(ushort CardNo , ushort Comparechannel , int start , int end , uint interval , ushort first_level_on_off);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_position_cmp_table_level")]
        public static extern short _CS_m314_position_cmp_table_level(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset , ushort first_level_on_off);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_trigger_enable")]
        public static extern short _CS_m314_set_trigger_enable(ushort CardNo , ushort Comparechannel , ushort enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_trigger_src")]
        public static extern short _CS_m314_set_trigger_src(ushort CardNo , ushort Comparechannel , ushort CompareSrc , ushort OutputSrc , ushort OupPulseWidth);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_trigger_cnt")]
        public static extern short _CS_m314_get_trigger_cnt(ushort CardNo , ushort Comparechannel , ref int trigger_cnt);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_cmp_oneshut")]
        public static extern short _CS_m314_cmp_oneshut(ushort CardNo , ushort Comparechannel);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_cmp_gpio")]
        public static extern short _CS_m314_cmp_gpio(ushort CardNo , ushort Comparechannel , ushort on_off);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_position_cmp_high_speed")]
        public static extern short _CS_m314_position_cmp_high_speed(ushort CardNo , ushort Comparechannel , int start , ushort dir , ushort interval , uint trigger_cnt);

        ///VELOCITY MOVE
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_tv_move")]
        public static extern short _CS_m314_tv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_sv_move")]
        public static extern short _CS_m314_sv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_v_change")]
        public static extern short _CS_m314_v_change(ushort CardNo , ushort AxisNo , int NewVel , double Time);

        //Pulse Input/Output Configuration
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pls_outmode")]
        public static extern short _CS_m314_set_pls_outmode(ushort CardNo , ushort AxisNo , short pls_outmode);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pls_outfastmode")]
        public static extern short _CS_m314_set_pls_outfastmode(ushort CardNo , ushort AxisNo , short enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pls_outwidth")]
        public static extern short _CS_m314_set_pls_outwidth(ushort CardNo , ushort AxisNo , short pls_outwidth);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pls_iptmode")]
        public static extern short _CS_m314_set_pls_iptmode(ushort CardNo , ushort AxisNo , short inputMode , short pls_logic);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_feedback_src")]
        public static extern short _CS_m314_set_feedback_src(ushort CardNo , ushort AxisNo , short Src);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_a_move_feedback_src")]
        public static extern short _CS_m314_set_a_move_feedback_src(ushort CardNo , ushort AxisNo , short Src);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_link_interrupt")]
        public static extern short _CS_m314_link_interrupt(ushort CardNo , PM314UserCbk callback);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_int_factor")]
        public static extern short _CS_m314_set_int_factor(ushort CardNo , ushort AxisNo , ushort int_factor);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_int_enable")]
        public static extern short _CS_m314_int_enable(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_int_disable")]
        public static extern short _CS_m314_int_disable(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_int_status")]
        public static extern short _CS_m314_get_int_status(ushort CardNo , ushort AxisNo , ref ushort event_int_status);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_int_count")]
        public static extern short _CS_m314_get_int_count(ushort CardNo , ushort AxisNo , ref ushort count);

        ////////SOFT LIMIT
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_soft_limit")]
        public static extern short _CS_m314_set_soft_limit(ushort CardNo , ushort AxisNo , int PLimit , int NLimit);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_enable_soft_limit")]
        public static extern short _CS_m314_enable_soft_limit(ushort CardNo , ushort AxisNo , short Action);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_disable_soft_limit")]
        public static extern short _CS_m314_disable_soft_limit(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_soft_limit_status")]
        public static extern short _CS_m314_get_soft_limit_status(ushort CardNo , ushort AxisNo , ref ushort PLimit_sts , ref ushort NLimit_sts);

        ////////DIO
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_dio_output")]
        public static extern short _CS_m314_set_dio_output(ushort CardNo , ushort AxisNo , ushort output);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_dio_input")]
        public static extern short _CS_m314_set_dio_input(ushort CardNo , ushort AxisNo , ref ushort input);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_dio_input")]
        public static extern short _CS_m314_get_dio_input(ushort CardNo , ushort AxisNo , ref ushort input);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_dio_output")]
        public static extern short _CS_m314_get_dio_output(ushort CardNo , ushort AxisNo , ref ushort output);

        //MISC
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_dwell_buffer")]
        public static extern short _CS_m314_dwell_buffer(ushort CardNo , ushort AxisNo , uint TimeCnt);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_interrupt_buffer")]
        public static extern short _CS_m314_set_interrupt_buffer(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_compare_int")]
        public static extern short _CS_m314_set_compare_int(ushort CardNo , ushort AxisNo , ref int table , ushort total_cnt , ushort compare_dir);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_compare_count")]
        public static extern short _CS_m314_get_compare_count(ushort CardNo , ushort AxisNo , ref ushort index);

        [DllImport("PCI_M314.dll" , EntryPoint = "_misc_app_get_circle_endpoint")]
        public static extern short _CS_misc_app_get_circle_endpoint(int Start_X , int Start_Y , int Center_X , int Center_Y , double Angle , ref int end_x , ref int end_y);

        //Trace
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_enable_tracing_axis")]
        public static extern short _CS_m314_enable_tracing_axis(ushort CardNo , ref ushort AxisNo , ushort enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_monitor_tracing_axis")]
        public static extern short _CS_m314_monitor_tracing_axis(ushort CardNo , ushort AxisNo , ushort mon_enable , ushort mon_type , int pos_err);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_tracing_axis_lag")]
        public static extern short _CS_m314_get_tracing_axis_lag(ushort CardNo , ushort AxisNo , ref int pos_lag);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_tracing_passing_timer")]
        public static extern short _CS_m314_set_tracing_passing_timer(ushort CardNo , ushort AxisNo , ushort timer);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_trace_rate")]
        public static extern short _CS_m314_set_trace_rate(ushort CardNo , ushort AxisNo , ushort numerator , ushort denominator);

        //SYNC Move
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_sync_move")]
        public static extern short _CS_m314_sync_move(short CardNo);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_sync_move_config")]
        public static extern short _CS_m314_sync_move_config(short CardNo , short AxisNo , short enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_motion_dio_sync_start")]
        public static extern short _CS_m314_set_motion_dio_sync_start(short CardNo , short AxisNo , ushort enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_premove_config")]
        public static extern short _CS_m314_set_premove_config(short CardNo , short AxisNo , short enable , short src_axis , short dir , int pos);

        //dda go
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_dda_table")]
        public static extern short _CS_m314_dda_table(ushort CardNo , ushort AxisNo , ref short dda_table , int count , int offset);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_dda_enable")]
        public static extern short _CS_m314_dda_enable(ushort CardNo , ref ushort Axis_dda_active , ushort enable);

        ////////Special DIO for COMWEB
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_dio_output")]
        public static extern short _CS_m314_dio_output(ushort CardNo , ushort AxisNo , ushort output);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_dio_input")]
        public static extern short _CS_m314_dio_input(ushort CardNo , ushort AxisNo , ref ushort input);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_dda_from_file")]
        public static extern short _CS_m314_dda_from_file(ushort CardNo , ushort AxisNo , string file_name);

        //20080918
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_cam_param")]
        public static extern short _CS_m314_set_cam_param(ushort CardNo , int xOriginalPos , int xPitch , ref int camTable , int camTableLen);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_enable_cam_func")]
        public static extern short _CS_m314_enable_cam_func(ushort CardNo , ushort AxisNo , ushort enable);

        //Feed Hold
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_feedhold_stop")]
        public static extern short _CS_m314_feedhold_stop(ushort CardNo , ushort AxisNo , double Tdec , ushort On_off);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_feedhold_enable")]
        public static extern short _CS_m314_feedhold_enable(ushort CardNo , ushort enable);

        //Software IO set record position
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_io_record_position_cfg")]
        public static extern short _CS_m314_set_io_record_position_cfg(ushort CardNo , ushort DIchannel , ushort axis_pos , ushort polarity , ushort filter_time , ushort enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_io_record_position_cnt")]
        public static extern short _CS_m314_get_io_record_position_cnt(ushort CardNo , ushort DIchannel , ref ushort cnt);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_io_record_position")]
        public static extern short _CS_m314_get_io_record_position(ushort CardNo , ushort DIchannel , ushort Index , ref int pos);

        //Muliti Axis Function
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_multi_axes_move")]
        public static extern short _CS_m314_start_multi_axes_move(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_spiral_xy")]
        public static extern short _CS_m314_start_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_spiral2_xy")]
        public static extern short _CS_m314_start_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_move")]
        public static extern short _CS_m314_start_v3_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_move_xy")]
        public static extern short _CS_m314_start_v3_move_xy(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_arc_xy")]
        public static extern short _CS_m314_start_v3_arc_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_arc2_xy")]
        public static extern short _CS_m314_start_v3_arc2_xy(ushort CardNo , ref ushort AxisNo , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_arc3_xy")]
        public static extern short _CS_m314_start_v3_arc3_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int End_x , int End_y , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_move_xyz")]
        public static extern short _CS_m314_start_v3_move_xyz(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_heli_xy")]
        public static extern short _CS_m314_start_v3_heli_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_multi_axes")]
        public static extern short _CS_m314_start_v3_multi_axes(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_spiral_xy")]
        public static extern short _CS_m314_start_v3_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_start_v3_spiral2_xy")]
        public static extern short _CS_m314_start_v3_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_scurve_rate")]
        public static extern short _CS_m314_set_scurve_rate(ushort CardNo , ushort AxisNo , ushort scurve_rate);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_feedrate_overwrite")]
        public static extern short _CS_m314_set_feedrate_overwrite(ushort CardNo , ushort AxisNo , ushort Mode , int New_Speed , double sec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_sd_mode")]
        public static extern short _CS_m314_set_sd_mode(ushort CardNo , ushort AxisNo , ushort mode);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_sd_time")]
        public static extern short _CS_m314_set_sd_time(ushort CardNo , ushort AxisNo , double sd_dec);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_DLL_path")]
        public static extern short _CS_m314_get_DLL_path(byte[] lpFilePath , uint nSize , ref uint nLength);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_DLL_version")]
        public static extern short _CS_m314_get_DLL_version(byte[] lpBuf , uint nSize , ref uint nLength);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_ini_from_file")]
        public static extern short _CS_m314_ini_from_file(ushort CardNo , string file_name);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_Monitor_Counter")]
        public static extern short _CS_m314_Monitor_Counter(ushort CardNo , ushort AxisNo , ref ushort DSP_Cnt , ref ushort PC_Cnt);

        //20130204
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_backlash")]
        public static extern short _CS_m314_set_backlash(short CardNo , ushort AxisNo , short enable);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_backlash_info")]
        public static extern short _CS_m314_set_backlash_info(short CardNo , ushort AxisNo , short backlash , ushort accstep , ushort contstep , ushort decstep);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pitcherr_pitch")]
        public static extern short _CS_m314_set_pitcherr_pitch(ushort CardNo , ushort AxisNo , int pitch);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pitcherr_org")]
        public static extern short _CS_m314_set_pitcherr_org(short CardNo , ushort AxisNo , short Dir , int orgpos);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pitcherr_enable")]
        public static extern short _CS_m314_set_pitcherr_enable(short CardNo , ushort AxisNo , ushort on_off);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pitcherr_mode")]
        public static extern short _CS_m314_set_pitcherr_mode(short CardNo , ushort AxisNo , ushort Mode);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pitcherr_table")]
        public static extern short _CS_m314_set_pitcherr_table(short CardNo , ushort AxisNo , short Dir , ref int table);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_pitcherr_table2")]
        public static extern short _CS_m314_set_pitcherr_table2(short CardNo , ushort AxisNo , short Dir , ref int table , int Num);

        //20130205
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_enable_electcam")]
        public static extern short _CS_m314_enable_electcam(short CardNo , ushort AxisNo , ushort enable , ushort axisbit , ushort mode);

        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_set_monitor_tracing_lag_offset")]
        public static extern short _CS_m314_set_monitor_tracing_lag_offset(ushort CardNo , ushort AxisNo , int lag_offset);

        //20130325
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_gear")]
        public static extern short _CS_m314_get_gear(ushort CardNo , ushort AxisNo , ref short numerator , ref short denominator , ref ushort Enable);

        //20130411
        [DllImport("PCI_M314.dll" , EntryPoint = "_m314_get_servo_command")]
        public static extern short _CS_m314_get_servo_command(ushort CardNo , ushort AxisNo , ref int cmd);

        public short CS_m314_open(ref ushort existcard)
        {
            return _CS_m314_open(ref existcard);
        }

        public short CS_m314_close(ushort CardNo)
        {
            return _CS_m314_close(CardNo);
        }

        public short CS_m314_initial_card(ushort CardNo)
        {
            return _CS_m314_initial_card(CardNo);
        }

        public short CS_m314_reset_card(ushort CardNo)
        {
            return _CS_m314_reset_card(CardNo);
        }

        public short CS_m314_check_dsp_running(ushort CardNo , ref ushort running)
        {
            return _CS_m314_check_dsp_running(CardNo , ref  running);
        }

        public short CS_m314_buffer_enable(ushort CardNo , ushort Enable)
        {
            return _CS_m314_buffer_enable(CardNo , Enable);
        }

        public short CS_m314_get_cardno(ushort seq , ref ushort CardNo)
        {
            return _CS_m314_get_cardno(seq , ref  CardNo);
        }

        public short CS_m314_get_version(ushort CardNo , ref ushort ver)
        {
            return _CS_m314_get_version(CardNo , ref ver);
        }

        public short CS_m314_config_from_file(ushort CardNo , string file_name)
        {
            return _CS_m314_config_from_file(CardNo , file_name);
        }

        public short CS_m314_get_buffer_length(ushort CardNo , ushort AxisNo , ref ushort bufferLength)
        {
            return _CS_m314_get_buffer_length(CardNo , AxisNo , ref  bufferLength);
        }

        public short CS_m314_get_cardVer(ushort CardNo , ref ushort ver)
        {
            return _CS_m314_get_cardVer(CardNo , ref  ver);
        }

        public short CS_m314_get_firmwarever(ushort CardNo , ref ushort ver)
        {
            return _CS_m314_get_firmwarever(CardNo , ref  ver);
        }

        public short CS_m314_set_motion_disable(ushort CardNo , ushort on_off)
        {
            return _CS_m314_set_motion_disable(CardNo , on_off);
        }

        public short CS_m314_buffer_clear(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_buffer_clear(CardNo , AxisNo);
        }

        public short CS_m314_set_position_cycle(ushort CardNo , ushort value)
        {
            return _CS_m314_set_position_cycle(CardNo , value);
        }

        public short CS_m314_set_alm(ushort CardNo , ushort AxisNo , short alm_logic , short alm_mode)
        {
            return _CS_m314_set_alm(CardNo , AxisNo , alm_logic , alm_mode);
        }

        public short CS_m314_set_inp(ushort CardNo , ushort AxisNo , short inp_enable , short inp_logic)
        {
            return _CS_m314_set_inp(CardNo , AxisNo , inp_enable , inp_logic);
        }

        public short CS_m314_set_erc(ushort CardNo , ushort AxisNo , short erc_on_time)
        {
            return _CS_m314_set_erc(CardNo , AxisNo , erc_on_time);
        }

        public short CS_m314_set_servo(ushort CardNo , ushort AxisNo , short on_off)
        {
            return _CS_m314_set_servo(CardNo , AxisNo , on_off);
        }

        public short CS_m314_set_sd(ushort CardNo , ushort AxisNo , short enable , short sd_logic , short sd_mode)
        {
            return _CS_m314_set_sd(CardNo , AxisNo , enable , sd_logic , sd_mode);
        }

        public short CS_m314_set_ralm(ushort CardNo , ushort AxisNo , short on_off)
        {
            return _CS_m314_set_ralm(CardNo , AxisNo , on_off);
        }

        public short CS_m314_set_erc_on(ushort CardNo , ushort AxisNo , short on_off)
        {
            return _CS_m314_set_erc_on(CardNo , AxisNo , on_off);
        }

        public short CS_m314_set_ell(ushort CardNo , ushort AxisNo , short ell_logic)
        {
            return _CS_m314_set_ell(CardNo , AxisNo , ell_logic);
        }

        public short CS_m314_set_org(ushort CardNo , ushort AxisNo , short org_logic)
        {
            return _CS_m314_set_org(CardNo , AxisNo , org_logic);
        }

        public short CS_m314_set_emg(ushort CardNo , ushort AxisNo , short emg_logic)
        {
            return _CS_m314_set_emg(CardNo , AxisNo , emg_logic);
        }

        public short CS_m314_set_ez(ushort CardNo , ushort AxisNo , short ez_logic)
        {
            return _CS_m314_set_ez(CardNo , AxisNo , ez_logic);
        }

        public short CS_m314_set_ltc_logic(ushort CardNo , ushort AxisNo , short ltc_logic)
        {
            return _CS_m314_set_ltc_logic(CardNo , AxisNo , ltc_logic);
        }

        public short CS_m314_set_ltc_src(ushort CardNo , ushort AxisNo , short ltc_src)
        {
            return _CS_m314_set_ltc_src(CardNo , AxisNo , ltc_src);
        }

        public short CS_m314_get_ltc_src(ushort CardNo , ushort AxisNo , ref short ltc_src)
        {
            return _CS_m314_get_ltc_src(CardNo , AxisNo , ref  ltc_src);
        }

        public short CS_m314_motion_done(ushort CardNo , ushort AxisNo , ref ushort MC_status)
        {
            return _CS_m314_motion_done(CardNo , AxisNo , ref  MC_status);
        }

        public short CS_m314_get_io_status(ushort CardNo , ushort AxisNo , ref ushort io_sts)
        {
            return _CS_m314_get_io_status(CardNo , AxisNo , ref  io_sts);
        }

        public short CS_m314_start_tr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_move(CardNo , AxisNo , Dist , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_move(CardNo , AxisNo , Dist , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_move(CardNo , AxisNo , Dist , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_move(CardNo , AxisNo , Dist , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_move_xy(CardNo , ref  AxisArray , DisX , DisY , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_move_xy(CardNo , ref  AxisArray , DisX , DisY , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_move_xy(CardNo , ref  AxisArray , DisX , DisY , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_move_xy(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_move_xy(CardNo , ref  AxisArray , DisX , DisY , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_arc_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_arc_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_arc_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_arc_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_arc_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_arc2_xy(CardNo , ref  AxisArray , End_X , End_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_arc2_xy(CardNo , ref  AxisArray , End_X , End_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_arc2_xy(CardNo , ref  AxisArray , End_X , End_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_arc2_xy(ushort CardNo , ref ushort AxisArray , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_arc2_xy(CardNo , ref  AxisArray , End_X , End_Y , Angle , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_arc3_xy(CardNo , ref  AxisArray , Center_X , Center_Y , End_X , End_Y , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_arc3_xy(CardNo , ref  AxisArray , Center_X , Center_Y , End_X , End_Y , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_arc3_xy(CardNo , ref  AxisArray , Center_X , Center_Y , End_X , End_Y , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_arc3_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int End_X , int End_Y , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_arc3_xy(CardNo , ref  AxisArray , Center_X , Center_Y , End_X , End_Y , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_move_xyz(CardNo , ref  AxisArray , DisX , DisY , DisZ , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_move_xyz(CardNo , ref  AxisArray , DisX , DisY , DisZ , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_move_xyz(CardNo , ref  AxisArray , DisX , DisY , DisZ , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_move_xyz(ushort CardNo , ref ushort AxisArray , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_move_xyz(CardNo , ref  AxisArray , DisX , DisY , DisZ , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_tr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_tr_heli_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sr_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sr_heli_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_ta_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_ta_heli_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_start_sa_heli_xy(ushort CardNo , ref ushort AxisArray , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , double Tacc , double Tdec)
        {
            return _CS_m314_start_sa_heli_xy(CardNo , ref  AxisArray , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , Tacc , Tdec);
        }

        public short CS_m314_emg_stop(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_emg_stop(CardNo , AxisNo);
        }

        public short CS_m314_emg_stop_erc(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_emg_stop_erc(CardNo , AxisNo);
        }

        public short CS_m314_sd_stop(ushort CardNo , ushort AxisNo , double Tdec)
        {
            return _CS_m314_sd_stop(CardNo , AxisNo , Tdec);
        }

        public short CS_m314_set_home_config(ushort CardNo , ushort AxisNo , short home_mode , short org_logic , short ez_logic , short ez_count)
        {
            return _CS_m314_set_home_config(CardNo , AxisNo , home_mode , org_logic , ez_logic , ez_count);
        }

        public short CS_m314_home_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir)
        {
            return _CS_m314_home_move(CardNo , AxisNo , StrVel , MaxVel , Tacc , Dir);
        }

        public short CS_m314_disable_home_move(ushort CardNo , ushort AxisNo , double Tdec)
        {
            return _CS_m314_disable_home_move(CardNo , AxisNo , Tdec);
        }

        public short CS_m314_set_home_offset_position(ushort CardNo , ushort AxisNo , int pos)
        {
            return _CS_m314_set_home_offset_position(CardNo , AxisNo , pos);
        }

        public short CS_m314_set_home_finish_reset(ushort CardNo , ushort AxisNo , ushort enable)
        {
            return _CS_m314_set_home_finish_reset(CardNo , AxisNo , enable);
        }

        public short CS_m314_get_ltc_position(ushort CardNo , ushort AxisNo , ref double ltc_pos)
        {
            return _CS_m314_get_ltc_position(CardNo , AxisNo , ref  ltc_pos);
        }

        public short CS_m314_get_ltc_position_manual_clr(ushort CardNo , ushort AxisNo , ref double ltc_pos , ushort clr)
        {
            return _CS_m314_get_ltc_position_manual_clr(CardNo , AxisNo , ref  ltc_pos , clr);
        }

        public short CS_m314_get_current_speed(ushort CardNo , ushort AxisNo , ref double speed)
        {
            return _CS_m314_get_current_speed(CardNo , AxisNo , ref  speed);
        }

        public short CS_m314_get_position(ushort CardNo , ushort AxisNo , ref double pos)
        {
            return _CS_m314_get_position(CardNo , AxisNo , ref  pos);
        }

        public short CS_m314_set_position(ushort CardNo , ushort AxisNo , double pos)
        {
            return _CS_m314_set_position(CardNo , AxisNo , pos);
        }

        public short CS_m314_get_command(ushort CardNo , ushort AxisNo , ref int cmd)
        {
            return _CS_m314_get_command(CardNo , AxisNo , ref  cmd);
        }

        public short CS_m314_set_command(ushort CardNo , ushort AxisNo , int cmd)
        {
            return _CS_m314_set_command(CardNo , AxisNo , cmd);
        }

        public short CS_m314_set_move_ratio(ushort CardNo , ushort AxisNo , double move_ratio)
        {
            return _CS_m314_set_move_ratio(CardNo , AxisNo , move_ratio);
        }

        public short CS_m314_set_electronic_cam(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable)
        {
            return _CS_m314_set_electronic_cam(CardNo , AxisNo , numerator , denominator , Enable);
        }

        public short CS_m314_set_gear(ushort CardNo , ushort AxisNo , short numerator , short denominator , ushort Enable)
        {
            return _CS_m314_set_gear(CardNo , AxisNo , numerator , denominator , Enable);
        }

        public short CS_m314_get_error_counter(ushort CardNo , ushort AxisNo , ref short error)
        {
            return _CS_m314_get_error_counter(CardNo , AxisNo , ref  error);
        }

        public short CS_m314_reset_error_counter(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_reset_error_counter(CardNo , AxisNo);
        }

        public short CS_m314_get_target_pos(ushort CardNo , ushort AxisNo , ref double pos)
        {
            return _CS_m314_get_target_pos(CardNo , AxisNo , ref  pos);
        }

        public short CS_m314_p_change(ushort CardNo , ushort AxisNo , int NewPos)
        {
            return _CS_m314_p_change(CardNo , AxisNo , NewPos);
        }

        public short CS_m314_position_cmp(ushort CardNo , ushort Comparechannel , int start , int end , uint interval)
        {
            return _CS_m314_position_cmp(CardNo , Comparechannel , start , end , interval);
        }

        public short CS_m314_position_cmp_table(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset)
        {
            return _CS_m314_position_cmp_table(CardNo , Comparechannel , TriggerTable , count , offset);
        }

        public short CS_m314_position_cmp_level(ushort CardNo , ushort Comparechannel , int start , int end , uint interval , ushort first_level_on_off)
        {
            return _CS_m314_position_cmp_level(CardNo , Comparechannel , start , end , interval , first_level_on_off);
        }

        public short CS_m314_position_cmp_table_level(ushort CardNo , ushort Comparechannel , int[] TriggerTable , int count , int offset , ushort first_level_on_off)
        {
            return _CS_m314_position_cmp_table_level(CardNo , Comparechannel , TriggerTable , count , offset , first_level_on_off);
        }

        public short CS_m314_set_trigger_enable(ushort CardNo , ushort Comparechannel , ushort enable)
        {
            return _CS_m314_set_trigger_enable(CardNo , Comparechannel , enable);
        }

        public short CS_m314_set_trigger_src(ushort CardNo , ushort Comparechannel , ushort CompareSrc , ushort OutputSrc , ushort OupPulseWidth)
        {
            return _CS_m314_set_trigger_src(CardNo , Comparechannel , CompareSrc , OutputSrc , OupPulseWidth);
        }

        public short CS_m314_get_trigger_cnt(ushort CardNo , ushort Comparechannel , ref int trigger_cnt)
        {
            return _CS_m314_get_trigger_cnt(CardNo , Comparechannel , ref  trigger_cnt);
        }

        public short CS_m314_cmp_oneshut(ushort CardNo , ushort Comparechannel)
        {
            return _CS_m314_cmp_oneshut(CardNo , Comparechannel);
        }

        public short CS_m314_cmp_gpio(ushort CardNo , ushort Comparechannel , ushort on_off)
        {
            return _CS_m314_cmp_gpio(CardNo , Comparechannel , on_off);
        }

        public short CS_m314_position_cmp_high_speed(ushort CardNo , ushort Comparechannel , int start , ushort dir , ushort interval , uint trigger_cnt)
        {
            return _CS_m314_position_cmp_high_speed(CardNo , Comparechannel , start , dir , interval , trigger_cnt);
        }

        public short CS_m314_tv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir)
        {
            return _CS_m314_tv_move(CardNo , AxisNo , StrVel , MaxVel , Tacc , Dir);
        }

        public short CS_m314_sv_move(ushort CardNo , ushort AxisNo , int StrVel , int MaxVel , double Tacc , short Dir)
        {
            return _CS_m314_sv_move(CardNo , AxisNo , StrVel , MaxVel , Tacc , Dir);
        }

        public short CS_m314_v_change(ushort CardNo , ushort AxisNo , int NewVel , double Time)
        {
            return _CS_m314_v_change(CardNo , AxisNo , NewVel , Time);
        }

        public short CS_m314_set_pls_outmode(ushort CardNo , ushort AxisNo , short pls_outmode)
        {
            return _CS_m314_set_pls_outmode(CardNo , AxisNo , pls_outmode);
        }

        public short CS_m314_set_pls_outfastmode(ushort CardNo , ushort AxisNo , short enable)
        {
            return _CS_m314_set_pls_outfastmode(CardNo , AxisNo , enable);
        }

        public short CS_m314_set_pls_outwidth(ushort CardNo , ushort AxisNo , short pls_outwidth)
        {
            return _CS_m314_set_pls_outwidth(CardNo , AxisNo , pls_outwidth);
        }

        public short CS_m314_set_pls_iptmode(ushort CardNo , ushort AxisNo , short inputMode , short pls_logic)
        {
            return _CS_m314_set_pls_iptmode(CardNo , AxisNo , inputMode , pls_logic);
        }

        public short CS_m314_set_feedback_src(ushort CardNo , ushort AxisNo , short Src)
        {
            return _CS_m314_set_feedback_src(CardNo , AxisNo , Src);
        }

        public short CS_m314_set_a_move_feedback_src(ushort CardNo , ushort AxisNo , short Src)
        {
            return _CS_m314_set_a_move_feedback_src(CardNo , AxisNo , Src);
        }

        public short CS_m314_link_interrupt(ushort CardNo , PM314UserCbk callback)
        {
            return _CS_m314_link_interrupt(CardNo , callback);
        }

        public short CS_m314_set_int_factor(ushort CardNo , ushort AxisNo , ushort int_factor)
        {
            return _CS_m314_set_int_factor(CardNo , AxisNo , int_factor);
        }

        public short CS_m314_int_enable(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_int_enable(CardNo , AxisNo);
        }

        public short CS_m314_int_disable(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_int_disable(CardNo , AxisNo);
        }

        public short CS_m314_get_int_status(ushort CardNo , ushort AxisNo , ref ushort event_int_status)
        {
            return _CS_m314_get_int_status(CardNo , AxisNo , ref  event_int_status);
        }

        public short CS_m314_get_int_count(ushort CardNo , ushort AxisNo , ref ushort count)
        {
            return _CS_m314_get_int_count(CardNo , AxisNo , ref  count);
        }

        public short CS_m314_set_soft_limit(ushort CardNo , ushort AxisNo , int PLimit , int NLimit)
        {
            return _CS_m314_set_soft_limit(CardNo , AxisNo , PLimit , NLimit);
        }

        public short CS_m314_enable_soft_limit(ushort CardNo , ushort AxisNo , short Action)
        {
            return _CS_m314_enable_soft_limit(CardNo , AxisNo , Action);
        }

        public short CS_m314_disable_soft_limit(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_disable_soft_limit(CardNo , AxisNo);
        }

        public short CS_m314_get_soft_limit_status(ushort CardNo , ushort AxisNo , ref ushort PLimit_sts , ref ushort NLimit_sts)
        {
            return _CS_m314_get_soft_limit_status(CardNo , AxisNo , ref  PLimit_sts , ref  NLimit_sts);
        }

        public short CS_m314_set_dio_output(ushort CardNo , ushort AxisNo , ushort output)
        {
            return _CS_m314_set_dio_output(CardNo , AxisNo , output);
        }

        public short CS_m314_set_dio_input(ushort CardNo , ushort AxisNo , ref ushort input)
        {
            return _CS_m314_set_dio_input(CardNo , AxisNo , ref  input);
        }

        public short CS_m314_get_dio_input(ushort CardNo , ushort AxisNo , ref ushort input)
        {
            return _CS_m314_get_dio_input(CardNo , AxisNo , ref  input);
        }

        public short CS_m314_get_dio_output(ushort CardNo , ushort AxisNo , ref ushort output)
        {
            return _CS_m314_get_dio_output(CardNo , AxisNo , ref  output);
        }

        public short CS_m314_dwell_buffer(ushort CardNo , ushort AxisNo , uint TimeCnt)
        {
            return _CS_m314_dwell_buffer(CardNo , AxisNo , TimeCnt);
        }

        public short CS_m314_set_interrupt_buffer(ushort CardNo , ushort AxisNo)
        {
            return _CS_m314_set_interrupt_buffer(CardNo , AxisNo);
        }

        public short CS_m314_set_compare_int(ushort CardNo , ushort AxisNo , ref int table , ushort total_cnt , ushort compare_dir)
        {
            return _CS_m314_set_compare_int(CardNo , AxisNo , ref  table , total_cnt , compare_dir);
        }

        public short CS_m314_get_compare_count(ushort CardNo , ushort AxisNo , ref ushort index)
        {
            return _CS_m314_get_compare_count(CardNo , AxisNo , ref  index);
        }

        public short CS_misc_app_get_circle_endpoint(int Start_X , int Start_Y , int Center_X , int Center_Y , double Angle , ref int end_x , ref int end_y)
        {
            return _CS_misc_app_get_circle_endpoint(Start_X , Start_Y , Center_X , Center_Y , Angle , ref  end_x , ref  end_y);
        }

        public short CS_m314_enable_tracing_axis(ushort CardNo , ref ushort AxisNo , ushort enable)
        {
            return _CS_m314_enable_tracing_axis(CardNo , ref  AxisNo , enable);
        }

        public short CS_m314_monitor_tracing_axis(ushort CardNo , ushort AxisNo , ushort mon_enable , ushort mon_type , int pos_err)
        {
            return _CS_m314_monitor_tracing_axis(CardNo , AxisNo , mon_enable , mon_type , pos_err);
        }

        public short CS_m314_get_tracing_axis_lag(ushort CardNo , ushort AxisNo , ref int pos_lag)
        {
            return _CS_m314_get_tracing_axis_lag(CardNo , AxisNo , ref  pos_lag);
        }

        public short CS_m314_set_tracing_passing_timer(ushort CardNo , ushort AxisNo , ushort timer)
        {
            return _CS_m314_set_tracing_passing_timer(CardNo , AxisNo , timer);
        }

        public short CS_m314_set_trace_rate(ushort CardNo , ushort AxisNo , ushort numerator , ushort denominator)
        {
            return _CS_m314_set_trace_rate(CardNo , AxisNo , numerator , denominator);
        }

        public short CS_m314_sync_move(short CardNo)
        {
            return _CS_m314_sync_move(CardNo);
        }

        public short CS_m314_sync_move_config(short CardNo , short AxisNo , short enable)
        {
            return _CS_m314_sync_move_config(CardNo , AxisNo , enable);
        }

        public short CS_m314_set_motion_dio_sync_start(short CardNo , short AxisNo , ushort enable)
        {
            return _CS_m314_set_motion_dio_sync_start(CardNo , AxisNo , enable);
        }

        public short CS_m314_set_premove_config(short CardNo , short AxisNo , short enable , short src_axis , short dir , int pos)
        {
            return _CS_m314_set_premove_config(CardNo , AxisNo , enable , src_axis , dir , pos);
        }

        public short CS_m314_dda_table(ushort CardNo , ushort AxisNo , ref short dda_table , int count , int offset)
        {
            return _CS_m314_dda_table(CardNo , AxisNo , ref  dda_table , count , offset);
        }

        public short CS_m314_dda_enable(ushort CardNo , ref ushort Axis_dda_active , ushort enable)
        {
            return _CS_m314_dda_enable(CardNo , ref  Axis_dda_active , enable);
        }

        public short CS_m314_dio_output(ushort CardNo , ushort AxisNo , ushort output)
        {
            return _CS_m314_dio_output(CardNo , AxisNo , output);
        }

        public short CS_m314_dio_input(ushort CardNo , ushort AxisNo , ref ushort input)
        {
            return _CS_m314_dio_input(CardNo , AxisNo , ref  input);
        }

        public short CS_m314_dda_from_file(ushort CardNo , ushort AxisNo , string file_name)
        {
            return _CS_m314_dda_from_file(CardNo , AxisNo , file_name);
        }

        public short CS_m314_set_cam_param(ushort CardNo , int xOriginalPos , int xPitch , ref int camTable , int camTableLen)
        {
            return _CS_m314_set_cam_param(CardNo , xOriginalPos , xPitch , ref  camTable , camTableLen);
        }

        public short CS_m314_enable_cam_func(ushort CardNo , ushort AxisNo , ushort enable)
        {
            return _CS_m314_enable_cam_func(CardNo , AxisNo , enable);
        }

        public short CS_m314_feedhold_stop(ushort CardNo , ushort AxisNo , double Tdec , ushort On_off)
        {
            return _CS_m314_feedhold_stop(CardNo , AxisNo , Tdec , On_off);
        }

        public short CS_m314_feedhold_enable(ushort CardNo , ushort enable)
        {
            return _CS_m314_feedhold_enable(CardNo , enable);
        }

        public short CS_m314_set_io_record_position_cfg(ushort CardNo , ushort DIchannel , ushort axis_pos , ushort polarity , ushort filter_time , ushort enable)
        {
            return _CS_m314_set_io_record_position_cfg(CardNo , DIchannel , axis_pos , polarity , filter_time , enable);
        }

        public short CS_m314_get_io_record_position_cnt(ushort CardNo , ushort DIchannel , ref ushort cnt)
        {
            return _CS_m314_get_io_record_position_cnt(CardNo , DIchannel , ref  cnt);
        }

        public short CS_m314_get_io_record_position(ushort CardNo , ushort DIchannel , ushort Index , ref int pos)
        {
            return _CS_m314_get_io_record_position(CardNo , DIchannel , Index , ref  pos);
        }

        public short CS_m314_start_multi_axes_move(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_multi_axes_move(CardNo , AxisNum , ref  AxisNo , ref  DistArrary , StrVel , MaxVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_spiral_xy(CardNo , ref  AxisNo , Center_X , Center_Y , spiral_interval , spiral_angle , StrVel , MaxVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_spiral2_xy(CardNo , ref  AxisNo , center_x , center_y , end_x , end_y , dir , circlenum , StrVel , MaxVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_move(ushort CardNo , ushort AxisNo , int Dist , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_move(CardNo , AxisNo , Dist , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_move_xy(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_move_xy(CardNo , ref  AxisNo , DisX , DisY , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_arc_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_arc_xy(CardNo , ref  AxisNo , Center_X , Center_Y , Angle , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_arc2_xy(ushort CardNo , ref ushort AxisNo , int End_X , int End_Y , double Angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_arc2_xy(CardNo , ref  AxisNo , End_X , End_Y , Angle , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_arc3_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int End_x , int End_y , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_arc3_xy(CardNo , ref  AxisNo , Center_X , Center_Y , End_x , End_y , Dir , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_move_xyz(ushort CardNo , ref ushort AxisNo , int DisX , int DisY , int DisZ , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_move_xyz(CardNo , ref  AxisNo , DisX , DisY , DisZ , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_heli_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int Depth , int Pitch , short Dir , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_heli_xy(CardNo , ref  AxisNo , Center_X , Center_Y , Depth , Pitch , Dir , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_multi_axes(ushort CardNo , ushort AxisNum , ref ushort AxisNo , ref int DistArrary , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_multi_axes(CardNo , AxisNum , ref  AxisNo , ref  DistArrary , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_spiral_xy(ushort CardNo , ref ushort AxisNo , int Center_X , int Center_Y , int spiral_interval , uint spiral_angle , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_spiral_xy(CardNo , ref  AxisNo , Center_X , Center_Y , spiral_interval , spiral_angle , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_start_v3_spiral2_xy(ushort CardNo , ref ushort AxisNo , int center_x , int center_y , int end_x , int end_y , ushort dir , ushort circlenum , int StrVel , int MaxVel , int EndVel , double Tacc , double Tdec , ushort m_curve , ushort m_r_a)
        {
            return _CS_m314_start_v3_spiral2_xy(CardNo , ref  AxisNo , center_x , center_y , end_x , end_y , dir , circlenum , StrVel , MaxVel , EndVel , Tacc , Tdec , m_curve , m_r_a);
        }

        public short CS_m314_set_scurve_rate(ushort CardNo , ushort AxisNo , ushort scurve_rate)
        {
            return _CS_m314_set_scurve_rate(CardNo , AxisNo , scurve_rate);
        }

        public short CS_m314_set_feedrate_overwrite(ushort CardNo , ushort AxisNo , ushort Mode , int New_Speed , double sec)
        {
            return _CS_m314_set_feedrate_overwrite(CardNo , AxisNo , Mode , New_Speed , sec);
        }

        public short CS_m314_set_sd_mode(ushort CardNo , ushort AxisNo , ushort mode)
        {
            return _CS_m314_set_sd_mode(CardNo , AxisNo , mode);
        }

        public short CS_m314_set_sd_time(ushort CardNo , ushort AxisNo , double sd_dec)
        {
            return _CS_m314_set_sd_time(CardNo , AxisNo , sd_dec);
        }

        public short CS_m314_get_DLL_path(byte[] lpFilePath , uint nSize , ref uint nLength)
        {
            return _CS_m314_get_DLL_path(lpFilePath , nSize , ref  nLength);
        }

        public short CS_m314_get_DLL_version(byte[] lpBuf , uint nSize , ref uint nLength)
        {
            return _CS_m314_get_DLL_version(lpBuf , nSize , ref  nLength);
        }

        public short CS_m314_ini_from_file(ushort CardNo , string file_name)
        {
            return _CS_m314_ini_from_file(CardNo , file_name);
        }

        public short CS_m314_Monitor_Counter(ushort CardNo , ushort AxisNo , ref ushort DSP_Cnt , ref ushort PC_Cnt)
        {
            return _CS_m314_Monitor_Counter(CardNo , AxisNo , ref  DSP_Cnt , ref  PC_Cnt);
        }

        public short CS_m314_set_backlash(short CardNo , ushort AxisNo , short enable)
        {
            return _CS_m314_set_backlash(CardNo , AxisNo , enable);
        }

        public short CS_m314_set_backlash_info(short CardNo , ushort AxisNo , short backlash , ushort accstep , ushort contstep , ushort decstep)
        {
            return _CS_m314_set_backlash_info(CardNo , AxisNo , backlash , accstep , contstep , decstep);
        }

        public short CS_m314_set_pitcherr_pitch(ushort CardNo , ushort AxisNo , int pitch)
        {
            return _CS_m314_set_pitcherr_pitch(CardNo , AxisNo , pitch);
        }

        public short CS_m314_set_pitcherr_org(short CardNo , ushort AxisNo , short Dir , int orgpos)
        {
            return _CS_m314_set_pitcherr_org(CardNo , AxisNo , Dir , orgpos);
        }

        public short CS_m314_set_pitcherr_enable(short CardNo , ushort AxisNo , ushort on_off)
        {
            return _CS_m314_set_pitcherr_enable(CardNo , AxisNo , on_off);
        }

        public short CS_m314_set_pitcherr_mode(short CardNo , ushort AxisNo , ushort Mode)
        {
            return _CS_m314_set_pitcherr_mode(CardNo , AxisNo , Mode);
        }

        public short CS_m314_set_pitcherr_table(short CardNo , ushort AxisNo , short Dir , ref int table)
        {
            return _CS_m314_set_pitcherr_table(CardNo , AxisNo , Dir , ref  table);
        }

        public short CS_m314_set_pitcherr_table2(short CardNo , ushort AxisNo , short Dir , ref int table , int Num)
        {
            return _CS_m314_set_pitcherr_table2(CardNo , AxisNo , Dir , ref  table , Num);
        }

        public short CS_m314_enable_electcam(short CardNo , ushort AxisNo , ushort enable , ushort axisbit , ushort mode)
        {
            return _CS_m314_enable_electcam(CardNo , AxisNo , enable , axisbit , mode);
        }

        public short CS_m314_set_monitor_tracing_lag_offset(ushort CardNo , ushort AxisNo , int lag_offset)
        {
            return _CS_m314_set_monitor_tracing_lag_offset(CardNo , AxisNo , lag_offset);
        }

        public short CS_m314_get_gear(ushort CardNo , ushort AxisNo , ref short numerator , ref short denominator , ref ushort Enable)
        {
            return _CS_m314_get_gear(CardNo , AxisNo , ref  numerator , ref  denominator , ref  Enable);
        }

        public short CS_m314_get_servo_command(ushort CardNo , ushort AxisNo , ref int cmd)
        {
            return _CS_m314_get_servo_command(CardNo , AxisNo , ref  cmd);
        }
    }

    /*
    public class CPCI_M314_SysWOW64 : IM314
    {
        //Initial
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_open")]
        public static extern short _CS_m314_open(ref ushort existcard);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_close")]
        public static extern short _CS_m314_close(ushort CardNo);

        //Card Operate
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_initial_card")]
        public static extern short _CS_m314_initial_card(ushort CardNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_reset_card")]
        public static extern short _CS_m314_reset_card(ushort CardNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_check_dsp_running")]
        public static extern short _CS_m314_check_dsp_running(ushort CardNo, ref ushort running);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_buffer_enable")]
        public static extern short _CS_m314_buffer_enable(ushort CardNo, ushort Enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_cardno")]
        public static extern short _CS_m314_get_cardno(ushort seq, ref ushort CardNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_version")]
        public static extern short _CS_m314_get_version(ushort CardNo, ref ushort ver);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_config_from_file")]
        public static extern short _CS_m314_config_from_file(ushort CardNo, string file_name);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_buffer_length")]
        public static extern short _CS_m314_get_buffer_length(ushort CardNo, ushort AxisNo, ref ushort bufferLength);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_cardVer")]
        public static extern short _CS_m314_get_cardVer(ushort CardNo, ref ushort ver);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_firmwarever")]
        public static extern short _CS_m314_get_firmwarever(ushort CardNo, ref ushort ver);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_motion_disable")]
        public static extern short _CS_m314_set_motion_disable(ushort CardNo, ushort on_off);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_buffer_clear")]
        public static extern short _CS_m314_buffer_clear(ushort CardNo, ushort AxisNo);

        //0:1.25ms,1:2.5ms,2:5ms,3:10ms
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_position_cycle")]
        public static extern short _CS_m314_set_position_cycle(ushort CardNo, ushort value);

        //Motion Interface I/O
        ///////SERVO IO
        //CONFIGURATION
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_alm")]
        public static extern short _CS_m314_set_alm(ushort CardNo, ushort AxisNo, short alm_logic, short alm_mode);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_inp")]
        public static extern short _CS_m314_set_inp(ushort CardNo, ushort AxisNo, short inp_enable, short inp_logic);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_erc")]
        public static extern short _CS_m314_set_erc(ushort CardNo, ushort AxisNo, short erc_on_time);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_servo")]
        public static extern short _CS_m314_set_servo(ushort CardNo, ushort AxisNo, short on_off);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_sd")]
        public static extern short _CS_m314_set_sd(ushort CardNo, ushort AxisNo, short enable, short sd_logic, short sd_mode);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_ralm")]
        public static extern short _CS_m314_set_ralm(ushort CardNo, ushort AxisNo, short on_off);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_erc_on")]
        public static extern short _CS_m314_set_erc_on(ushort CardNo, ushort AxisNo, short on_off);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_ell")]
        public static extern short _CS_m314_set_ell(ushort CardNo, ushort AxisNo, short ell_logic);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_org")]
        public static extern short _CS_m314_set_org(ushort CardNo, ushort AxisNo, short org_logic);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_emg")]
        public static extern short _CS_m314_set_emg(ushort CardNo, ushort AxisNo, short emg_logic);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_ez")]
        public static extern short _CS_m314_set_ez(ushort CardNo, ushort AxisNo, short ez_logic);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_ltc_logic")]
        public static extern short _CS_m314_set_ltc_logic(ushort CardNo, ushort AxisNo, short ltc_logic);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_ltc_src")]
        public static extern short _CS_m314_set_ltc_src(ushort CardNo, ushort AxisNo, short ltc_src);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_ltc_src")]
        public static extern short _CS_m314_get_ltc_src(ushort CardNo, ushort AxisNo, ref short ltc_src);

        //Motion status
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_motion_done")]
        public static extern short _CS_m314_motion_done(ushort CardNo, ushort AxisNo, ref ushort MC_status);

        //Motion IO Monitor
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_io_status")]
        public static extern short _CS_m314_get_io_status(ushort CardNo, ushort AxisNo, ref ushort io_sts);

        //Motion P Command Control
        //{
        //Motion Velocity mode
        //Motion Single Axis
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_tr_move")]
        public static extern short _CS_m314_start_tr_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_ta_move")]
        public static extern short _CS_m314_start_ta_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sr_move")]
        public static extern short _CS_m314_start_sr_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sa_move")]
        public static extern short _CS_m314_start_sa_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, double Tacc, double Tdec);

        //Motion Multi Axes
        //2 Axes Linear move
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_tr_move_xy")]
        public static extern short _CS_m314_start_tr_move_xy(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sr_move_xy")]
        public static extern short _CS_m314_start_sr_move_xy(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_ta_move_xy")]
        public static extern short _CS_m314_start_ta_move_xy(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sa_move_xy")]
        public static extern short _CS_m314_start_sa_move_xy(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int StrVel, int MaxVel, double Tacc, double Tdec);

        //2 Axes Circle Move
        //Angle>0:CW..Angle<0:CCW   //Center Point ,Angle
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_tr_arc_xy")]
        public static extern short _CS_m314_start_tr_arc_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sr_arc_xy")]
        public static extern short _CS_m314_start_sr_arc_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_ta_arc_xy")]
        public static extern short _CS_m314_start_ta_arc_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sa_arc_xy")]
        public static extern short _CS_m314_start_sa_arc_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec);

        //Angle>0:CW..Angle<0:CCW   //End Point ,Angle
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_tr_arc2_xy")]
        public static extern short _CS_m314_start_tr_arc2_xy(ushort CardNo, ref ushort AxisArray, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sr_arc2_xy")]
        public static extern short _CS_m314_start_sr_arc2_xy(ushort CardNo, ref ushort AxisArray, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_ta_arc2_xy")]
        public static extern short _CS_m314_start_ta_arc2_xy(ushort CardNo, ref ushort AxisArray, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sa_arc2_xy")]
        public static extern short _CS_m314_start_sa_arc2_xy(ushort CardNo, ref ushort AxisArray, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec);

        //Dir=1:CW..Dir=0:CCW   //End Point ,Center Point
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_tr_arc3_xy")]
        public static extern short _CS_m314_start_tr_arc3_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int End_X, int End_Y, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sr_arc3_xy")]
        public static extern short _CS_m314_start_sr_arc3_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int End_X, int End_Y, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_ta_arc3_xy")]
        public static extern short _CS_m314_start_ta_arc3_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int End_X, int End_Y, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sa_arc3_xy")]
        public static extern short _CS_m314_start_sa_arc3_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int End_X, int End_Y, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec);

        //3 Axes Linear move
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_tr_move_xyz")]
        public static extern short _CS_m314_start_tr_move_xyz(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sr_move_xyz")]
        public static extern short _CS_m314_start_sr_move_xyz(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_ta_move_xyz")]
        public static extern short _CS_m314_start_ta_move_xyz(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sa_move_xyz")]
        public static extern short _CS_m314_start_sa_move_xyz(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, double Tacc, double Tdec);

        //3 Axes Heli move Dir=1:CW..Dir=0:CCW
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_tr_heli_xy")]
        public static extern short _CS_m314_start_tr_heli_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sr_heli_xy")]
        public static extern short _CS_m314_start_sr_heli_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_ta_heli_xy")]
        public static extern short _CS_m314_start_ta_heli_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_sa_heli_xy")]
        public static extern short _CS_m314_start_sa_heli_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec);

        //}	//Motion Multi Axes

        //Motion Stop
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_emg_stop")]
        public static extern short _CS_m314_emg_stop(ushort CardNo, ushort AxisNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_emg_stop_erc")]
        public static extern short _CS_m314_emg_stop_erc(ushort CardNo, ushort AxisNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_sd_stop")]
        public static extern short _CS_m314_sd_stop(ushort CardNo, ushort AxisNo, double Tdec);

        ///////HOMING
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_home_config")]
        public static extern short _CS_m314_set_home_config(ushort CardNo, ushort AxisNo, short home_mode, short org_logic, short ez_logic, short ez_count);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_home_move")]
        public static extern short _CS_m314_home_move(ushort CardNo, ushort AxisNo, int StrVel, int MaxVel, double Tacc, short Dir);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_disable_home_move")]
        public static extern short _CS_m314_disable_home_move(ushort CardNo, ushort AxisNo, double Tdec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_home_offset_position")]
        public static extern short _CS_m314_set_home_offset_position(ushort CardNo, ushort AxisNo, int pos);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_home_finish_reset")]
        public static extern short _CS_m314_set_home_finish_reset(ushort CardNo, ushort AxisNo, ushort enable);

        //Motion Counter Operating
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_ltc_position")]
        public static extern short _CS_m314_get_ltc_position(ushort CardNo, ushort AxisNo, ref double ltc_pos);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_ltc_position_manual_clr")]
        public static extern short _CS_m314_get_ltc_position_manual_clr(ushort CardNo, ushort AxisNo, ref double ltc_pos, ushort clr);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_current_speed")]
        public static extern short _CS_m314_get_current_speed(ushort CardNo, ushort AxisNo, ref double speed);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_position")]
        public static extern short _CS_m314_get_position(ushort CardNo, ushort AxisNo, ref double pos);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_position")]
        public static extern short _CS_m314_set_position(ushort CardNo, ushort AxisNo, double pos);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_command")]
        public static extern short _CS_m314_get_command(ushort CardNo, ushort AxisNo, ref int cmd);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_command")]
        public static extern short _CS_m314_set_command(ushort CardNo, ushort AxisNo, int cmd);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_move_ratio")]
        public static extern short _CS_m314_set_move_ratio(ushort CardNo, ushort AxisNo, double move_ratio);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_electronic_cam")]
        public static extern short _CS_m314_set_electronic_cam(ushort CardNo, ushort AxisNo, short numerator, short denominator, ushort Enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_gear")]
        public static extern short _CS_m314_set_gear(ushort CardNo, ushort AxisNo, short numerator, short denominator, ushort Enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_error_counter")]
        public static extern short _CS_m314_get_error_counter(ushort CardNo, ushort AxisNo, ref short error);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_reset_error_counter")]
        public static extern short _CS_m314_reset_error_counter(ushort CardNo, ushort AxisNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_target_pos")]
        public static extern short _CS_m314_get_target_pos(ushort CardNo, ushort AxisNo, ref double pos);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_p_change")]
        public static extern short _CS_m314_p_change(ushort CardNo, ushort AxisNo, int NewPos);

        //Position Compare
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_position_cmp")]
        public static extern short _CS_m314_position_cmp(ushort CardNo, ushort Comparechannel, int start, int end, uint interval);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_position_cmp_table")]
        public static extern short _CS_m314_position_cmp_table(ushort CardNo, ushort Comparechannel, int[] TriggerTable, int count, int offset);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_position_cmp_level")]
        public static extern short _CS_m314_position_cmp_level(ushort CardNo, ushort Comparechannel, int start, int end, uint interval, ushort first_level_on_off);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_position_cmp_table_level")]
        public static extern short _CS_m314_position_cmp_table_level(ushort CardNo, ushort Comparechannel, int[] TriggerTable, int count, int offset, ushort first_level_on_off);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_trigger_enable")]
        public static extern short _CS_m314_set_trigger_enable(ushort CardNo, ushort Comparechannel, ushort enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_trigger_src")]
        public static extern short _CS_m314_set_trigger_src(ushort CardNo, ushort Comparechannel, ushort CompareSrc, ushort OutputSrc, ushort OupPulseWidth);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_trigger_cnt")]
        public static extern short _CS_m314_get_trigger_cnt(ushort CardNo, ushort Comparechannel, ref int trigger_cnt);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_cmp_oneshut")]
        public static extern short _CS_m314_cmp_oneshut(ushort CardNo, ushort Comparechannel);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_cmp_gpio")]
        public static extern short _CS_m314_cmp_gpio(ushort CardNo, ushort Comparechannel, ushort on_off);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_position_cmp_high_speed")]
        public static extern short _CS_m314_position_cmp_high_speed(ushort CardNo, ushort Comparechannel, int start, ushort dir, ushort interval, uint trigger_cnt);

        ///VELOCITY MOVE
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_tv_move")]
        public static extern short _CS_m314_tv_move(ushort CardNo, ushort AxisNo, int StrVel, int MaxVel, double Tacc, short Dir);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_sv_move")]
        public static extern short _CS_m314_sv_move(ushort CardNo, ushort AxisNo, int StrVel, int MaxVel, double Tacc, short Dir);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_v_change")]
        public static extern short _CS_m314_v_change(ushort CardNo, ushort AxisNo, int NewVel, double Time);

        //Pulse Input/Output Configuration
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pls_outmode")]
        public static extern short _CS_m314_set_pls_outmode(ushort CardNo, ushort AxisNo, short pls_outmode);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pls_outfastmode")]
        public static extern short _CS_m314_set_pls_outfastmode(ushort CardNo, ushort AxisNo, short enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pls_outwidth")]
        public static extern short _CS_m314_set_pls_outwidth(ushort CardNo, ushort AxisNo, short pls_outwidth);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pls_iptmode")]
        public static extern short _CS_m314_set_pls_iptmode(ushort CardNo, ushort AxisNo, short inputMode, short pls_logic);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_feedback_src")]
        public static extern short _CS_m314_set_feedback_src(ushort CardNo, ushort AxisNo, short Src);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_a_move_feedback_src")]
        public static extern short _CS_m314_set_a_move_feedback_src(ushort CardNo, ushort AxisNo, short Src);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_link_interrupt")]
        public static extern short _CS_m314_link_interrupt(ushort CardNo, PM314UserCbk callback);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_int_factor")]
        public static extern short _CS_m314_set_int_factor(ushort CardNo, ushort AxisNo, ushort int_factor);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_int_enable")]
        public static extern short _CS_m314_int_enable(ushort CardNo, ushort AxisNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_int_disable")]
        public static extern short _CS_m314_int_disable(ushort CardNo, ushort AxisNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_int_status")]
        public static extern short _CS_m314_get_int_status(ushort CardNo, ushort AxisNo, ref ushort event_int_status);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_int_count")]
        public static extern short _CS_m314_get_int_count(ushort CardNo, ushort AxisNo, ref ushort count);

        ////////SOFT LIMIT
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_soft_limit")]
        public static extern short _CS_m314_set_soft_limit(ushort CardNo, ushort AxisNo, int PLimit, int NLimit);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_enable_soft_limit")]
        public static extern short _CS_m314_enable_soft_limit(ushort CardNo, ushort AxisNo, short Action);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_disable_soft_limit")]
        public static extern short _CS_m314_disable_soft_limit(ushort CardNo, ushort AxisNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_soft_limit_status")]
        public static extern short _CS_m314_get_soft_limit_status(ushort CardNo, ushort AxisNo, ref ushort PLimit_sts, ref ushort NLimit_sts);

        ////////DIO
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_dio_output")]
        public static extern short _CS_m314_set_dio_output(ushort CardNo, ushort AxisNo, ushort output);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_dio_input")]
        public static extern short _CS_m314_set_dio_input(ushort CardNo, ushort AxisNo, ref ushort input);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_dio_input")]
        public static extern short _CS_m314_get_dio_input(ushort CardNo, ushort AxisNo, ref ushort input);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_dio_output")]
        public static extern short _CS_m314_get_dio_output(ushort CardNo, ushort AxisNo, ref ushort output);

        //MISC
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_dwell_buffer")]
        public static extern short _CS_m314_dwell_buffer(ushort CardNo, ushort AxisNo, uint TimeCnt);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_interrupt_buffer")]
        public static extern short _CS_m314_set_interrupt_buffer(ushort CardNo, ushort AxisNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_compare_int")]
        public static extern short _CS_m314_set_compare_int(ushort CardNo, ushort AxisNo, ref int table, ushort total_cnt, ushort compare_dir);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_compare_count")]
        public static extern short _CS_m314_get_compare_count(ushort CardNo, ushort AxisNo, ref ushort index);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_misc_app_get_circle_endpoint")]
        public static extern short _CS_misc_app_get_circle_endpoint(int Start_X, int Start_Y, int Center_X, int Center_Y, double Angle, ref int end_x, ref int end_y);

        //Trace
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_enable_tracing_axis")]
        public static extern short _CS_m314_enable_tracing_axis(ushort CardNo, ref ushort AxisNo, ushort enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_monitor_tracing_axis")]
        public static extern short _CS_m314_monitor_tracing_axis(ushort CardNo, ushort AxisNo, ushort mon_enable, ushort mon_type, int pos_err);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_tracing_axis_lag")]
        public static extern short _CS_m314_get_tracing_axis_lag(ushort CardNo, ushort AxisNo, ref int pos_lag);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_tracing_passing_timer")]
        public static extern short _CS_m314_set_tracing_passing_timer(ushort CardNo, ushort AxisNo, ushort timer);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_trace_rate")]
        public static extern short _CS_m314_set_trace_rate(ushort CardNo, ushort AxisNo, ushort numerator, ushort denominator);

        //SYNC Move
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_sync_move")]
        public static extern short _CS_m314_sync_move(short CardNo);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_sync_move_config")]
        public static extern short _CS_m314_sync_move_config(short CardNo, short AxisNo, short enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_motion_dio_sync_start")]
        public static extern short _CS_m314_set_motion_dio_sync_start(short CardNo, short AxisNo, ushort enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_premove_config")]
        public static extern short _CS_m314_set_premove_config(short CardNo, short AxisNo, short enable, short src_axis, short dir, int pos);

        //dda go
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_dda_table")]
        public static extern short _CS_m314_dda_table(ushort CardNo, ushort AxisNo, ref short dda_table, int count, int offset);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_dda_enable")]
        public static extern short _CS_m314_dda_enable(ushort CardNo, ref ushort Axis_dda_active, ushort enable);

        ////////Special DIO for COMWEB
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_dio_output")]
        public static extern short _CS_m314_dio_output(ushort CardNo, ushort AxisNo, ushort output);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_dio_input")]
        public static extern short _CS_m314_dio_input(ushort CardNo, ushort AxisNo, ref ushort input);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_dda_from_file")]
        public static extern short _CS_m314_dda_from_file(ushort CardNo, ushort AxisNo, string file_name);

        //20080918
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_cam_param")]
        public static extern short _CS_m314_set_cam_param(ushort CardNo, int xOriginalPos, int xPitch, ref int camTable, int camTableLen);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_enable_cam_func")]
        public static extern short _CS_m314_enable_cam_func(ushort CardNo, ushort AxisNo, ushort enable);

        //Feed Hold
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_feedhold_stop")]
        public static extern short _CS_m314_feedhold_stop(ushort CardNo, ushort AxisNo, double Tdec, ushort On_off);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_feedhold_enable")]
        public static extern short _CS_m314_feedhold_enable(ushort CardNo, ushort enable);

        //Software IO set record position
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_io_record_position_cfg")]
        public static extern short _CS_m314_set_io_record_position_cfg(ushort CardNo, ushort DIchannel, ushort axis_pos, ushort polarity, ushort filter_time, ushort enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_io_record_position_cnt")]
        public static extern short _CS_m314_get_io_record_position_cnt(ushort CardNo, ushort DIchannel, ref ushort cnt);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_io_record_position")]
        public static extern short _CS_m314_get_io_record_position(ushort CardNo, ushort DIchannel, ushort Index, ref int pos);

        //Muliti Axis Function
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_multi_axes_move")]
        public static extern short _CS_m314_start_multi_axes_move(ushort CardNo, ushort AxisNum, ref ushort AxisNo, ref int DistArrary, int StrVel, int MaxVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_spiral_xy")]
        public static extern short _CS_m314_start_spiral_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, int spiral_interval, uint spiral_angle, int StrVel, int MaxVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_spiral2_xy")]
        public static extern short _CS_m314_start_spiral2_xy(ushort CardNo, ref ushort AxisNo, int center_x, int center_y, int end_x, int end_y, ushort dir, ushort circlenum, int StrVel, int MaxVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_move")]
        public static extern short _CS_m314_start_v3_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_move_xy")]
        public static extern short _CS_m314_start_v3_move_xy(ushort CardNo, ref ushort AxisNo, int DisX, int DisY, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_arc_xy")]
        public static extern short _CS_m314_start_v3_arc_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_arc2_xy")]
        public static extern short _CS_m314_start_v3_arc2_xy(ushort CardNo, ref ushort AxisNo, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_arc3_xy")]
        public static extern short _CS_m314_start_v3_arc3_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, int End_x, int End_y, short Dir, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_move_xyz")]
        public static extern short _CS_m314_start_v3_move_xyz(ushort CardNo, ref ushort AxisNo, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_heli_xy")]
        public static extern short _CS_m314_start_v3_heli_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_multi_axes")]
        public static extern short _CS_m314_start_v3_multi_axes(ushort CardNo, ushort AxisNum, ref ushort AxisNo, ref int DistArrary, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_spiral_xy")]
        public static extern short _CS_m314_start_v3_spiral_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, int spiral_interval, uint spiral_angle, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_start_v3_spiral2_xy")]
        public static extern short _CS_m314_start_v3_spiral2_xy(ushort CardNo, ref ushort AxisNo, int center_x, int center_y, int end_x, int end_y, ushort dir, ushort circlenum, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_scurve_rate")]
        public static extern short _CS_m314_set_scurve_rate(ushort CardNo, ushort AxisNo, ushort scurve_rate);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_feedrate_overwrite")]
        public static extern short _CS_m314_set_feedrate_overwrite(ushort CardNo, ushort AxisNo, ushort Mode, int New_Speed, double sec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_sd_mode")]
        public static extern short _CS_m314_set_sd_mode(ushort CardNo, ushort AxisNo, ushort mode);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_sd_time")]
        public static extern short _CS_m314_set_sd_time(ushort CardNo, ushort AxisNo, double sd_dec);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_DLL_path")]
        public static extern short _CS_m314_get_DLL_path(byte[] lpFilePath, uint nSize, ref uint nLength);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_DLL_version")]
        public static extern short _CS_m314_get_DLL_version(byte[] lpBuf, uint nSize, ref uint nLength);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_ini_from_file")]
        public static extern short _CS_m314_ini_from_file(ushort CardNo, string file_name);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_Monitor_Counter")]
        public static extern short _CS_m314_Monitor_Counter(ushort CardNo, ushort AxisNo, ref ushort DSP_Cnt, ref ushort PC_Cnt);

        //20130204
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_backlash")]
        public static extern short _CS_m314_set_backlash(short CardNo, ushort AxisNo, short enable);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_backlash_info")]
        public static extern short _CS_m314_set_backlash_info(short CardNo, ushort AxisNo, short backlash, ushort accstep, ushort contstep, ushort decstep);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pitcherr_pitch")]
        public static extern short _CS_m314_set_pitcherr_pitch(ushort CardNo, ushort AxisNo, int pitch);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pitcherr_org")]
        public static extern short _CS_m314_set_pitcherr_org(short CardNo, ushort AxisNo, short Dir, int orgpos);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pitcherr_enable")]
        public static extern short _CS_m314_set_pitcherr_enable(short CardNo, ushort AxisNo, ushort on_off);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pitcherr_mode")]
        public static extern short _CS_m314_set_pitcherr_mode(short CardNo, ushort AxisNo, ushort Mode);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pitcherr_table")]
        public static extern short _CS_m314_set_pitcherr_table(short CardNo, ushort AxisNo, short Dir, ref int table);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_pitcherr_table2")]
        public static extern short _CS_m314_set_pitcherr_table2(short CardNo, ushort AxisNo, short Dir, ref int table, int Num);

        //20130205
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_enable_electcam")]
        public static extern short _CS_m314_enable_electcam(short CardNo, ushort AxisNo, ushort enable, ushort axisbit, ushort mode);

        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_set_monitor_tracing_lag_offset")]
        public static extern short _CS_m314_set_monitor_tracing_lag_offset(ushort CardNo, ushort AxisNo, int lag_offset);

        //20130325
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_gear")]
        public static extern short _CS_m314_get_gear(ushort CardNo, ushort AxisNo, ref short numerator, ref short denominator, ref ushort Enable);

        //20130411
        [DllImport("C:\\Windows\\SysWOW64\\PCI_M314.dll", EntryPoint = "_m314_get_servo_command")]
        public static extern short _CS_m314_get_servo_command(ushort CardNo, ushort AxisNo, ref int cmd);

        public short CS_m314_open(ref ushort existcard)
        {
            return _CS_m314_open(ref existcard);
        }

        public short CS_m314_close(ushort CardNo)
        {
            return _CS_m314_close(CardNo);
        }

        public short CS_m314_initial_card(ushort CardNo)
        {
            return _CS_m314_initial_card(CardNo);
        }

        public short CS_m314_reset_card(ushort CardNo)
        {
            return _CS_m314_reset_card(CardNo);
        }

        public short CS_m314_check_dsp_running(ushort CardNo, ref ushort running)
        {
            return _CS_m314_check_dsp_running(CardNo, ref  running);
        }

        public short CS_m314_buffer_enable(ushort CardNo, ushort Enable)
        {
            return _CS_m314_buffer_enable(CardNo, Enable);
        }

        public short CS_m314_get_cardno(ushort seq, ref ushort CardNo)
        {
            return _CS_m314_get_cardno(seq, ref  CardNo);
        }

        public short CS_m314_get_version(ushort CardNo, ref ushort ver)
        {
            return _CS_m314_get_version(CardNo, ref ver);
        }

        public short CS_m314_config_from_file(ushort CardNo, string file_name)
        {
            return _CS_m314_config_from_file(CardNo, file_name);
        }

        public short CS_m314_get_buffer_length(ushort CardNo, ushort AxisNo, ref ushort bufferLength)
        {
            return _CS_m314_get_buffer_length(CardNo, AxisNo, ref  bufferLength);
        }

        public short CS_m314_get_cardVer(ushort CardNo, ref ushort ver)
        {
            return _CS_m314_get_cardVer(CardNo, ref  ver);
        }

        public short CS_m314_get_firmwarever(ushort CardNo, ref ushort ver)
        {
            return _CS_m314_get_firmwarever(CardNo, ref  ver);
        }

        public short CS_m314_set_motion_disable(ushort CardNo, ushort on_off)
        {
            return _CS_m314_set_motion_disable(CardNo, on_off);
        }

        public short CS_m314_buffer_clear(ushort CardNo, ushort AxisNo)
        {
            return _CS_m314_buffer_clear(CardNo, AxisNo);
        }

        public short CS_m314_set_position_cycle(ushort CardNo, ushort value)
        {
            return _CS_m314_set_position_cycle(CardNo, value);
        }

        public short CS_m314_set_alm(ushort CardNo, ushort AxisNo, short alm_logic, short alm_mode)
        {
            return _CS_m314_set_alm(CardNo, AxisNo, alm_logic, alm_mode);
        }

        public short CS_m314_set_inp(ushort CardNo, ushort AxisNo, short inp_enable, short inp_logic)
        {
            return _CS_m314_set_inp(CardNo, AxisNo, inp_enable, inp_logic);
        }

        public short CS_m314_set_erc(ushort CardNo, ushort AxisNo, short erc_on_time)
        {
            return _CS_m314_set_erc(CardNo, AxisNo, erc_on_time);
        }

        public short CS_m314_set_servo(ushort CardNo, ushort AxisNo, short on_off)
        {
            return _CS_m314_set_servo(CardNo, AxisNo, on_off);
        }

        public short CS_m314_set_sd(ushort CardNo, ushort AxisNo, short enable, short sd_logic, short sd_mode)
        {
            return _CS_m314_set_sd(CardNo, AxisNo, enable, sd_logic, sd_mode);
        }

        public short CS_m314_set_ralm(ushort CardNo, ushort AxisNo, short on_off)
        {
            return _CS_m314_set_ralm(CardNo, AxisNo, on_off);
        }

        public short CS_m314_set_erc_on(ushort CardNo, ushort AxisNo, short on_off)
        {
            return _CS_m314_set_erc_on(CardNo, AxisNo, on_off);
        }

        public short CS_m314_set_ell(ushort CardNo, ushort AxisNo, short ell_logic)
        {
            return _CS_m314_set_ell(CardNo, AxisNo, ell_logic);
        }

        public short CS_m314_set_org(ushort CardNo, ushort AxisNo, short org_logic)
        {
            return _CS_m314_set_org(CardNo, AxisNo, org_logic);
        }

        public short CS_m314_set_emg(ushort CardNo, ushort AxisNo, short emg_logic)
        {
            return _CS_m314_set_emg(CardNo, AxisNo, emg_logic);
        }

        public short CS_m314_set_ez(ushort CardNo, ushort AxisNo, short ez_logic)
        {
            return _CS_m314_set_ez(CardNo, AxisNo, ez_logic);
        }

        public short CS_m314_set_ltc_logic(ushort CardNo, ushort AxisNo, short ltc_logic)
        {
            return _CS_m314_set_ltc_logic(CardNo, AxisNo, ltc_logic);
        }

        public short CS_m314_set_ltc_src(ushort CardNo, ushort AxisNo, short ltc_src)
        {
            return _CS_m314_set_ltc_src(CardNo, AxisNo, ltc_src);
        }

        public short CS_m314_get_ltc_src(ushort CardNo, ushort AxisNo, ref short ltc_src)
        {
            return _CS_m314_get_ltc_src(CardNo, AxisNo, ref  ltc_src);
        }

        public short CS_m314_motion_done(ushort CardNo, ushort AxisNo, ref ushort MC_status)
        {
            return _CS_m314_motion_done(CardNo, AxisNo, ref  MC_status);
        }

        public short CS_m314_get_io_status(ushort CardNo, ushort AxisNo, ref ushort io_sts)
        {
            return _CS_m314_get_io_status(CardNo, AxisNo, ref  io_sts);
        }

        public short CS_m314_start_tr_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_tr_move(CardNo, AxisNo, Dist, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_ta_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_ta_move(CardNo, AxisNo, Dist, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sr_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sr_move(CardNo, AxisNo, Dist, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sa_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sa_move(CardNo, AxisNo, Dist, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_tr_move_xy(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_tr_move_xy(CardNo, ref  AxisArray, DisX, DisY, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sr_move_xy(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sr_move_xy(CardNo, ref  AxisArray, DisX, DisY, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_ta_move_xy(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_ta_move_xy(CardNo, ref  AxisArray, DisX, DisY, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sa_move_xy(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sa_move_xy(CardNo, ref  AxisArray, DisX, DisY, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_tr_arc_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_tr_arc_xy(CardNo, ref  AxisArray, Center_X, Center_Y, Angle, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sr_arc_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sr_arc_xy(CardNo, ref  AxisArray, Center_X, Center_Y, Angle, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_ta_arc_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_ta_arc_xy(CardNo, ref  AxisArray, Center_X, Center_Y, Angle, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sa_arc_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sa_arc_xy(CardNo, ref  AxisArray, Center_X, Center_Y, Angle, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_tr_arc2_xy(ushort CardNo, ref ushort AxisArray, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_tr_arc2_xy(CardNo, ref  AxisArray, End_X, End_Y, Angle, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sr_arc2_xy(ushort CardNo, ref ushort AxisArray, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sr_arc2_xy(CardNo, ref  AxisArray, End_X, End_Y, Angle, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_ta_arc2_xy(ushort CardNo, ref ushort AxisArray, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_ta_arc2_xy(CardNo, ref  AxisArray, End_X, End_Y, Angle, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sa_arc2_xy(ushort CardNo, ref ushort AxisArray, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sa_arc2_xy(CardNo, ref  AxisArray, End_X, End_Y, Angle, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_tr_arc3_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int End_X, int End_Y, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_tr_arc3_xy(CardNo, ref  AxisArray, Center_X, Center_Y, End_X, End_Y, Dir, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sr_arc3_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int End_X, int End_Y, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sr_arc3_xy(CardNo, ref  AxisArray, Center_X, Center_Y, End_X, End_Y, Dir, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_ta_arc3_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int End_X, int End_Y, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_ta_arc3_xy(CardNo, ref  AxisArray, Center_X, Center_Y, End_X, End_Y, Dir, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sa_arc3_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int End_X, int End_Y, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sa_arc3_xy(CardNo, ref  AxisArray, Center_X, Center_Y, End_X, End_Y, Dir, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_tr_move_xyz(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_tr_move_xyz(CardNo, ref  AxisArray, DisX, DisY, DisZ, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sr_move_xyz(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sr_move_xyz(CardNo, ref  AxisArray, DisX, DisY, DisZ, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_ta_move_xyz(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_ta_move_xyz(CardNo, ref  AxisArray, DisX, DisY, DisZ, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sa_move_xyz(ushort CardNo, ref ushort AxisArray, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sa_move_xyz(CardNo, ref  AxisArray, DisX, DisY, DisZ, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_tr_heli_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_tr_heli_xy(CardNo, ref  AxisArray, Center_X, Center_Y, Depth, Pitch, Dir, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sr_heli_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sr_heli_xy(CardNo, ref  AxisArray, Center_X, Center_Y, Depth, Pitch, Dir, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_ta_heli_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_ta_heli_xy(CardNo, ref  AxisArray, Center_X, Center_Y, Depth, Pitch, Dir, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_start_sa_heli_xy(ushort CardNo, ref ushort AxisArray, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, double Tacc, double Tdec)
        {
            return _CS_m314_start_sa_heli_xy(CardNo, ref  AxisArray, Center_X, Center_Y, Depth, Pitch, Dir, StrVel, MaxVel, Tacc, Tdec);
        }

        public short CS_m314_emg_stop(ushort CardNo, ushort AxisNo)
        {
            return _CS_m314_emg_stop(CardNo, AxisNo);
        }

        public short CS_m314_emg_stop_erc(ushort CardNo, ushort AxisNo)
        {
            return _CS_m314_emg_stop_erc(CardNo, AxisNo);
        }

        public short CS_m314_sd_stop(ushort CardNo, ushort AxisNo, double Tdec)
        {
            return _CS_m314_sd_stop(CardNo, AxisNo, Tdec);
        }

        public short CS_m314_set_home_config(ushort CardNo, ushort AxisNo, short home_mode, short org_logic, short ez_logic, short ez_count)
        {
            return _CS_m314_set_home_config(CardNo, AxisNo, home_mode, org_logic, ez_logic, ez_count);
        }

        public short CS_m314_home_move(ushort CardNo, ushort AxisNo, int StrVel, int MaxVel, double Tacc, short Dir)
        {
            return _CS_m314_home_move(CardNo, AxisNo, StrVel, MaxVel, Tacc, Dir);
        }

        public short CS_m314_disable_home_move(ushort CardNo, ushort AxisNo, double Tdec)
        {
            return _CS_m314_disable_home_move(CardNo, AxisNo, Tdec);
        }

        public short CS_m314_set_home_offset_position(ushort CardNo, ushort AxisNo, int pos)
        {
            return _CS_m314_set_home_offset_position(CardNo, AxisNo, pos);
        }

        public short CS_m314_set_home_finish_reset(ushort CardNo, ushort AxisNo, ushort enable)
        {
            return _CS_m314_set_home_finish_reset(CardNo, AxisNo, enable);
        }

        public short CS_m314_get_ltc_position(ushort CardNo, ushort AxisNo, ref double ltc_pos)
        {
            return _CS_m314_get_ltc_position(CardNo, AxisNo, ref  ltc_pos);
        }

        public short CS_m314_get_ltc_position_manual_clr(ushort CardNo, ushort AxisNo, ref double ltc_pos, ushort clr)
        {
            return _CS_m314_get_ltc_position_manual_clr(CardNo, AxisNo, ref  ltc_pos, clr);
        }

        public short CS_m314_get_current_speed(ushort CardNo, ushort AxisNo, ref double speed)
        {
            return _CS_m314_get_current_speed(CardNo, AxisNo, ref  speed);
        }

        public short CS_m314_get_position(ushort CardNo, ushort AxisNo, ref double pos)
        {
            return _CS_m314_get_position(CardNo, AxisNo, ref  pos);
        }

        public short CS_m314_set_position(ushort CardNo, ushort AxisNo, double pos)
        {
            return _CS_m314_set_position(CardNo, AxisNo, pos);
        }

        public short CS_m314_get_command(ushort CardNo, ushort AxisNo, ref int cmd)
        {
            return _CS_m314_get_command(CardNo, AxisNo, ref  cmd);
        }

        public short CS_m314_set_command(ushort CardNo, ushort AxisNo, int cmd)
        {
            return _CS_m314_set_command(CardNo, AxisNo, cmd);
        }

        public short CS_m314_set_move_ratio(ushort CardNo, ushort AxisNo, double move_ratio)
        {
            return _CS_m314_set_move_ratio(CardNo, AxisNo, move_ratio);
        }

        public short CS_m314_set_electronic_cam(ushort CardNo, ushort AxisNo, short numerator, short denominator, ushort Enable)
        {
            return _CS_m314_set_electronic_cam(CardNo, AxisNo, numerator, denominator, Enable);
        }

        public short CS_m314_set_gear(ushort CardNo, ushort AxisNo, short numerator, short denominator, ushort Enable)
        {
            return _CS_m314_set_gear(CardNo, AxisNo, numerator, denominator, Enable);
        }

        public short CS_m314_get_error_counter(ushort CardNo, ushort AxisNo, ref short error)
        {
            return _CS_m314_get_error_counter(CardNo, AxisNo, ref  error);
        }

        public short CS_m314_reset_error_counter(ushort CardNo, ushort AxisNo)
        {
            return _CS_m314_reset_error_counter(CardNo, AxisNo);
        }

        public short CS_m314_get_target_pos(ushort CardNo, ushort AxisNo, ref double pos)
        {
            return _CS_m314_get_target_pos(CardNo, AxisNo, ref  pos);
        }

        public short CS_m314_p_change(ushort CardNo, ushort AxisNo, int NewPos)
        {
            return _CS_m314_p_change(CardNo, AxisNo, NewPos);
        }

        public short CS_m314_position_cmp(ushort CardNo, ushort Comparechannel, int start, int end, uint interval)
        {
            return _CS_m314_position_cmp(CardNo, Comparechannel, start, end, interval);
        }

        public short CS_m314_position_cmp_table(ushort CardNo, ushort Comparechannel, int[] TriggerTable, int count, int offset)
        {
            return _CS_m314_position_cmp_table(CardNo, Comparechannel, TriggerTable, count, offset);
        }

        public short CS_m314_position_cmp_level(ushort CardNo, ushort Comparechannel, int start, int end, uint interval, ushort first_level_on_off)
        {
            return _CS_m314_position_cmp_level(CardNo, Comparechannel, start, end, interval, first_level_on_off);
        }

        public short CS_m314_position_cmp_table_level(ushort CardNo, ushort Comparechannel, int[] TriggerTable, int count, int offset, ushort first_level_on_off)
        {
            return _CS_m314_position_cmp_table_level(CardNo, Comparechannel, TriggerTable, count, offset, first_level_on_off);
        }

        public short CS_m314_set_trigger_enable(ushort CardNo, ushort Comparechannel, ushort enable)
        {
            return _CS_m314_set_trigger_enable(CardNo, Comparechannel, enable);
        }

        public short CS_m314_set_trigger_src(ushort CardNo, ushort Comparechannel, ushort CompareSrc, ushort OutputSrc, ushort OupPulseWidth)
        {
            return _CS_m314_set_trigger_src(CardNo, Comparechannel, CompareSrc, OutputSrc, OupPulseWidth);
        }

        public short CS_m314_get_trigger_cnt(ushort CardNo, ushort Comparechannel, ref int trigger_cnt)
        {
            return _CS_m314_get_trigger_cnt(CardNo, Comparechannel, ref  trigger_cnt);
        }

        public short CS_m314_cmp_oneshut(ushort CardNo, ushort Comparechannel)
        {
            return _CS_m314_cmp_oneshut(CardNo, Comparechannel);
        }

        public short CS_m314_cmp_gpio(ushort CardNo, ushort Comparechannel, ushort on_off)
        {
            return _CS_m314_cmp_gpio(CardNo, Comparechannel, on_off);
        }

        public short CS_m314_position_cmp_high_speed(ushort CardNo, ushort Comparechannel, int start, ushort dir, ushort interval, uint trigger_cnt)
        {
            return _CS_m314_position_cmp_high_speed(CardNo, Comparechannel, start, dir, interval, trigger_cnt);
        }

        public short CS_m314_tv_move(ushort CardNo, ushort AxisNo, int StrVel, int MaxVel, double Tacc, short Dir)
        {
            return _CS_m314_tv_move(CardNo, AxisNo, StrVel, MaxVel, Tacc, Dir);
        }

        public short CS_m314_sv_move(ushort CardNo, ushort AxisNo, int StrVel, int MaxVel, double Tacc, short Dir)
        {
            return _CS_m314_sv_move(CardNo, AxisNo, StrVel, MaxVel, Tacc, Dir);
        }

        public short CS_m314_v_change(ushort CardNo, ushort AxisNo, int NewVel, double Time)
        {
            return _CS_m314_v_change(CardNo, AxisNo, NewVel, Time);
        }

        public short CS_m314_set_pls_outmode(ushort CardNo, ushort AxisNo, short pls_outmode)
        {
            return _CS_m314_set_pls_outmode(CardNo, AxisNo, pls_outmode);
        }

        public short CS_m314_set_pls_outfastmode(ushort CardNo, ushort AxisNo, short enable)
        {
            return _CS_m314_set_pls_outfastmode(CardNo, AxisNo, enable);
        }

        public short CS_m314_set_pls_outwidth(ushort CardNo, ushort AxisNo, short pls_outwidth)
        {
            return _CS_m314_set_pls_outwidth(CardNo, AxisNo, pls_outwidth);
        }

        public short CS_m314_set_pls_iptmode(ushort CardNo, ushort AxisNo, short inputMode, short pls_logic)
        {
            return _CS_m314_set_pls_iptmode(CardNo, AxisNo, inputMode, pls_logic);
        }

        public short CS_m314_set_feedback_src(ushort CardNo, ushort AxisNo, short Src)
        {
            return _CS_m314_set_feedback_src(CardNo, AxisNo, Src);
        }

        public short CS_m314_set_a_move_feedback_src(ushort CardNo, ushort AxisNo, short Src)
        {
            return _CS_m314_set_a_move_feedback_src(CardNo, AxisNo, Src);
        }

        public short CS_m314_link_interrupt(ushort CardNo, PM314UserCbk callback)
        {
            return _CS_m314_link_interrupt(CardNo, callback);
        }

        public short CS_m314_set_int_factor(ushort CardNo, ushort AxisNo, ushort int_factor)
        {
            return _CS_m314_set_int_factor(CardNo, AxisNo, int_factor);
        }

        public short CS_m314_int_enable(ushort CardNo, ushort AxisNo)
        {
            return _CS_m314_int_enable(CardNo, AxisNo);
        }

        public short CS_m314_int_disable(ushort CardNo, ushort AxisNo)
        {
            return _CS_m314_int_disable(CardNo, AxisNo);
        }

        public short CS_m314_get_int_status(ushort CardNo, ushort AxisNo, ref ushort event_int_status)
        {
            return _CS_m314_get_int_status(CardNo, AxisNo, ref  event_int_status);
        }

        public short CS_m314_get_int_count(ushort CardNo, ushort AxisNo, ref ushort count)
        {
            return _CS_m314_get_int_count(CardNo, AxisNo, ref  count);
        }

        public short CS_m314_set_soft_limit(ushort CardNo, ushort AxisNo, int PLimit, int NLimit)
        {
            return _CS_m314_set_soft_limit(CardNo, AxisNo, PLimit, NLimit);
        }

        public short CS_m314_enable_soft_limit(ushort CardNo, ushort AxisNo, short Action)
        {
            return _CS_m314_enable_soft_limit(CardNo, AxisNo, Action);
        }

        public short CS_m314_disable_soft_limit(ushort CardNo, ushort AxisNo)
        {
            return _CS_m314_disable_soft_limit(CardNo, AxisNo);
        }

        public short CS_m314_get_soft_limit_status(ushort CardNo, ushort AxisNo, ref ushort PLimit_sts, ref ushort NLimit_sts)
        {
            return _CS_m314_get_soft_limit_status(CardNo, AxisNo, ref  PLimit_sts, ref  NLimit_sts);
        }

        public short CS_m314_set_dio_output(ushort CardNo, ushort AxisNo, ushort output)
        {
            return _CS_m314_set_dio_output(CardNo, AxisNo, output);
        }

        public short CS_m314_set_dio_input(ushort CardNo, ushort AxisNo, ref ushort input)
        {
            return _CS_m314_set_dio_input(CardNo, AxisNo, ref  input);
        }

        public short CS_m314_get_dio_input(ushort CardNo, ushort AxisNo, ref ushort input)
        {
            return _CS_m314_get_dio_input(CardNo, AxisNo, ref  input);
        }

        public short CS_m314_get_dio_output(ushort CardNo, ushort AxisNo, ref ushort output)
        {
            return _CS_m314_get_dio_output(CardNo, AxisNo, ref  output);
        }

        public short CS_m314_dwell_buffer(ushort CardNo, ushort AxisNo, uint TimeCnt)
        {
            return _CS_m314_dwell_buffer(CardNo, AxisNo, TimeCnt);
        }

        public short CS_m314_set_interrupt_buffer(ushort CardNo, ushort AxisNo)
        {
            return _CS_m314_set_interrupt_buffer(CardNo, AxisNo);
        }

        public short CS_m314_set_compare_int(ushort CardNo, ushort AxisNo, ref int table, ushort total_cnt, ushort compare_dir)
        {
            return _CS_m314_set_compare_int(CardNo, AxisNo, ref  table, total_cnt, compare_dir);
        }

        public short CS_m314_get_compare_count(ushort CardNo, ushort AxisNo, ref ushort index)
        {
            return _CS_m314_get_compare_count(CardNo, AxisNo, ref  index);
        }

        public short CS_misc_app_get_circle_endpoint(int Start_X, int Start_Y, int Center_X, int Center_Y, double Angle, ref int end_x, ref int end_y)
        {
            return _CS_misc_app_get_circle_endpoint(Start_X, Start_Y, Center_X, Center_Y, Angle, ref  end_x, ref  end_y);
        }

        public short CS_m314_enable_tracing_axis(ushort CardNo, ref ushort AxisNo, ushort enable)
        {
            return _CS_m314_enable_tracing_axis(CardNo, ref  AxisNo, enable);
        }

        public short CS_m314_monitor_tracing_axis(ushort CardNo, ushort AxisNo, ushort mon_enable, ushort mon_type, int pos_err)
        {
            return _CS_m314_monitor_tracing_axis(CardNo, AxisNo, mon_enable, mon_type, pos_err);
        }

        public short CS_m314_get_tracing_axis_lag(ushort CardNo, ushort AxisNo, ref int pos_lag)
        {
            return _CS_m314_get_tracing_axis_lag(CardNo, AxisNo, ref  pos_lag);
        }

        public short CS_m314_set_tracing_passing_timer(ushort CardNo, ushort AxisNo, ushort timer)
        {
            return _CS_m314_set_tracing_passing_timer(CardNo, AxisNo, timer);
        }

        public short CS_m314_set_trace_rate(ushort CardNo, ushort AxisNo, ushort numerator, ushort denominator)
        {
            return _CS_m314_set_trace_rate(CardNo, AxisNo, numerator, denominator);
        }

        public short CS_m314_sync_move(short CardNo)
        {
            return _CS_m314_sync_move(CardNo);
        }

        public short CS_m314_sync_move_config(short CardNo, short AxisNo, short enable)
        {
            return _CS_m314_sync_move_config(CardNo, AxisNo, enable);
        }

        public short CS_m314_set_motion_dio_sync_start(short CardNo, short AxisNo, ushort enable)
        {
            return _CS_m314_set_motion_dio_sync_start(CardNo, AxisNo, enable);
        }

        public short CS_m314_set_premove_config(short CardNo, short AxisNo, short enable, short src_axis, short dir, int pos)
        {
            return _CS_m314_set_premove_config(CardNo, AxisNo, enable, src_axis, dir, pos);
        }

        public short CS_m314_dda_table(ushort CardNo, ushort AxisNo, ref short dda_table, int count, int offset)
        {
            return _CS_m314_dda_table(CardNo, AxisNo, ref  dda_table, count, offset);
        }

        public short CS_m314_dda_enable(ushort CardNo, ref ushort Axis_dda_active, ushort enable)
        {
            return _CS_m314_dda_enable(CardNo, ref  Axis_dda_active, enable);
        }

        public short CS_m314_dio_output(ushort CardNo, ushort AxisNo, ushort output)
        {
            return _CS_m314_dio_output(CardNo, AxisNo, output);
        }

        public short CS_m314_dio_input(ushort CardNo, ushort AxisNo, ref ushort input)
        {
            return _CS_m314_dio_input(CardNo, AxisNo, ref  input);
        }

        public short CS_m314_dda_from_file(ushort CardNo, ushort AxisNo, string file_name)
        {
            return _CS_m314_dda_from_file(CardNo, AxisNo, file_name);
        }

        public short CS_m314_set_cam_param(ushort CardNo, int xOriginalPos, int xPitch, ref int camTable, int camTableLen)
        {
            return _CS_m314_set_cam_param(CardNo, xOriginalPos, xPitch, ref  camTable, camTableLen);
        }

        public short CS_m314_enable_cam_func(ushort CardNo, ushort AxisNo, ushort enable)
        {
            return _CS_m314_enable_cam_func(CardNo, AxisNo, enable);
        }

        public short CS_m314_feedhold_stop(ushort CardNo, ushort AxisNo, double Tdec, ushort On_off)
        {
            return _CS_m314_feedhold_stop(CardNo, AxisNo, Tdec, On_off);
        }

        public short CS_m314_feedhold_enable(ushort CardNo, ushort enable)
        {
            return _CS_m314_feedhold_enable(CardNo, enable);
        }

        public short CS_m314_set_io_record_position_cfg(ushort CardNo, ushort DIchannel, ushort axis_pos, ushort polarity, ushort filter_time, ushort enable)
        {
            return _CS_m314_set_io_record_position_cfg(CardNo, DIchannel, axis_pos, polarity, filter_time, enable);
        }

        public short CS_m314_get_io_record_position_cnt(ushort CardNo, ushort DIchannel, ref ushort cnt)
        {
            return _CS_m314_get_io_record_position_cnt(CardNo, DIchannel, ref  cnt);
        }

        public short CS_m314_get_io_record_position(ushort CardNo, ushort DIchannel, ushort Index, ref int pos)
        {
            return _CS_m314_get_io_record_position(CardNo, DIchannel, Index, ref  pos);
        }

        public short CS_m314_start_multi_axes_move(ushort CardNo, ushort AxisNum, ref ushort AxisNo, ref int DistArrary, int StrVel, int MaxVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_multi_axes_move(CardNo, AxisNum, ref  AxisNo, ref  DistArrary, StrVel, MaxVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_spiral_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, int spiral_interval, uint spiral_angle, int StrVel, int MaxVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_spiral_xy(CardNo, ref  AxisNo, Center_X, Center_Y, spiral_interval, spiral_angle, StrVel, MaxVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_spiral2_xy(ushort CardNo, ref ushort AxisNo, int center_x, int center_y, int end_x, int end_y, ushort dir, ushort circlenum, int StrVel, int MaxVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_spiral2_xy(CardNo, ref  AxisNo, center_x, center_y, end_x, end_y, dir, circlenum, StrVel, MaxVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_move(ushort CardNo, ushort AxisNo, int Dist, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_move(CardNo, AxisNo, Dist, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_move_xy(ushort CardNo, ref ushort AxisNo, int DisX, int DisY, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_move_xy(CardNo, ref  AxisNo, DisX, DisY, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_arc_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, double Angle, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_arc_xy(CardNo, ref  AxisNo, Center_X, Center_Y, Angle, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_arc2_xy(ushort CardNo, ref ushort AxisNo, int End_X, int End_Y, double Angle, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_arc2_xy(CardNo, ref  AxisNo, End_X, End_Y, Angle, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_arc3_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, int End_x, int End_y, short Dir, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_arc3_xy(CardNo, ref  AxisNo, Center_X, Center_Y, End_x, End_y, Dir, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_move_xyz(ushort CardNo, ref ushort AxisNo, int DisX, int DisY, int DisZ, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_move_xyz(CardNo, ref  AxisNo, DisX, DisY, DisZ, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_heli_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, int Depth, int Pitch, short Dir, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_heli_xy(CardNo, ref  AxisNo, Center_X, Center_Y, Depth, Pitch, Dir, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_multi_axes(ushort CardNo, ushort AxisNum, ref ushort AxisNo, ref int DistArrary, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_multi_axes(CardNo, AxisNum, ref  AxisNo, ref  DistArrary, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_spiral_xy(ushort CardNo, ref ushort AxisNo, int Center_X, int Center_Y, int spiral_interval, uint spiral_angle, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_spiral_xy(CardNo, ref  AxisNo, Center_X, Center_Y, spiral_interval, spiral_angle, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_start_v3_spiral2_xy(ushort CardNo, ref ushort AxisNo, int center_x, int center_y, int end_x, int end_y, ushort dir, ushort circlenum, int StrVel, int MaxVel, int EndVel, double Tacc, double Tdec, ushort m_curve, ushort m_r_a)
        {
            return _CS_m314_start_v3_spiral2_xy(CardNo, ref  AxisNo, center_x, center_y, end_x, end_y, dir, circlenum, StrVel, MaxVel, EndVel, Tacc, Tdec, m_curve, m_r_a);
        }

        public short CS_m314_set_scurve_rate(ushort CardNo, ushort AxisNo, ushort scurve_rate)
        {
            return _CS_m314_set_scurve_rate(CardNo, AxisNo, scurve_rate);
        }

        public short CS_m314_set_feedrate_overwrite(ushort CardNo, ushort AxisNo, ushort Mode, int New_Speed, double sec)
        {
            return _CS_m314_set_feedrate_overwrite(CardNo, AxisNo, Mode, New_Speed, sec);
        }

        public short CS_m314_set_sd_mode(ushort CardNo, ushort AxisNo, ushort mode)
        {
            return _CS_m314_set_sd_mode(CardNo, AxisNo, mode);
        }

        public short CS_m314_set_sd_time(ushort CardNo, ushort AxisNo, double sd_dec)
        {
            return _CS_m314_set_sd_time(CardNo, AxisNo, sd_dec);
        }

        public short CS_m314_get_DLL_path(byte[] lpFilePath, uint nSize, ref uint nLength)
        {
            return _CS_m314_get_DLL_path(lpFilePath, nSize, ref  nLength);
        }

        public short CS_m314_get_DLL_version(byte[] lpBuf, uint nSize, ref uint nLength)
        {
            return _CS_m314_get_DLL_version(lpBuf, nSize, ref  nLength);
        }

        public short CS_m314_ini_from_file(ushort CardNo, string file_name)
        {
            return _CS_m314_ini_from_file(CardNo, file_name);
        }

        public short CS_m314_Monitor_Counter(ushort CardNo, ushort AxisNo, ref ushort DSP_Cnt, ref ushort PC_Cnt)
        {
            return _CS_m314_Monitor_Counter(CardNo, AxisNo, ref  DSP_Cnt, ref  PC_Cnt);
        }

        public short CS_m314_set_backlash(short CardNo, ushort AxisNo, short enable)
        {
            return _CS_m314_set_backlash(CardNo, AxisNo, enable);
        }

        public short CS_m314_set_backlash_info(short CardNo, ushort AxisNo, short backlash, ushort accstep, ushort contstep, ushort decstep)
        {
            return _CS_m314_set_backlash_info(CardNo, AxisNo, backlash, accstep, contstep, decstep);
        }

        public short CS_m314_set_pitcherr_pitch(ushort CardNo, ushort AxisNo, int pitch)
        {
            return _CS_m314_set_pitcherr_pitch(CardNo, AxisNo, pitch);
        }

        public short CS_m314_set_pitcherr_org(short CardNo, ushort AxisNo, short Dir, int orgpos)
        {
            return _CS_m314_set_pitcherr_org(CardNo, AxisNo, Dir, orgpos);
        }

        public short CS_m314_set_pitcherr_enable(short CardNo, ushort AxisNo, ushort on_off)
        {
            return _CS_m314_set_pitcherr_enable(CardNo, AxisNo, on_off);
        }

        public short CS_m314_set_pitcherr_mode(short CardNo, ushort AxisNo, ushort Mode)
        {
            return _CS_m314_set_pitcherr_mode(CardNo, AxisNo, Mode);
        }

        public short CS_m314_set_pitcherr_table(short CardNo, ushort AxisNo, short Dir, ref int table)
        {
            return _CS_m314_set_pitcherr_table(CardNo, AxisNo, Dir, ref  table);
        }

        public short CS_m314_set_pitcherr_table2(short CardNo, ushort AxisNo, short Dir, ref int table, int Num)
        {
            return _CS_m314_set_pitcherr_table2(CardNo, AxisNo, Dir, ref  table, Num);
        }

        public short CS_m314_enable_electcam(short CardNo, ushort AxisNo, ushort enable, ushort axisbit, ushort mode)
        {
            return _CS_m314_enable_electcam(CardNo, AxisNo, enable, axisbit, mode);
        }

        public short CS_m314_set_monitor_tracing_lag_offset(ushort CardNo, ushort AxisNo, int lag_offset)
        {
            return _CS_m314_set_monitor_tracing_lag_offset(CardNo, AxisNo, lag_offset);
        }

        public short CS_m314_get_gear(ushort CardNo, ushort AxisNo, ref short numerator, ref short denominator, ref ushort Enable)
        {
            return _CS_m314_get_gear(CardNo, AxisNo, ref  numerator, ref  denominator, ref  Enable);
        }

        public short CS_m314_get_servo_command(ushort CardNo, ushort AxisNo, ref int cmd)
        {
            return _CS_m314_get_servo_command(CardNo, AxisNo, ref  cmd);
        }
    }
     */
}