using DataAccess.Entities;
using DataAccess.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class DtoMapping
    {
        #region Candidates

        public struct DtoCandidate
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Summary { get; set; }
            public List<DtoSkill>? Skills { get; set; }
            public List<DtoOffer>? Offers { get; set; }
            public List<DtoFormation>? Formations { get; set; }
        }
        public static Candidate ToCandidate(this DtoCandidate dtoCandidate)
        {
            Candidate candidate = new()
            {
                Id = dtoCandidate.Id,
                Email = dtoCandidate.Email,
                Name = dtoCandidate.Name,
                Summary = dtoCandidate.Summary
            };

            return candidate;
        }

        public static bool setSkillStatus(DtoSkill skillSearch, List<DtoSkill> candidateSkills)
        {
            if (candidateSkills.FindAll(skill=>skill.Id == skillSearch.Id).Count > 0){
                return true;
            }
            return false;
        }
        public static DtoCandidate ToCandidateDto(this Candidate candidate)
        {
            DtoCandidate candidateDto = new()
            {
                Id = candidate.Id,
                Email = candidate.Email,
                Name = candidate.Name,
                Summary = candidate.Summary,
                Skills = candidate.Skills.ToList().ToDtoList(),
                Offers = candidate.Offers.ToList().ToDtoList(),
                Formations = candidate.Formations.ToList().ToDtoList().OrderByDescending(formation => formation.Id).ToList(),
            };

            candidateDto.Offers.ForEach(offer => offer.Skills?.ForEach(skill => skill.Status = setSkillStatus(skill, candidateDto.Skills)));

            return candidateDto;
        }
        public static List<DtoCandidate> ToDtoList(this List<Candidate> lstCandidate)
        {
            return lstCandidate.ConvertAll(new Converter<Candidate, DtoCandidate>(ToCandidateDto));
        }
        public static List<DtoSkill> ToDtoList(this List<Skill> lstSkill)
        {
            return lstSkill.ConvertAll(new Converter<Skill, DtoSkill>(ToSkillDto));
        }

        #endregion

        #region Formations

        public struct DtoFormation
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int CandidateId { get; set; }
            public string Date { get; set; }
        }

        public static DtoFormation ToFormationDto(this Formation formation)
        {
            DtoFormation formationDto = new()
            {
                Id = formation.Id,
                Title = formation.Title,
                Description = formation.Description,
                CandidateId = formation.CandidateId,
                Date = formation.Date.ToShortDateString()
            };

            return formationDto;
        }

        public static Formation ToFormation(this DtoFormation formationDto)
        {
            Formation formation = new()
            {
                Id = formationDto.Id,
                Title = formationDto.Title,
                Description = formationDto.Description,
                CandidateId = formationDto.CandidateId,
                Date = DateTime.Parse(formationDto.Date)
            };

            return formation;
        }

        public static List<DtoFormation> ToDtoList(this List<Formation> lstFormation)
        {
            return lstFormation.ConvertAll(new Converter<Formation, DtoFormation>(ToFormationDto));
        }
        #endregion

        #region Skills    

        public class DtoSkill
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Status { get; set; }
        }
        public static Skill ToSkill(this DtoSkill dtoSkill)
        {
            Skill skill = new()
            {
                Id = dtoSkill.Id,
                Name = dtoSkill.Name

            };

            return skill;
        }
        public static DtoSkill ToSkillDto(this Skill skill)
        {
            DtoSkill skillDto = new()
            {
                Id = skill.Id,
                Name = skill.Name

            };

            return skillDto;
        }

        #endregion

        #region Companies
        public struct DtoCompany
        {
            public int Id { get; set; }

            public string? Name { get; set; }
        }
        #endregion

        #region Offers
        public struct DtoOffer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DtoCompany Company { get; set; }
            public int CompanyId { get; set; }
            public DateTime CreatedDate { get; set; }
            public List<DtoSkill>? Skills { get; set; }
        }

        public static DtoOffer ToOfferDto(this Offer offer)
        {
            DtoOffer offerDto = new()
            {
                Id = offer.Id,
                Company = new() { Name = offer.Company?.Name },
                CompanyId = offer.CompanyId,
                Name = offer.Name,
                Description = offer.Description,
                CreatedDate = offer.CreatedDate,
                Skills = offer.Skills?.ToList().ToDtoList()
            };

            return offerDto;
        }

        public static Offer ToOffer(this DtoOffer dtoOffer)
        {
            Offer offer = new()
            {
                Id = dtoOffer.Id,
                CompanyId = dtoOffer.CompanyId,
                Name = dtoOffer.Name,
                Description = dtoOffer.Description
            };

            return offer;
        }
        public static List<DtoOffer> ToDtoList(this List<Offer> lstOffer)
        {
            return lstOffer.ConvertAll(new Converter<Offer, DtoOffer>(ToOfferDto));
        }
        #endregion
    } 
}

