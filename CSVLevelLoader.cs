using System.Net.Sockets;
using System.Text;
using CSVLevelGen.Application;

namespace CSVLevelGen;

public class CsvLevelLoader
{
    private DataDic _dataDic;
    private StreamReader _stream;
    private char separator = '\t';


    public CsvLevelLoader(string path, char separator)
    {
        path = Application.DataPath.ToString() + path;
        if (!File.Exists(path))
        {
            Console.WriteLine("File not found");
            return;
        }

        this.separator = separator;

        _dataDic = new DataDic();
        _stream = new StreamReader(path);
        ReadCSVSetCSV();
        _stream.Close();
    }

    private void ReadCSVSetCSV()
    {
        while (!_stream.EndOfStream)
        {
            Line = _stream.ReadLine()?.Split(separator);
            LoadType(0);
            if (!_stream.EndOfStream && Line[0] != "")
            {
                if (!loadTitle(2)) continue;
                _stream.ReadLine();
                if (!loadDescription(2)) continue;
                LoadAmountAndReward(3);
            }
        }
    }


    private bool LoadAmountAndReward(int index)
    {
        _dataDic.DataDictionary[currentType!][variant].LevelRequirements = new List<LevelRequirement>();
        for (int i = index; i < Line?.Length; i += 2)
        {
            if (Line[i] != "")
            {
                LevelRequirement levelRequirement = new LevelRequirement(int.Parse(Line[i] ?? string.Empty),
                    int.Parse(Line[i + 1] ?? String.Empty));
                _dataDic.DataDictionary[currentType][variant].LevelRequirements?.Add(levelRequirement);
            }
        }

        return _dataDic.DataDictionary[currentType][variant].LevelRequirements?.Count > 0;
    }

    private bool loadDescription(int index)
    {
        Line = _stream.ReadLine()?.Split(separator);
        string? words = Line?[index];
        if (currentType != null &&
            words is { Length: > 0 } &&
            _dataDic.DataDictionary[currentType][variant].Title != "")
        {
            _dataDic.DataDictionary[currentType][variant].Description = words;
            return LoadAmountAndReward(index + 1);
        }

        return false;
    }


    public string? currentType = "";
    public string?[]? Line;
    private int variant = 0;

    public bool LoadType(int index)
    {
        string? words = Line?[index];
        if (words is { Length: > 0 } && words[0] != '#')
        {
            _dataDic.DataDictionary[words] = new List<Data>();
            variant = -1;
            currentType = words;
            return true;
        }

        return false;
    }

    public bool loadTitle(int index)
    {
        string? words = Line?[index];
        if (words is { Length: > 0 } && currentType.Length > 0)
        {
            Data data = new Data
            {
                Title = words
            };
            _dataDic.DataDictionary[currentType].Add(data);
            variant++;
            return true;
        }

        return false;
    }
}