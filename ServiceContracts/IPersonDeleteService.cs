using System;
using System.Net.Http.Headers;
using System.Xml.XPath;
using ServiceContracts.DTO;
using ServiceContracts.Enums;


namespace ServiceContracts
{
    public interface IPersonDeleteService
    {
        
        public bool DeletePerson(Guid id);
    }
}
