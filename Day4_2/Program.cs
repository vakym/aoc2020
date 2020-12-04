using Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Day4_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var validPassportsCount = (await InputReader.GetInput(4))
                .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(Passport.Create)
                .Count(p =>
                {
                    var  context = new  ValidationContext(p);
                    return Validator.TryValidateObject(p,context,null,true);
                });
            Console.WriteLine(validPassportsCount);
        }
    }
}
