using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullContact.Application;
using FullContact.Domain;
using FullContact.Infrastructure;

namespace FullContact
{
    public interface IFullContactApi
    {
       Task<FullContactPerson> 
            LookupPersonByEmailAsync(string email);
    }


    public class FullContactPerson : IFullContactApi
    {

        public async Task<FullContactPerson> LookupPersonByEmailAsync(string email)
        {
            FullContactPerson person = new FullContactPerson();
            await Task.Delay(5000);

            return person;
        }

    }
}
