using System;
using System.Threading.Tasks;
using System.Linq;
using Input;
using System.Collections.Generic;
using System.Collections;

namespace Day10
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var data = (await InputReader.GetInput(10)).Split("\n", StringSplitOptions.RemoveEmptyEntries)
                                                       .Select(int.Parse)
                                                       .Append(0)
                                                       .OrderBy(i=>i);
            var dataArr = data.Append(data.Last() + 3)
                              .ToArray();
            var answer = dataArr.Zip(dataArr.Skip(1), (first, second) => second - first)
                                .GroupBy(i=>i)
                                .Select(g=>g.Count());
            Console.WriteLine(answer.First() * answer.Last()); // part one

            Console.WriteLine(FindDistinctWays(dataArr));//part two
           
           
        }

        private static long FindDistinctWays(int[] data)
        {
            var preamble = new Queue<(int value, long pathCount)>();
            preamble.Enqueue((0, 1));
            foreach (var node in data.Skip(1))
            {
                FindPathCountForNode(node);
            }
            return preamble.Last().pathCount;
            
            void FindPathCountForNode(int value)
            { 
                var currentPathCount = preamble.Where(n => n.value >= value - 3).Sum(n => n.pathCount);
                preamble.Enqueue((value, currentPathCount));
                if (preamble.Count > 3)
                    preamble.Dequeue();
            }
        }
    }
}
