using System;
using System.IO;
using System.Linq;

namespace Day1_2
{
    class Program
    {
        static string pathToInput = "input.data";
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(pathToInput)
                            .Select(line => int.Parse(line))
                            .ToArray();
            var values = FindThreeValues();
            Console.WriteLine(values.Item1 * values.Item2 * values.Item3);


            (int, int, int) FindThreeValues()
            {
                for (var i = 0; i < input.Length; i++)
                    for (var j = 0; j < input.Length - 1; j++)
                        for (var k = 0; k < input.Length; k++)
                            if (i != j && i!=k && k!=j)
                                if (input[i] + input[j] + input[k] == 2020)
                                {
                                    return (input[i], input[j], input[k]);
                                }
                return (0, 0, 0);
            }
        }
    }
}
