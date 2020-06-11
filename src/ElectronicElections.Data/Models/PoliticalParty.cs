using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicElections.Data.Models
{
    public class PoliticalParty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Goals { get; set; }

        [StringLength(200)]
        public string WikiLink { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LogoBase64 { get; internal set; }

        public virtual IEnumerable<PoliticalPartyElectionType> ParticipantInElections { get;set; }

        public virtual IEnumerable<Politician> Politicians { get; set; }
    }
}
