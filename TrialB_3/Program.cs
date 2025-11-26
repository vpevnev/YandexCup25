using System;

class VikingsCampSelection
{
    static void Main()
    {
        // Чтение входных данных
        string[] dataInput = Console.ReadLine().Trim().Split();
        int villageCount = int.Parse(dataInput[0]);     // Всего деревень
        int raidCount = int.Parse(dataInput[1]);       // Количество набегов

        // Чтение списка координат деревень
        string[] coordinatesRaw = Console.ReadLine().Trim().Split();
        int[] coordinates = Array.ConvertAll(coordinatesRaw, x => int.Parse(x));

        // Сначала отсортируем деревни по координатам
        Array.Sort(coordinates);

        // Перемещаемся по селениям и ищем лучшее место для лагеря
        double bestMaxDistance = double.PositiveInfinity;
        double bestPosition = 0;

        // Перебираем возможные позиции лагеря между всеми парами деревень
        for (int pos = 0; pos < villageCount - 1; ++pos)
        {
            // Попробуем поставить лагерь между текущей и следующей деревней
            double midPosition = (coordinates[pos] + coordinates[pos + 1]) * 0.5;

            // Найдем максимальную дистанцию до K ближайших поселков
            double maxDist = FindMaximumDistanceToKNearest(midPosition, coordinates, raidCount);

            // Сохраняем лучшую найденную позицию
            if (maxDist < bestMaxDistance)
            {
                bestMaxDistance = maxDist;
                bestPosition = midPosition;
            }
        }

        // Дополнительно проверим крайнюю левую и правую деревню,
        // так как там тоже может оказаться хорошее место для лагеря
        foreach (int end in new[] { coordinates.First(), coordinates.Last() })
        {
            double maxDistEnd = FindMaximumDistanceToKNearest(end, coordinates, raidCount);
            if (maxDistEnd < bestMaxDistance)
            {
                bestMaxDistance = maxDistEnd;
                bestPosition = end;
            }
        }

        // Результат выводится как лучшая позиция лагеря
        Console.WriteLine(bestPosition);
    }

    private static double FindMaximumDistanceToKNearest(double position, int[] coords, int k)
    {
        // Бинарный поиск максимального расстояния
        double lo = 0, hi = Math.Abs(coords - coords[0]), result = 0;

        while (lo <= hi)
        {
            double mid = (lo + hi) / 2;
            bool isValid = CanCoverWithKClosest(position, coords, k, mid);

            if (isValid)
            {
                result = mid;
                hi = mid - 1e-6; // немного уменьшаем верхний предел
            }
            else
            {
                lo = mid + 1e-6; // увеличиваем нижний предел
            }
        }

        return result;
    }

    private static bool CanCoverWithKClosest(double position, int[] coords, int k, double maxDistance)
    {
        // Берём ближайшие k деревень и считаем максимальную дистанцию до них
        int count = 0;
        foreach (var coord in coords)
        {
            if (Math.Abs(coord - position) <= maxDistance)
            {
                count++;
            }
        }

        return count >= k;
    }
}