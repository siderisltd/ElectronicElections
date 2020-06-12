using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ElectronicElections.Infrastructure.Models
{
    public class VerifyVoteModel : IValidatableObject
    {
        [DisplayName("Код за потвърждаване")]
        public string Code { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Code))
            {
                yield return new ValidationResult("Полето е задължително", new List<string> { nameof(this.Code) });
            }

            if (!Guid.TryParse(this.Code?.ToString(), out Guid guid))
            {
                yield return new ValidationResult("Невалиден формат. Моля, копирайте кода точно както е написан в пощата ви", new List<string> { nameof(this.Code) });
            }
        }
    }
}
