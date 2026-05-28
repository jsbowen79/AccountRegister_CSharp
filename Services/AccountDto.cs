namespace CheckRegister.Services;
using System.Dynamic;
using CheckRegister.Models;
/**************************************************************************************************************************************************
* This class provides the skeleton for a deconstructed Account object.  It contains all of the information necessary with public getters and setters
* to allow for the deconstruction and reconstruction of an Account object.  It is used in the process of saving and loading files.  This construct
* is necessary because JSON Serialization does not manage relationships between derived classes.  As such many of the fields fail to be populated 
* during deserialization unless they are reconstructed manually.  
*
****************************************************************************************************************************************************/

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