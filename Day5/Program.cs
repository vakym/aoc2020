using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Input;
 
namespace Day5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var data = (await InputReader.GetInput(5)).Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var seatsId = new List<int>();
            foreach (var line in data)
            {
                var row = GetRow(line.Substring(0, 7));
                var seat = GetSeat(line.Substring(7, 3));
                var seatId = row * 8 + seat;
                seatsId.Add(seatId);
            }
            Console.WriteLine(seatsId.Max(s=>s)); //part one
            Console.WriteLine(GetSeatId(seatsId)); // part two

        }

        private static int GetSeatId(List<int> list)
        {
            var sum = Enumerable.Range(list.Min(s => s), list.Max(s => s)).Sum();
            return sum - list.Sum();
        }

        private static int GetValue((int,int) range,char lower, char upper, string inputString)
        {
            foreach (var letter in inputString)
            {
                var mid = ((range.Item2 - range.Item1) / 2) + range.Item1;
                if (letter == lower)
                    range = (range.Item1, mid);
                if (letter == upper)
                    range = (mid + 1, range.Item2);
            }
            return range.Item1;
        }

        private static int GetRow(string value)
        {
            var range = (0, 127);
            return GetValue(range, 'F', 'B', value);
        }

        private static int GetSeat(string value)
        {
            var range = (0, 7);
            return GetValue(range, 'L', 'R', value);
        }
    }
}
