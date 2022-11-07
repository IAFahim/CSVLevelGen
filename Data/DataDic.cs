using System.Text;

namespace CSVLevelGen.Application;

public class AchievementDataList
{
    public Dictionary<string, Data> Achievements;

    public AchievementDataList()
    {
        Achievements = new Dictionary<string, Data>();
    }
}

public class Data
{
    public string Icon;
    public string Title;
    public string Description;
    public List<LevelRequirement>? LevelRequirements;
}


public abstract class LevelRequirement
{
    public int Amount;
    public int Reward;
}