using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicElections.Data.Models
{
    public class Vote
    {
        public Vote()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.VerificationCode = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public ElectionTypeId ElectionsType { get; set; }

        public Guid VoterId { get; set; }

        public Guid PoliticalPartyId { get; set; }

        public virtual PoliticalParty PoliticalParty { get; set; }

        public virtual Voter Voter { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string VotedFromIp { get; set; }

        public bool IsVerified { get; set; }

        public Guid VerificationCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? VerifiedOn { get; set; }
    }
}
