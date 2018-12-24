using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AOISystem.Utilities.Modules.Syntek;
using System.Resources;

namespace AOISystem.Utilities.Modules
{
    public interface IMotion
    {
        /// <summary> 裝置名稱 </summary>
        string DeviceName { get; set; }

        /// <summary> 取得目前設定位置 (Command Counter) </summary>
        double Position { get; }

        ///<summary> 取得編碼器位置 (Position Counter) </summary>
        double Encoder { get; }

        /// <summary> 取得原點復歸的旗標 </summary>
        bool IsHome { get; }

        /// <summary> 取得原點Sensor訊號 </summary>
        bool IsORG { get; }

        /// <summary> 取得正極限訊號 </summary>
        bool IsLimitP { get; }

        /// <summary> 取得負極限訊號 </summary>
        bool IsLimitN { get; }

        /// <summary> 取得警報訊號 </summary>
        bool IsAlarm { get; }

        /// <summary> 軸是否啟用(紀錄到xml中) </summary>
        bool IsActive { get; }

        /// <summary> 軸是否啟用 </summary>
        bool Enabled { get; }

        /// <summary> 是否在移動中 </summary>
        bool IsBusy { get; }

        /// <summary> 是否到位 </summary>
        bool IsReached { get; }

        bool IsSoftLimitN { get; }

        bool IsSoftLimitP { get; }

        /// <summary>
        /// 清除postion和command的counter
        /// </summary>
        void ResetPos();

        /// <summary>
        /// 裝置啟停
        /// </summary>
        /// <param name="option">The option.</param>
        void ServoOn(CmdStatus option);

        /// <summary>
        /// 清除警報
        /// </summary>
        void ResetAlarm();

        /// <summary>
        /// 絕對移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="inAuto">手動或自動速度</param>
        bool AbsolueMove(double dest, SpeedMode inAuto = SpeedMode.Auto);

        /// <summary>
        /// 絕對移動 (需要自定義速度，初速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="speed"> 速度值 </param>
        /// <param name="s_curve"> T-Curve = true, S-Curve = false </param>
        bool AbsolueMove(double dest, double speed, bool curve = true);

        /// <summary>
        /// 相對移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="inAuto">手動或自動速度</param>
        bool RelativeMove(double dest, SpeedMode inAuto = SpeedMode.Auto);

        /// <summary>
        /// 相對移動 (自定義速度，初速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="speed">速度值</param>
        bool RelativeMove(double dest, double speed);

        /// <summary>
        /// 連續移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dir">移動方向</param>
        /// <param name="inAuto">手動或自動速度</param>
        /// <returns></returns>
        bool ContinuousMove(RotationDirection dir, SpeedMode inAuto = SpeedMode.Auto);

        bool Jog(JogSpeed jogSpeed, RotationDirection dir);

        bool JogM(double speed, RotationDirection dir);

        /// <summary>
        /// 停止裝置 (復歸流程中不停止homeTask)
        /// </summary>
        void Stop(StopType type, bool isStopTask = true);

        /// <summary>
        /// 啟動原點復歸 (參數定義在軸參中)
        /// </summary>
        void Home();

        /// <summary>
        /// Shows the M2X4 Configuration.
        /// </summary>
        void ConfigurationShow(FormClosingEventHandler action = null);

        /// <summary>
        /// Shows the Motion Slim Configuration.
        /// </summary>
        void ConfigurationSlimShow(FormClosingEventHandler action = null);

        //set command counter
        void SetCommandCounter(double position);

        //set encode
        void SetPositionCounter(double position);
    }
}
