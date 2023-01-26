using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace AdventCode2022
{
    class Day15
    {
        string[] input = File.ReadAllLines("..\\..\\..\\Day15.txt");
        List<DistressSignal> signalList = new List<DistressSignal>();
        public void Execute()
        {

            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];
                line = line.Replace("Sensor at x=","");
                line = line.Replace(" y=","");
                line = line.Replace(": closest beacon is at x=", ",");
                line = line.Replace(" y=", ",");
                Vector2D sensor = new Vector2D(int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1]));
                Vector2D beacon = new Vector2D(int.Parse(line.Split(',')[2]), int.Parse(line.Split(',')[3]));

                signalList.Add(new DistressSignal(sensor, beacon));
            }
            int notBeaconPos = 0;

            for (int i = 0; i < signalList.Count; i++)
            {

            }
        }



        class DistressSignal
        {
            public Vector2D sensor;
            public Vector2D beacon;
            public int distance { get; private set; }

            public DistressSignal(Vector2D sensor , Vector2D beacon)
            {
                this.sensor = sensor;
                this.beacon = beacon;
                distance = Distance();

            }
            public int Distance()
            {
                Vector2D dir = sensor - beacon;
                return Math.Abs(dir.x) + Math.Abs(dir.y);
            }
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
