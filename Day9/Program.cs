using System;
using System.Threading.Tasks;
using System.Linq;
using Input;
using System.Collections.Generic;
using System.Collections;

namespace Day9
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var data = (await InputReader.GetInput(9)).Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(str =>long.Parse(str)).ToArray();
            if (!TryFindFirstInvalidNumber(data, out var firstInvalidNumber)) return;
            Console.WriteLine(firstInvalidNumber);
            if (!TryFindSumSequence(data, firstInvalidNumber, out var sequence)) return;
            Console.WriteLine(sequence.Max() + sequence.Min());
        }


        private static bool TryFindSumSequence(long[] data,long key, out List<long> sequence)
        {
            sequence = new List<long>();
            for (int i = 0; i < data.Length; i++)
            {
                sequence = new List<long>() { data[i] };
                var sum = data[i];
                var index = i + 1;
                while (sum < key)
                {
                    sequence.Add(data[index]);
                    sum += data[index++];
                }
                if (sum == key)
                    return true;
            }
            return false;
        }

        private static bool TryFindFirstInvalidNumber(long[] data, out long invalidNumber)
        {
            var preamble = data.Take(25).ToList();
            invalidNumber = 0;
            foreach (var number in data.Skip(25))
            {
                invalidNumber = number;
                var sorted = preamble.OrderBy(i => i).ToList();
                if (!CheckLimits(number, sorted) && !IsValidNumber(number, sorted))
                    return true;
                preamble.RemoveAt(0);
                preamble.Add(number);
            }
            return false;
        }

        private static bool IsValidNumber(long number, List<long> data)
        {
            var leftIndex = 0;
            var rigthIndex = 24;
            var value = 0L;
            while(leftIndex != rigthIndex && value != number)
            {
                value = data[leftIndex] + data[rigthIndex];
                if (value > number)
                    rigthIndex--;
                if (value < number)
                    leftIndex++;
            }
            return value == number;
        }

        private static bool CheckLimits(long number, List<long> data)
        {
            if (data[0] + data[1] > number) return false;
            if (data[24] + data[23] < number) return false;
            return true;
        }
    }
}
