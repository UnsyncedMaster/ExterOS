using Cosmos.System;
using System;
using System.Globalization;

namespace Programs
{
    public class Programs
    {
        VFSTools vFSTools = new VFSTools();

        public void WriteProg(string File, string Input)
        {
            var FileExists = vFSTools.FileExistanceCheck(File).Result;
            if (!FileExists)
            {
                System.Console.WriteLine("This file does not exist. Do you want to create it now?\n\r[Y] [N]");
                ConsoleKeyInfo ConsoleKeyInfo = System.Console.ReadKey(true);
                if (ConsoleKeyInfo.Key == ConsoleKey.Y)
                {
                    vFSTools.CreateFile(File);
                    vFSTools.Write(File, Input);
                }
            }
        }
        public void ListProg(string Directory)
        {
            var ListEntries = vFSTools.List(Directory).Result;
            foreach (var Entry in ListEntries)
            {
                System.Console.WriteLine(Entry);
            }
        }
        public void ReadProg(string File)
        {
            System.Console.WriteLine(vFSTools.Read(File).Result);

        }
    }
}
