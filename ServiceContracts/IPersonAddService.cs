using System;
using System.Net.Http.Headers;
using System.Xml.XPath;
using ServiceContracts.DTO;
using ServiceContracts.Enums;


namespace ServiceContracts
{
    public interface IPersonAddService
    {
        public Task<PersonResponse> AddPerson(PersonAddRequest request);
        
    }
}
