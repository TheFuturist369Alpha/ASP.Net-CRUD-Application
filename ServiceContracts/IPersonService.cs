using System;
using System.Net.Http.Headers;
using System.Xml.XPath;
using ServiceContracts.DTO;
using ServiceContracts.Enums;


namespace ServiceContracts
{
    public interface IPersonService
    {
        public Task<PersonResponse> AddPerson(PersonAddRequest request);
        public Task<List<PersonResponse>> GetPeople();
        public Task<PersonResponse> GetPersonByPersonId(Guid PId);
        public Task<List<PersonResponse>> SearchPerson(string searchby, string searchname);
        public Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> all, string sortby, SortOrder x);
        public Task<PersonResponse> PersonUpdate(UpdatePerson up);
        public bool DeletePerson(Guid id);
    }
}
