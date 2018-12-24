using System;

namespace AOISystem.Utilities.Component
{
    public class IONumberAttribute : Attribute
    {
        public IONumberAttribute(string ioNumber)
        {
            this.IONumber = ioNumber;
        }

        public string IONumber { get; set; }
    }
}
