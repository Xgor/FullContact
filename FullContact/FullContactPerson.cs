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

    // FullContactPerson is containing
    public class FullContactPerson
    {
        public string familyName, givenName, fullName;
        public List<string> middleNames;

        public int age;
        public string city, continent, country, county, deducedLocation, gender, NormalizedLocation, State;
        public decimal likelihood,locationLikelihood;

       
        public string Organizations;
    }

    public class FullContactApi : IFullContactApi
    {
        Container container;

        public FullContactApi()
        {
            container = new Container();
            #region Dependency Injection (DI) using Simple Injector

            container.RegisterSingleton<IServiceProvider>(container);
            container.Register<IFullContactAppServiceFactory, FullContactAppServiceFactory>();
            container.Register<IHttpClientFactory, HttpClientFactory>();


            #endregion
        }
        public async Task<FullContactPerson> LookupPersonByEmailAsync(string email)
        {

            
            FullContactPerson p = new FullContactPerson();
            try
            {
                ///Get container full contact app service factory  
                var fullContactAppServiceFactory = container.GetInstance<IFullContactAppServiceFactory>();

                ///Create full contact app service  
                var fullContactAppService = fullContactAppServiceFactory.Create<Person>("https://api.fullcontact.com/v2", "dae0fac8e1ede966", Serializer.Json);

                /// Call full contact api by get 
                var person = await fullContactAppService.GetAsync(Lookup.Email, email);
                //        person.Status
                //person.ContactInfo.WebSites

                p.familyName = person.ContactInfo.FamilyName;
                p.givenName = person.ContactInfo.GivenName;
                p.fullName = person.ContactInfo.FullName;
                p.middleNames = person.ContactInfo.MiddleNames;

                p.likelihood = person.Likelihood;
                
                p.age = person.Demographics.locationDeduced.Age;
                if(person.Demographics.locationDeduced.City != null)
                    p.city = person.Demographics.locationDeduced.City.Name;
                
                p.continent = person.Demographics.locationDeduced.Continent.Name;
                if(person.Demographics.locationDeduced.Country != null)
                    p.country = person.Demographics.locationDeduced.Country.Name;

                if (person.Demographics.locationDeduced.county != null)
                    p.county = person.Demographics.locationDeduced.county.Name;

                p.deducedLocation = person.Demographics.locationDeduced.DeducedLocation;

                p.gender= person.Demographics.locationDeduced.Gender.ToString();
                p.locationLikelihood=person.Demographics.locationDeduced.Likelihood;

                if (person.Demographics.locationDeduced.State != null)
                    p.State= person.Demographics.locationDeduced.State.Name;
                
                p.NormalizedLocation=person.Demographics.locationDeduced.NormalizedLocation;
                
                
                System.Console.Write(fullContactAppService.ResponseSerializer.Serialize(person).Result);

                return p;
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
