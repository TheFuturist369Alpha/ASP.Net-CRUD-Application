using ServiceContracts.DTO;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ICountryService
    {
        public Task<TheCountryResponse> AddCountry(CountryAddRequest? request);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<TheCountryResponse>> GetAllCountries();

        public Task<TheCountryResponse> GetCountryById(Guid? Id);
    }
}