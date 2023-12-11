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
    public class UpdatedPersonService : IPersonUpdateService
    {
        private readonly IPersonRepo _pr;
        
        private ICountryService countryService;

        public UpdatedPersonService(IPersonRepo pr,ICountryService cs)
        {
            this._pr = pr;
            countryService =cs;
        }

      

        public async Task<PersonResponse> PersonUpdate(UpdatePerson up)
        {
            if (up == null)
            {
                throw new ArgumentNullException();
            }


            ValidationHelper.Validate(up);

            foreach(Person person in  await _pr.People.ToListAsync())
            {
                if (person.PersonId == up.Id)
                {

                    person.Name = up.Name;
                    person.Address = up.Address;
                    person.DateOfBirth = up.DateOfBirth;
                    person.Email = up.Email;
                    person.Password = up.Password;
                    person.Gender = up.Gender.ToString();
                    person.RecieveNewsLetters = up.RecieveNewsLetters;
                    person.CountryId= up.CountryId;

                     await _pr.SaveChangesAsync();

                    return person.ToResponse();

                }
            }
            throw new ArgumentException("The Person You're trying to update doesn't exist");


        }

      
    }
}
