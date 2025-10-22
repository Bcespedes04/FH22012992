using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class TheModel
{
    [Required(ErrorMessage = "La frase es requerida.")]
    [MinLength(5, ErrorMessage = "La frase debe tener al menos 5 caracteres.")]
    [MaxLength(25, ErrorMessage = "La frase no debe superar los 25 caracteres.")]
    public string Phrase { get; set; } = string.Empty;

    public Dictionary<char, int> Counts { get; set; } = [];

    public string Lower { get; set; } = string.Empty;

    public string Upper { get; set; } = string.Empty;
}

// Tamaño requerido: https://blogs.visoftinc.com/2014/02/25/you-have-chosen-poorly-maxlength-vs-stringlength/
