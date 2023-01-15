using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode2022
{
    class Day11
    {
        class Monkey {
            public List<ulong> startingItems = new List<ulong>();

            string name = "";
            public ulong inspectedItems = 0;
            public string op = "+";
            public string old = "";
            public ulong div = 0;
            public int t = 0;
            public int f = 0;

            public Monkey(string name) {
                this.name = name;
            }

        }

        string[] inputs = File.ReadAllLines("..\\..\\..\\Day11.txt");
        ulong superModule; // for part 2
        List<Monkey> monkeys;
        public void Execute()
        {

            Console.Write("Part 1 => ");
            GetData();
            CalculateInspectedItems(20, monkeys, 0L, true);
            
            Console.Write("Part 2 => ");
            GetData();
            CalculateInspectedItems(10000, monkeys, superModule, false);

        }

        void GetData()
        {
            monkeys = new List<Monkey>();
            superModule = 1L;
            for (int i = 0; i < inputs.Length; i++)
            {
                string line = inputs[i];

                if (line.Contains("Monkey")) monkeys.Add(new Monkey(monkeys.Count() + ""));
                else if (line.Contains("Starting"))
                {
                    string[] numbers = line.Replace("Starting items: ", "").Split(',');
                    for (int n = 0; n < numbers.Length; n++)
                        monkeys[monkeys.Count - 1].startingItems.Add(ulong.Parse(numbers[n]));
                }
                else if (line.Contains("Operation"))
                {
                    if (line.Contains("+")) { monkeys[monkeys.Count - 1].op = "+"; line = line.Replace('+', ' '); }
                    else if (line.Contains("*")) { monkeys[monkeys.Count - 1].op = "*"; line = line.Replace('*', ' '); }
                    monkeys[monkeys.Count - 1].old = line.Replace("Operation: new = old ", "").Trim();

                }
                else if (line.Contains("Test"))
                {
                    monkeys[monkeys.Count - 1].div = ulong.Parse(line.Replace("Test: divisible by ", "").Trim());
                    superModule *= (ulong)monkeys[monkeys.Count - 1].div;
                }
                else if (line.Contains("true"))
                {
                    monkeys[monkeys.Count - 1].t = int.Parse(line.Replace("If true: throw to monkey ", "").Trim() + "");
                }
                else if (line.Contains("false"))
                {
                    monkeys[monkeys.Count - 1].f = int.Parse(line.Replace("If false: throw to monkey ", "").Trim() + "");
                }
            }
        }
        void CalculateInspectedItems(int maxRounds, List<Monkey> monkeys, ulong superModule, bool byThree = false)
        {
            for (int rounds = 0; rounds < maxRounds; rounds++)
            {
                for (int i = 0; i < monkeys.Count; i++)
                {
                    Monkey currentMonkey = monkeys[i];
                    for (int n = 0; n < currentMonkey.startingItems.Count; n++)
                    {
                        currentMonkey.inspectedItems++;
                        ulong worryLevel = currentMonkey.startingItems[n];

                        ulong m = currentMonkey.old.Equals("old") ? (ulong)worryLevel : ulong.Parse(currentMonkey.old);

                        if (currentMonkey.op == "+") worryLevel += m;
                        else if (currentMonkey.op == "*") worryLevel *= m;

                        if(byThree)     worryLevel = (ulong)Math.Floor((decimal)worryLevel / (decimal)3);
                        else            worryLevel %= superModule;

                        if ((worryLevel % (ulong)currentMonkey.div) == 0L) monkeys[currentMonkey.t].startingItems.Add(worryLevel);
                        else if ((worryLevel % (ulong)currentMonkey.div) != 0L) monkeys[currentMonkey.f].startingItems.Add(worryLevel);
                    }
                    currentMonkey.startingItems = new List<ulong>();
                }
            }

            monkeys = monkeys.OrderByDescending(x => x.inspectedItems).ToList();

            Console.WriteLine(((ulong)monkeys[0].inspectedItems * (ulong)monkeys[1].inspectedItems));
        }
    }
}
      