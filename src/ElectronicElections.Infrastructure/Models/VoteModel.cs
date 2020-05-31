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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полето е задължително")]
        [StringLength(20, ErrorMessage = "Максималната дължина на името е 20 символа")]
        [DisplayName("Име")]
        public string VoterFirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полето е задължително")]
        [StringLength(20, ErrorMessage = "Максималната дължина на фамилията е 20 символа")]
        [DisplayName("Фамилия")]
        public string VoterLastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полето е задължително")]
        [Range(18, 120, ErrorMessage = "Изберете възраст между 18 и 120 години")]
        [DisplayName("Години")]
        public byte VoterAge { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полето е задължително")]
        [EmailAddress(ErrorMessage = "Полето трябва да е валиден e-mail адрес")]
        [DisplayName("Електронна поща (използва се за потвърждаване на гласа ви)")]
        public string VoterEmail { get; set; }

        [Required(ErrorMessage = "IP то ви не може да бъде локирано")]
        public string VoterIp { get; set; }

        public PoliticalPartyModel PoliticalParty { get; set; }
    }
}
