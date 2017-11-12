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
        
        static void Main(string[] args)
        {
            Task t = MainAsync(args);
            t.Wait();
        }

        static async Task MainAsync(string[] args)
        {

            Console.Write("Enter E-mail: ");
            
            string email = Console.ReadLine();
            FullContactSearch search = new FullContactSearch();

            Task<FullContactPerson> task = search.LookupPersonByEmailAsync(email);
            Console.Write("Loading email");

            Timer timer = new Timer(100);
            timer.Elapsed += async (sender, e) => await AddLoadingDot();
            timer.Start();


            FullContactPerson p = await task;
            timer.Stop();
            Console.WriteLine("\nMail Loaded!");


            Console.WriteLine("Name: " + p.fullName);
            Console.WriteLine("Gender: "+p.gender);

            Console.ReadLine();
        }
        

        static Task AddLoadingDot()
        {
            Console.Write(".");
            return Task.FromResult(0);
        }
    }
}
