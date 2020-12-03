using System;
using Input;
using System.Linq;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var input = (await InputReader.GetInput(1))
                            .Split('\n',StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();
            var values = FindTwoValues();
            Console.WriteLine(values.Item1 * values.Item2);
           

            (int, int) FindTwoValues()
            {
                for (var i = 0; i < input.Length - 1; i++)
                    for (var j = i + 1; j < input.Length - 1; j++)
                    {
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
