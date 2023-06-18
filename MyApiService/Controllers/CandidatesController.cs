using DataAccess.Entities;
using DataAccess.Entities.Relationships;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using static Services.Extensions.DtoMapping;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        public readonly IGenericSv<Candidate> _candidateSv;
        public readonly IGenericSv<CandidateOffer> _candidateOfferSv;

        public CandidatesController(IGenericSv<Candidate> candidateSv, IGenericSv<CandidateOffer> candidateOfferSv)
        {
            _candidateSv = candidateSv;
            _candidateOfferSv = candidateOfferSv;
        }

        // GET: api/<CandidatesController>
        [HttpGet]
        //[Authorize]
        public List<DtoCandidate> Get()
        {
            return _candidateSv.GetAll().ToDtoList().OrderByDescending(candidate=>candidate.Id).ToList();
        }

        // GET api/<CandidatesController>/5
        [HttpGet("{id}")]
        public DtoCandidate Get(int id)
        {
            return _candidateSv.GetByCondition(candidate => candidate.Id == id, "Skills,Offers,Formations").ToCandidateDto();
        }

        // POST api/<CandidatesController>
        [HttpPost]
        public Candidate Post([FromBody] DtoCandidate candidateRequest)
        {
            return _candidateSv.Add(candidateRequest.ToCandidate());
        }


        // POST api/<SkillsController>
        [HttpPost]
        [Route("apply")]
        public CandidateOffer AssignCandidateOffer([FromBody] CandidateOffer candidateOfferRequest)
        {
            var email = candidateOfferRequest.email;
            

            Candidate candidate = _candidateSv.GetByCondition(candidate => candidate.Email.Equals(email));
            candidateOfferRequest.CandidatesId = candidate.Id;

            return _candidateOfferSv.Add(candidateOfferRequest);
        }

        // POST api/<SkillsController>
        [HttpPost]
        [Route("unapply")]
        public void UnassignCandidateOffer([FromBody] CandidateOffer candidateOfferRequest)
        {
           _candidateOfferSv.Delete(candidateOfferRequest.CandidatesId, candidateOfferRequest.OffersId);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _candidateSv.Delete(id);
        }
    }
}
