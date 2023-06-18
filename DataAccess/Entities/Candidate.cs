using DataAccess.Entities.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Candidate: BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Summary { get; set; } = default!;

        public ICollection<Skill>? Skills { get; set; } = Enumerable.Empty<Skill>().ToList();
        public ICollection<Offer>? Offers { get; set; } = Enumerable.Empty<Offer>().ToList();
        public ICollection<Formation>? Formations { get; set; } = Enumerable.Empty<Formation>().ToList();
    }
}
