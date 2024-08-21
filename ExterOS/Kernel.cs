using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace ExterOS
{
    public class Kernel : Sys.Kernel
    {
        private bool isRunning = true;
        //Home directory.
        private string CurrentDirectory = "0:\\";

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("ExterOS Booted Successfully. Type 'help' To See Available Commands.");
            Console.Beep();
        }

        protected override void Run()
        {
            while (isRunning)
            {
                Console.Write("ExterOS>: ");
                var input = Console.ReadLine().Trim().ToLower();
                var commandArgs = input.Split(' ');

                switch (commandArgs[0])
                {
                    case "help":
                        ShowHelp();
                    break;
                    case "clear":
                        Console.Clear();
                    break;
                    case "exit":
                        ExitOS();
                    break;
                    case "time":
                        ShowTime();
                    break;
                    case "date":
                        ShowDate();
                    break;
                    case "echo":
                        Echo(commandArgs);
                    break;
                    case "reboot":
                        Sys.Power.Reboot();
                    break;
                    case "titties":
                        ShowTitties(commandArgs);
                        break;
                    case "wr":
                        ShowWrite(commandArgs);
                        break;
                    case "ls":
                        ShowList(commandArgs);
                        break;
                    case "rd":
                        ShowRead(commandArgs);
                        break;
                    default:
                        Console.WriteLine($"Unknown Command: {commandArgs[0]}. Type 'help' For A List Of Commands.");
                        break;
                }
            }
        }
        Programs.Programs programs = new Programs.Programs();
        private void ShowRead(string[] commandArgs)
        {
            if (commandArgs[1] == null)
            {
                Console.WriteLine("File argument not provided.");
            }
            else
            {
                programs.ReadProg(commandArgs[1]);
            }
        }

        private void ShowList(string[] commandArgs)
        {
            if (commandArgs[1] == null)
            {
                Console.WriteLine("Directory argument not provided.");
            }
            else
            {
                programs.ListProg(commandArgs[1]);
            }
        }

        private void ShowWrite(string[] commandArgs)
        {
            //I'm so tired bruh. JUST LET ME FINISH THIS PROTO.
            if (commandArgs[1] == null && commandArgs[2] == null)
            {
                Console.WriteLine("File, Input arguments not provided.");
            }
            if (commandArgs[1] == null && commandArgs[2] != null)
            {
                Console.WriteLine("Cannot write to file \" \".");

            }
            if (commandArgs[1] != null && commandArgs[2] == null)
            {
                Console.WriteLine("Cannot write blank input to file.");
            }
            else
            {
                programs.ReadProg(commandArgs[1]);
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine("Available Commands:");
            Console.WriteLine("  help    - Shows This Help Text.");
            Console.WriteLine("  clear   - Clears The Screen.");
            Console.WriteLine("  exit    - Shuts Down The OS.");
            Console.WriteLine("  time    - Displays The Current Time.");
            Console.WriteLine("  date    - Displays The Current Date.");
            Console.WriteLine("  echo    - Repeats The Text You Type.");
            Console.WriteLine("  titties - Displays Boobies");
            Console.WriteLine("  rd      - Read a file");
            Console.WriteLine("  wr      - Write to file");
            Console.WriteLine("  ls      - List files");
            Console.WriteLine("  reboot  - Reboots The OS.");
        }

        private void ExitOS()
        {
            Console.WriteLine("Shutting Down...");
            Sys.Power.Shutdown();
            isRunning = false;
        }

        private void ShowTime()
        {
            Console.WriteLine($"Current Time: {DateTime.Now.ToShortTimeString()}");
        }

        private void ShowDate()
        {
            Console.WriteLine($"Current Date: {DateTime.Now.ToShortDateString()}");
        }

        private void Echo(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine(string.Join(" ", args, 1, args.Length - 1));
            }
            else
            {
                Console.WriteLine("Usage: echo [text]");
            }
        }
        private void ShowTitties(string[] commandArgs)
        {
            //Editable 'titties' graphics to enable more tomfoolery.
            //If only I could debug this project on my machine...
            switch (commandArgs[1])
            {
                case "big":
                case "b":
                    Console.WriteLine(File.ReadAllText("image/titties_big"));
                    break;
                case "small":
                case "s":
                    Console.WriteLine(File.ReadAllText("image/titties_small"));
                    break;
                default:
                    Console.WriteLine("Usage: titties { s/small b/big }");
                    break;
            }
        }

    }
}