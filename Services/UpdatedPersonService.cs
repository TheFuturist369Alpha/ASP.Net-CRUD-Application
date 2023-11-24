using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UpdatedPersonService : IPersonUpdateService
    {
        public DBDemoDbContext _db;
        
        private ICountryService countryService;

        public UpdatedPersonService(DBDemoDbContext db,ICountryService cs)
        {
            this._db = db;
            countryService =cs;
        }

      

        public async Task<PersonResponse> PersonUpdate(UpdatePerson up)
        {
            if (up == null)
            {
                throw new ArgumentNullException();
            }


            ValidationHelper.Validate(up);

            foreach(Person person in  await _db.People.ToListAsync())
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

                     await _db.SaveChangesAsync();

                    return person.ToResponse();

                }
            }
            throw new ArgumentException("The Person You're trying to update doesn't exist");


        }

      
    }
}
