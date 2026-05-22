namespace CheckRegister.Services;

public class Utilities
{
    public static bool ValidateAmount(decimal amount)
    {
        if (amount > 0)
        {
            return true;
        }
        return false;
    }


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