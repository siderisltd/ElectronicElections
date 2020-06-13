using ElectronicElections.Data.Models;
using System;
using System.Linq;

namespace ElectronicElections.Data.Managers
{
    public class ElectionsManager
    {
        private readonly ElectronicElectionsDbContext ctx;
        private readonly EncryptionManager encryptionManager;

        public ElectionsManager(ElectronicElectionsDbContext ctx, EncryptionManager encryptionManager)
        {
            this.ctx = ctx;
            this.encryptionManager = encryptionManager;
        }

        public IQueryable<ElectionType> GetElectionTypes()
        {
            return this.ctx.ElectionTypes;
        }

        public IQueryable<PoliticalParty> GetPoliticalParties(ElectionTypeId electionType)
        {
            return this.ctx.PoliticalParties.Where(p => p.ParticipantInElections.Any(etype => etype.ElectionTypeId == electionType));
        }

        public IQueryable<PoliticalParty> GetById(Guid id)
        {
            return this.ctx.PoliticalParties.Where(p => p.Id == id);
        }

        public void PostVote(Vote vote)
        {
            var superSecretSymetricKey = "b14ca5898a4e4133bbce2ea2315a1916";

            var firstNameIdentifier = vote.Voter.FirstName[0];
            var lastNameIdentifier = vote.Voter.LastName[0];

            vote.Voter.Email = this.encryptionManager.EncryptString(superSecretSymetricKey, vote.Voter.Email);
            vote.Voter.FirstName = $"{firstNameIdentifier}.{this.encryptionManager.EncryptString(superSecretSymetricKey, vote.Voter.FirstName)}";
            vote.Voter.LastName = $"{lastNameIdentifier}.{this.encryptionManager.EncryptString(superSecretSymetricKey, vote.Voter.LastName)}";

            this.ctx.Votes.Add(vote);
            this.ctx.SaveChanges();
        }

        public bool Verify(Guid verificationCode)
        {
            var vote = this.ctx.Votes.FirstOrDefault(v => v.VerificationCode == verificationCode);
            vote.IsVerified = true;
            this.ctx.Votes.Update(vote);
            this.ctx.SaveChanges();

            return true;
        }
    }
}
