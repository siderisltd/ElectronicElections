using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicElections.Data.Models
{
    public class Voter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [Range(18, 120)]
        public byte Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
