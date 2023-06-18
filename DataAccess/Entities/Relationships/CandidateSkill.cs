using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.Relationships
{
    public class CandidateSkill
    {
        public int CandidatesId { get; set; }
        public int SkillsId { get; set; }
    }
}
