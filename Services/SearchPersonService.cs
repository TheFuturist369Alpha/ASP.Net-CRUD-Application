using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class SearchPersonService : IPersonSearchService
    {
        private IPersonRepo _pr;
        
        private ICountryService countryService;

        public SearchPersonService(IPersonRepo db,ICountryService cs)
        {
            this._pr = db;
            countryService =cs;
        }

       

        public async Task<List<PersonResponse>> SearchPerson(string searchby, string? searchname)
        {
            List<PersonResponse> pr = (searchby) switch
            {
                nameof(PersonResponse.Name) =>
                    (await _pr.GetFilteredPersons((temp => temp.Name.Contains(searchname))))
                    .Select(temp => temp.ToResponse()).ToList(),


                nameof(PersonResponse.Email) =>
                     (await _pr.GetFilteredPersons((temp => temp.Email.Contains(searchname))))
                    .Select(temp => temp.ToResponse()).ToList(),

                nameof(PersonResponse.Address) =>
                     (await _pr.GetFilteredPersons((temp => temp.Address.Contains(searchname))))
                    .Select(temp => temp.ToResponse()).ToList(),

                nameof(PersonResponse.Age) =>

                     (await _pr.GetFilteredPersons((temp => Convert.ToString(temp.ToResponse().Age).Contains(searchname))))
                    .Select(temp => temp.ToResponse()).ToList(),

                nameof(PersonResponse.DateOfBirth) =>
                    (await _pr.GetFilteredPersons((temp => temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchname))))
                  .Select(temp => temp.ToResponse()).ToList(),

                nameof(PersonResponse.Gender) =>
                (await _pr.GetFilteredPersons((temp => temp.Name.Contains(searchname))))
              .Select(temp => temp.ToResponse()).ToList(),

              _=> ( await _pr.GetAllPersons()).Select(temp=>temp.ToResponse()).ToList()

            };
            return pr;
        }
    }
}
