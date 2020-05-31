using ElectronicElections.Data.Managers;
using ElectronicElections.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ElectronicElections.Data
{
    public class ElectronicElectionsDbContext : DbContext
    {
        public DbSet<Voter> Voters { get; set; }

        public DbSet<ElectionType> ElectionTypes { get; set; }

        public DbSet<PoliticalParty> PoliticalParties { get; set; }

        public DbSet<Politician> Politicians { get; set; }

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
            dataSeedManager.SeedElectionPoliticalParties(modelBuilder);
            dataSeedManager.SeedPoliticians(modelBuilder);
            dataSeedManager.SeedPoliticalPartyElectionTypeRelations(modelBuilder);
        }

        private static void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PoliticalPartyElectionType>()
                .HasKey(bc => new { bc.PoliticalPartyId, bc.ElectionTypeId });

            modelBuilder.Entity<PoliticalPartyElectionType>()
                .HasOne(bc => bc.PoliticalParty)
                .WithMany(b => b.ParticipantInElections)
                .HasForeignKey(bc => bc.PoliticalPartyId);

            modelBuilder.Entity<PoliticalPartyElectionType>()
                .HasOne(bc => bc.ElectionType)
                .WithMany(c => c.PoliticalParties)
                .HasForeignKey(bc => bc.ElectionTypeId);
        }
    }
}
