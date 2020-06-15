using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicElections.Data.Models
{
    public class Candidate
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
        [StringLength(200)]
        public string ImgLink { get; internal set; }

        public CandidateTypeId CandidateType { get; set; }

        public virtual IEnumerable<CandidateElectionType> ParticipantInElections { get;set; }
    }
}
