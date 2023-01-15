using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode2022
{
    class Day10
    {
        public void Execute()
        {
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day10.txt");
             
            //Part 1
            int currentCycle = 1;
            int pendingCycles = 0;
            int currentInstruction = 0;

            int currentValue =1;
            int nextValue = 0;
            int sum = 0;

            bool stillComputing = true;
            while (stillComputing)
            {
                if(currentCycle > pendingCycles)
                {
                    currentValue += nextValue;
                    nextValue = 0;
                }
                if(currentInstruction < inputs.Length && currentCycle > pendingCycles)
                {
                    if (inputs[currentInstruction].Contains("noop"))    pendingCycles++;
                    else
                    {
                        pendingCycles += 2;
                        string[] instruction = inputs[currentInstruction].Split(" ");
                        nextValue = int.Parse(instruction[1]);
                    }
                    currentInstruction++;
                }
                else if (currentInstruction >= inputs.Length)
                {
                    break;
                }
                if (currentCycle == 20 || currentCycle == 60 || currentCycle == 100 || currentCycle == 140 || currentCycle == 180 || currentCycle == 220 )
                {
                    sum += (currentValue * currentCycle);
                    //Console.WriteLine("cycle: " + currentCycle + " currentValue : " + currentValue + " CycleValue : " + (currentCycle * currentValue) + " currentInstruction : " + currentInstruction);
                }
               // Console.WriteLine("cycle: " + currentCycle + " currentValue : " + currentValue + " CycleValue : " + (currentCycle * currentValue) + " currentInstruction : " + currentInstruction + " Module : " );
                currentCycle++;
            }
            Console.WriteLine(sum);

            //Part2
            currentCycle = 1;
            pendingCycles = 0;
            currentInstruction = 0;

            currentValue = 1;
            nextValue = 0;
            sum = 0;

            List<string> lines = Enumerable.Repeat("", 6).ToList();
            int currentCrtH = 0;
            int currentLine = 0;
            stillComputing = true;
            while (stillComputing)
            {
                if (currentCycle > pendingCycles)
                {
                    currentValue += nextValue;
                    nextValue = 0;
                }

                if (currentInstruction < inputs.Length && currentCycle > pendingCycles)
                {
                    if (inputs[currentInstruction].Contains("noop")) pendingCycles++;
                    else
                    {
                        pendingCycles += 2;
                        string[] instruction = inputs[currentInstruction].Split(" ");
                        nextValue = int.Parse(instruction[1]);
                    }
                    currentInstruction++;
                }
                else if (currentInstruction >= inputs.Length)   break;

                Console.WriteLine("cycle: " + currentCycle + " currentValue : " + currentValue + " currentCrtH : " + currentCrtH  + " Module : " + (currentCycle % 40) + " Line : " + currentLine);
                if((currentValue - 1) == currentCrtH || currentValue == currentCrtH || (currentValue + 1) == currentCrtH)
                {
                    lines[currentLine] = lines[currentLine] + "#";
                }
                else lines[currentLine] = lines[currentLine] + ".";


                currentCrtH++;
                currentCrtH = currentCrtH == 40 ? 0 : currentCrtH;
                currentLine = (currentCycle % 40) == 0 ? currentLine + 1 : currentLine;
                currentCycle++;

            }

            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
    }
}
