//This class is static because we only need to track one set of scrap,
//and for ease of access throughout the game
public static class Scrap
{
    //how much scrap the player has this round
    private static int amount;

    /// <summary>
    /// Get how much scrap the player currently has.
    /// </summary>
    public static int Amount => amount;

    /// <summary>
    /// Try to expend an amount of scrap. Returns false if scrap is too low. Else, reduces scrap by amount and returns true.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static bool TryToSpend(int amount)
    {
        if (Scrap.amount < amount)
        return false;
        
        Scrap.amount -= amount;
        return true;
    }

    /// <summary>
    /// Increase the current amount of scrap by amount.
    /// </summary>
    /// <param name="amount"></param>
    public static void Add(int amount)
    {
        Scrap.amount += amount;
    }

}
