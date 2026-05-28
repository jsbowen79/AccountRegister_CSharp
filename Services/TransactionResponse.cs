namespace  CheckRegister.Services; 
using System.Dynamic;

/**************************************************************************************************************************************************
* This class creates a response object that is returned when a transaction is requested.  The object is used to share the transaction status 
* (success: boolean) and provide the user with a customized message based on the status of the transaction request.  This was necessary to provide
* users with all needed information after a transaction request.
*
*
****************************************************************************************************************************************************/
public class TransactionResponse
{
    public bool Success { get;  }
    public string Message { get; } 
public TransactionResponse(bool success, string message){
    Success = success;
    Message = message; 
}
}