namespace CheckRegister.Models;

using System.Buffers;
using System.ComponentModel.Design.Serialization;
using System.Dynamic;
using System.Globalization;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using CheckRegister.Services;
using System.Text.Json;
using System.IO;

public  abstract class Account
{
    public enum Type
    {
        Checking,
        Savings,
        MoneyMarket,
        Credit

    }
    public enum AccountStatus
    {
        Open,
        BankClosed,
        CustomerClosed,
        Suspended,
        Frozen,

    }

    public decimal Balance { get; private set; }
    public decimal PendingBalance { get; private set; }
    public string CurrencyType { get; set; }
    public AccountStatus AcctStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Transaction> Transactions { get; set; } = [];

    public int AcctId { get; private set; }
    public List<int> UsedId { get; set; }
    public string AcctName { get; private set; }
    public Type AcctType { get; private set; }
    public string AcctOwner { get; private set; }
    public int AcctNumber { get; private set; }

    public Account(
        string currencyType,
        string acctName,
        Type acctType,
        string acctOwner)
    {
        CurrencyType = currencyType;
        UsedId = new List<int>();
        AcctName = acctName;
        AcctType = acctType;
        AcctOwner = acctOwner;
        Balance = 0;
        PendingBalance = 0;
        AcctStatus = AccountStatus.Open;
        CreatedAt = DateTime.Now;
        AcctId = Utilities.FindUnusedRandomInt(UsedId);
        UsedId.Add(AcctId);
        AcctNumber = Utilities.FindUnusedRandomInt(UsedId);
    }

    protected Account(
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
{
    CurrencyType = currencyType;
    AcctName = acctName;
    AcctType = acctType;
    AcctOwner = acctOwner;

    Balance = balance;
    PendingBalance = pendingBalance;

    AcctStatus = acctStatus;

    CreatedAt = createdAt;

    AcctNumber = acctNumber;

    AcctId = acctId;

    Transactions = transactions;

    UsedId = usedId;
}

    public void AddTransaction(Transaction entry)
    {
        this.Transactions.Add(entry);
    }

  //This function was designed to allow for the UI to display groups of transactions on the screen
  //using forward and back buttons.

  public List<Transaction> getTransactionHistory(int start, int qty) {
    if (qty < this.Transactions.Count-start && qty > 0) {
      return this.Transactions.GetRange(start, qty);
    } else return this.Transactions.GetRange(start, Transactions.Count-start);
  }

    private (decimal, decimal) CalculateBalance()
    {
        decimal availableBalance = 0;
        decimal pendingBalance = 0;
        foreach (Transaction entry in this.Transactions)
        {

            if (entry.TransType == Transaction.TypeTrans.Credit)
            {
                if (entry.TransStatus == Transaction.Status.Complete)
                {
                    availableBalance += entry.Amount;
                    pendingBalance += entry.Amount;
                    entry.EndingBalance = availableBalance;
                }
                else if (entry.TransStatus == Transaction.Status.Pending)
                {
                    pendingBalance += entry.Amount;
                    entry.EndingBalance = availableBalance;
                }
            }
            else if (entry.TransType == Transaction.TypeTrans.Debit)
            {
                if (entry.TransStatus == Transaction.Status.Complete)
                {
                    availableBalance -= entry.Amount;
                    pendingBalance -= entry.Amount;
                    entry.EndingBalance = availableBalance;
                }
                else if (entry.TransStatus == Transaction.Status.Pending)
                {
                    pendingBalance -= entry.Amount;
                    entry.EndingBalance = availableBalance;
                }
                else if (entry.TransStatus == Transaction.Status.Declined)
                {
                    entry.EndingBalance = availableBalance;
                }
            }
        }

        return (availableBalance, pendingBalance);
    }
  
  public async Task<Services.TransactionResponse> SaveAccountInfo() {
        string message;
    try {
            AccountDto dto = AccountMapper.ToDto(this);
      string jsonData = JsonSerializer.Serialize(dto,
      new JsonSerializerOptions
      {
          WriteIndented = true
      });

      await File.WriteAllTextAsync($"{this.AcctName}.json", jsonData);
            Console.WriteLine("File has been Saved.");
            message = $"{this.AcctName}.json has been successfully saved.";
    }catch (Exception) {
            message = "There was an error saving the file.";
            throw new ArgumentException(); 
    }

      return new Services.TransactionResponse(true,  message);
    } 


 public static async Task<Account?> LoadAccountInfo(string filename)
    {
        try
        {
            string jsonString = await File.ReadAllTextAsync(filename);
           AccountDto? dto  =
            JsonSerializer.Deserialize<AccountDto>(jsonString);
            if (dto == null)
            {
                throw new Exception("Load failed.");
            }
            Account account = AccountMapper.ToAccount(dto);
            account.CalculateBalance(); 
        return account; 
        } catch (Exception ex)
        {
            Console.WriteLine($"Load Failed {ex.Message}");
            return null; 
        }
    }




    public async Task<TransactionResponse> DepositAsync(
        int transId,
        decimal amount,
        Transaction.Medium transMedia,
        CategoryNode.Category category,
        string transMemo = "" )
    {
        if (Utilities.ValidateAmount(amount))
        {
            Transaction entry = new Transaction(
              amount,
              transMedia,
              category,
              transId,
              this.AcctId,
              Transaction.TypeTrans.Credit,
              transMemo ?? null
            );
            entry.DetermineStatus();
            this.AddTransaction(entry);

            (decimal, decimal) balances = this.CalculateBalance();
            (this.Balance, this.PendingBalance) = balances;
            Console.WriteLine($" this balances: {this.Balance}  {this.PendingBalance}");
            await this.SaveAccountInfo();
            return new Services.TransactionResponse(true,
            $"Your new available balance is {this.Balance}. Your balance Pending is {this.PendingBalance}.  Thank you!");
        }
        else
        {
            return new Services.TransactionResponse(false,
              "Amount is invalid.  Please try again.");
        }

    }


    public async Task<TransactionResponse> WithdrawAsync(
      int transId,
      Decimal amount,
      Transaction.Medium transMedia,
      CategoryNode.Category category,
      string transMemo = "")
    {
        if (!Utilities.ValidateAmount(amount))
        {
            return new Services.TransactionResponse(false, "Amount is invalid.  Please try again");
        }
        Transaction.TypeTrans transType = Transaction.TypeTrans.Debit;
        Transaction entry = new Transaction(
          amount,
          transMedia,
          category,
          transId,
          this.AcctId,
          transType,
          transMemo ?? null
        );
        if (amount <= this.Balance)
        {
            string message = entry.DetermineStatus().ToString();
            if (entry.TransStatus != Transaction.Status.Declined)
            {
                this.AddTransaction(entry);
                (Decimal, Decimal) balances = this.CalculateBalance();
                (this.Balance, this.PendingBalance) = balances;
                await this.SaveAccountInfo();
                return new TransactionResponse(true,
                $"Your new available balance is {this.Balance}. Your balance Pending is {this.PendingBalance}.  Thank you!");
            }
            else
            {
                this.AddTransaction(entry);
                await this.SaveAccountInfo();
                return new TransactionResponse(false,
                $"Sorry, your transaction status was {message}. Please check your information and try again.");
            }
        }
        else
        {
            entry.TransStatus = Transaction.Status.Declined;
            this.AddTransaction(entry);
            this.CalculateBalance();
            await this.SaveAccountInfo();
            return new TransactionResponse(false, "Transaction declined--Insufficient funds");
        }

    }
    
    public abstract string GetAccountType();

}