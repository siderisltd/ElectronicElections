using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicElections.Data.Models
{
    public class Politician
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(18, 100)]
        public byte Age { get; set; }

        [StringLength(200)]
        public string WikiLink { get; set; }

        public virtual PoliticalParty PoliticalParty { get; set; }
    }
}
