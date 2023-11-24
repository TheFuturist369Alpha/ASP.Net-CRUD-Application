using System;
using System.Net.Http.Headers;
using System.Xml.XPath;
using ServiceContracts.DTO;
using ServiceContracts.Enums;


namespace ServiceContracts
{
    public interface IPersonUpdateService
    {
      
        public Task<PersonResponse> PersonUpdate(UpdatePerson up);
       
    }
}
