using System;

namespace AOISystem.Utilities.Component
{
    public class PermissionsAttribute : Attribute
    {
        private byte value;

        /// <summary> 權限值 </summary>
        public byte Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
