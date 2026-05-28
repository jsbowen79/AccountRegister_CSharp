namespace CheckRegister.Services;
using System.Dynamic;
using CheckRegister.Models;
/**************************************************************************************************************************************************
* This class provides the logic for taking the information in the Account Data Transfer Object (AccountDto) class and creating the appropriate  
* Account Object with it depending on the AccountType.  This ensures that all accounts are recreated as Savings, Checking, MoneyMarket, or Credit
* Accounts.  It also initializes the list objects in the accounts to prevent deserialization errors in the repopulation of those lists. 
*
*
****************************************************************************************************************************************************/

public static class AccountMapper
{
    //Provides logic for transformation of DTO to an assembled Account object.
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

    //Provides logic for the destruction of an Account Object and creation of an 
    //Account Data Transfer Object (Dto)
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