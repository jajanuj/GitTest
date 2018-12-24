
namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard
{
    public class SlaveInfo
    {
        public SlaveInfo()
        {
        }

        public ushort CardNo { get; set; }

        public ushort SeqID { get; set; }

        public ushort NodeID { get; set; }

        public uint VendorID { get; set; }

        public uint ProductCode { get; set; }

        public uint RevisionNo { get; set; }

        public uint DCTime { get; set; }
    }
}
