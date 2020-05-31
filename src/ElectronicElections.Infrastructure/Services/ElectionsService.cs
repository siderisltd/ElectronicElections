using ElectronicElections.Data.Managers;
using ElectronicElections.Data.Models;
using ElectronicElections.Infrastructure.Extensions;
using ElectronicElections.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicElections.Infrastructure.Services
{
    public class ElectionsService
    {
        private readonly ElectionsManager electionsManager;

        public ElectionsService(ElectionsManager electionsManager)
        {
            this.electionsManager = electionsManager;
        }

        public IEnumerable<PoliticalPartyModel> GetPoliticalParties(ElectionTypeId electionType)
        {
            return this.electionsManager.GetPoliticalParties(electionType).To<PoliticalPartyModel>().ToList();
        }
    }
}
