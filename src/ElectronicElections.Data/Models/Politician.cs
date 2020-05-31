using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicElections.Data.Models
{
    public class Politician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(PoliticalParty))]
        public Guid PoliticalPartyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(18, 100)]
        public byte Age { get; set; }

        [StringLength(200)]
        public string WikiLink { get; set; }

        public virtual PoliticalParty PoliticalParty { get; set; }
    }
}
