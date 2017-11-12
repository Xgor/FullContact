using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Timers;

namespace FullContact
{
    class Program
    {
        static int timerLoops = 0;
        static int CursorTopLoad = 0;
        static bool timerOn = false;
        static void Main(string[] args)
        {
            Task t = MainAsync(args);
            t.Wait();
        }

        static async Task MainAsync(string[] args)
        {
            int i = 0;
            string choice = "y";
            while (choice[0] == 'y')
            {
                Console.Write("Enter E-mail: ");

                

                string email = Console.ReadLine();
                FullContactApi search = new FullContactApi();

                Task<FullContactPerson> task = search.LookupPersonByEmailAsync(email);
                Console.Write("Loading email");
                CursorTopLoad = Console.CursorTop+1;

                Timer timer = new Timer(50);
                timer.Elapsed += async (sender, e) => await WriteLoadingSpinner();
                timer.Start();
                timerOn = true;


                FullContactPerson p = await task;
                timer.Stop();
                timerOn = false;
                Console.WriteLine("\nMail Loaded!");
                search.printLatestPersonInfo();

                Console.WriteLine("Want to check another mail (y/n)");
                choice = Console.ReadLine();
      //          Console.Clear();
            }
        }
        

        static Task WriteLoadingSpinner()
        {
            timerLoops++;
            Console.CursorLeft = 0;
            Console.CursorTop = CursorTopLoad;

            int xPos = 3;
            switch(timerLoops%8)
            {
                case 0:
                    
                    WriteLinePlus(" .o ",xPos);
                    WriteLinePlus("   O",xPos);
                    break;
                case 1:
                    WriteLinePlus("  . ",xPos);
                    WriteLinePlus("   o",xPos);
                    WriteLinePlus("   O",xPos);
                    break;
                case 2:
                    WriteLinePlus("    ",xPos);
                    WriteLinePlus("   .",xPos);
                    WriteLinePlus("   o",xPos);
                    WriteLinePlus("  O ",xPos);
                    break;
                case 3:
                    WriteLinePlus("    ",xPos);
                    WriteLinePlus("    ",xPos);
                    WriteLinePlus("   .",xPos);
                    WriteLinePlus(" Oo ",xPos);
                    break;
                case 4:
                    WriteLinePlus("    ",xPos);
                    WriteLinePlus("    ",xPos);
                    WriteLinePlus("O   ",xPos);
                    WriteLinePlus(" o. ",xPos);
                    break;
                case 5:
                    WriteLinePlus("",xPos);
                    WriteLinePlus("O",xPos);
                    WriteLinePlus("o",xPos);
                    WriteLinePlus(" .",xPos);
                    break;
                case 6:
                    WriteLinePlus(" O",xPos);
                    WriteLinePlus("o",xPos);
                    WriteLinePlus(".",xPos);
                    WriteLinePlus("    ",xPos);
                    break;
                case 7:
                    WriteLinePlus(" oO",xPos);
                    WriteLinePlus(".",xPos);
                    WriteLinePlus("    ",xPos);
                    break;
            }

            return Task.FromResult(0);
        }
        static void WriteLinePlus(string text,int offsetLeft)
        {
            Console.CursorLeft = offsetLeft;
            Console.WriteLine(text);
        }
    }
}
