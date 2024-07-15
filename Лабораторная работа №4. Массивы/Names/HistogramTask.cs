using System.Net.Http.Headers;

namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        string[] days = new string[31];
        for (int i = 0; i < 31; i++)
        {
            days[i] = (i + 1).ToString();
        }
        var countDay = new double[31];
        foreach (var item in names)
        {
            if (item.Name == name)
                countDay[item.BirthDate.Day - 1]++;
        }
        countDay[0] = 0;
        return new HistogramData($"Рождаемость людей с именем '{name}'", days, countDay);
    }
}