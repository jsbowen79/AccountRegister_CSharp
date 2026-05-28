namespace CheckRegister.Models; 

public class MoneyMarketAccount: Account
{
    public MoneyMarketAccount(
        string currencyType,
        string acctName,
        Type acctType,
        string acctOwner
) : base(currencyType, acctName, acctType, acctOwner) { }

 public MoneyMarketAccount(
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

    public override string GetAccountType()
    {
        return "MoneyMarket";
    }  
}