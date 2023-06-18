using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.Relationships
{
    public class CandidateOffer
    {
        public int CandidatesId { get; set; }
        public int OffersId { get; set; }

        public string? email { get; set; }
    }
}
