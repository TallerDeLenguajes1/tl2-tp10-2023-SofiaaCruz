using tl2_tp10_2023_SofiaaCruz.Models;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class UpdateTableroViewModel
{
    public int Id {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(30)]
    [Display(Name = "Nombre de Tablero")]
    public string Nombre {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(30)]
    [Display(Name = "Descripción de Tablero")]
    public string Descripcion {get; set;}
    public int IdUsuarioPropietario {get; set;}
    public UpdateTableroViewModel(){}
    public UpdateTableroViewModel (Tablero tablero)
    {
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
}