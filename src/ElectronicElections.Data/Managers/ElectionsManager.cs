using ElectronicElections.Data.Models;
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
    }
}
