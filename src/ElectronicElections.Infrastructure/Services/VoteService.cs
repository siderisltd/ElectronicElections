using ElectronicElections.Data.Managers;
using ElectronicElections.Data.Models;
using ElectronicElections.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System;

namespace ElectronicElections.Infrastructure.Services
{
    public class VoteService
    {
        private readonly ElectionsManager electionsManager;

        private readonly ILogger<VoteService> logger;

        public VoteService(ILogger<VoteService> logger, ElectionsManager electionsManager)
        {
            this.electionsManager = electionsManager;
            this.logger = logger;
        }

        public bool Vote(VoteModel voteModel)
        {
            //TODO: Encrypt data

            var voter = new Voter
            {
                FirstName = voteModel.VoterFirstName,
                LastName = voteModel.VoterLastName,
                Age = voteModel.VoterAge,
                Email = voteModel.VoterEmail
            };

            var vote = new Vote
            {
                PoliticalPartyId = voteModel.PoliticalPartyId,
                VotedFromIp = voteModel.VoterIp,
                ElectionsType = voteModel.ElectionType,
                Voter = voter
            };

            try
            {
                this.electionsManager.PostVote(vote);

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
