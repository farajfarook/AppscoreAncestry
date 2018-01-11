using System.Linq;
using System.Threading.Tasks;
using AppscoreAncestry.Domain.Models.PlaceAggregate;
using AppscoreAncestry.Domain.Services;
using AppscoreAncestry.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppscoreAncestry.Web.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController: Controller
    {
        private readonly IPersonSearchService _searchService;

        public PeopleController(IPersonSearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(PersonSearch search)
        {
            var result = await _searchService.SearchAsync(search);
            return Ok(new PersonSearchResultViewModel(result));
        }
    }
}