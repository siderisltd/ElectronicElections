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

        public CandidateModel GetCandidate(Guid id)
        {
            return this.electionsManager.GetById(id).To<CandidateModel>().FirstOrDefault();
        }

        public IEnumerable<CandidateModel> GetCandidates(ElectionTypeId electionType)
        {
            return this.electionsManager.GetCandidates(electionType).To<CandidateModel>().ToList();
        }
    }
}
