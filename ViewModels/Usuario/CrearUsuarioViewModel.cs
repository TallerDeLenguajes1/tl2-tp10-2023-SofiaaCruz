using tl2_tp10_2023_SofiaaCruz.Models;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class CrearUsuarioViewModel
{
    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(30)]
    [Display(Name = "Nombre de Usuario")]
    public string NombreUsuario {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(8)]
    [Display(Name ="Contraseña")]
    public string Password {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    public Roles Rol {get;set;}

    public CrearUsuarioViewModel(){}
}