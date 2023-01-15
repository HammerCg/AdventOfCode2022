using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode2022
{
    class Day7
    {
        class FileSystem {
            public string directoryName { get; private set; }
            public FileSystem parentDirectory { get; private set; }
            public List<FileSystem> directories = new List<FileSystem>();
            public Dictionary<string, int> files = new Dictionary<string, int>();

            public FileSystem(string directoryName, FileSystem parentDirectory) {
                this.directoryName = directoryName;
                this.parentDirectory = parentDirectory;
            }
            public int GetFilesSpace(ref int spaceOver,List<int> directoriesSpace)
            {
                int space = 0;
                for (int i = 0; i < directories.Count; i++)
                {
                    int dirSpace = directories[i].GetFilesSpace(ref spaceOver, directoriesSpace);
                    space += dirSpace;
                    directoriesSpace.Add(dirSpace);
                }
                for (int i = 0; i < files.Values.Count; i++)
                {
                    space += files.Values.ToList()[i];
                }
                if (space < 100000) spaceOver += space;
                return space;
            }
            public void AddDirectory(string directoryName) { directories.Add(new FileSystem(directoryName,this)); }
            public void AddFile(int fileSpace, string fileName)
            {
                files.Add(fileName, fileSpace);
            }
            public FileSystem GoDirectory(string directory)
            {
                for (int i = 0; i < directories.Count; i++)
                {
                    if (directories[i].directoryName.Equals(directory.Trim())) return directories[i];
                }
                return null;
            }
        }

        public void Execute()
        {
            string[] inputs = File.ReadAllLines("..\\..\\..\\Day7.txt");
            FileSystem root = new FileSystem("root",null);
            FileSystem currentDirectory = root;

            for (int i = 1; i < inputs.Length; i++)
            {
                string line = inputs[i];

                if (line.Contains("$ cd .."))
                {
                    currentDirectory = currentDirectory.parentDirectory;
                }
                else if (line.Contains("$ cd"))
                {
                    currentDirectory = currentDirectory.GoDirectory(line.Substring(4, line.Length-4));
                }
                else if (line.Contains("dir"))
                {
                    currentDirectory.AddDirectory(line.Split(" ")[1]);
                }
                else if (line.Contains("$ ls")) { }
                else
                {
                    currentDirectory.AddFile(int.Parse(line.Split(" ")[0]), line.Split(" ")[1]);
                }
            }

            int spaceOver = 0;
            List<int> directoriesSpace = new List<int>();
            int rootFileSpace = root.GetFilesSpace(ref spaceOver,directoriesSpace);
            Console.WriteLine("Part 1:" + spaceOver);

            int spaceNeeded = 30000000 -(70000000 - rootFileSpace);
            directoriesSpace = directoriesSpace.Where(x => x > spaceNeeded).OrderByDescending(x => x).ToList();
            Console.WriteLine("Part 2:" + directoriesSpace.Last());
        }

    }
}
