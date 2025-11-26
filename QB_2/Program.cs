using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace QB
{
    class Program
    {
        public static List<Manufacture> Manufactures = new();

        public static int AddManufacture(string tool, string metal, string verdict)
        {
            var manufactures = Manufactures.Where(x => x.Tool == tool && x.Metal == metal).ToList();

            if (manufactures.Exists(x => x.Tool == tool && x.Metal == metal && x.Verdict != verdict))
            {
                return 1;
            }

            if (manufactures.Count == 0)
            {
                Manufactures.Add(new Manufacture
                {
                    Tool = tool,
                    Metal = metal,
                    Verdict = verdict
                });
            }

            return 0;
        }

        public static string GetAnswer(string tool, string metal)
        {
            var toolsInSameMetal = Manufactures.Where(x => x.Metal == metal && x.Tool != tool).Select(x => x.Tool).ToList();
            var anotherMetals = Manufactures.Where(x => toolsInSameMetal.Contains(x.Tool) && x.Metal != metal).Select(x => x.Metal).ToList();

            var existsPair = Manufactures.FirstOrDefault(x => (x.Tool == tool && x.Metal == metal)
                                                    // || (x.Tool != tool && x.Metal == metal)
                                                    || (anotherMetals.Any() && x.Tool != tool && x.Metal == metal));// && x.Tool == tool););//.Verdict ?? "UNKNOWN";

            return existsPair?.Verdict ?? "UNKNOWN";

            //var answer = "UNKNOWN";

            //if (existsPair != null)
            //{
            //    answer = toolsInSameMetal.FirstOrDefault()?.Verdict;
            //}

            //return answer ?? "UNKNOWN";
        }

        static void Main(string[] args)
        {
            var firstRow = Console.ReadLine().Split(" ");
            var instrumentAmount = int.Parse(firstRow[0]);
            var metalAmount = int.Parse(firstRow[1]);
            var messageAmount = int.Parse(firstRow[2]);

            var path = "output.txt";

            using (StreamWriter writer = new(path))
            {
                for (var i = 1; i <= messageAmount; i++)
                {
                    var message = Console.ReadLine().Split(" ");

                    switch (message[0])
                    {
                        case "info":
                            var result = AddManufacture(message[1], message[2], message[3]);
                            if (result == 1)
                            {
                                writer.Dispose();
                                File.WriteAllText(path, $"{i} WRONG");

                                Console.WriteLine($"{i} WRONG");
                                return;
                            }
                            break;
                        case "ask":
                            var answer = GetAnswer(message[1], message[2]);
                            writer.WriteLine($"{i} {answer}");

                            Console.WriteLine($"{i} {answer}");
                            break;
                    }
                }
            }
        }
    }

    public class Manufacture()
    {
        public string Tool { get; set; }
        public string Metal { get; set; }
        public string Verdict { get; set; }
    }
}