using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectronicElections.Data.Models
{
    public class PoliticalParty
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Goals { get; set; }

        [StringLength(200)]
        public string WikiLink { get; set; }

        public virtual IEnumerable<Politician> Politicians { get; set; }
    }
}
