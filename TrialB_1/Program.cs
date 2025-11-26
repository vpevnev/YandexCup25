using System;

namespace TrialB
{
    // 38 тестов ок

    class Program
    {
        static void Main(string[] args)
        {
            var firstRow = Console.ReadLine().Split(" ");

            var villages = Convert.ToInt32(firstRow[0]);
            var raids = Convert.ToInt32(firstRow[1]);

            var coordinates = Console.ReadLine().Split(" ");

            var orderedCoordinates = new int[coordinates.Length];
            orderedCoordinates[0] = Convert.ToInt32(coordinates[0]);

            // 

            var sum = orderedCoordinates[0];

            for (var i = 1; i < coordinates.Length; i++)
            {
                var currentValue = Convert.ToInt32(coordinates[i]);

                sum += currentValue;

                if (currentValue < orderedCoordinates[i - 1])
                {
                    orderedCoordinates[i] = orderedCoordinates[i - 1];
                    orderedCoordinates[i - 1] = currentValue;
                }
                else
                {
                    orderedCoordinates[i] = currentValue;
                }
            }

            var average = sum / orderedCoordinates.Length;
            var result = average;

            // так 2 теста только 1 и 2

            //for (var i = 0; i < orderedCoordinates.Length - 1; i++)
            //{
            //    var left = orderedCoordinates[i];
            //    var right = orderedCoordinates[i + 1];

            //    if (left <= average && average <= right)
            //    {
            //        var leftStep = average - left;
            //        var rightStep = right - average;

            //        result = leftStep <= rightStep ? left : right;

            //        break;
            //    }
            //}

            Console.WriteLine(result);
        }
    }
}