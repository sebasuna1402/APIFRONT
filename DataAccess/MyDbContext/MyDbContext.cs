using Azure;
using DataAccess.Entities;
using DataAccess.Entities.Relationships;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MyDbContext : DbContext //, IMyDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }


        //TABLES PROJECT
        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<Candidate> Candidates { get; set; } = default!;
        public DbSet<Formation> Formations { get; set; } = default!;
        public DbSet<Offer> Offers { get; set; } = default!;
        public DbSet<Skill> Skills { get; set; } = default!;

        //public Task<int> SaveChangesAsync()
        //{
        //    throw new NotImplementedException();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to Many
            modelBuilder.Entity<Offer>()
            .HasOne<Company>(o => o.Company)
            .WithMany(c => c.Offers)
            .HasForeignKey(o => o.CompanyId);

            modelBuilder.Entity<Formation>()
            .HasOne<Candidate>(o => o.Candidate)
            .WithMany(c => c.Formations)
            .HasForeignKey(o => o.CandidateId);


            // Many to Many

            modelBuilder.Entity<Candidate>()
                .HasMany(e => e.Skills)
                .WithMany(e => e.Candidates)
                .UsingEntity<CandidateSkill>(
                    l => l.HasOne<Skill>().WithMany().HasForeignKey(e => e.SkillsId),
                    r => r.HasOne<Candidate>().WithMany().HasForeignKey(e => e.CandidatesId));

            modelBuilder.Entity<Offer>()
                .HasMany(e => e.Skills)
                .WithMany(e => e.Offers)
                .UsingEntity<OfferSkill>(
                    l => l.HasOne<Skill>().WithMany().HasForeignKey(e => e.SkillsId),
                    r => r.HasOne<Offer>().WithMany().HasForeignKey(e => e.OffersId));

            modelBuilder.Entity<Candidate>()
               .HasMany(e => e.Offers)
               .WithMany(e => e.Candidates)
               .UsingEntity<CandidateOffer>(
                   l => l.HasOne<Offer>().WithMany().HasForeignKey(e => e.OffersId),
                   r => r.HasOne<Candidate>().WithMany().HasForeignKey(e => e.CandidatesId));

            //Auto Includes
            modelBuilder.Entity<Offer>().Navigation(e => e.Company).AutoInclude();
            modelBuilder.Entity<Offer>().Navigation(e => e.Skills).AutoInclude();        

        }
    }
}
