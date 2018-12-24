
namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard
{
    public class CardInfo
    {
        public CardInfo()
        {
            this.CardNo = 0;
            this.ConnectStatus = "No Mode";
            this.InitialStatus = "Not Initial";
        }

        public ushort CardNo { get; set; }

        public string ConnectStatus { get; set; }
        
        public string InitialStatus { get; set; }
    }
}
