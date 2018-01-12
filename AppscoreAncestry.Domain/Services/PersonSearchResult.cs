using System.Collections.Generic;
using AppscoreAncestry.Domain.Models.PersonAggregate;

namespace AppscoreAncestry.Domain.Services
{
    public class PersonSearchResult
    {
        public PersonSearchResult(IEnumerable<Person> people, int total, int? skip, int? take)
        {
            People = people;
            Total = total;
            Skip = skip ?? 0;
            Take = (int) ((take == null || take > total) ? total: take);
        }
        public IEnumerable<Person> People { get; }
        public int Total { get; }
        public int Skip { get; }
        public int Take { get; }
    }
}