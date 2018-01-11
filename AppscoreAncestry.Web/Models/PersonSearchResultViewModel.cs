using System.Collections.Generic;
using AppscoreAncestry.Domain.Services;

namespace AppscoreAncestry.Web.Models
{
    public class PersonSearchResultViewModel
    {
        public PersonSearchResultViewModel()
        {
            
        }

        public PersonSearchResultViewModel(PersonSearchResult result)
        {
         //@TODO   
        }
        public IEnumerable<PersonViewModel> Data { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Total { get; set; }
    }
}