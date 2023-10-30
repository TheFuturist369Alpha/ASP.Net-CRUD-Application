using System;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using System.ComponentModel.DataAnnotations;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PersonService : IPersonService
    {
        public DBDemoDbContext _db;
        
        private ICountryService countryService;

        public PersonService(DBDemoDbContext db,ICountryService cs)
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

        public async Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> all, string sortby, SortOrder x)
        {
           
            if (!string.IsNullOrEmpty(sortby))
                return all;
            List<PersonResponse> sorted = (sortby, x) switch
            {
                (nameof(PersonResponse.Name),SortOrder.Asc)=>all.OrderBy(temp =>temp.Name,StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Name), SortOrder.Desc) => all.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrder.Asc)=> all.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Email), SortOrder.Desc) => all.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Password), SortOrder.Asc) => all.OrderBy(temp => temp.Password, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Password) , SortOrder.Desc) => all.OrderByDescending(temp => temp.Password, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrder.Asc) => all.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Address), SortOrder.Desc) => all.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Age), SortOrder.Asc) => all.OrderBy(temp => temp.Age).ToList(),
                (nameof(PersonResponse.Age), SortOrder.Desc) => all.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.country), SortOrder.Asc) => all.OrderBy(temp => temp.country, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.country), SortOrder.Desc) => all.OrderByDescending(temp => temp.country, StringComparer.OrdinalIgnoreCase).ToList(),

                _=>all

            };
            return sorted;
        }

        public async Task<PersonResponse> PersonUpdate(UpdatePerson up)
        {
            if (up == null)
            {
                throw new ArgumentNullException();
            }


            ValidationHelper.Validate(up);

            foreach(Person person in  await _db.People.ToListAsync())
            {
                if (person.PersonId == up.Id)
                {

                    person.Name = up.Name;
                    person.Address = up.Address;
                    person.DateOfBirth = up.DateOfBirth;
                    person.Email = up.Email;
                    person.Password = up.Password;
                    person.Gender = up.Gender.ToString();
                    person.RecieveNewsLetters = up.RecieveNewsLetters;
                    person.CountryId= up.CountryId;

                     await _db.SaveChangesAsync();

                    return person.ToResponse();

                }
            }
            throw new ArgumentException("The Person Youre trying to update doesnt exist");


        }

        public async Task<List<PersonResponse>> SearchPerson(string searchby, string? searchname)
        {
            List<PersonResponse> pr = await GetPeople();
            List<PersonResponse> matching = pr;
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
