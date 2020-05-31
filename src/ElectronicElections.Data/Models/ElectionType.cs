using System.ComponentModel.DataAnnotations;

namespace ElectronicElections.Data.Models
{
    public class ElectionType
    {
        public ElectionTypeId Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        public string WikiLink { get; set; }
    }
}
