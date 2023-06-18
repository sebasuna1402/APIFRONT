using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Formation : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;

        public DateTime Date { get; set; }

        public Candidate? Candidate { get; set; } = default!;
        public int CandidateId { get; set; }
    }
}
