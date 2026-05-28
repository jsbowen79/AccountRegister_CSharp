namespace CheckRegister.Services;
using System.Dynamic;
using CheckRegister.Models;
public static class AccountMapper{

   public static Account ToAccount(AccountDto dto)
{
    switch (dto.AcctType)
    {
        case Account.Type.Checking:

            return new CheckingAccount(
                dto.CurrencyType,
                dto.AcctName,
                dto.AcctType,
                dto.AcctOwner,

                dto.Balance,
                dto.PendingBalance,

                dto.AcctStatus,

                dto.CreatedAt,

                dto.AcctNumber,
                dto.AcctId,

                dto.Transactions ?? new List<Transaction>(),
                dto.UsedId ?? new List<int>()
            );

        case Account.Type.Savings:

            return new SavingsAccount(
                dto.CurrencyType,
                dto.AcctName,
                dto.AcctType,
                dto.AcctOwner,

                dto.Balance,
                dto.PendingBalance,

                dto.AcctStatus,

                dto.CreatedAt,

                dto.AcctNumber,
                dto.AcctId,

                dto.Transactions ?? new List<Transaction>(),
                dto.UsedId ?? new List<int>()
            );

        case Account.Type.MoneyMarket:

            return new MoneyMarketAccount(
                dto.CurrencyType,
                dto.AcctName,
                dto.AcctType,
                dto.AcctOwner,

                dto.Balance,
                dto.PendingBalance,

                dto.AcctStatus,

                dto.CreatedAt,

                dto.AcctNumber,
                dto.AcctId,

                dto.Transactions ?? new List<Transaction>(),
                dto.UsedId ?? new List<int>()
            );

        case Account.Type.Credit:

            return new CreditAccount(
                dto.CurrencyType,
                dto.AcctName,
                dto.AcctType,
                dto.AcctOwner,

                dto.Balance,
                dto.PendingBalance,

                dto.AcctStatus,

                dto.CreatedAt,

                dto.AcctNumber,
                dto.AcctId,

                dto.Transactions ?? new List<Transaction>(),
                dto.UsedId ?? new List<int>()
            );

        default:
            throw new Exception("Unknown account type.");
    }
}
   public static AccountDto ToDto(Account account)
{
    return new AccountDto
    {
        Balance = account.Balance,
        PendingBalance = account.PendingBalance,

        CurrencyType = account.CurrencyType,

        AcctStatus = account.AcctStatus,

        CreatedAt = account.CreatedAt,

        Transactions = account.Transactions,

        AcctId = account.AcctId,

        UsedId = account.UsedId,

        AcctName = account.AcctName,

        AcctType = account.AcctType,

        AcctOwner = account.AcctOwner,

        AcctNumber = account.AcctNumber
    };
}

}