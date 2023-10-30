using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CountryService : ICountryService
    {
        private DBDemoDbContext _db;

        public CountryService(DBDemoDbContext db)
        {
            _db = db;

            
        }
       
        public async Task<TheCountryResponse> AddCountry(CountryAddRequest request)
        {
            //Validation: request is null
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            //Validation: Country name is null
            if(request.CountryName == null)
            {
                throw new ArgumentException(nameof(request.CountryName));
            }
            //Validation: Duplicate country name
            if ( await _db.Countries.Where(country => country.Name == request.CountryName).CountAsync()>0)
            {
                throw new ArgumentException("Country name already exists");
            }
            Country cntry = request.ToCountry();
            cntry.Id=Guid.NewGuid();
            _db.Countries.Add(cntry);
            await _db.SaveChangesAsync();
            return cntry.ToResponse();
        }

        public async Task<List<TheCountryResponse>> GetAllCountries()
        {
            return await _db.Countries.Select(country => country.ToResponse()).ToListAsync();
        }

        public async Task<TheCountryResponse?> GetCountryById(Guid? Id)
        {
            if (Id == null)
            {
                return null;
            }

            if (_db.Countries == null)
            {
                return null;
            }
            foreach(var country in await _db.Countries.ToListAsync())
            {
                if (country.Id == Id)
                {
                    return country.ToResponse();
                }
            }
            return null;
        }
    }
}