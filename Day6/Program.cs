using System;
using System.Threading.Tasks;
using System.Linq;
using Input;
using System.Collections.Generic;

namespace Day6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";

            var data = (await InputReader.GetInput(6)).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            var answer1 = data.Select(groupAnswers => groupAnswers.Where(letter => char.IsLetter(letter)))
                              .Select(groupAnswers => groupAnswers.Distinct())
                              .Select(uniqueGroupAnswers => uniqueGroupAnswers.Count())
                              .Sum();//part one
            var answer2 = data.Select(groupAnswers => groupAnswers.Split("\n", StringSplitOptions.RemoveEmptyEntries))
                              .Select(GetIntersectOfStrings)
                              .Select(answers => answers.Length)
                              .Sum();//part two
        }

        private static string GetIntersectOfStrings(IEnumerable<string> data)
        {
            var intersect = data.First().ToCharArray();
            foreach (var line in data)
            {
                intersect = intersect.Intersect(line).ToArray();
            }
            return new string(intersect);
        }
    }
}
