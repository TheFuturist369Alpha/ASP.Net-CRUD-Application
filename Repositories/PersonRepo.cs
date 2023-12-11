using Entities;
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


        public Task AddPerson(Person person)
        {
            _dbContext.Add(person);
            _dbContext.SaveChanges();
        }

        public Task DeletePerson(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}