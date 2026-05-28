namespace CheckRegister.Models; 
using CheckRegister.Models;

public class ApplicationState
{
    public List<Account> Accounts { get; set; } = new();
    public List<int> UsedAcctID { get; set; } = new();
    public List<int> UsedTransID { get; set; } = new(); 
    public Account? CurrentAccount { get; set; }
}