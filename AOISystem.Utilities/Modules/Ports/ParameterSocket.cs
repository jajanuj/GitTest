using AOISystem.Utilities.Common;
using System;
using System.ComponentModel;

namespace AOISystem.Utilities.Modules.Ports
{
    [Serializable]
    public class ParameterSocket : ParameterINI
    {
        private string _Address;
        private int _Port;
        private int _ConnectTimeout;
        private int _ReceiveTimeout;

        [Browsable(true), Category("Ethernet"), Description("IP Address")]
        public string Address { get { return _Address; } set { _Address = value; WriteAndNotify("Address"); } }

        [Browsable(true), Category("Ethernet"), Description("IP Port")]
        public int Port { get { return _Port; } set { _Port = value; WriteAndNotify("Port"); } }

        [Browsable(true), Category("Ethernet"), Description("Connect Timeout")]
        public int ConnectTimeout { get { return _ConnectTimeout; } set { _ConnectTimeout = value; WriteAndNotify("ConnectTimeout"); } }

        [Browsable(true), Category("Ethernet"), Description("Receive Timeout")]
        public int ReceiveTimeout { get { return _ReceiveTimeout; } set { _ReceiveTimeout = value; WriteAndNotify("ReceiveTimeout"); } }

        public ParameterSocket(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            _Address = "127.0.0.1";
            _Port = 80;
            _ConnectTimeout = 1000;
            _ReceiveTimeout = 1000;
        }
    }
}
