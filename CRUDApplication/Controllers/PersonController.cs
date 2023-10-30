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
        private readonly IPersonService _personservice;
        private readonly ICountryService _countryservice;
        private readonly ValidationHelper _vh;
        public PersonController(IPersonService personservice, ICountryService cs, ILogger<PersonController> pl)
        {
            _personservice = personservice;
            _countryservice = cs;
            plogg = pl;
           
            
        }

        [Route("[action]")]
        public async Task<IActionResult> Index(string? searchby, string? searchchar, SortOrder sortorder=SortOrder.Asc, string sortby = nameof(PersonResponse.Name))
        {
            plogg.LogInformation("Youve reached the Index method");
            plogg.LogDebug($"Search by:{searchby}\nSearch char:{searchchar}\nOrder:{sortorder}\n");
            ViewBag.Dict = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.Name),"Person name" },
                {nameof(PersonResponse.Email),"Email" },
                {nameof(PersonResponse.DateOfBirth),"Date of birth" },
                {nameof(PersonResponse.Gender),"Gender" },
                {nameof(PersonResponse.Address),"Address" },
               {nameof(PersonResponse.Age),"Age" }
                

            };
            ViewBag.Currentsearch = searchchar;
            ViewBag.CurrentBy = searchby;
            ViewBag.Currentsortorder = nameof(sortorder);
            ViewBag.CurrentSortby = sortby;
            
            
           
            List<PersonResponse> pr =await _personservice.SearchPerson(searchby,searchchar);
            List<PersonResponse?> sb = await _personservice.GetSortedPersons(pr, sortby,sortorder);
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
            PersonResponse pr =  await _personservice.AddPerson(par);

            return RedirectToAction("Index","Person");
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid par)
        {
            PersonResponse pr = await _personservice.GetPersonByPersonId(par);
            UpdatePerson up = pr.Update();
            return View(up);
        
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePerson par)
        {
           
            PersonResponse pr = await _personservice.PersonUpdate(par);
            return RedirectToAction("Index","Person");
        }
    }
}
