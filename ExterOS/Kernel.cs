using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Sys = Cosmos.System;

namespace ExterOS
{
    public class Kernel : Sys.Kernel
    {
        private bool isRunning = true;

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
                    default:
                        Console.WriteLine($"Unknown Command: {commandArgs[0]}. Type 'help' For A List Of Commands.");
                        break;
                }
            }
        }


        private void ShowHelp()
        {
            Console.WriteLine("Available Commands:");
            Console.WriteLine("  help  - Shows This Help Text.");
            Console.WriteLine("  clear - Clears The Screen.");
            Console.WriteLine("  exit  - Shuts Down The OS.");
            Console.WriteLine("  time  - Displays The Current Time.");
            Console.WriteLine("  date  - Displays The Current Date.");
            Console.WriteLine("  echo  - Repeats The Text You Type.");
            Console.WriteLine("  reboot - Reboots The OS.");
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
    }
}