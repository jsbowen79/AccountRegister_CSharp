namespace CheckRegister.Services;

using System.ComponentModel;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using CheckRegister.Models;
using CheckRegister.Services;

/**************************************************************************************************************************************************
* This Class provides all of the methods called by the menu class.  These methods create relevant objects and pass control of those objects along  
* with the _appState object to the classes that will actually perform the requested actions.  When necessary, these functions gather additional
* information from the user as needed by the requested operation. 
*
*
****************************************************************************************************************************************************/

public class MenuFunctions
{

    private readonly ApplicationState _appState;
    public MenuFunctions(ApplicationState appState)
    {
        _appState = appState;
    }

    //Gathers information from user necessary to create an account.  Validates information
    // obtained from user. Calls Account Class to create account.

    public void StartCreateAccount()
    {
        string acctOwner = "";
        bool valid = false;
        string currencyType = "";
        string acctName = "";
        while (!valid)
        {
            Console.WriteLine("Please enter the Currency type that will be used.");
            currencyType = Console.ReadLine() ?? "";
            if (currencyType != "")
            {
                valid = true;
            }
            else { Console.WriteLine("Please enter a currency type (USD)."); }
            ;
        }
        ;
        valid = false;

        while (!valid)
        {
            Console.WriteLine("Please enter your Account name.");
            acctName = Console.ReadLine() ?? "";
            if (acctName != "")
            {
                valid = true;
            }
            else
            {
                Console.WriteLine("Please enter an account Name (string).");
            }

        }
        ;


        valid = false;
        while (!valid)
        {
            Console.WriteLine("Please enter the Account Owner's name: ");
            acctOwner = Console.ReadLine()?.Replace(" ", "") ?? "";
            if (acctOwner != "")
            {
                valid = true;
                continue;
            }
            else
            {
                Console.WriteLine("Please enter the Account Owner's name (string): ");
                continue;
            }
        }

        valid = false;

        while (!valid)
        {
            Account.Type acctType = Account.Type.Checking;
            Console.WriteLine("Please select your account Type. ");
            Console.WriteLine("For Checking, press 1: ");
            Console.WriteLine("For Savings, press 2: ");
            Console.WriteLine("For Money Market, press 3: ");
            Console.WriteLine("For Credit Account, press 4:");
            int selection = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            switch (selection)
            {
                case 1:
                    acctType = Account.Type.Checking;
                    Account account = new CheckingAccount(currencyType, acctName, acctType, acctOwner);
                    InitializeAccount(account);
                    valid = true;
                    break;
                case 2:
                    acctType = Account.Type.Savings;
                    account = new SavingsAccount(currencyType, acctName, acctType, acctOwner);
                    InitializeAccount(account);
                    valid = true;
                    break;
                case 3:
                    acctType = Account.Type.MoneyMarket;
                    account = new MoneyMarketAccount(currencyType, acctName, acctType, acctOwner);
                    InitializeAccount(account);
                    valid = true;
                    break;
                case 4:
                    acctType = Account.Type.Credit;
                    account = new CreditAccount(currencyType, acctName, acctType, acctOwner);
                    InitializeAccount(account);
                    valid = true;
                    break;
                default:
                    Console.WriteLine("Please choose an option 1-4.");
                    break;
            }
            ;
        }
        ;


    }

    //Adds accounts to the _appState object so that it can be accessed and used globally.
    //Provides user with a message indicating success of operation. 

    private void InitializeAccount(Account account)
    {

        _appState.Accounts.Add(account);
        _appState.CurrentAccount = account;
        Console.WriteLine($"Congratulations, you have created Account {_appState.CurrentAccount.AcctName}. ");
        Console.WriteLine("Future operations will utilize this account.");
    }

    //Gathers information necessary to add a transaction to an account.  Validates that 
    // information.  Initiates creation of the transaction.  Saves transaction to Account
    // object.  Provides user with appropriate message. 

    public async Task StartAddTransaction()
    {
        int amount = 0;
        CategoryNode.Category category = CategoryNode.Category.Uncategorized;
        Transaction.Medium transMedia = Transaction.Medium.Cash;
        Console.WriteLine($"You are currently utilizing Account {_appState.CurrentAccount!.AcctName}.");
        Console.WriteLine("Do You wish to continue?");
        Console.WriteLine("\n\nPress 1 for YES or 2 for No.");
        int response = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
        switch (response)
        {
            case 1:
                break;
            case 2:
                return;
            default:
                Console.WriteLine("Your selection was invalid!\n\n");
                return;
        }

        while (true)
        {
            Console.WriteLine("Please enter the amount for your transaction.");
            amount = int.TryParse(Console.ReadLine(), out result) ? result : 0;
            if (amount != 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input for amount.  Please input a number.");
            }
        }
        bool invalid = true;
        while (invalid)
        {
            Console.WriteLine("Please choose from the following Transaction Media Types: \n\n");
            Console.WriteLine("Press 1 for Cash ");
            Console.WriteLine("Press 2 for Credit");
            Console.WriteLine("Press 3 for Transfer");
            Console.WriteLine("Press 4 for ATM");
            Console.WriteLine("Press 5 for Check");
            response = int.TryParse(Console.ReadLine(), out result) ? result : 0;
            switch (response)
            {
                case 1:
                    transMedia = Transaction.Medium.Cash;
                    invalid = false;
                    break;
                case 2:
                    transMedia = Transaction.Medium.Credit;
                    invalid = false;
                    break;
                case 3:
                    transMedia = Transaction.Medium.Transfer;
                    invalid = false;
                    break;
                case 4:
                    transMedia = Transaction.Medium.ATM;
                    invalid = false;
                    break;
                case 5:
                    transMedia = Transaction.Medium.Check;
                    invalid = false;
                    break;
                default:
                    Console.WriteLine("Please make a valid selection (1-5).");
                    continue;
            }
        }
        ;
        Console.WriteLine("\n\nWhat kind of transaction would you like to make?");
        Console.WriteLine("Select 1 for DEPOSIT or 2 for WITHDRAWAL. ");
        response = int.TryParse(Console.ReadLine(), out result) ? result : 0;

        int acctId = _appState.CurrentAccount.AcctId;
        int transId = Utilities.FindUnusedRandomInt(_appState.UsedTransID);

        Console.WriteLine("\n\nPlease enter a memo for your transaction.");
        Console.WriteLine("Press enter to skip.");

        string transMemo = Console.ReadLine() ?? "";
        TransactionResponse transResponse;
        switch (response)
        {
            case 1:
                transResponse =
                await _appState.CurrentAccount.
                DepositAsync(transId, amount, transMedia, category, transMemo);
                break;
            case 2:
                transResponse =
                await _appState.CurrentAccount.
                WithdrawAsync(transId, amount, transMedia, category, transMemo);
                break;
            default:
                Console.WriteLine("Invalid Selection.  Please try again.");
                return;
        }

        if (transResponse.Success)
        {
            Console.WriteLine($"\n\nYour transaction is complete.  {transResponse.Message}");
            return;
        }
        else
        {
            Console.WriteLine($"Transaction failed.  {transResponse.Message}");
            return;
        }
    }

    //Prints a list of requested transactions to the console in a user readable table. 

    public async Task StartListTransactions()
    {
        if (_appState.CurrentAccount != null)
        {
            int totalTrans = _appState.CurrentAccount.Transactions.Count;
            if (totalTrans > 0)
            {
                Console.WriteLine($"\n\nThere are {totalTrans} transactions in " +
                $"{_appState.CurrentAccount.AcctName}.  How many would you like to view?");
                Console.WriteLine($"Please enter an integer between 1 and {totalTrans}.\n\n");
                int recordCount = int.TryParse(Console.ReadLine(), out int result) ? result : totalTrans;

                Console.WriteLine($"\n\nPrinting the last {recordCount} transactions.\n\n");
                string formattedString = Transaction.FormatTransactions(_appState.CurrentAccount.Transactions);
                Console.WriteLine(formattedString);
            }
            else
            {
                Console.WriteLine($"\n\nThere are no transactions in Account {_appState.CurrentAccount.AcctName}");
                Console.WriteLine("Please choose another account.\n\n");
            }
            ;
        }
        else
        {
            Console.WriteLine($"There is no active account.  Please Create" +
            " or Load an account to see transactions.");
            return;
            ;
        }

    }

    //Displays a Demo of C# Unions on the Console. 

    public static void StartUnionDemo()
    {
        NumberUnion number = new NumberUnion();

        number.IntegerValue = 1065353216;

        Console.WriteLine("\n\nClassic C Unions use the same memory space to store multiple data types.\n");
        Console.WriteLine("In this Demo, the variable 'number' is approximating the behavior of a classic C Union");
        Console.WriteLine("This means that number.IntegerValue is saved in the same location as number.float.");
        Console.WriteLine("The data representing the following values is the same data, but it represents different values based on the data Type.");

        Console.WriteLine($"\n\nnumber.integerValue: {number.IntegerValue}");
        Console.WriteLine($"number.FloatValue: {number.FloatValue}");

        Console.WriteLine("\n\n As shown, the same data represents different values in a classic C union. ");
    }


    //Gathers information necessary to load an account.  Loads the account.  Provides user with an appropriate message. 
    public async Task StartLoadAccount()
    {
        Console.WriteLine("\n\n Please enter the filename for the account you would like to load.");
        string fileName = Console.ReadLine() ?? "";
        Console.WriteLine($"\n\nAttempting to load {fileName}...\n\n");
        Account? loadedAccount = await Account.LoadAccountInfo(fileName);

        if (loadedAccount != null)
        {
            _appState.Accounts.Add(loadedAccount);
            _appState.CurrentAccount = loadedAccount;
            Console.WriteLine($"\n\nLoaded Account {_appState.CurrentAccount.AcctName} Successfully!\n\n");
            Console.WriteLine($"You have {_appState.Accounts.Count()} accounts currently loaded.\n\n");
        }
    }

    //Switches between accounts that have been created during current session or have been loaded into the 
    //session by the user.  Allows users to manage multiple accounts simultaneously.  Provides and appropriate
    //message. 

    public void SwitchAccounts()
    {
        Account? desiredAccount;
        Console.WriteLine("You currently have the following Active Accounts:");
        _appState.Accounts.ForEach((account) => { Console.WriteLine($"{account.AcctName}"); });

        Console.WriteLine("Please enter the name of the account you would like to use.");
        string response = Console.ReadLine() ?? "";
        if (response != "")
        {
            desiredAccount = _appState.Accounts.FirstOrDefault((account) => account.AcctName == response);
            if (desiredAccount != null)
            {
                _appState.CurrentAccount = desiredAccount;
                Console.WriteLine($"Loaded Account {_appState.CurrentAccount.AcctName}\nYou are now working with that account.");
                return;
            }
            else
            {
                Console.WriteLine("That account does not exist.  Please try again. ");
                return;
            }
        }
        else
        {
            Console.WriteLine("You must make an entry.  Please try again. ");
        }
    }




}