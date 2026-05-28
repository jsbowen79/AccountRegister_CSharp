namespace CheckRegister.Services;
using System.Dynamic;
using CheckRegister.Models;

public class AccountDto
{
    public decimal Balance { get; set; }
    public decimal PendingBalance { get; set; }
    public string CurrencyType { get; set; }
    public Account.AccountStatus AcctStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
    public int AcctId { get; set; }
    public List<int> UsedId { get; set; } = new(); 
    public string AcctName { get; set; }
    public Account.Type AcctType { get; set; }
    public string AcctOwner { get; set; }
    public int AcctNumber { get; set; }
}