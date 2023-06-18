using DataAccess.Entities;
using DataAccess.Entities.Relationships;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using static Services.Extensions.DtoMapping;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormationsController : ControllerBase
    {
        public readonly IGenericSv<Formation> _formationSv;

        public FormationsController(IGenericSv<Formation> formationSv)
        {
            _formationSv = formationSv;
        }

        // GET: api/<FormationsController>
        [HttpGet]
        public List<DtoFormation> Get()
        {
            return _formationSv.GetAll().ToDtoList().OrderByDescending(formation => formation.Id).ToList();
        }

        // GET api/<FormationsController>/5
        [HttpGet("{id}")]
        public DtoFormation Get(int id)
        {
            return _formationSv.GetByCondition(formation => formation.Id == id, "Skills,Offers").ToFormationDto();
        }

        // POST api/<FormationsController>
        [HttpPost]
        public Formation Post([FromBody] DtoFormation formationRequest)
        {
            return _formationSv.Add(formationRequest.ToFormation());
        }

        
        [HttpDelete("{id}")]
        public void DeleteFormation(int id)
        {
            _formationSv.Delete(id);
        }
    }
}
