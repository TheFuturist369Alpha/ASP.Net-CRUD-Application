using System;
using System.Net.Http.Headers;
using System.Xml.XPath;
using ServiceContracts.DTO;
using ServiceContracts.Enums;


namespace ServiceContracts
{
    public interface IPersonSearchService
    {

        public Task<List<PersonResponse>> SearchPerson(string searchby, string searchname);
        
    }
}
