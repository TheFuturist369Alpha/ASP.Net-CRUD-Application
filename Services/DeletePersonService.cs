using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class DeletePersonService : IPersonDeleteService
    {
        public DBDemoDbContext _db;
        
        private ICountryService countryService;

        public DeletePersonService(DBDemoDbContext db,ICountryService cs)
        {
            this._db = db;
            countryService =cs;
        }


        public bool DeletePerson(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));    
            }

            Person?person =_db.People.FirstOrDefault(temp=>temp.PersonId==id);
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
