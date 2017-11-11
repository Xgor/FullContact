using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
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
            FullContactPerson person = new FullContactPerson();

            Task<FullContactPerson> task = person.LookupPersonByEmailAsync(email);

            Console.WriteLine("Loading email... ");
                
            FullContactPerson p = await task;
            
            Console.ReadLine();
        }

    }
}
