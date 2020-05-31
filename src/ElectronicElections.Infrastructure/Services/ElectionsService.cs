using ElectronicElections.Data.Managers;
using ElectronicElections.Data.Models;
using System.Collections.Generic;

namespace ElectronicElections.Infrastructure.Services
{
    public class ElectionsService
    {
        private readonly ElectionsManager electionsManager;

        public ElectionsService(ElectionsManager electionsManager)
        {
            this.electionsManager = electionsManager;
        }

        public IEnumerable<PoliticalParty> GetPoliticalParties(ElectionTypeId electionType)
        {
            return this.electionsManager.GetPoliticalParties(electionType);
        }
    }
}
