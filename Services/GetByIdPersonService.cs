using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class GetByIdPersonService : IPersonGetByIdService
    {
        public DBDemoDbContext _db;
        
        private ICountryService countryService;

        public GetByIdPersonService(DBDemoDbContext db,ICountryService cs)
        {
            this._db = db;
            countryService =cs;
        }


        public async Task<PersonResponse?> GetPersonByPersonId(Guid PId)
        {
            if (PId == Guid.Empty)
                return null;
            foreach(var person in await _db.People.ToListAsync())
            {
                if (person.PersonId == PId)
                {
                   return person.ToResponse();
                }
                
            }
            return null;
        }

    }
}
