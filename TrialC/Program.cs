using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine();
        var modifiedString = GetModifiedString(input);
        Console.WriteLine(modifiedString);
    }

    static string GetModifiedString(string input)
    {
        const string yandex = "Yandex";
        const string cup = "Cup";

        Dictionary<int, int> yandexCostToMinIndex = new Dictionary<int, int>();
        Dictionary<int, int> cupCostToMaxIndex = new Dictionary<int, int>();

        char[] chars = input.ToCharArray(); // Преобразуем строку в массив символов для удобной модификации

        for (int i = 0; i < chars.Length; i++)
        {
            if (i + yandex.Length <= chars.Length && yandexCostToMinIndex.Count < 6)
            {
                int yandexCost = CalculateChangeCost(chars.Skip(i).Take(yandex.Length), yandex);
                if (!yandexCostToMinIndex.ContainsKey(yandexCost))
                    yandexCostToMinIndex.Add(yandexCost, i);
            }

            if (i >= yandex.Length && i + cup.Length <= chars.Length)
            {
                int cupCost = CalculateChangeCost(chars.Skip(i).Take(cup.Length), cup);
                cupCostToMaxIndex[cupCost] = i;
            }
        }

        int yandexStartIndex = 0;
        int cupStartIndex = 0;

        for (int totalCost = 0; totalCost <= yandex.Length + cup.Length; totalCost++)
        {
            bool foundValidPlacement = false;
            int maxPossibleYandexCost = Math.Min(totalCost, yandex.Length);

            for (int currentYandexCost = 0; currentYandexCost <= maxPossibleYandexCost; currentYandexCost++)
            {
                int currentCupCost = totalCost - currentYandexCost;

                if (yandexCostToMinIndex.TryGetValue(currentYandexCost, out int yIndex) &&
                   cupCostToMaxIndex.TryGetValue(currentCupCost, out int cIndex) &&
                   yIndex + yandex.Length <= cIndex)
                {
                    yandexStartIndex = yIndex;
                    cupStartIndex = cIndex;
                    foundValidPlacement = true;
                    break;
                }
            }

            if (foundValidPlacement)
                break;
        }

        // Изменяем символы исходной строки
        for (int i = 0; i < yandex.Length; i++)
            chars[yandexStartIndex + i] = yandex[i];

        for (int i = 0; i < cup.Length; i++)
            chars[cupStartIndex + i] = cup[i];

        return new string(chars);
    }

    private static int CalculateChangeCost(IEnumerable<char> source, string target)
    {
        int changesCount = 0;
        using (var enumerator = source.GetEnumerator())
        {
            foreach (char ch in target)
            {
                if (!enumerator.MoveNext()) break;
                if (ch != enumerator.Current) changesCount++;
            }
        }
        return changesCount;
    }
}