namespace CheckRegister.Models; 
using CheckRegister.Models;

/**************************************************************************************************************************************************
* This Class is the heart of the application.  It provides global variables that the application needs in various classes in the application. 
* This Class allows for these variables to be centrally accessed and modified.  This allows the program to handle multiple accounts simultaneously. 
* It also allows for the centralized management of Account and Transaction Id's to prevent inserting an non-unique id.  (Feature not fully functional)
*
*
****************************************************************************************************************************************************/
public class ApplicationState
{
    public List<Account> Accounts { get; set; } = new();
    public List<int> UsedAcctID { get; set; } = new();
    public List<int> UsedTransID { get; set; } = new();
    public Account? CurrentAccount { get; set; }
}