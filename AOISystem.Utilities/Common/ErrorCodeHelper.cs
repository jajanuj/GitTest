using AOISystem.Utilities.IO;
using AOISystem.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace AOISystem.Utilities.Common
{
    public class ErrorCodeHelper
    {
        private string _fileName = "ErrorCode";

        private Dictionary<string, ErrorCode> _dicAlarmCodeCollection;
        private Dictionary<string, ErrorCode> _dicWarningCodeCollection;

        public ErrorCodeHelper(string folderPath)
        {
            _dicAlarmCodeCollection = new Dictionary<string, ErrorCode>();
            _dicWarningCodeCollection = new Dictionary<string, ErrorCode>();

            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.csv");
            foreach (FileInfo fileInfo in fileInfos)
            {
                string fileName = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                if (fileName.Contains(_fileName))
                {
                    string[] subNames = fileName.Split('.');
                    CultureInfo cultureInfo = null;
                    if (subNames.Length == 2)
                    {
                        cultureInfo = new CultureInfo(subNames[1]);
                    }
                    else if (subNames.Length == 1)
                    {
                        //預設檔名ErrorCode.csv，如忘記改名視為ErrorCode.en.csv
                        cultureInfo = new CultureInfo("en");
                    }
                    else
                    {
                        throw new CultureNotFoundException(string.Format("To analyze filename's culture feature [{0}] is error.", fileName));
                    }
                    Load(folderPath, fileInfo.Name, cultureInfo.ToString());
                }
            }
        }

        public ErrorCode GetErrorCode(ErrorType errorType, int code)
        {
            ErrorCode errorCode;
            Dictionary<string, ErrorCode> dicErrorCodeCollection = GetDicErrorCodeCollection(errorType);
            string key = string.Format("{0}_{1}", Thread.CurrentThread.CurrentUICulture, code);
            if (dicErrorCodeCollection.ContainsKey(key))
            {
                errorCode = dicErrorCodeCollection[key];
            }
            else
            {
                //在zh-TW語系找不到，改到en語系找
                key = string.Format("{0}_{1}", "en", code);
                if (dicErrorCodeCollection.ContainsKey(key))
                {
                    errorCode = dicErrorCodeCollection[key];
                }
                else
                {
                    errorCode = new ErrorCode();
                    errorCode.ErrorType = errorType;
                }
            }
            return errorCode;
        }

        public string GetErrorMessage(ErrorType errorType, int code)
        {
            string message;
            Dictionary<string, ErrorCode> dicErrorCodeCollection = GetDicErrorCodeCollection(errorType);
            string key = string.Format("{0}_{1}", Thread.CurrentThread.CurrentUICulture, code);
            if (dicErrorCodeCollection.ContainsKey(key))
            {
                message = dicErrorCodeCollection[key].Message;
            }
            else
            {
                message = "Unknow Error Message";
            }
            return message;
        }

        private Dictionary<string, ErrorCode> GetDicErrorCodeCollection(ErrorType errorType)
        {
            Dictionary<string, ErrorCode> dicErrorCodeCollection;
            if (errorType == ErrorType.Alarm)
            {
                dicErrorCodeCollection = _dicAlarmCodeCollection;
            }
            else
            {
                dicErrorCodeCollection = _dicWarningCodeCollection;
            }
            return dicErrorCodeCollection;
        }

        private void Load(string folderPath, string fileName, string cultureName)
        {
            try
            {
                string filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                if (!File.Exists(filePath))
                {
                    return;
                }
                List<string> csvStrings = SimpleCsvHelper.ReadCsvToList(filePath);
                for (int i = 1; i < csvStrings.Count; i++)
                {
                    string[] subItems = csvStrings[i].Split(',');
                    ErrorCode errorCode = new ErrorCode()
                    {
                        ErrorType = (ErrorType)Enum.Parse(typeof(ErrorType), subItems[0]),
                        Code = int.Parse(subItems[1]),
                        Message = subItems[2],
                        Remark = subItems[3]
                    };
                    if (errorCode.ErrorType == ErrorType.Alarm)
                    {
                        _dicAlarmCodeCollection.Add(string.Format("{0}_{1}", cultureName, errorCode.Code), errorCode);
                    }
                    else
                    {
                        _dicWarningCodeCollection.Add(string.Format("{0}_{1}", cultureName, errorCode.Code), errorCode);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Exception(ex);
                NotifyLogger.Post(ex);
            }
        }

        private void Save(string folderPath, string fileName, string cultureName)
        {
            //ErrorCode error = new ErrorCode()
            //{
            //    ErrorType = ErrorType.Alarm,
            //    Code = -1,
            //    Message = "Unknow Error",
            //    Remark = "中文測試"
            //};
            //ErrorCode error2 = new ErrorCode()
            //{
            //    ErrorType = ErrorType.Warning,
            //    Code = -2,
            //    Message = "Unknow Error",
            //    Remark = "English Test"
            //};
            //_dicAlarmCodeCollection.Add(error.Code, error);
            //_dicWarningCodeCollection.Add(error2.Code, error2);

            string filePath = string.Format(@"{0}\{1}", folderPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            SimpleCsvHelper.AddData(folderPath, fileName, "{0},{1},{2},{3}", "ErrorType", "Code", "Message", "Remark");
            foreach (var item in _dicAlarmCodeCollection)
            {
                ErrorCode errorCode = item.Value;
                SimpleCsvHelper.AddData(folderPath, fileName, errorCode.ErrorType, errorCode.Code, errorCode.Message, errorCode.Remark);
            }
            foreach (var item in _dicWarningCodeCollection)
            {
                ErrorCode errorCode = item.Value;
                SimpleCsvHelper.AddData(folderPath, fileName, errorCode.ErrorType, errorCode.Code, errorCode.Message, errorCode.Remark);
            }
        }
    }
}
