using ElectronicElections.Data.Models;
using System;
using System.Linq;

namespace ElectronicElections.Data.Managers
{
    public class ElectionsManager
    {
        private readonly ElectronicElectionsDbContext ctx;

        public ElectionsManager(ElectronicElectionsDbContext ctx)
        {
            this.ctx = ctx;
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
