using System;
using System.Collections.Generic;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using Services;
using Xunit;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace TestDemo
{
    public class PersonServiceTest
    {
        private readonly IPersonService _personService;

        public PersonServiceTest()
        {
            _personService = new PersonService(new DBDemoDbContext(new DbContextOptionsBuilder<DBDemoDbContext>().Options), new CountryService(new DBDemoDbContext(new DbContextOptionsBuilder<DBDemoDbContext>().Options)));
        }
        #region Addperson
        [Fact]
        public void Request_IsNull()
        {
            PersonAddRequest? request = null;
          

            Assert.Throws<ArgumentNullException>(() => { PersonResponse pr= _personService.AddPerson(request);});
        } 
        [Fact]
        public void Request_NameIsNull()
        {
            PersonAddRequest request = new PersonAddRequest() { Name=null};
            
            Assert.Throws<ArgumentException>(() => { PersonResponse pr = _personService.AddPerson(request); });
        }
        #endregion

        #region GetAllPersons
        [Fact]
        public void GetPeople_IfNull()
        {
            List<PersonResponse> response = _personService.GetPeople();
            Assert.Empty(response);

        }
        #endregion

        #region UpdatePerson

        [Fact]
        public void null_person_update_test()
        {
            UpdatePerson up = null;

            PersonResponse pr=_personService.PersonUpdate(up);
        }

        #endregion

    }
}
