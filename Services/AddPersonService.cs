using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class AddPersonService : IPersonAddService
    {
        public DBDemoDbContext _db;
        
        private ICountryService countryService;

        public AddPersonService(DBDemoDbContext db,ICountryService cs)
        {
            this._db = db;
            countryService =cs;
        }

        public async Task<PersonResponse> AddPerson(PersonAddRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ValidationHelper.Validate(request);


            if ( await _db.People.Where(person => person.Email == request.Email).CountAsync() > 0)
            {
                throw new ArgumentException("Email already exists");
            }
            Person person = request.ToPerson();
           


                _db.People.Add(person);
            _db.SaveChangesAsync();
            PersonResponse peopleResponse = person.ToResponse();
           
            
                return peopleResponse;
                    
        }

        
    }
}
