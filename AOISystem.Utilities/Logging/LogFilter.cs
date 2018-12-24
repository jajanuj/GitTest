
namespace AOISystem.Utilities.Logging
{
    public enum LogFilter 
    {
        //Alarm, 如: 重大錯誤
        Alarm,
        //Warning, 如: 警告
        Warning, 
        //程式流程, 如: Home、進片、取像等
        Flow, 
        //程式操作, 如: UI操作
        Operation,
        //演算法
        Algorithm,
        //程式例外
        Exception, 
        //通訊交握訊息
        Handshake,
        //除錯
        Debug,
        //通告訊息
        Notify,
        //監控訊息
        Monitor,
        //參數編輯
        Parameter,
        PN
    }
}
