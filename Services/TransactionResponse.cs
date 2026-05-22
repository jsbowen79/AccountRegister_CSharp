namespace  CheckRegister.Services; 
using System.Dynamic;


public class TransactionResponse
{
    public bool Success { get;  }
    public string Message { get; } 
public TransactionResponse(bool success, string message){
    Success = success;
    Message = message; 
}
}