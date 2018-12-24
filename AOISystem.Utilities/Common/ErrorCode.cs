using System;

namespace AOISystem.Utilities.Common
{
    public enum ErrorType
    {
        Alarm,
        Warning
    }

    public class ErrorCode
    {

        public const int UnknowCode = -1;

        public const string UnknowMessage = "Unknow Error Message";

        public ErrorCode()
            : this(ErrorType.Alarm, UnknowCode, UnknowMessage, string.Empty)
        {
        }

        public ErrorCode(int code)
            : this(ErrorType.Alarm, code, UnknowMessage, string.Empty)
        {
        }

        public ErrorCode(int code, string message)
            : this(ErrorType.Alarm, code, message, string.Empty)
        {
        }

        public ErrorCode(ErrorType errorType, int code, string message)
            : this(errorType, code, message, string.Empty)
        {
        }

        public ErrorCode(ErrorType errorType, int code, string message, string remark)
        {
            this.ErrorType = errorType;
            this.Code = code;
            this.Message = message.Replace("\r\n", " ");
            this.Remark = remark;
        }

        public ErrorType ErrorType { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string Remark { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}] Code : {1} Message : {2}", this.ErrorType, this.Code, this.Message);
        }
    }
}
