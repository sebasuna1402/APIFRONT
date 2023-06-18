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
    //[Authorize]
    public class OffersController : ControllerBase
    {
        public readonly IGenericSv<Offer> _offerSv;
        public readonly IGenericSv<OfferSkill> _offerSkillSv;

        public OffersController(IGenericSv<Offer> offerSv, IGenericSv<OfferSkill> offerSkillSv)
        {
            _offerSv = offerSv;
            _offerSkillSv = offerSkillSv;
        }

        // GET: api/<OffersController>
        [HttpGet]
        public List<DtoOffer> Get()
        {
            return _offerSv.GetAll(includeProperties:"Company,Skills").ToDtoList();
        }

        // GET api/<OffersController>/5
        [HttpGet("{id}")]
        public DtoOffer Get(int id)
        {
            return _offerSv.GetByCondition(offer => offer.Id == id, "Company,Skills").ToOfferDto();
        }

        // POST api/<OffersController>
        [HttpPost]
        public Offer Post([FromBody] DtoOffer offerRequest)
        {
            return _offerSv.Add(offerRequest.ToOffer());
        }

        [HttpPost]
        [Route("assign")]
        public OfferSkill AssignOfferSkill([FromBody] OfferSkill offerSkillRequest)
        {
            return _offerSkillSv.Add(offerSkillRequest);
        }

    }
}
