using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicElections.Data.Models
{
    public class Vote
    {
        public Vote()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public ElectionTypeId ElectionsType { get; set; }

        [ForeignKey(nameof(Voter))]
        public Guid VoterId { get; set; }

        [ForeignKey(nameof(Candidate))]
        public Guid CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual Voter Voter { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string VotedFromIp { get; set; }

        public bool IsVerified { get; set; }

        public Guid VerificationCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? VerifiedOn { get; set; }
    }
}
