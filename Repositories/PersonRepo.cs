using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System.Linq.Expressions;

namespace Repositories
{
    public class PersonRepo : IPersonRepo
    {
        private readonly DBDemoDbContext _dbContext;


        public PersonRepo(DBDemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddPerson(Person person)
        {
            _dbContext.People.Add(person);
           await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePerson(Guid id)
        {
            _dbContext.People.Remove(_dbContext.People.FirstOrDefault(temp => temp.PersonId == id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Person>> GetAllPersons()
        {
            return await _dbContext.People.ToListAsync();
        }

        public async Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
           return await _dbContext.People.Where(predicate).ToListAsync();
        }

        public async Task<Person?> GetPersonById(Guid id)
        {
           return await _dbContext.People.FirstOrDefaultAsync(temp => temp.PersonId == id);
        }

        public async Task UpdatePerson(Person person)
        {
           Person? p=await _dbContext.People.FirstOrDefaultAsync(temp=>temp.PersonId == person.PersonId);
            if (p == null) return;
            p.TIN = person.TIN;
            p.Address = person.Address;
            p.Gender = person.Gender;
            p.RecieveNewsLetters = person.RecieveNewsLetters;
            p.DateOfBirth = person.DateOfBirth;
            p.Email = person.Email;
            p.Name = person.Name;
            p.Password = person.Password;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Person>> GetPersonByEmail(string email)
        {
            return await _dbContext.People.Where(p => p.Email == email).ToListAsync();
        }
    }
}