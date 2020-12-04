using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Day4_2
{
    public class Passport
    {
        [Required]
        [Range(1920,2002)]
        public int BirthYear { get; set; }

        [Required]
        [Range(2010, 2020)]
        public int IssueYear { get; set; }

        [Required]
        [Range(2020, 2030)]
        public int ExpirationYear { get; set; }

        [Required]
        [Range(150, 193)]
        public int Heigth { get; set; }

        [Required]
        [RegularExpression(@"^#\w{6}")]
        public string HairColor { get; set; }

        [Required]
        [EyeColorValidation]
        public string EyeColor { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}")]
        public string PID { get; set; }

        public string CID { get; set; }

        public static Passport Create(string data)
        {
            var passportFields = data.Split(new[] {'\n', ' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(str =>
                {
                    var pair = str.Split(':');
                    return (pair[0], pair[1]);
                })
                .ToDictionary(k => k.Item1, e => e.Item2);
            var  passport = new  Passport();
            if (TryGetIntField("byr", out var byr))
            {
                passport.BirthYear = byr;
            }
            if (TryGetIntField("iyr", out var iyr))
            {
                passport.IssueYear = iyr;
            }
            if (TryGetIntField("eyr", out var eyr))
            {
                passport.ExpirationYear = eyr;
            }
            if (passportFields.TryGetValue("hgt", out var  hgtStrValue))
            {
                if (hgtStrValue.Contains("cm"))
                {
                    passport.Heigth = int.Parse(hgtStrValue.Trim('c', 'm'));
                }

                if (hgtStrValue.Contains("in"))
                {
                    passport.Heigth = (int)Math.Round(int.Parse(hgtStrValue.Trim('i', 'n'))*2.54);
                }
            }
            if (passportFields.TryGetValue("hcl",out var hcl))
            {
                passport.HairColor = hcl;
            }
            if (passportFields.TryGetValue("ecl", out var ecl))
            {
                passport.EyeColor = ecl;
            }
            if (passportFields.TryGetValue("pid", out var pid))
            {
                passport.PID = pid;
            }
            if (passportFields.TryGetValue("cid", out var cid))
            {
                passport.CID = cid;
            }
            return passport;
            
            bool TryGetIntField(string key, out int value)
            {
                if (passportFields.TryGetValue(key, out var strValue))
                {
                    return int.TryParse(strValue, out value);
                }

                value = default;
                return false;
            }
        }
    }

    public class EyeColorValidationAttribute : ValidationAttribute
    {
        private readonly List<string> correctColors
            = new List<string>() { "amb","blu","brn","gry","grn","hzl","oth" };
        
        public override bool IsValid(object value)
        {
            var val = value as string;
            return correctColors.Contains(val);
        }
    }
}
