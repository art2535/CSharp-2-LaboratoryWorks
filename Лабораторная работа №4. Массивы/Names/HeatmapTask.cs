namespace Names;

internal static class HeatmapTask
{
    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        string[] days = new string[31];
        for (int i = 0; i < 31; i++)
        {
            days[i] = (i + 1).ToString();
        }

        string[] months = new string[12];
        for (int i = 0; i < 12; i++)
        {
            months[i] = (i + 1).ToString();
        }

        var counts = new double[31, 12];
        foreach(var item in names)
        {
            counts[item.BirthDate.Day - 1, item.BirthDate.Month - 1]++;
        }
        return new HeatmapData("Пример карты интенсивностей", counts, days, months);
    }
}