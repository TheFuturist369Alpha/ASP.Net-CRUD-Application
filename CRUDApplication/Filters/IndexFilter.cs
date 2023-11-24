using CRUDApplication.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using ServiceContracts.DTO;

namespace CRUDApplication.Filters.ActionFilters
{
    public class IndexFilter : IActionFilter
    {

        private readonly ILogger<IndexFilter> _logger;
        public IndexFilter(ILogger<IndexFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Person-Index method executed");
            PersonController controller =(PersonController) context.Controller;
            IDictionary<string, object?>? parameters = 
                (IDictionary<string,object?>?)context.HttpContext.Items["Arguments"];
            
            if (parameters != null)
            {
                if (parameters.ContainsKey("searchby")){
                    controller.ViewData["CurrentBy"] = (string?)parameters["searchby"];
                }
                if (parameters.ContainsKey("searchar")){
                    controller.ViewData["CurrentSearch"] = (string?)parameters["searchar"];
                }
                if (parameters.ContainsKey("sortby")){
                    controller.ViewData["Currentsortby"] =(string?) parameters["sortby"];
                }
                if (parameters.ContainsKey("sortorder")){
                    controller.ViewData["Currentsortorder"] = ((SortOrder)parameters["sortorder"]).ToString();
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Executing the Person-Index method");

            context.HttpContext.Items["Arguments"] = context.ActionArguments;

            if (context.ActionArguments.ContainsKey("searchby"))
            {
                string? SearchBy = (string?)context.ActionArguments["searchby"];
                if (!string.IsNullOrEmpty(SearchBy))
                {
                    List<string> pot = new List<string>()
                    {
                        nameof(PersonResponse.Name),
                        nameof(PersonResponse.Email),
                        nameof(PersonResponse.Gender),
                        nameof(PersonResponse.Address),
                        nameof(PersonResponse.country),
                        nameof(PersonResponse.CountryId)
                    };
                    if (pot.Any(temp => temp == SearchBy)==false)
                    {
                        _logger.LogInformation("Unexpected value of searchby for action method \"Index\" is {SearchBy}",SearchBy);
                        context.ActionArguments["searchby"] = nameof(PersonResponse.Name);
                    }
                }
            }
        }
    }
}
