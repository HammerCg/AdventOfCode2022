using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventCode2022
{
    class Day9
    {
        struct Vector2D : IEquatable<Vector2D> 
        {
            public int x { get; set; }
            public int y { get; set; }
            public Vector2D(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public void NormalizeThis()
            {
                float mag = MathF.Sqrt(x * x + (y * y));
                this.x = (int)Math.Round((float)x / mag);
                this.y = (int)Math.Round((float)y / mag);
            }
            public bool Equals([AllowNull] Vector2D other)
            {
                if (this.x == other.x && this.y == other.y) return true;
                return false;
            }

            public static Vector2D operator -(Vector2D a, Vector2D b)
            {
                Vector2D result = new Vector2D(a.x - b.x, a.y - b.y);
                return result;            
            }
        }
        public void Execute()
        {
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day9.txt");

            List<Vector2D> tailPositions = Enumerable.Repeat(new Vector2D(0, 0), 2).ToList();
            List<Vector2D> uniqueTailPositions = new List<Vector2D>();

            for (int i = 0; i < inputs.Length; i++)
            {
                string[] command = inputs[i].Split(' ');
                Vector2D direction;
                switch (command[0])
                {
                    case "L": direction = new Vector2D(-1, 0); break;
                    case "U": direction = new Vector2D(0, 1); break;
                    case "R": direction = new Vector2D(1, 0); break;
                    case "D": direction = new Vector2D(0, -1); break;
                    default: direction = new Vector2D(0, 0); break;
                }
                int move = int.Parse(command[1]);

                for (int m = 0; m < move; m++)
                {                                     
                    tailPositions[0] = new Vector2D(tailPositions[0].x + direction.x, tailPositions[0].y + direction.y);

                    Vector2D positionDiff = tailPositions[0] - tailPositions[1];
                    if (Math.Abs(positionDiff.x) > 1 || Math.Abs(positionDiff.y) > 1)
                    {                                                                      
                        tailPositions[1] = new Vector2D(tailPositions[0].x - direction.x, tailPositions[0].y - direction.y);
                    }

                    if (!uniqueTailPositions.Contains(tailPositions[1])) uniqueTailPositions.Add(tailPositions[1]);
                }
            }
            Console.WriteLine("Unique places end tail:" + uniqueTailPositions.Count);

            //Part 2
            tailPositions = Enumerable.Repeat(new Vector2D(0, 0), 10).ToList();
            uniqueTailPositions = new List<Vector2D>();

            for (int i = 0; i < inputs.Length; i++)
            {
                string[] command = inputs[i].Split(' ');
                Vector2D direction;
                switch (command[0])
                {
                    case "L": direction = new Vector2D(-1, 0); break;
                    case "U": direction = new Vector2D(0, 1); break;
                    case "R": direction = new Vector2D(1, 0); break;
                    case "D": direction = new Vector2D(0, -1); break;
                    default: direction = new Vector2D(0, 0); break;
                }
                int move = int.Parse(command[1]);

                for (int m = 0; m < move; m++)
                {
                    tailPositions[0] = new Vector2D(tailPositions[0].x + direction.x, tailPositions[0].y + direction.y);

                    for (int t = 1; t < tailPositions.Count; t++)
                    {
                        Vector2D positionDiff = tailPositions[t-1] - tailPositions[t];
                        if (Math.Abs(positionDiff.x) > 1 || Math.Abs(positionDiff.y) > 1)
                        {
                            positionDiff.NormalizeThis();
                            tailPositions[t] = new Vector2D(tailPositions[t-1].x - positionDiff.x, tailPositions[t-1].y - positionDiff.y);
                        }
                    }
                    if (!uniqueTailPositions.Contains(tailPositions[9])) uniqueTailPositions.Add(tailPositions[9]);
                }
            }
            Console.WriteLine("Unique places end long tail:" + uniqueTailPositions.Count);
        }       
    }
}
