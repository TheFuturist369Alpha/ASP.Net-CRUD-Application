using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using RepositoryContracts
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class SearchPersonService : IPersonSearchService
    {
        private IPersonRepo _pr;
        
        private ICountryService countryService;

        public SearchPersonService(DBDemoDbContext db,ICountryService cs)
        {
            this._pr = db;
            countryService =cs;
        }

       

        public async Task<List<PersonResponse>> SearchPerson(string searchby, string? searchname)
        {
          
            if (string.IsNullOrEmpty(searchname))
                return matching;

            switch (searchby)
            {
                case nameof(PersonResponse.Name):
                    matching = pr.Where(temp => (!string.IsNullOrEmpty(temp.Name)) ? temp.Name.Contains(searchname, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    return matching;    
                    
                case nameof(PersonResponse.Email):
                    matching = pr.Where(temp => (!string.IsNullOrEmpty(temp.Email)) ? temp.Email.Contains(searchname, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    return matching;
                    
                case nameof(PersonResponse.Address):
                    matching = pr.Where(temp => (string.IsNullOrEmpty(temp.Address)) ? temp.Address.Contains(searchname, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    return matching;
                    
                case nameof(PersonResponse.Age):
                   
                    matching = pr.Where(temp => (!(temp.Age==null)) ? Convert.ToString(temp.Age).Contains(searchname) : true).ToList();
                    return matching;
                    
                case nameof(PersonResponse.DateOfBirth):
                    matching = pr.Where(temp => (temp.DateOfBirth!=null) ? temp.DateOfBirth.Value.ToString("dd-mm-yyyy").Contains(searchname, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    return matching;
                case nameof(PersonResponse.Gender):
                    matching = pr.Where(temp => (!string.IsNullOrEmpty(temp.Gender)) ? temp.Gender.Contains(searchname) : true).ToList();
                    return matching;
                    
                default:return matching;
            }
        }
    }
}
