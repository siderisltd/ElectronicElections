using ElectronicElections.Data.Managers;
using ElectronicElections.Data.Models;
using ElectronicElections.Infrastructure.Extensions;
using ElectronicElections.Infrastructure.Models;
using System;
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

        public PoliticalPartyModel GetPoliticalParty(Guid id)
        {
            return this.electionsManager.GetById(id).To<PoliticalPartyModel>().FirstOrDefault();
        }

        public IEnumerable<PoliticalPartyModel> GetPoliticalParties(ElectionTypeId electionType)
        {
            return this.electionsManager.GetPoliticalParties(electionType).To<PoliticalPartyModel>().ToList();
        }
    }
}
