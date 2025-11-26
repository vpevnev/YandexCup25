using System;

namespace TrialB
{
    class Program
    {
        static void Main()
        {
            string[] inputData = Console.ReadLine().Split(' ');
            int numVillages = int.Parse(inputData[0]); // Количество деревень
            int numRaids = int.Parse(inputData[1]);   // Количество рейдов

            string[] rawCoordinates = Console.ReadLine().Split(' '); // Координаты

            // Преобразовываем строки в числовой массив и сортируем
            int[] sortedCoordinates = Array.ConvertAll(rawCoordinates, s => int.Parse(s));
            Array.Sort(sortedCoordinates); // Упорядочиваем по возрастанию

            // Подсчет количества возможных интервалов
            int numberOfWindows = numVillages - numRaids + 1;

            double minDifference = double.MaxValue;
            double bestAverage = 0;

            // Проходим по каждому возможному окну длины 'numRaids'
            for (int startIndex = 0; startIndex < numberOfWindows; startIndex++)
            {
                // Рассчитываем сумму элементов в окне
                long sum = 0;
                for (int i = startIndex; i < startIndex + numRaids; i++)
                {
                    sum += sortedCoordinates[i];
                }

                // Среднее значение окна
                double avg = (double)sum / numRaids;

                // Разница между максимальным и минимальным значением в окне
                double diff = sortedCoordinates[startIndex + numRaids - 1] - sortedCoordinates[startIndex];

                // Проверяем условие минимума
                if (diff < minDifference || (diff == minDifference && avg > bestAverage))
                {
                    minDifference = diff;
                    bestAverage = avg;
                }
            }

            // Печать результата
            Console.WriteLine($"{(int)bestAverage}");
        }
    }
}