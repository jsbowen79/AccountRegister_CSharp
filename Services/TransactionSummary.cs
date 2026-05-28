namespace CheckRegister.Services;

using System.Reflection;
using System.Security.Cryptography.X509Certificates;

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

