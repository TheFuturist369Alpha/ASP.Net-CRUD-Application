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
    public class GetAllPersonService : IPersonGetAllService
    {
        public IPersonRepo _personrepo;
        
        private ICountryService countryService;

        public GetAllPersonService(IPersonRepo personrepo,ICountryService cs)
        {
            this._personrepo = personrepo;
            countryService =cs;
        }

       

        public async Task<List<PersonResponse>> GetPeople()
        {
            return  (await _personrepo.GetAllPersons()).Select(temp => temp.ToResponse()).ToList();
        }

       
    }
}
