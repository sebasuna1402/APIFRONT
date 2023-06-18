using DataAccess.Entities.Relationships;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Skill : BaseEntity
    {
        
        public string Name { get; set; } = default!;

        public IEnumerable<Candidate> Candidates { get; set; } = Enumerable.Empty<Candidate>().ToList();
        public IEnumerable<Offer> Offers { get; set; } = Enumerable.Empty<Offer>().ToList();
    }
}
