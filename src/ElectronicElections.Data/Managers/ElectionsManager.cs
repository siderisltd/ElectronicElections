using ElectronicElections.Data.Models;
using System.Collections.Generic;
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

        public IEnumerable<ElectionType> GetElectionTypes()
        {
            return this.ctx.ElectionTypes.ToList();
        }

        public IEnumerable<PoliticalParty> GetPoliticalParties(ElectionTypeId electionType)
        {
            return this.ctx.PoliticalParties.ToList();
        }
    }
}
