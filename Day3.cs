using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventCode2022
{
    class Day3
    {
        public void Execute()
        {
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day3.txt");
            string abecedary = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int count = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                string truck1 = inputs[i].Substring(0,inputs[i].Length/2);
                string truck2 = inputs[i].Substring(inputs[i].Length / 2, inputs[i].Length/2);

                for (int x = 0; x < truck1.Length; x++)
                {
                    if (truck2.Contains(truck1[x]))
                    {                           
                        count += abecedary.IndexOf(truck1[x]) + 1;
                        break;
                    }
                }                
            }

            Console.WriteLine("Sum : " + count);

            count = 0;
            for (int i = 0; i < inputs.Length; i+=3)
            {
                string line1 = inputs[i];
                string line2 = inputs[i + 1];
                string line3 = inputs[i + 2];
                
                for (int x = 0; x < line1.Length; x++)
                {
                    if (line2.Contains(line1[x]) && line3.Contains(line1[x]))
                    {                           
                        count += abecedary.IndexOf(line1[x]) + 1;
                        break;
                    }
                }       
            }
            Console.WriteLine("Sum : " + count);
        }




    }
}
