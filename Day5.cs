using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventCode2022
{
    class Day5
    {
        public void Execute()
        {
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day5.txt");
            List<List<char>> partOneCargoSetup = new List<List<char>>();
            List<string> cargoLines = new List<string>();
            List<List<string>> moveLines = new List<List<string>>();

            bool StartMoves = false;

            //Setup input data
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Equals("") && StartMoves == false)
                {
                    StartMoves = true;
                    continue;
                }

                if (!StartMoves) cargoLines.Add(inputs[i]);
                else
                {
                    string s = String.Join("" ,inputs[i].Where(c => char.IsDigit(c) || char.IsWhiteSpace(c)).ToList());
                    moveLines.Add(s.Split(' ',StringSplitOptions.RemoveEmptyEntries).ToList<string>());                               
                }
            }

            for (int i = 0; i < cargoLines[cargoLines.Count-1].ToCharArray().Count(); i++)
            {
                char currentChar = cargoLines[cargoLines.Count - 1].ToCharArray()[i];
                if (!char.IsDigit(currentChar)) continue;
                
                partOneCargoSetup.Add(new List<char>());
                for (int x = cargoLines.Count-2; x >= 0 ; x--)
                {
                    char c = cargoLines[x][i];
                    if (!char.IsLetter(c)) continue;

                    partOneCargoSetup[partOneCargoSetup.Count-1].Add(c);
                }
            }

            //Data copy for part 2                                                                                
            List<List<char>> partTwoCargoSetup = new List<List<char>>();
            for (int i = 0; i < partOneCargoSetup.Count; i++)
            {
                partTwoCargoSetup.Add(new List<char>());
                for (int n = 0; n < partOneCargoSetup[i].Count; n++)
                {
                    partTwoCargoSetup[i].Add(partOneCargoSetup[i][n]);
                }
            }
            
            //Part 1
            for (int i = 0; i < moveLines.Count; i++)
            {
                int move = int.Parse(moveLines[i].ElementAt(0) + "");
                int from = int.Parse(moveLines[i].ElementAt(1) + "");
                int to = int.Parse(moveLines[i].ElementAt(2) + "");

                for (int n = 0; n < move; n++)
                {
                    partOneCargoSetup[to - 1].Add(partOneCargoSetup[from - 1].Last());
                    partOneCargoSetup[from - 1].RemoveAt(partOneCargoSetup[from - 1].Count - 1);
                }  
            }

            string result = "";
            for (int i = 0; i < partOneCargoSetup.Count; i++)
            {
                result += partOneCargoSetup[i].Last();
            }
            Console.WriteLine(result);

            //Part2
            for (int i = 0; i < moveLines.Count; i++)
            {
                int move = int.Parse(moveLines[i].ElementAt(0) + "");
                int from = int.Parse(moveLines[i].ElementAt(1) + "");
                int to = int.Parse(moveLines[i].ElementAt(2) + "");

                partTwoCargoSetup[to - 1].AddRange(partTwoCargoSetup[from - 1].TakeLast(move));
                partTwoCargoSetup[from - 1].RemoveRange(partTwoCargoSetup[from - 1].Count - move,move);
            }
            
            result = "";
            for (int i = 0; i < partTwoCargoSetup.Count; i++)
            {
                result += partTwoCargoSetup[i].Last();
            }
            Console.WriteLine(result);
        }
    }
}
