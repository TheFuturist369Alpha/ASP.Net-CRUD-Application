using CRUDApplication.Filters;
using CRUDApplication.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace CRUDApplication.Controllers
{


    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> plogg;
        private readonly IPersonAddService _addpersonservice;
        private readonly IPersonDeleteService _deletepersonservice;
        private readonly IPersonGetAllService _getallpersonservice;
        private readonly IPersonGetByIdService _getbyidpersonservice;
        private readonly IPersonSearchService _searchpersonservice;
        private readonly IPersonSortedService _sortpersonservice;
        private readonly IPersonUpdateService _updatepersonservice;
        private readonly ICountryService _countryservice;
        private readonly ILogger<PersonController> _logger;
        private readonly ValidationHelper _vh;

        public PersonController(IPersonAddService addpersonservice,
            IPersonDeleteService deletepersonservice,
            IPersonGetAllService getallpersonservice,
            IPersonSearchService searchpersonservice,
            IPersonSortedService sortpersonservice,
            IPersonUpdateService updatepersonservice,
            IPersonGetByIdService getbyidpersonservice,
            ICountryService cs, ILogger<PersonController> pl, ValidationHelper valh, ILogger<PersonController> logger )
        {

            _addpersonservice = addpersonservice;
            _deletepersonservice = deletepersonservice;
            _getallpersonservice = getallpersonservice;
            _searchpersonservice = searchpersonservice;
            _sortpersonservice = sortpersonservice;
            _updatepersonservice = updatepersonservice;
            _getbyidpersonservice = getbyidpersonservice;
            _countryservice = cs;
            plogg = pl;
            _vh = valh;
            _logger = logger;
           
            
        }

        [Route("[action]")]
        [TypeFilter(typeof(IndexFilter))] 
        public async Task<IActionResult> Index(string? searchby, string? searchchar, 
            SortOrder sortorder=SortOrder.Asc, string sortby = nameof(PersonResponse.Name))
        {
            plogg.LogInformation("You've reached the Index method");
            plogg.LogDebug($"Search by:{searchby}\nSearch char:{searchchar}\nOrder:{sortorder}\n");
            ViewBag.Dict = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.Name),"Name" },
                {nameof(PersonResponse.Email),"Email" },
                {nameof(PersonResponse.DateOfBirth),"Date of birth" },
                {nameof(PersonResponse.Gender),"Gender" },
                {nameof(PersonResponse.Address),"Address" },
               {nameof(PersonResponse.Age),"Age" }
                

            };
           
            
            
           
            List<PersonResponse> pr =await _searchpersonservice.SearchPerson(searchby,searchchar);
            List<PersonResponse?> sb = await _sortpersonservice.GetSortedPersons(pr, sortby,sortorder);
            return View(sb);
        }


        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Countries = await _countryservice.GetAllCountries();
            
            return View();
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(PersonAddRequest par)
        {
            PersonResponse pr =  await _addpersonservice.AddPerson(par);

            return RedirectToAction("Index","Person");
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid par)
        {
            PersonResponse pr = await _getbyidpersonservice.GetPersonByPersonId(par);
            UpdatePerson up = pr.Update();
            return View(up);
        
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePerson par)
        {
           
            PersonResponse pr = await _updatepersonservice.PersonUpdate(par);
            return RedirectToAction("Index","Person");
        }

        [Route("/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            string name = await _getbyidpersonservice.GetPersonByPersonId(id)?.Name;
           if(_deletepersonservice.DeletePerson(id) == true)
            {
                _logger.LogDebug("")
            }
            
            return RedirectToAction("Index", "Person");
        }
    }
}
