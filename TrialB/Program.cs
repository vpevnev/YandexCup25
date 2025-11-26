using System;

namespace TrialB
{
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

            for (var i = 1; i < coordinates.Length; i++)
            {
                var currentValue = Convert.ToInt32(coordinates[i]);

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

            var windowAmount = villages - raids + 1;
            var windowLength = raids;

            var windows = new Window[windowAmount];

            for (var w = 0; w < windowAmount; w++)
            {
                windows[w] = new Window();

                var sum = 0L;

                for (var i = w; i < windowLength + w; i++)
                {
                    sum += orderedCoordinates[i];
                }

                windows[w].Difference = orderedCoordinates[windowLength + w - 1] - orderedCoordinates[w];
                windows[w].Average = sum / windowLength;
                //windows[w].BestPoint = windows[w].Average;
                //for (var i = w; i < windowLength + w - 1; i++)
                //{
                //    var left = orderedCoordinates[i];
                //    var right = orderedCoordinates[i + 1];

                //    var average = windows[w].Average;

                //    if (left <= average && average <= right)
                //    {
                //        var leftStep = average - left;
                //        var rightStep = right - average;

                //        windows[w].BestPoint = leftStep <= rightStep ? left : right;

                //        break;
                //    }
                //}
            }

            var bestWindow = windows[0];

            for (var i = 1; i < windows.Length; i++)
            {
                if (windows[i].Difference < bestWindow.Difference)
                {
                    bestWindow = windows[i]; 
                }
            }

            Console.WriteLine(bestWindow.Average);
        }
    }

    public class Window
    {
        public long Average { get; set; }
        public int Difference { get; set; }
        public long BestPoint { get; set; }
    }
}