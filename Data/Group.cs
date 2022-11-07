namespace CSVLevelGen.Data;

public class Group
{
    public readonly Dictionary<string, List<Data>> AchievementGroup;

    public Group()
    {
        AchievementGroup = new Dictionary<string, List<Data>>();
    }
}

public class Data
{
    public string Icon;
    public string? Title;
    public string? Description;
    public List<LevelRequirement>? LevelRequirements;

    public override string ToString()
    {
        return $"{Icon} {Title} {Description} {LevelRequirements}";
    }
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

    public override string ToString()
    {
        return $"{Amount} {Reward}";
    }
}