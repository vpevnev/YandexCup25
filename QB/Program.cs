using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace QB
{
    class Program
    {
        public static Dictionary<string, Dictionary<string, string>> Manufactures = new();

        public static int AddManufacture(string instrument, string metal, string verdict) 
        {
            
            if (!Manufactures.TryGetValue(metal, out Dictionary<string, string> instruments))
            {
                instruments = new()
                {
                    { instrument, verdict }
                };
                Manufactures.Add(metal, instruments);
            }
            else 
            {
                var existingInstrument = instruments.First();

                if (existingInstrument.Value != verdict)
                {
                    return 1;
                }

                if (!instruments.TryGetValue(instrument, out string decision))
                {
                    instruments.Add(instrument, verdict);
                }
            }

            return 0;
        }

        public static string GetAnswer(string instrument, string metal)
        {
            var result = "UNKNOWN";

            if (Manufactures.TryGetValue(metal, out Dictionary<string, string> instruments))
            {
                result = instruments.First(x => x.Key != instrument).Value;
            }

            return result;
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
                                return;
                            }
                            break;
                        case "ask":
                            var answer = GetAnswer(message[1], message[2]);
                            writer.WriteLine($"{i} {answer}");
                            break;
                    }
                }
            }
        }
    }
}