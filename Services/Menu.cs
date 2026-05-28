namespace CheckRegister.Services; 
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CheckRegister.Models;
using CheckRegister.Services;

/**************************************************************************************************************************************************
* This class creates and manages the Menu for the Console base CLI User Interface. It creates the program loop, provides options, and calls
* functions to complete user requests. 
*
*
*
****************************************************************************************************************************************************/

public class Menu
{
    private readonly MenuFunctions _functions;
    public Menu(ApplicationState appState)
    {
        _functions = new MenuFunctions(appState);
    }


    public async Task DisplayMenu()
    {
        Console.WriteLine("Welcome to CheckRegister!\n");
        Console.WriteLine("This program will provide you with the ability to create financial " +
        "accounts and track banking transactions!\n\n");

        while (true)
        {

            Console.WriteLine("\n\nPlease Select from the options below.");
            Console.WriteLine("To create an account, press 1: \n");
            Console.WriteLine("To add a transaction to an account, press 2: \n");
            Console.WriteLine("To see a list of Transactions, press 3:\n");
            Console.WriteLine("To Switch to a different account, press 4: \n");
            Console.WriteLine("To Load and existing account, press 5:\n");
            Console.WriteLine("To Exit the program, press 6: \n");
            Console.WriteLine("To see a C# Union Demo, press 7:");

            int option = int.TryParse(Console.ReadLine(), out int result) ? result : 0;

            switch (option)
            {
                case 1:
                    _functions.StartCreateAccount();
                    break;
                case 2:
                    await _functions.StartAddTransaction();
                    break;
                case 3:
                    await _functions.StartListTransactions();
                    break;
                case 4:
                    _functions.SwitchAccounts();
                    break;
                case 5:
                    await _functions.StartLoadAccount();
                    break;
                case 6:
                    Console.WriteLine("\n\nThank you for using CheckRegister...Goodbye!");
                    return;
                case 7:
                    MenuFunctions.StartUnionDemo();
                    break;
                default:
                    Console.WriteLine("Please make a valid selection 1-6.");
                    break;
            }
            continue;
        }
    }

}