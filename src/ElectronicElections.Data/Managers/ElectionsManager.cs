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

        public IQueryable<Candidate> GetCandidates(ElectionTypeId electionType)
        {
            return this.ctx.Candidates.Where(p => p.ParticipantInElections.Any(etype => etype.ElectionTypeId == electionType));
        }

        public IQueryable<Candidate> GetById(Guid id)
        {
            return this.ctx.Candidates.Where(p => p.Id == id);
        }

        public bool CheckIpHasVoted(string ip, ElectionTypeId electionType)
        {
            return this.ctx.Votes.Any(v => v.VotedFromIp == this.encryptionManager.EncryptString(ip) && v.IsVerified);
        }

        public string PostVote(Vote vote)
        {
            var firstNameIdentifier = vote.Voter.FirstName[0];
            var lastNameIdentifier = vote.Voter.LastName[0];

            vote.Voter.Email = this.encryptionManager.EncryptString(vote.Voter.Email);
            vote.Voter.FirstName = $"{firstNameIdentifier}.{this.encryptionManager.EncryptString(vote.Voter.FirstName)}";
            vote.Voter.LastName = $"{lastNameIdentifier}.{this.encryptionManager.EncryptString(vote.Voter.LastName)}";
            vote.Voter.IpInfo = this.encryptionManager.EncryptString(vote.Voter.IpInfo);
            vote.VotedFromIp = this.encryptionManager.EncryptString(vote.VotedFromIp);

            this.ctx.Votes.Add(vote);
            this.ctx.SaveChanges();

            return this.encryptionManager.EncryptString(vote.VerificationCode.ToString());
        }

        public bool Verify(Guid verificationCode, string nonce)
        {
            var vote = this.ctx.Votes.FirstOrDefault(v => v.VerificationCode == verificationCode);

            if (vote == null)
            {
                return false;
            }

            var isCorrectNonce = this.encryptionManager.EncryptString(vote.VerificationCode.ToString()) == nonce;

            if (!isCorrectNonce)
            {
                return false;
            }

            vote.IsVerified = true;
            this.ctx.Votes.Update(vote);
            this.ctx.SaveChanges();

            return true;
        }
    }
}
