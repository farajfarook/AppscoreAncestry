using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppscoreAncestry.Domain.Models.PersonAggregate;
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
        public async Task<IActionResult> Index(string name, bool male, bool female, PersonSearch.SearchMode mode, uint page = 1, uint pagesize = 10)
        {
            var search = new PersonSearch(name, mode, page, pagesize);
            if (male) search.Genders.Add(PersonGender.Male);
            if (female) search.Genders.Add(PersonGender.Female);
            var result = await _searchService.SearchAsync(search);
            return Ok(new PersonSearchResultViewModel(result));
        }
    }
}