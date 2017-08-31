public class Population
{
    private int currentPopulation;
    private int maxPopulation;

    public Population(int maxPopulation)
    {
        this.currentPopulation = 0;
        this.maxPopulation = maxPopulation;
    }

    public void addPopulation(int addedPopulation)
    {
        this.currentPopulation += addedPopulation;
    }

    public int getPopulation()
    {
        return this.currentPopulation;
    }

    public int getMaxPopulation()
    {
        return this.maxPopulation;
    }
}

