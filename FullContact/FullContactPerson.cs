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

    public class FullContactPerson
    {
        
    }
}
