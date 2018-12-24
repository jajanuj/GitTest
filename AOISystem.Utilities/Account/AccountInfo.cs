
namespace AOISystem.Utilities.Account
{
    public class AccountInfo
    {
        public AccountInfo()
        {
        }
        public string Name { get; set; }
        public string Password { get; set; }
        public AccountLevel Level { get; set; }
    }

    public enum AccountLevel
    {
        Operator,
        Engineer,
        Administrator,
        Developer
    }
}
