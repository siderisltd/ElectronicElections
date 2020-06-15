using ElectronicElections.Data.Managers;
using ElectronicElections.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicElections.Data
{
    public class ElectronicElectionsDbContext : DbContext
    {
        public DbSet<Voter> Voters { get; set; }

        public DbSet<ElectionType> ElectionTypes { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public ElectronicElectionsDbContext(DbContextOptions<ElectronicElectionsDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureRelationships(modelBuilder);
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            var dataSeedManager = new DataSeedManager();

            dataSeedManager.SeedElectionTypes(modelBuilder);
            dataSeedManager.SeedCandidates(modelBuilder);
            dataSeedManager.SeedCandidateElectionTypeRelations(modelBuilder);
        }

        private static void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateElectionType>()
                .HasKey(bc => new { bc.CandidateId, bc.ElectionTypeId });

            modelBuilder.Entity<CandidateElectionType>()
                .HasOne(bc => bc.Candidate)
                .WithMany(b => b.ParticipantInElections)
                .HasForeignKey(bc => bc.CandidateId);

            modelBuilder.Entity<CandidateElectionType>()
                .HasOne(bc => bc.ElectionType)
                .WithMany(c => c.Candidates)
                .HasForeignKey(bc => bc.ElectionTypeId);
        }
    }
}
