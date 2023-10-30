using System;
using System.Collections.Generic;
using ServiceContracts.DTO;
using Entities;
using Services;
using ServiceContracts;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace TestDemo
{
    public class CountriesServiceTest
    {
        private string _country = "usa";
        public readonly ICountryService services;
        public CountriesServiceTest()
        {
            services = new CountryService(new DBDemoDbContext(new DbContextOptionsBuilder<DBDemoDbContext>().Options));
        }

        //When AddRequest is null
        #region AddCountry
        [Fact]
        public void AddRequest_IsNull()
        {
            CountryAddRequest? adr = null;
            Assert.Throws<ArgumentNullException>(()=>{
                TheCountryResponse tr = services.AddCountry(adr);
            });
            
        }
        //When CountryName is null
        [Fact]
        public void AddRequest_CountryNameIsNull()
        {
            CountryAddRequest? adr = new CountryAddRequest()
            {
                CountryName = null
            };
            Assert.Throws<ArgumentException>(() => {
                TheCountryResponse tr = services.AddCountry(adr);
            });

        }
        [Fact]
        public void AddRequest_DuplicateCountry()
        {
            CountryAddRequest? adr1 = new CountryAddRequest()
            {
                CountryName = _country
            };
            CountryAddRequest? adr2= new CountryAddRequest()
            {
                CountryName = null
            };
            Assert.Throws<ArgumentException>(() => {
                TheCountryResponse tr = services.AddCountry(adr1);
                TheCountryResponse tr1 = services.AddCountry(adr2);
            });

             
        }
        [Fact]
        public void AddRequest_ProperCountryDetails()
        {
            CountryAddRequest? adr = new CountryAddRequest()
            {
                CountryName = _country
            };
            TheCountryResponse theResponse = services.AddCountry(adr);
            List<TheCountryResponse> rlist = services.GetAllCountries();
            Assert.True(theResponse.Id != Guid.Empty);
            Assert.Contains(theResponse,rlist);

        }
        #endregion

        #region GetCountries
        [Fact]
        public void GetCountries()
        {
            List<TheCountryResponse> tr = services.GetAllCountries();

            Assert.Empty(tr);
        }
        List<TheCountryResponse> h=new List<TheCountryResponse>();

        [Fact]
        public void GetCountries_AddFew()
        {
            List<CountryAddRequest> adr = new List<CountryAddRequest>()
            {
                new CountryAddRequest()
                {
                    CountryName="Usa"
                },
                new CountryAddRequest()
                {
                    CountryName="China"
                },
                new CountryAddRequest()
                {
                    CountryName="Nigeria"
                },
                new CountryAddRequest()
                {
                    CountryName="France"
                }
            };
            
            foreach(CountryAddRequest r in adr)
            {
                h.Add(services.AddCountry(r));
            }
        }
        [Fact]
        public void GetCountry_ByNullID()
        {
            Guid? value = null;

            TheCountryResponse? tr = services.GetCountryById(value);
            Assert.Null(tr);
        }
        [Fact]
        public void GetCountry_ByProperId()
        {
            CountryAddRequest adr = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            TheCountryResponse? tr = services.AddCountry(adr);
           TheCountryResponse? trg= services.GetCountryById(tr.Id);
            Assert.Equal(tr.Name, trg.Name);
        }
        #endregion



    }
}
