using ElectronicElections.Data.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ElectronicElections.Infrastructure.Models
{
    public class VoteModel
    {
        public Guid PoliticalPartyId { get; set; }

        public ElectionTypeId ElectionType { get; set; }

        public string ElectionTypeName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        [DisplayName("Име")]
        public string VoterFirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        [DisplayName("Фамилия")]
        public string VoterLastName { get; set; }

        [Required]
        [Range(18, 120)]
        [DisplayName("Години")]
        public byte VoterAge { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Електронна поща (използва се за потвърждаване на гласа ви)")]
        public string VoterEmail { get; set; }

        public string VoterIp { get; set; }

        public PoliticalPartyModel PoliticalParty { get; set; }
    }
}
