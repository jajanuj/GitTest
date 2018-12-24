using System.Runtime.InteropServices;

namespace AOISystem.Utilities.Modules.Syntek.L132.Library
{
    public class CPCI_L132
    {
        //Hardware Initial
        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_open")]
        public static extern short CS_l132_open(ref short existcard);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_close")]
        public static extern void CS_l132_close(short CardNo);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_dsp_pci_boot")]
        public static extern short CS_l132_dsp_pci_boot(ushort CardNo);

        //High Speed Trigger
        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_position_cmp")]
        public static extern short CS_l132_position_cmp(ushort CardNo , int start , int end , uint interval);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_reset_cpld_position")]
        public static extern short CS_l132_reset_cpld_position(ushort CardNo , ushort AxisNo);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_set_trigger_interval")]
        public static extern short CS_l132_set_trigger_interval(ushort CardNo , ushort enable);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_get_Para_data")]
        public static extern short CS_l132_get_Para_data(ushort CardNo , ushort para_no , ref int data);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_position_cmp_auto_supply")]
        public static extern short CS_l132_position_cmp_auto_supply(ushort CardNo , uint start , uint end , uint interval , uint count);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_position_cmp_table")]
        public static extern short CS_l132_position_cmp_table(ushort CardNo , ref int TriggerTable , uint Count);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_get_cmp_times")]
        public static extern short CS_l132_get_cmp_times(ushort CardNo , ref int cmp_times);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_get_cmp_position")]
        public static extern short CS_l132_get_cmp_position(ushort CardNo , ref int cmp_pos);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_get_cmp_param")]
        public static extern short CS_l132_get_cmp_param(ushort CardNo , ref int start , ref int end , ref int interval , ref int end2);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_get_trigger_interval")]
        public static extern short CS_l132_get_trigger_interval(ushort CardNo , ref ushort enable);

        // Local I/O Control
        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_write_master_gpio")]
        public static extern short CS_l132_write_master_gpio(ushort CardNo , ushort value);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_read_master_gpio")]
        public static extern short CS_l132_read_master_gpio(ushort CardNo , ref ushort value);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_word_write")]
        public static extern short CS_l132_word_write(short CardNo , uint addr , ushort value);

        [DllImport("PCI_L132.dll" , EntryPoint = "_l132_word_read")]
        public static extern short CS_l132_word_read(short CardNo , uint addr , ref ushort value);
    }
}