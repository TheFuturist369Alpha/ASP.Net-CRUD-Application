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
    public class GetByIdPersonService : IPersonGetByIdService
    {
        private readonly IPersonRepo _personRepo;
        
        private ICountryService countryService;

        public GetByIdPersonService(IPersonRepo pr,ICountryService cs)
        {
            this._personRepo = pr;
            countryService =cs;
        }


        public async Task<PersonResponse?> GetPersonByPersonId(Guid PId)
        {
            if (PId == Guid.Empty)
                return null;
            foreach(var person in await _personRepo.People.ToListAsync())
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
