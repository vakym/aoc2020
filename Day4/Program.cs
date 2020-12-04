using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Input;

namespace Day4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var validPassportsCount = (await InputReader.GetInput(4))
                .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(data => data.Split(new[] {'\n', ' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(str =>
                    {
                        var pair = str.Split(':');
                        return (pair[0], pair[1]);
                    }).ToDictionary(k => k.Item1, e => e.Item2))
                .Count(IsValid);
            Console.WriteLine(validPassportsCount);
        }

        private static string[] reqFields =
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid"
        };

        private static bool IsValid(Dictionary<string, string> data)
        {
            foreach (var field in reqFields)
                if (!data.ContainsKey(field))
                    return false;
            return true;
        }
    }
}
