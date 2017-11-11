using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullContact.Application.Service.Factories;
using FullContact.Domain.DataTransferObject;
using FullContact.Domain.Enum;
using FullContact.Domain.Service.Factories;
using FullContact.Infrastructure.Factories;
using SimpleInjector;

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

            var container = new Container();

            try
            {
                #region Dependency Injection (DI) using Simple Injector

                container.RegisterSingleton<IServiceProvider>(container);
                container.Register<IFullContactAppServiceFactory, FullContactAppServiceFactory>();
                container.Register<IHttpClientFactory, HttpClientFactory>();

                #endregion
                ///Get container full contact app service factory  
                var fullContactAppServiceFactory = container.GetInstance<IFullContactAppServiceFactory>();

                ///Create full contact app service  
                var fullContactAppService = fullContactAppServiceFactory.Create<Person>("https://api.fullcontact.com/v2", "dae0fac8e1ede966", Serializer.Json);

                /// Call full contact api by get 
                var person = await fullContactAppService.GetAsync(Lookup.Email, email);

                System.Console.Write(fullContactAppService.ResponseSerializer.Serialize(person).Result);

                Console.WriteLine("Mail Loaded!");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Unexpected error: " + ex.Message);
                
            }

         //   container.RegisterSingleton<IServiceProvider>(container);
        //    FullContactPerson person = new FullContactPerson();
        //    await Task.Delay(5000);
            
            return null;
        }

    }
}
