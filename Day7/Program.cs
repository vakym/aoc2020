using System;
using System.Threading.Tasks;
using System.Linq;
using Input;
using System.Collections.Generic;

namespace Day7
{
    class Program
    {
        private static Dictionary<string, Bag> bagsTypes = new Dictionary<string, Bag>();
        private static Dictionary<string, Rule> rules;

        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var data = (await InputReader.GetInput(7)).Split("\n", StringSplitOptions.RemoveEmptyEntries);
            rules = ParseRules(data);
            var startBag = bagsTypes["shiny gold"];
            var answer = GetBagsContainingBag(startBag);
            Console.WriteLine(answer);
            var answer2 = GetBagsCountInside(startBag);
            Console.WriteLine(answer2);
        }

        private static long GetBagsCountInside(Bag bag)
        {
            long  answer = 0;
            var rule = rules[bag.Color];
            foreach (var condition in rule.Conditions)
            {
                answer += condition.Value + condition.Value * GetBagsCountInside(condition.Key);
            }
            return answer;
        }

        private static int GetBagsContainingBag(Bag bag)
        {
            var foundBags = new Dictionary<string,Bag>();
            var answer = Calculate(bag);
            return answer;
            
            int Calculate(Bag currentBag)
            {
                var partAnswer = 0;
                foreach (var rule in currentBag.Rules)
                {
                    if (!foundBags.ContainsKey(rule.Bag.Color))
                    {
                        partAnswer++;
                        foundBags.Add(rule.Bag.Color, rule.Bag);
                        partAnswer += Calculate(rule.Bag);
                    }
                }
                return partAnswer;
            }
        }

        private static Dictionary<string, Rule> ParseRules(string[] data)
        {
            var rules = new Dictionary<string, Rule>();
            foreach (var line in data)
            {
                var newRule = ParseRule(line);
                rules.Add(newRule.Bag.Color, newRule);
            }
            return rules;
        }

        private static Rule ParseRule(string data)
        {
            var splited = data.Split(new[] { "bags contain", "," }, StringSplitOptions.RemoveEmptyEntries);
            var newRule = new Rule(GetOrAddBagType(splited[0].Trim()));
            if (!splited[1].Contains("no other"))
                foreach (var condition in splited.Skip(1))
                {
                    var clearCondition = condition.Trim('.',' ').Replace("bags", "").Replace("bag", "");
                    newRule.AddBagToConditions(GetOrAddBagType(clearCondition.Substring(2).Trim()),
                                               int.Parse(clearCondition.Substring(0, 1).Trim()));
                }
            return newRule;
        }

        private static Bag GetOrAddBagType(string color)
        {
            if (!bagsTypes.ContainsKey(color))
                bagsTypes.Add(color, new Bag(color));
            return bagsTypes[color];
        }
    }



    class Rule
    {
        public Rule(Bag bag)
        {
            Bag = bag ?? throw new ArgumentNullException(nameof(bag));
        }

        public Bag Bag { get; }

        public Dictionary<Bag, int> Conditions { get; } = new Dictionary<Bag, int>();

        public void AddBagToConditions(Bag bag, int count)
        {
            Conditions.Add(bag, count);
            bag.Rules.Add(this);
        }
    }

    class Bag
    {
        public Bag(string color)
        {
            Color = color ?? throw new ArgumentNullException(nameof(color));
        }

        public string Color { get; }

        public List<Rule> Rules { get; } = new List<Rule>();

    }
}
