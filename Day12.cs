using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode2022
{
    class Day12
    {
       

        static string dict = "abcdefghijklmnopqrstuvwxyz";
        static string[] input = File.ReadAllLines("..\\..\\..\\Day12.txt").ToArray();  
        public void Execute()
        {
            /*
            for (int y = 0; y < inputs.Length; y++)
            {
                string line = "";
                for (int x = 0; x < inputs[y].Length; x++)
                {
                    line += inputs[y][x];
                }
                Console.WriteLine(line);
            }
            */

            Point startPositionS = new Point(0, 0);
            Point endPosition = new Point(0, 0);
            List<Point> startPositionsA = new List<Point>();

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == 'S') startPositionS = new Point(x, y);
                    else if (input[y][x] == 'E') endPosition = new Point(x, y);
                    else if (input[y][x] == 'a') startPositionsA.Add(new Point(x,y));
                }
            }

            //Part 1
            Console.WriteLine("Less steps path : " + (CalculateLesserPath(startPositionS, endPosition).Length - 3));

            //Part 2
            int lessResultA = 0;
            for (int i = 0; i < startPositionsA.Count; i++)
            {
                int result = CalculateLesserPath(startPositionsA[i], endPosition).Length -3;

                if (lessResultA == 0 && result > 0) 
                    lessResultA = result;
                else if (lessResultA > result && result > 0)
                    lessResultA = result;
            }
            Console.WriteLine("Less steps path : " + lessResultA);

        }

        Path CalculateLesserPath(Point startPosition, Point endPosition)
        {
            PriorityPathList pathList = new PriorityPathList();
            Path currentPath = new Path();
            Path lessStepsPath = new Path();
            currentPath.AddStep(startPosition);
            pathList.Push(currentPath);
            while (pathList.Any())
            {
                currentPath = pathList.Peek();
                if (currentPath == null) break;

                if (currentPath.Length > lessStepsPath.Length && lessStepsPath.Length > 0)
                {
                    pathList.Pop(currentPath);
                    continue;
                }

                Point[] neightbours = GetNeigbours(currentPath.Last());
                for (int i = 0; i < neightbours.Length; i++)
                {
                    if (currentPath.Contains(neightbours[i]))
                        continue;

                    int diff = GetHeight(currentPath.Last()) - GetHeight(neightbours[i]);
                    if (diff < -1)
                        continue;

                    if (endPosition == neightbours[i])
                    {
                        lessStepsPath = CloneList(currentPath, neightbours[i]);
                        break;
                    }
                    else
                        pathList.Push(CloneList(currentPath, neightbours[i]));

                }

                pathList.Pop(currentPath);
            }

            //PrintPath(lessStepsPath);
            return lessStepsPath;
        }

        void PrintPath(Path path)
        {
            for (int y = 0; y < input.Length; y++)
            {
                Console.ForegroundColor = (ConsoleColor)15;
                string line = "";
                for (int x = 0; x < input[y].Length; x++)
                {
                    int pointAt = path.GetPoint(x, y);    
                    if (pointAt > 0)
                    {
                        Console.ForegroundColor = (ConsoleColor)9;
                        /*
                        if (path.At(pointAt).x > path.At(pointAt - 1).x)
                            line += ">";
                        else if (path.At(pointAt).x < path.At(pointAt - 1).x)
                            line += "<";
                        else if (path.At(pointAt).y > path.At(pointAt - 1).y)
                            line += "v";
                        else if (path.At(pointAt).y < path.At(pointAt - 1).y)
                            line += "^";
                        */
                        Console.Write(input[y][x]);
                        line += input[y][x];
                        Console.ForegroundColor = (ConsoleColor)15;
                    }
                    else
                    Console.Write(input[y][x]);
                    line +=  input[y][x];
                  
                }
                Console.Write("\n");
                //Console.WriteLine(line);
            }
        }

        Path CloneList(Path listToClone, Point addItem = null)
        {
            Path newList = new Path();
            for (int i = 0; i < listToClone.Length; i++)
            {
                newList.AddStep(listToClone.At(i));
            }
            if (addItem != null)
                newList.AddStep(addItem);
            return newList;
        }
        Point[] GetNeigbours(Point point)
        {
            List<Point> neighbours = new List<Point>();

            if (point.x > 0) neighbours.Add(new Point(point.x - 1, point.y));
            if (point.x < input[point.y].Length-1) neighbours.Add(new Point(point.x + 1, point.y));
            if (point.y > 0) neighbours.Add(new Point(point.x, point.y - 1));
            if (point.y < input.Length-1) neighbours.Add(new Point(point.x, point.y + 1));

            return neighbours.OrderBy(x => GetHeight(x)).ToArray();
        }
        static int GetHeight(Point point)
        {
            if (input[point.y][point.x] == 'S') return -1;
            else if (input[point.y][point.x] == 'E') return dict.Length;
            else
            {
                return dict.IndexOf(input[point.y][point.x]);
            }
        }

        class Point
        {
            public int x;
            public int y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public override bool Equals(object obj) { return Equals(obj as Point); }
            public bool Equals(Point other) { return other != null && x == other.x && y == other.y; }
            public override int GetHashCode() { return HashCode.Combine(x, y); }
            public static bool operator ==(Point left, Point right) { return EqualityComparer<Point>.Default.Equals(left, right); }
            public static bool operator !=(Point left, Point right) { return !(left == right); }

        }
        class Path
        {
            private List<Point> path;

            public Path()
            {
                path = new List<Point>();
            }
            public void AddStep(Point point)
            {
                path.Add(point);
            }
            public Point Last()
            {
                return path.Last();
            }
            public Point At(int position)
            {
                return path[position];
            }
            public int GetPoint(int x, int y)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    if (path[i].x == x && path[i].y == y) return i;
                }
                return -1;
            }
            public bool Contains(Point point)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    if (path[i].Equals(point))
                        return true;
                }
                return false;
            }
            public int Length
            {
                get { return path.Count(); }
            }
        }
        class PriorityPathList
        {
            public PriorityPathList() { }

            private List<Path> pathList = new List<Path>();
            Dictionary<Point, Path> dictPathList = new Dictionary<Point, Path>();

            //Insert new element ordered in list with his cost/priority
            public void Push(Path path)
            {
                if (!dictPathList.ContainsKey(path.Last()) || dictPathList[path.Last()].Length > path.Length)
                {
                    if(!dictPathList.ContainsKey(path.Last()))
                        dictPathList.Add(path.Last(), path);
                    else
                        dictPathList[path.Last()] = path;
                    pathList.Add(path);
                    pathList = pathList.OrderBy(x => x.Length).ToList();
                }
            }
            //Eliminates lowest cast element
            public void Pop(Path currentPath)
            {
                if (pathList.Count > 0)
                    pathList.Remove(currentPath);
            }
            //Returns lowest cost element
            public Path Peek()
            {
                if (pathList.Count > 0)
                    return pathList.First();
                else
                    return null;
            }
            //Resturn if any element in list
            public bool Any()
            {
                return pathList.Count > 0;
            }

        }
        
        
    }
}
