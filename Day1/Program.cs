using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static string pathToInput;
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(pathToInput)
                            .Select(line => int.Parse(line))
                            .ToArray();
            var values = FindValues();
            Console.WriteLine(values.Item1*values.Item2);
           

            (int, int) FindValues()
            {
                for (var i = 0; i < input.Length; i++)
                    for (var j = 0; j < input.Length - 1; j++)
                    {
                        if (i != j)
                            if (input[i] + input[j] == 2020)
                            {
                                return (input[i],input[j]);
                            }
                    }
                return (0, 0);
            }
        }
    }
}
