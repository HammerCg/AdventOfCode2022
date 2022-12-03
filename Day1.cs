using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode2022
{
    class Day1
    {
        public void Execute()
        {               
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day1.txt");
            int count = 0;
            List<int> elvesCalories = new List<int>();

            foreach (var calories in inputs)
            {
                if (calories.Equals("")) {
                    elvesCalories.Add(count);
                    count = 0;
                }
                else
                {
                    count += int.Parse(calories);
                }
            }
            
            //Part 1
            Console.WriteLine("Elve position with max calories :" + elvesCalories.Max());
            //Part 2
            Console.WriteLine("Max 3 calories :" + elvesCalories.OrderByDescending(x => x).Take(3).Sum());

        }
    }
}
