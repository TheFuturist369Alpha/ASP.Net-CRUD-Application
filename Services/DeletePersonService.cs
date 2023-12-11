using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class DeletePersonService : IPersonDeleteService
    {
        public readonly DBDemoDbContext _db;
        
        private ICountryService countryService;
        private readonly ILogger<DeletePersonService> _logger;

        public DeletePersonService(DBDemoDbContext db,ICountryService cs, ILogger<DeletePersonService> logger)
        {
            _logger = logger;
            this._db = db;
            countryService =cs;
        }


        public bool DeletePerson(Guid id)
        {
            _logger.LogInformation("DeletePerson method executing...");
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));    
            }

            Person person =_db.People.FirstOrDefault(temp=>temp.PersonId==id);
            _logger.LogDebug($"{person.Name} deleted.");
            if(person == null)
            {
                return false;
            }
            _db.People.Remove(_db.People.FirstOrDefault(temp=>temp.PersonId==id));
            _db.SaveChanges();
            return true;

        }

    }
}
