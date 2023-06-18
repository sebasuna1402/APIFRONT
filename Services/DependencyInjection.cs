using DataAccess.Entities;
using DataAccess.Entities.Relationships;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IGenericSv<Candidate>, GenericSv<Candidate>>();            
            services.AddScoped<IGenericSv<Formation>, GenericSv<Formation>>();            
            services.AddScoped<IGenericSv<Offer>, GenericSv<Offer>>();
            services.AddScoped<IGenericSv<Skill>, GenericSv<Skill>>();
            services.AddScoped<IGenericSv<Company>, GenericSv<Company>>();

            services.AddScoped<IGenericSv<CandidateSkill>, GenericSv<CandidateSkill>>();
            services.AddScoped<IGenericSv<OfferSkill>, GenericSv<OfferSkill>>();
            services.AddScoped<IGenericSv<CandidateOffer>, GenericSv<CandidateOffer>>();

            return services;
        }
    }
}
