using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Offer : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Company? Company { get; set; } = default!;
        public int CompanyId { get; set; }

        public ICollection<Skill>? Skills { get; set; } = Enumerable.Empty<Skill>().ToList();
        public ICollection<Candidate>? Candidates { get; set; } = Enumerable.Empty<Candidate>().ToList();
    }
}
