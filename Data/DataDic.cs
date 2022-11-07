using System.Text;

namespace CSVLevelGen.Application;

public class DataDic
{
    public Dictionary<string, List<Data>> DataDictionary;

    public DataDic()
    {
        DataDictionary = new Dictionary<string, List<Data>>();
    }
}

public class Data
{
    public string Icon;
    public string? Title;
    public string? Description;
    public List<LevelRequirement>? LevelRequirements;
}


public  class LevelRequirement
{
    public int Amount;
    public int Reward;
    

    public LevelRequirement(int amount, int reward)
    {
        Amount = amount;
        Reward = reward;
    }
}