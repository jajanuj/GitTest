using System.Runtime.InteropServices;

namespace AOISystem.Utilities.Modules.Syntek.L132.Library
{
    public class CA104_L132
    {
        //============ API Function  definition ================
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_write_register")]
        public static extern short CS_mnet_ao4_write_register(ushort CardNo , ushort RingNo , ushort SlaveIP , byte Cmd , ushort Data1 , ushort Data2 , ushort Data3 , ushort Data4);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_Read_Register")]
        public static extern short CS_mnet_ao4_Read_Register(ushort CardNo , ushort RingNo , ushort SlaveIP , byte Cmd , ref ushort Low , ref ushort High);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_write_AD5764_register")]
        public static extern short CS_mnet_ao4_write_AD5764_register(ushort CardNo , ushort RingNo , ushort DeviceIP , byte RegType , byte Channel , ushort WriteData);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_read_AD5764_register")]
        public static extern short CS_mnet_ao4_read_AD5764_register(ushort CardNo , ushort RingNo , ushort DeviceIP , byte RegType , byte Channel , ref short ReadData);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_write_ARM_function")]
        public static extern short CS_mnet_ao4_write_ARM_function(ushort CardNo , ushort RingNo , ushort DeviceIP , byte Func , ushort Param1 , ushort Param2 , ushort Param3 , ushort Param4);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_read_ARM_function")]
        public static extern short CS_mnet_ao4_read_ARM_function(ushort CardNo , ushort RingNo , ushort DeviceIP , byte Func , ref ushort ReadData);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_get_hardware_info")]
        public static extern short CS_mnet_ao4_get_hardware_info(ushort CardNo , ushort RingNo , ushort SlaveIP , ref byte DeviceID , ref byte VHDL_Version);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_initial")]
        public static extern short CS_mnet_ao4_initial(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_clear_output_all")]
        public static extern short CS_mnet_ao4_clear_output_all(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_set_output")]
        public static extern short CS_mnet_ao4_set_output(ushort CardNo , ushort RingNo , ushort DeviceIP , byte ChannelNo , short SetValue);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_set_output_all")]
        public static extern short CS_mnet_ao4_set_output_all(ushort CardNo , ushort RingNo , ushort DeviceIP , short SetValue1 , short SetValue2 , short SetValue3 , short SetValue4);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_set_coarse_gain")]
        public static extern short CS_mnet_ao4_set_coarse_gain(ushort CardNo , ushort RingNo , ushort DeviceIP , byte ChannelNo , short SetValue);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_set_fine_gain")]
        public static extern short CS_mnet_ao4_set_fine_gain(ushort CardNo , ushort RingNo , ushort DeviceIP , byte ChannelNo , short SetValue);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_set_offset")]
        public static extern short CS_mnet_ao4_set_offset(ushort CardNo , ushort RingNo , ushort DeviceIP , byte ChannelNo , short SetValue);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_set_voltage")]
        public static extern short CS_mnet_ao4_set_voltage(ushort CardNo , ushort RingNo , ushort DeviceIP , byte ChannelNo , double Voltage);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_set_voltage_all")]
        public static extern short CS_mnet_ao4_set_voltage_all(ushort CardNo , ushort RingNo , ushort DeviceIP , double Voltage1 , double Voltage2 , double Voltage3 , double Voltage4);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_reset_DAC")]
        public static extern short CS_mnet_ao4_reset_DAC(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_set_voltage1")]
        public static extern short CS_mnet_ao4_set_voltage1(ushort CardNo , ushort RingNo , ushort DeviceIP , byte ChannelNo , double Voltage , ref short Value);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ao4_set_voltage_all1")]
        public static extern short CS_mnet_ao4_set_voltage_all1(ushort CardNo , ushort RingNo , ushort DeviceIP , double Voltage1 , double Voltage2 , double Voltage3 , double Voltage4 , ref short Value1 , ref short Value2 , ref short Value3 , ref short Value4);
    }
}