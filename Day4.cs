using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode2022
{
    class Day4
    {
        public void Execute()
        {
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day4.txt");

            //Part 1
            int count = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                string[] pairs = inputs[i].Split(',');
                int[] rangeFirstElf = pairs[0].Split('-').Select(int.Parse).ToArray();
                int[] rangeSecondElf = pairs[1].Split('-').Select(int.Parse).ToArray();

                if ( (rangeSecondElf[0] >= rangeFirstElf[0] && rangeSecondElf[1] <= rangeFirstElf[1])
                    || (rangeSecondElf[0] <= rangeFirstElf[0] && rangeSecondElf[1] >= rangeFirstElf[1]) )
                        count++;
            }
            Console.WriteLine("Full Pairs containing :" + count);

            //Part2 
            count = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                string[] pairs = inputs[i].Split(',');
                int[] rangeFirstElf = pairs[0].Split('-').Select(int.Parse).ToArray();
                int[] rangeSecondElf = pairs[1].Split('-').Select(int.Parse).ToArray();

                int oldCount = count;
                for (int x = rangeFirstElf[0]; x <= rangeFirstElf[1]; x++)
                {
                    if (oldCount != count) break;

                    for (int y = rangeSecondElf[0]; y <= rangeSecondElf[1]; y++)
                    {
                        if (x == y)
                        {
                            count++;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine("Pairs overlaping :" + count);
        }
    }
}
