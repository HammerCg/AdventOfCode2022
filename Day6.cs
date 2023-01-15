using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode2022
{
    class Day6
    {
        public void Execute()
        {
            string inputs = File.ReadAllText("..\\..\\..\\Day6.txt");
            //Part 1
            for (int i = 3; i < inputs.Length; i++)
            {
                string s = inputs.Substring(i-3,4);  
                Dictionary<char, int> ocurrences = new Dictionary<char, int>();
                for (int n = 0; n < s.Length; n++)
                {
                    if (!ocurrences.Keys.Contains(s[n])) { ocurrences.Add(s[n], 1); }
                    else ocurrences[s[n]]++;
                }
                if(ocurrences.Values.Max() == 1)
                {
                    Console.WriteLine("First atart-of-packet  marker: " + (i+1));
                    break;
                }
            }

            //Part 2
            for (int i = 13; i < inputs.Length; i++)
            {
                string s = inputs.Substring(i - 13, 14);    
                Dictionary<char, int> ocurrences = new Dictionary<char, int>();
                for (int n = 0; n < s.Length; n++)
                {
                    if (!ocurrences.Keys.Contains(s[n])) { ocurrences.Add(s[n], 1); }
                    else ocurrences[s[n]]++;
                }
                if (ocurrences.Values.Max() == 1)
                {
                    Console.WriteLine("First start-of-message marker : " + (i + 1));
                    break;
                }
            }

        }
    }
}
