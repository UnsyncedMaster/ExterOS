using Cosmos.System;
using System;
using System.Globalization;

namespace Programs
{
    public class Programs
    {

        public void WriteProg(string File, string Input)
        {
            var FileExists = VFSTools.FileExistanceCheck(File);
            if (!FileExists)
            {
                System.Console.WriteLine("This file does not exist. Do you want to create it now?\n\r[Y] [N]");
                ConsoleKeyInfo ConsoleKeyInfo = System.Console.ReadKey(true);
                if (ConsoleKeyInfo.Key == ConsoleKey.Y)
                {
                    VFSTools.CreateFile(File);
                    VFSTools.Write(File, Input);
                }
            }
        }
        public void ListProg(string Directory)
        {
            var ListEntries = VFSTools.List(Directory);
            foreach (var Entry in ListEntries)
            {
                System.Console.WriteLine(Entry);
            }
        }
        public void ReadProg(string File)
        {
            System.Console.WriteLine(VFSTools.Read(File));

        }
    }
}
