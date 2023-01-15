using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode2022
{
    class Day8
    {
        struct Vector2D
        {
            public int x { get; }
            public int y { get; }
            public Vector2D(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public void Execute()
        {
            int[][] inputs = File.ReadAllLines("..\\..\\..\\Day8.txt").Select(x => x.ToCharArray().Select(n => int.Parse(n.ToString())).ToArray()).ToArray();

            int edgeVisibles = 0;
            int visibles = 0;

           List<Vector2D> visibleTrees = new List<Vector2D>();

            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[y].Length; x++)
                {
                    int thisHeight = inputs[y][x];
                    if (x==0 || y==0 || x==inputs[y].Length-1 || y==inputs.Length-1)
                    {
                        edgeVisibles++;
                    }
                    else
                    {
                        bool visible = false;
                        bool toEdgeVisible = true;
                        for (int i = y-1; i >= 0; i--)
                        {
                            if (!toEdgeVisible) continue;
                            if (inputs[i][x] >= thisHeight) toEdgeVisible = false;
                        }
                        if (toEdgeVisible) visible = true; 

                        if (!visible)
                        {
                            toEdgeVisible = true;
                            for (int i = x-1; i >= 0; i--)
                            {
                                if (!toEdgeVisible) continue;
                                if (inputs[y][i] >= thisHeight) toEdgeVisible = false;
                            }
                        }
                        if (toEdgeVisible) visible = true; 

                        if (!visible)
                        {
                            toEdgeVisible = true;
                            for (int i = y+1; i < inputs.Length; i++)
                            {
                                if (!toEdgeVisible) continue;
                                if (inputs[i][x] >= thisHeight) toEdgeVisible = false;
                            }
                        }
                        if (toEdgeVisible) visible = true;

                        if (!visible)
                        {
                            toEdgeVisible = true;
                            for (int i = x+1; i < inputs[y].Length; i++)
                            {
                                if (!toEdgeVisible) continue;
                                if (inputs[y][i] >= thisHeight) toEdgeVisible = false;
                            }
                        }
                        if (toEdgeVisible) visible = true;

                        if (visible) { 
                            visibles++;
                            visibleTrees.Add(new Vector2D(x, y));
                        }
                    }
                    
                }
            }

            Console.WriteLine("Visibles: " + (visibles + edgeVisibles));


            int maxTreeScore = 0;
            for (int n = 0; n < visibleTrees.Count; n++)
            {
                int x = visibleTrees[n].x;
                int y = visibleTrees[n].y;
            
                int thisHeight = inputs[y][x];
                int treeScore = 1;
                int numberOfTrees = 0;

                for (int i = y - 1; i >= 0; i--)
                {
                    numberOfTrees++;
                    if (inputs[i][x] >= thisHeight) break;
                }
                treeScore *= (numberOfTrees == 0 ? 1 : numberOfTrees);

                numberOfTrees = 0;
                for (int i = x - 1; i >= 0; i--)
                {
                    numberOfTrees++;
                    if (inputs[y][i] >= thisHeight) break;
                }
                treeScore *= (numberOfTrees == 0 ? 1 : numberOfTrees);

                numberOfTrees = 0;
                for (int i = y + 1; i < inputs.Length; i++)
                {
                    numberOfTrees++;
                    if (inputs[i][x] >= thisHeight) break;
                }
                treeScore *= (numberOfTrees == 0 ? 1 : numberOfTrees);

                numberOfTrees = 0;
                for (int i = x + 1; i < inputs[y].Length; i++)
                {
                    numberOfTrees++;
                    if (inputs[y][i] >= thisHeight) break;
                }
                treeScore *= (numberOfTrees == 0 ? 1 : numberOfTrees);

                maxTreeScore = maxTreeScore <= treeScore ? treeScore : maxTreeScore;
            }
            Console.WriteLine("Max tree score:" + maxTreeScore);
        }
    }
}
