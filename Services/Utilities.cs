namespace CheckRegister.Services;


/**************************************************************************************************************************************************
*This Class contains miscellaneous utility methods used by other classes.
*
*
*
*
****************************************************************************************************************************************************/
public class Utilities
{

    //This utility method ensures that amounts input by users are greater than 0. 
    public static bool ValidateAmount(decimal amount)
    {
        if (amount > 0)
        {
            return true;
        }
        return false;
    }

    //This utility method is used to generate random unused numbers for account and transaction id's. 

    public static int FindUnusedRandomInt(List<int> usedId)
    {
        int unusedNumber = 0;
        while (usedId.Contains(unusedNumber))
        {
            Random random = new Random();
            unusedNumber = random.Next(10000000, 99999999);
        }


        return unusedNumber;
    }
}