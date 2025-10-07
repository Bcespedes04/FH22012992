using System.ComponentModel.DataAnnotations;

namespace BinarioApp.Models
{
    public class MultipleOfTwoLengthAttribute : ValidationAttribute
    {
        public MultipleOfTwoLengthAttribute()
        {
            ErrorMessage = "La longitud debe ser múltiplo de 2 (2, 4, 6 u 8).";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var s = value as string;
            if (string.IsNullOrWhiteSpace(s)) return ValidationResult.Success; // lo valida [Required]
            return (s.Length % 2 == 0)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        }
    }
}
