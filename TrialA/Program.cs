using System;

namespace TrialA
{
    // Решение на 100 баллов.

    class Program
    {
        public static Papyrus[] PapyrusSet = new Papyrus[]
        {
            new Papyrus
            {
                Pattern = new int[] { 1 },
            },
            new Papyrus
            {
                Pattern = new int[] { 2, 2 },
            },
            new Papyrus
            {
                Pattern = new int[] { 3, 3, 3 },
            },
            new Papyrus
            {
                Pattern = new int[] { 4, 4, 4, 4 },
            },
            new Papyrus
            {
                Pattern = new int[] { 5, 5, 0, 0, 5, 5 },
            },
            new Papyrus
            {
                Pattern = new int[] { 6, 6, 6, 0, 6, 6, 6 },
            },
            new Papyrus
            {
                Pattern = new int[] { 7, 7, 7, 7, 7, 7, 7, 7 },
            },
            new Papyrus
            {
                Pattern = new int[] { 8, 8, 8, 0, 8, 8, 8, 0, 8, 8, 8 },
            },
        };

        static void Main(string[] args)
        {
            var walls = new Wall[Convert.ToInt32(Console.ReadLine())];

            for (var i = 0; i < walls.Length; i++)
            {
                walls[i] = new Wall();

                var planRows = 10;
                var planColumns = 4;

                for (var j = 0; j < planRows; j++)
                {
                    var row = Console.ReadLine().Split(' ');

                    for (var k = 0; k < planColumns; k++)
                    {
                        var value = Convert.ToInt32(row[k]);

                        if (j == 0)
                        {
                            walls[i].Line[j + k] = value;
                        }
                        else
                        {
                            walls[i].Line[j * planColumns + k] = value;
                        }
                    }
                }
            }

            for (var i = 0; i < walls.Length; i++)
            {
                var wall = walls[i];

                if (wall.Check(ref PapyrusSet))
                {
                    Console.WriteLine("YES");
                    Console.WriteLine(string.Join(" ", wall.Finds));
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
        }
    }

    public class Papyrus
    {
        public int[] Pattern { get; set; }
    }

    public class Wall
    {
        public int[] Line = new int[40];

        public int[] Finds = new int[8];

        public bool Check(ref Papyrus[] papyrusSet)
        {
            var result = true;

            for (var index = 0; index < Line.Length; index++)
            {
                if (Line[index] == 0)
                {
                    continue;
                }

                if (!Find(index, ref papyrusSet))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public bool Find(int startIndex, ref Papyrus[] papyrusSet)
        {
            var result = true;

            var papyrusId = Line[startIndex] - 1;
            var papyrus = papyrusSet[papyrusId];

            for (var i = startIndex; i < Line.Length && i - startIndex < papyrus.Pattern.Length; i++)
            {
                var patternPart = papyrus.Pattern[i - startIndex];

                if (patternPart == 0)
                {
                    continue;
                }

                if (patternPart != Line[i])
                {
                    result = false;
                    break;
                }

                Line[i] = 0;
            }

            Finds[papyrusId]++;

            return result;
        }
    }
}