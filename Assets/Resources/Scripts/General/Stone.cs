public class Stone
{
    private int stoneAmount;

    public Stone(int amount)
    {
        this.stoneAmount = amount;
    }

    public void addStone(int amount)
    {
        this.stoneAmount += amount;
    }

    public int getAmount()
    {
        return this.stoneAmount;
    }
}

