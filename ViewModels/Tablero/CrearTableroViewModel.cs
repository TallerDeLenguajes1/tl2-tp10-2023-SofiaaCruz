using tl2_tp10_2023_SofiaaCruz.Models;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class CrearTableroViewModel
{
    public int Id{get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Id de Usuario Propietario")]
    public int IdUsuarioPropietario {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(30)]
    [Display(Name = "Nombre de Tablero")]
    public string Nombre {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(30)]
    [Display(Name = "Descripción de Tablero")]
    public string Descripcion {get; set;}
    public CrearTableroViewModel(){}
    public CrearTableroViewModel(Tablero tablero)
    {
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
}