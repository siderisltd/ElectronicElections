using ElectronicElections.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

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
            modelBuilder.Entity<ElectionType>().HasData(new ElectionType
            {
                Id = ElectionTypeId.NationalAssembly,
                Name = Enum.GetName(typeof(ElectionTypeId), ElectionTypeId.NationalAssembly),
                Description = "Some description",
                WikiLink = "https://google.com"
            },
            new ElectionType
            {
                Id = ElectionTypeId.PresidentalElections,
                Name = Enum.GetName(typeof(ElectionTypeId), ElectionTypeId.PresidentalElections),
                Description = "Some description",
                WikiLink = "https://google.com"
            },
            new ElectionType
            {
                Id = ElectionTypeId.EuropeanParliament,
                Name = Enum.GetName(typeof(ElectionTypeId), ElectionTypeId.EuropeanParliament),
                Description = "Some description",
                WikiLink = "https://google.com"
            });
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
