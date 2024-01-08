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

            PersonResponse pr = (await _pr.UpdatePerson(up.ToPerson())).ToResponse();

                if(pr == null)
            throw new ArgumentException("The Person You're trying to update doesn't exist");

            return pr;


        }

      
    }
}
