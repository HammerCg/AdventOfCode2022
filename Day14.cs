using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode2022
{
    class Day14
    {
        public void Execute()
        {
            string[] input = File.ReadAllLines("..\\..\\..\\Day14.txt");

            string[] cave = new string[600];
            string[] cavePart2 = new string[600];

            //Create path rocks
            List<PathRock> pathsRocks = new List<PathRock>();
            for (int i = 0; i < input.Length; i++)
            {
                string[] paths = input[i].Split("->");
                PathRock pathRock = new PathRock();
                for (int n = 0; n < paths.Length; n++)
                {
                    string[] xy = paths[n].Split(",");
                    pathRock.paths.Add(new Vector2D(int.Parse(xy[0]), int.Parse(xy[1])));
                }
                pathsRocks.Add(pathRock);
            }

            //Setup cave with path rocks
            int maxY = 0;
            cave =  Enumerable.Repeat(new string('.',10000), 200).ToList().ToArray();
            for (int i = 0; i < pathsRocks.Count; i++)
            {
                PathRock pathRock = pathsRocks[i];
                Vector2D lastPoint = pathRock.paths[0];
                for (int n = 1; n < pathRock.paths.Count; n++)
                {
                    Vector2D targetPoint = pathRock.paths[n];
                    int diffX = lastPoint.x - targetPoint.x;
                    int signX = diffX == 0 ? 0 : (diffX < 0 ? -1 : 1);
                        
                    int diffY = lastPoint.y - targetPoint.y;
                    int signY = diffY == 0 ? 0 : (diffY < 0 ? -1 : 1);   
                    for (int x = 0; x <= Math.Abs(diffX); x++)
                    {
                        cave[targetPoint.y] = cave[targetPoint.y].Remove(targetPoint.x + (x * signX), 1).Insert(targetPoint.x + (x * signX), "#");
                    }
                    for (int y = 0; y <= Math.Abs(diffY); y++)
                    {
                        cave[targetPoint.y + (y * signY)] = cave[targetPoint.y + (y * signY)].Remove(targetPoint.x , 1).Insert(targetPoint.x , "#");
                        maxY = (targetPoint.y + (y * signY)) > maxY ? (targetPoint.y + (y * signY)) : maxY;
                    }
                    lastPoint = pathRock.paths[n];
                }
            }

            //for part2
            maxY += 2;
            cavePart2 = (string[])cave.Clone();
            cavePart2[maxY] = new string('#', 10000);
            Console.WriteLine(maxY);
            //                 

            //Part 1
            Console.WriteLine("Sand units before falling forever : " + SimulateSandFall(cave));
            for (int i = 0; i < 15; i++)
            {
               // Console.WriteLine(cave[i].Substring(480, 600 - 480));
            }
            //Part 2
            Console.WriteLine("Sand units before falling until reach (500,0) point : " + SimulateSandFall(cavePart2));
            for (int i = 0; i < 200; i++)
            {
                //Console.WriteLine(cavePart2[i].Substring(480, 600-480));
            }
        }

        int SimulateSandFall(string[] cave)
        {
            bool computeSand = false;
            int sandUnits = 0;
            Vector2D sandOrigin = new Vector2D(500, 0);

            while (!computeSand)
            {
                sandUnits++;
                Vector2D currentSand = sandOrigin;
                bool sandRest = false;

                while (!sandRest)
                {
                    try
                    {
                        int fallPosibility = CheckMove(currentSand,cave);
                        if (fallPosibility != 0)
                        {
                            currentSand.y += 1;
                            currentSand.x += fallPosibility != 2 ? fallPosibility : 0;
                        }
                        else
                        {
                            cave[currentSand.y] = cave[currentSand.y].Remove(currentSand.x, 1).Insert(currentSand.x, "o");
                            sandRest = true;
                        }
                    }
                    catch (Exception e)
                    {
                        sandRest = true;
                        computeSand = true;
                        sandUnits--;
                    }
                }
                if(currentSand.Equals(sandOrigin))
                {
                    computeSand = true;
                }
            }
            return sandUnits;
        }

        int CheckMove(Vector2D currentSand,string[] cave)
        {
            try
            {
                if (cave[currentSand.y + 1].ElementAt(currentSand.x) == '.')
                {
                    return 2;
                }
                else if (cave[currentSand.y + 1].ElementAt(currentSand.x - 1) == '.')
                {
                    return -1;
                }
                else if (cave[currentSand.y + 1].ElementAt(currentSand.x + 1) == '.')
                {
                    return 1;
                }
                else
                    return 0;
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        class PathRock
        {
            public List<Vector2D> paths = new List<Vector2D>();
        }
        struct Vector2D
        {
            public int x;
            public int y;
            public Vector2D(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public static Vector2D operator -(Vector2D a, Vector2D b)
            {
                Vector2D result = new Vector2D(a.x - b.x, a.y - b.y);
                return result;
            }
            public bool Equals([AllowNull] Vector2D other)
            {
                if (this.x == other.x && this.y == other.y) return true;
                return false;
            }
        }
    }
}
