namespace CheckRegister.Models;

using System.Security.Cryptography.X509Certificates;
using CheckRegister.Services;



class Transaction
{

    public enum TypeTrans
    {
        Credit,
        Debit

    }

    public enum Status
    {
        Complete,
        Pending,
        Declined

    }

    public enum Medium
    {
        Cash,
        Credit,
        Transfer,
        ATM,
        Check

    }
    public decimal EndingBalance { get; internal set; }
    public decimal Amount { get; private set; }
    public Medium TransMedia { get; private set; }

    public CategoryNode.Category Category { get; private set; }
    public int TransId { get; private set; }
    public int AcctId { get; private set; }
    public TypeTrans TransType { get; private set; }
    public string? TransMemo { get; private set; }
    public DateTime TransDate { get; private set; }
    public Status TransStatus { get; internal set; }
    public Transaction(
        decimal amount,
        Medium transMedia,
        CategoryNode.Category category,
        int transId,
        int acctId,
        TypeTrans transType,
        string? transMemo = null
    )
    {
        if (amount > 0)
        {
            Amount = amount;
        }
        else throw new ArgumentException();

        TransMedia = transMedia;
        Category = category;
        TransId = transId;
        AcctId = acctId;
        TransType = transType;
        TransStatus = this.DetermineStatus();
        TransMemo = transMemo;
        TransDate = DateTime.Now;
        EndingBalance = 0; 

    }


    public Status DetermineStatus()
    {
        if (this.TransType == TypeTrans.Credit) {
            switch (this.TransMedia) {
                case Medium.Cash:
                case Medium.Transfer:
                    return Status.Complete;

                case Medium.Credit:
                    return Status.Declined;
                case Medium.ATM:
                case Medium.Check:
                    return Status.Pending;
                default:
                    return Status.Declined;
            };
        } else {
            return Status.Complete;
        };
    }

    public string formatTransactions(Transaction transaction) {
        return $"{"Date",-60}{"Transaction Type",-18}{"Transaction Media",20}" +
        $"{"Amount",20:C}{"Balance",20:C}{"Category",-20}{"Memo",-50}\n" +
            $"{transaction.TransDate.ToString("MM/dd/yyyy"),-60}{transaction.TransType.ToString(),-18}" +
            $"{transaction.TransMedia.ToString(),-20}{transaction.Amount.ToString(),20}{transaction.EndingBalance.ToString(),20}" +
            $"{(transaction.Category.ToString(), -20)}{transaction.TransMemo?.ToString() ?? "",-50}" +
            "\n";
    }

    public string FormatTransactions(Transaction[] transactions) {
        string formattedTransactions = ""; 
        foreach (var transaction in transactions) {
           formattedTransactions += $"{transaction.TransDate.ToString("MM/dd/yyyy"),-60}{transaction.TransType.ToString(),-18}" +
            $"{transaction.TransMedia.ToString(),-20}{transaction.Amount.ToString(),20}{transaction.EndingBalance.ToString(),20}" +
            $"{(transaction.Category.ToString(), -20)}{transaction.TransMemo?.ToString() ?? "",-50}" +
            "\n";
        };

        string formattedHeader = $"{"Date",-60}{"Transaction Type",-18}{"Transaction Media",20}" +
        $"{"Amount",20:C}{"Balance",20:C}{"Category",-20}{"Memo",-50}\n";
        return formattedHeader + formattedTransactions;  
  
    }
 



}