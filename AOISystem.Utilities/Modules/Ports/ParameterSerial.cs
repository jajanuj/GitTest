using AOISystem.Utilities.Common;
using System;
using System.ComponentModel;
using System.IO.Ports;

namespace AOISystem.Utilities.Modules.Ports
{
    [Serializable]
    public class ParameterSerial : ParameterINI
    {
        private string portNumber;
        private int baudRate;
        private Parity parity;
        private int dataBits;
        private StopBits stopBits;
        private int readTimeout;
        private int writeTimeout;
        private int readDelayTime;
        private int writeIntervalTime;

        [Category("Comport"), Browsable(true), Description("PortNumber : COM1 or COM2 etc..")]
        public string PortNumber { get { return portNumber; } set { portNumber = value; WriteAndNotify("PortNumber"); } }

        [Category("Comport"), Browsable(true), Description("BaudRate")]
        public int BaudRate { get { return baudRate; } set { baudRate = value; WriteAndNotify("BaudRate"); } }

        [Category("Comport"), Browsable(true), Description("Parity")]
        public Parity Parity { get { return parity; } set { parity = value; WriteAndNotify("Parity"); } }

        [Category("Comport"), Browsable(true), Description("DataBits : 5 ~ 8")]
        public int DataBits { get { return dataBits; } set { dataBits = value; WriteAndNotify("DataBits"); } }

        [Category("Comport"), Browsable(true), Description("StopBits")]
        public StopBits StopBits { get { return stopBits; } set { stopBits = value; WriteAndNotify("StopBits"); } }

        [Category("Comport"), Browsable(true), Description("ReadTimeout")]
        public int ReadTimeout { get { return readTimeout; } set { readTimeout = value; WriteAndNotify("ReadTimeout"); } }

        [Category("Comport"), Browsable(true), Description("WriteTimeout")]
        public int WriteTimeout { get { return writeTimeout; } set { writeTimeout = value; WriteAndNotify("WriteTimeout"); } }

        [Category("Comport"), Browsable(true), Description("ReadDelayTime")]
        public int ReadDelayTime { get { return readDelayTime; } set { readDelayTime = value; WriteAndNotify("ReadDelayTime"); } }

        [Category("Comport"), Browsable(true), Description("WriteIntervalTime")]
        public int WriteIntervalTime { get { return writeIntervalTime; } set { writeIntervalTime = value; WriteAndNotify("WriteIntervalTime"); } }

        public ParameterSerial(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            portNumber = "COM1";
            baudRate = 9600;
            parity = System.IO.Ports.Parity.None;
            dataBits = 8;
            stopBits = System.IO.Ports.StopBits.One;
            readTimeout = 3000;
            writeTimeout = 3000;
            readDelayTime = 100;
            writeIntervalTime = 250;
        }
    }
}
