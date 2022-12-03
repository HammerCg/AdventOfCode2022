using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventCode2022
{
    class Day2
    {
        public void Execute()
        {
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day2.txt");
            
            //Part 1
            // A & X = Rock = 1
            // B & Y = Paper = 2
            // C & Z = Scissor = 3
            //Loose = 0, Draw = 3, Win = 6
            
            int count = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                char elvePlay = inputs[i][0];
                char mePlay = inputs[i][2];
                count += mePlay == 'X' ? 1 : (mePlay == 'Y' ? 2 : 3);
                
                if (elvePlay == 'A')            count += (mePlay == 'X' ? 3 : (mePlay == 'Y' ? 6 : 0));
                else if (elvePlay == 'B')       count += (mePlay == 'Y' ? 3 : (mePlay == 'Z' ? 6 : 0));
                else if (elvePlay == 'C')       count += (mePlay == 'Z' ? 3 : (mePlay == 'X' ? 6 : 0));
            }
            Console.WriteLine("Score :" + count);

            //Part 2
            // A = Rock = 1
            // B = Paper = 2
            // C = Scissor = 3
            // X = Loose = 0 , Y = Draw = 3 , Z = Win = 6
            count = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                char elvePlay = inputs[i][0];
                char needsPlay = inputs[i][2];
                count += needsPlay == 'X' ? 0 : (needsPlay == 'Y' ? 3 : 6);
                //Loose
                if (needsPlay == 'X')           count += (elvePlay == 'A' ? 3 : (elvePlay == 'B' ? 1 : 2));
                //Draw
                else if(needsPlay == 'Y')       count += (elvePlay == 'A' ? 1 : (elvePlay == 'B' ? 2 : 3));
                //Win
                else if(needsPlay == 'Z')       count += (elvePlay == 'A' ? 2 : (elvePlay == 'B' ? 3 : 1));
            }
            Console.WriteLine("Score :" + count);
        }
    }
}
