using Input;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Day2_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var answer = (await InputReader.GetInput(2)).Split('\n', StringSplitOptions.RemoveEmptyEntries)
                                                        .Where(line => IsCorrectPassword(line))
                                                        .Count();
            Console.WriteLine(answer);
        }

        private static (int index1, int index2, char letter, string password) ParseLine(string line)
        {
            var splited = line.Split(new[] { ':',' ','-'},StringSplitOptions.RemoveEmptyEntries);
            var password = splited[3];
            var letter = splited[2][0];
            var index1 = int.Parse(splited[0])-1;
            var index2 = int.Parse(splited[1])-1;
            return (index1, index2, letter, password);
        }

        private static bool IsCorrectPassword(string line)
        {
            if (string.IsNullOrEmpty(line)) return false;
            var data = ParseLine(line);
            return (data.password[data.index1] == data.letter && data.password[data.index2] != data.letter)
                   || (data.password[data.index2] == data.letter && data.password[data.index1] != data.letter);
        }
    }
}
