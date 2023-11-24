using System;
using System.Net.Http.Headers;
using System.Xml.XPath;
using ServiceContracts.DTO;
using ServiceContracts.Enums;


namespace ServiceContracts
{
    public interface IPersonSortedService
    {

        public Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> all, string sortby, SortOrder x);
       
    }
}
