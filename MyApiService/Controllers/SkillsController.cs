using DataAccess.Entities;
using DataAccess.Entities.Relationships;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using static Services.Extensions.DtoMapping;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SkillsController : ControllerBase
    {
        public readonly IGenericSv<Skill> _skillSv;
        public readonly IGenericSv<CandidateSkill> _candidateSkillSv;
        public readonly IConfiguration _configuration;

        public SkillsController(IGenericSv<Skill> candidateSv, IGenericSv<CandidateSkill> candidateSkillSv, IConfiguration configuration)
        {
            _skillSv = candidateSv;
            _candidateSkillSv = candidateSkillSv;
            _configuration = configuration;
        }

        // GET: api/<SkillsController>
        [HttpGet]
        //[AllowAnonymous]
        public List<DtoSkill> Get()
        {
            try
            {
                return _skillSv.GetAll().ToDtoList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET api/<SkillsController>/5
        [HttpGet("{id}")]
        public DtoSkill Get(int id)
        {
            return _skillSv.GetById(id).ToSkillDto();
        }

        // POST api/<SkillsController>
        [HttpPost]
        public Skill Post([FromBody] DtoSkill skillRequest)
        {
            return _skillSv.Add(skillRequest.ToSkill());
        }

        // POST api/<SkillsController>
        [HttpPost]
        [Route("assign")]
        public CandidateSkill AssignCandidateSkill([FromBody] CandidateSkill candidateSkillRequest)
        {
            return _candidateSkillSv.Add(candidateSkillRequest);
        }

        [HttpPost]
        [Route("deassign")]
        public void DeassignCandidateSkill([FromBody] CandidateSkill candidateSkillRequest)
        {
            _candidateSkillSv.Delete(candidateSkillRequest.CandidatesId,candidateSkillRequest.SkillsId);
        }
    }
}
