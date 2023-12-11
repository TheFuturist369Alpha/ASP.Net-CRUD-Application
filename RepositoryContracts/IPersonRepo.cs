using Entities;
using ServiceContracts.Enums;
using System.Linq.Expressions;

namespace RepositoryContracts
{
    public interface IPersonRepo
    {
        public Task AddPerson(Person person);
        public Task<List<Person>> GetAllPersons();
        public Task DeletePerson(Guid id);
        public Task<Person> GetPersonById(Guid id);
        
        public Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate);

        public Task<Person> UpdatePerson(Person person);
    }
}