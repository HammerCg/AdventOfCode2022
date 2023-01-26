using System;

namespace AdventCode2022
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            while (true)
            {
                //if (DateTime.Now.Second % 3 == 0)
                  //  Console.ForegroundColor = (ConsoleColor)random.Next(0, 16);

                Console.WriteLine("\nTake a valid day from 1 to 30");
                string key = Console.ReadLine();
                Console.WriteLine();

                switch (key)
                {
                    case "1":
                        var day1 = new Day1();
                        day1.Execute();
                        break;
                    case "2":
                        var day2 = new Day2();
                        day2.Execute();                        
                        break;
                    case "3":
                        var day3 = new Day3();
                        day3.Execute();                        
                        break;
                    case "4":
                        var day4 = new Day4();
                        day4.Execute();                        
                        break;
                    case "5":
                        var day5 = new Day5();
                        day5.Execute();                        
                        break;
                    case "6":
                        var day6 = new Day6();
                        day6.Execute();
                        break;
                    case "7":
                        var day7 = new Day7();
                        day7.Execute();                        
                        break;
                    case "8":
                        var day8 = new Day8();
                        day8.Execute();
                        break;
                    case "9":
                        var day9 = new Day9();
                        day9.Execute();
                        break;
                    case "10":
                        var day10 = new Day10();
                        day10.Execute();
                        break;
                    case "11":
                        var day11 = new Day11();
                        day11.Execute();
                        break;
                    case "12":
                        var day12 = new Day12();
                        day12.Execute();
                        break;
                    case "13":
                        var day13 = new Day13();
                        day13.Execute();
                        break;
                    case "14":
                        var day14 = new Day14();
                        day14.Execute();
                        break;
                    case "15":
                        var day15 = new Day15();
                        day15.Execute();
                        break;
                        /*
                    case "16":
                        var day16 = new Day16();
                        day16.PartOne();
                        day16.PartTwo();
                        break;
                    case "17":
                        var day17 = new Day17();
                        day17.PartOne();
                        day17.PartTwo();
                        break;
                    case "18":
                        var day18 = new Day18();
                        day18.PartOne();
                        day18.PartTwo();
                        break;
                        */
                }

            }
        }                                 
    }
}
