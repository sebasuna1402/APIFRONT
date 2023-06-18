using DataAccess.Entities;
using DataAccess.Entities.Relationships;
using Services.Interfaces;

namespace MyApiService
{
    public class DataSeeder
    {
        public readonly IGenericSv<Company> _companySv;
        public readonly IGenericSv<Offer> _offerSv;
        public readonly IGenericSv<Skill> _skillSv;
        public readonly IGenericSv<OfferSkill> _offerSkillSv;
        public DataSeeder(IGenericSv<Company> companySv, IGenericSv<Offer> offerSv, IGenericSv<Skill> skillSv, IGenericSv<OfferSkill> offerSkillSv)
        {
            _companySv = companySv;
            _offerSv = offerSv;
            _skillSv = skillSv;
            _offerSkillSv = offerSkillSv;
        }

        public void Seed()
        {
            _companySv.Add(new Company { Id = 1, Name = "UNA" });
            _offerSv.Add(new Offer { Id = 1, Name = "BackEnd Developer", Description = "Testing Data", CompanyId = 1 });
            _offerSv.Add(new Offer { Id = 2, Name = "FullStack Developer", Description = "Testing Data", CompanyId = 1 });
            _offerSv.Add(new Offer { Id = 3, Name = "FrontEnd Developer", Description = "Testing Data", CompanyId = 1 });

            _skillSv.Add(new Skill { Id = 1, Name = "C#" });
            _skillSv.Add(new Skill { Id = 2, Name = "SQL" });
            _skillSv.Add(new Skill { Id = 3, Name = "Python" });
            _skillSv.Add(new Skill { Id = 4, Name = "Node" });
            _skillSv.Add(new Skill { Id = 5, Name = "React" });
            _skillSv.Add(new Skill { Id = 6, Name = "Angular" });

            _offerSkillSv.Add(new OfferSkill { OffersId = 1, SkillsId = 1 });

            _offerSkillSv.Add(new OfferSkill { OffersId = 2, SkillsId = 1 });
            _offerSkillSv.Add(new OfferSkill { OffersId = 2, SkillsId = 2 });

            _offerSkillSv.Add(new OfferSkill { OffersId = 3, SkillsId = 5 });
            _offerSkillSv.Add(new OfferSkill { OffersId = 3, SkillsId = 6 });

        }
    }

}
