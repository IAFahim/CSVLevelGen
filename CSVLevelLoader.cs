using System.Net.Sockets;
using System.Text;
using CSVLevelGen.Application;
using CSVLevelGen.Data;

namespace CSVLevelGen;

public class CsvLevelLoader
{
    private Group _group;
    private StreamReader _stream;
    private char separator = '\t';
    public string Path;


    public CsvLevelLoader(string path, char separator)
    {
        Path = Application.DataPath.ToString() + path;
        if (!File.Exists(Path))
        {
            Console.WriteLine("File not found");
            return;
        }
        this.separator = separator;
    }

    public Group ReadCSVSetCSV()
    {
        _group = new Group();
        _stream = new StreamReader(Path);
        while (!_stream.EndOfStream)
        {
            Line = _stream.ReadLine()?.Split(separator);
            LoadType(0);
            if (!_stream.EndOfStream && Line[0] != "")
            {
                while (!loadTitle(2))
                {
                    Line = _stream.ReadLine()?.Split(separator);
                }
                _stream.ReadLine();
                if (!loadDescription(2)) continue;
                LoadAmountAndReward(3);
            }
        }
        _stream.Close();
        return _group;
    }


    private bool LoadAmountAndReward(int index)
    {
        _group.AchievementGroup[currentType!][variant].LevelRequirements = new List<LevelRequirement>();
        for (int i = index; i < Line?.Length; i += 2)
        {
            if (Line[i] != "")
            {
                LevelRequirement levelRequirement = new LevelRequirement(int.Parse(Line[i] ?? string.Empty),
                    int.Parse(Line[i + 1] ?? String.Empty));
                _group.AchievementGroup[currentType][variant].LevelRequirements?.Add(levelRequirement);
            }
        }

        return _group.AchievementGroup[currentType][variant].LevelRequirements?.Count > 0;
    }

    private bool loadDescription(int index)
    {
        Line = _stream.ReadLine()?.Split(separator);
        string? words = Line?[index];
        if (currentType != null &&
            words is { Length: > 0 } &&
            _group.AchievementGroup[currentType][variant].Title != "")
        {
            _group.AchievementGroup[currentType][variant].Description = words;
            return LoadAmountAndReward(index + 1);
        }

        return false;
    }


    private string? currentType = "";
    private string?[]? Line;
    private int variant = 0;

    private bool LoadType(int index)
    {
        string? words = Line?[index];
        if (words is { Length: > 0 } && words[0] != '#')
        {
            _group.AchievementGroup[words] = new List<Data.Data>();
            variant = -1;
            currentType = words;
            return true;
        }

        return false;
    }

    private bool loadTitle(int index)
    {
        string? words = Line?[index];
        if (words is { Length: > 0 } && currentType.Length > 0)
        {
            Data.Data data = new Data.Data
            {
                Title = words
            };
            _group.AchievementGroup[currentType].Add(data);
            variant++;
            return true;
        }

        return false;
    }
}