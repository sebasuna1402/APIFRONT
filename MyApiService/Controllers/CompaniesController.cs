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
    public class CompaniesController : ControllerBase
    {
        public readonly IGenericSv<Company> _companySv;

        public CompaniesController(IGenericSv<Company> companySv)
        {
            _companySv = companySv;
        }

        // GET: api/<CompaniesController>
        [HttpGet]
        public List<Company> Get()
        {
            return _companySv.GetAll();
        }

        // GET api/<CompaniesController>/5
        [HttpGet("{id}")]
        public Company Get(int id)
        {
            return _companySv.GetByCondition(company => company.Id == id, "Offers");
        }

        // POST api/<CompaniesController>
        [HttpPost]
        public Company Post([FromBody] Company companyRequest)
        {
            return _companySv.Add(companyRequest);
        }

    }
}
