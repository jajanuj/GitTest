using System.Runtime.InteropServices;

namespace AOISystem.Utilities.Modules.Syntek.L132.Library
{
    public class CA180_L132
    {
        //====== API Function  definition =====================
        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_write_register")]
        public static extern short CS_mnet_ai8_write_register(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort CmdNo , ushort Data);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_Read_Register")]
        public static extern short CS_mnet_ai8_Read_Register(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort CmdNo , ref ushort Data);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_initial")]
        public static extern short CS_mnet_ai8_initial(ushort CardNo , ushort RingNo , ushort DeviceIP);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_enable_channel")]
        public static extern short CS_mnet_ai8_enable_channel(ushort CardNo , ushort RingNo , ushort SlaveIP , ushort ChannelNo , byte Enable);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_set_channel_gain")]
        public static extern short CS_mnet_ai8_set_channel_gain(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort ChannelNo , byte Gain);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_get_value")]
        public static extern short CS_mnet_ai8_get_value(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort ChannelNo , ref short GetValue);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_set_cycle_time")]
        public static extern short CS_mnet_ai8_set_cycle_time(ushort CardNo , ushort RingNo , ushort DeviceIP , byte SetValue);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_get_voltage")]
        public static extern short CS_mnet_ai8_get_voltage(ushort CardNo , ushort RingNo , ushort DeviceIP , ushort ChannelNo , ref double Voltage);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_set_reference")]
        public static extern short CS_mnet_ai8_set_reference(ushort CardNo , ushort RingNo , ushort DeviceIP , byte Data);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_get_hardware_info")]
        public static extern short CS_mnet_ai8_get_hardware_info(ushort CardNo , ushort RingNo , ushort DeviceIP , ref byte DeviceID , ref byte VHDL_Version);

        [DllImport("PCI_L132.dll" , EntryPoint = "_mnet_ai8_enable_device")]
        public static extern short CS_mnet_ai8_enable_device(ushort CardNo , ushort RingNo , ushort DeviceIP , byte EnableDevice);
    }
}