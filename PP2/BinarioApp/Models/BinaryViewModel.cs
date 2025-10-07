using System.ComponentModel.DataAnnotations;

namespace BinarioApp.Models
{
    public class OperationResult
    {
        public string Item { get; set; } = "";
        public string Bin  { get; set; } = "";
        public string Oct  { get; set; } = "";
        public string Dec  { get; set; } = "";
        public string Hex  { get; set; } = "";
    }

    public class BinaryViewModel
    {
        [Required(ErrorMessage = "Ingrese el valor para 'a'.")]
        [Display(Name = "a")]
        [RegularExpression("^[01]+$", ErrorMessage = "Solo se permiten los caracteres 0 y 1.")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "La longitud debe ser entre 1 y 8.")]
        [MultipleOfTwoLength]
        public string A { get; set; } = "";

        [Required(ErrorMessage = "Ingrese el valor para 'b'.")]
        [Display(Name = "b")]
        [RegularExpression("^[01]+$", ErrorMessage = "Solo se permiten los caracteres 0 y 1.")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "La longitud debe ser entre 1 y 8.")]
        [MultipleOfTwoLength]
        public string B { get; set; } = "";

        public List<OperationResult>? Results { get; set; }
    }
}
