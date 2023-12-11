using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Services
{
    public class AddPersonService : IPersonAddService
    {
        public IPersonRepo _personrepo;
        
        private ICountryService countryService;

        public AddPersonService(IPersonRepo db,ICountryService cs)
        {
            this._personrepo = db;
            countryService =cs;
        }

        public async Task<PersonResponse> AddPerson(PersonAddRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidationHelper.Validate(request);


            if (await _personrepo.GetPersonByEmail(request.Email). > 0)
            {
                throw new ArgumentException("Email already exists");
            }
            Person person = request.ToPerson();
           


                await _personrepo.AddPerson(person);
            
            PersonResponse peopleResponse = person.ToResponse();
           
            
                return peopleResponse;
                    
        }

        
    }
}
