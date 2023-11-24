using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class GetAllPersonService : IPersonGetAllService
    {
        public DBDemoDbContext _db;
        
        private ICountryService countryService;

        public GetAllPersonService(DBDemoDbContext db,ICountryService cs)
        {
            this._db = db;
            countryService =cs;
        }

       

        public async Task<List<PersonResponse>> GetPeople()
        {
            List<PersonResponse> prl=new List<PersonResponse>();
           
            foreach (var people in _db.dbCopyP())
            {
                PersonResponse pr = people.ToResponse();
                prl.Add(pr);
                
            }
            return prl; 
        }

       
    }
}
