using System;
using System.Collections.Generic;
using System.Linq;
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
            Skip = result.Skip;
            Take = result.Take;
            Total = result.Total;
            Pages = (int)Math.Ceiling(Total / (float)Take);
            CurrentPage = (int)Math.Floor(Skip / (float)Take) + 1;
            People = result.People.Select(m => new PersonViewModel(m));

        }
        public IEnumerable<PersonViewModel> People { get; set; }
        public int Pages { get; }
        public int CurrentPage { get; }
        public int Skip { get; }
        public int Take { get; }
        public int Total { get; }
    }
}