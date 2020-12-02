using Input;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Day1_2
{
    class Program
    {
       
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var input = (await InputReader.GetInput(1))
                            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                            .Select(line => int.Parse(line))
                            .ToArray();
            var values = FindThreeValues();
            Console.WriteLine(values.Item1 * values.Item2 * values.Item3);


            (int, int, int) FindThreeValues()
            {
                for (var i = 0; i < input.Length - 2; i++)
                    for (var j = i + 1; j < input.Length - 1; j++)
                        for (var k = j + 1; k < input.Length; k++)
                            if (input[i] + input[j] + input[k] == 2020)
                            {
                                return (input[i], input[j], input[k]);
                            }
                return (0, 0, 0);
            }
        }
    }
}
