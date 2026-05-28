namespace CheckRegister.Models;


/**************************************************************************************************************************************************
* Child class of Account.  Provides constructors to initialize an account for a Checking Account type.  
*
*
*
*
****************************************************************************************************************************************************/
public class CheckingAccount : Account
{
    public CheckingAccount(
        string currencyType,
        string acctName,
        Type acctType,
        string acctOwner
) : base(currencyType, acctName, acctType, acctOwner) { }

    public CheckingAccount(
       string currencyType,
       string acctName,
       Type acctType,
       string acctOwner,

       decimal balance,
       decimal pendingBalance,

       AccountStatus acctStatus,

       DateTime createdAt,

       int acctNumber,
       int acctId,

       List<Transaction> transactions,
       List<int> usedId)

       : base(
           currencyType,
           acctName,
           acctType,
           acctOwner,

           balance,
           pendingBalance,

           acctStatus,

           createdAt,

           acctNumber,
           acctId,

           transactions,
           usedId)
    {
    }
    // Override to provide account type Checking. 
    public override string GetAccountType()
    {
        return "Checking";
    }
}