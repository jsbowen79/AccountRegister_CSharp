namespace CheckRegister.Models;

using System.Security.Cryptography.X509Certificates;
using CheckRegister.Services;

/**************************************************************************************************************************************************
* This class defines and creates the structure for a transaction which is stored in an account.  All transactions are created using this class
* prior to being added to an account.  This ensures that transactions are uniform and meet information standards.  Contains Enums to define Transaction
* types, transaction status, and transaction mediums.  Contains methods determineStatus and formatTransactions.  DetermineStatus method analyzes 
* rules to determine if the status of a transaction should be pending, Complete, or Declined.  Returns status so that it can be utilized in the 
* calculate balance method to provide accurate balances and pendingBalances.
****************************************************************************************************************************************************/

public class Transaction
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

    // Determines and returns the status of a transaction according to predefined
    // banking rules. 

    public Status DetermineStatus()
    {
        if (this.TransType == TypeTrans.Credit)
        {
            switch (this.TransMedia)
            {
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
            }
            ;
        }
        else
        {
            return Status.Complete;
        }
        ;
    }

    //Formats a readable string for a single transaction. 
    public string formatTransactions(Transaction transaction)
    {
        string summary = new TransactionSummary(1, transaction.Amount).ToString();

        string formattedString = $"{"Date",-60}{"Transaction Type",-18}{"Transaction Media",20}" +
         $"{"Amount",20:C}{"Balance",20:C}{"Category",-20}{"Memo",-50}\n" +
             $"{transaction.TransDate.ToString("MM/dd/yyyy"),-60}{transaction.TransType.ToString(),-18}" +
             $"{transaction.TransMedia.ToString(),-20}{transaction.Amount.ToString(),20}{transaction.EndingBalance.ToString(),20}" +
             $"{(transaction.Category.ToString(), -20)}{transaction.TransMemo?.ToString() ?? "",-50}" +
             "\n";
        formattedString += summary;
        return formattedString;
    }
    //formats a readable string for multiple transactions. 
    public static string FormatTransactions(List<Transaction> transactions) {
        string formattedTransactions = ""; 
        foreach (var transaction in transactions) {
           formattedTransactions += $"{transaction.TransDate.ToString("MM/dd/yyyy"),-60}{transaction.TransType.ToString(),-18}" +
            $"{transaction.TransMedia.ToString(),-20}{transaction.Amount.ToString(),20}{transaction.EndingBalance.ToString(),20}" +
            $"{(transaction.Category.ToString(), -20)}{transaction.TransMemo?.ToString() ?? "",-50}" +
            "\n";
        };

        string formattedHeader = $"{"Date",-60}{"Transaction Type",-18}{"Transaction Media",20}" +
        $"{"Amount",20:C}{"Balance",20:C}{"Category",-20}{"Memo",-50}\n";

        string summary = new TransactionSummary(transactions.Count(), transactions.Sum(t => t.Amount)).ToString();
        return formattedHeader + formattedTransactions + summary;  
  
    }
 



}