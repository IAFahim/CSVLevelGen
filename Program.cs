namespace CSVLevelGen;

static class Program
{
    static void Main(string[] args)
    {
        CsvLevelLoader loader = new CsvLevelLoader("Temple Of Mask Achievement System - Sheet1.tsv", '\t');
        var groups = loader.ReadCSVSetCSV();
        Console.WriteLine(groups.LevelGroup["Collector"][0].Description);
        Console.WriteLine(groups.LevelGroup["Collector"][1].Description);
    }
}