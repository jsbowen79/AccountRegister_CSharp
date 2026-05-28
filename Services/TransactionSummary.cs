namespace CheckRegister.Services;

using System.Reflection;
using System.Security.Cryptography.X509Certificates;

/**************************************************************************************************************************************************
* This is a demo of a C# structure.  It is used to summarize the results of a request to view transactions.  It collects the transaction count and 
* total and provides a method to display them in readable format for the user.  
*
*
*
****************************************************************************************************************************************************/
public readonly struct TransactionSummary
{
    public int Count { get; }
    public decimal Total { get; }

    public TransactionSummary(int count, decimal total)
    {
        Count = count;
        Total = total;
    }

    public override string ToString()
    {
        return $"Transactions: {Count} , Total: {Total:C}";
    }
}

