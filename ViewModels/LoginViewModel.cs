using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage ="Este campo es requerido")]
    [Display(Name ="Nombre de usuario")]
    public string NombreUsuario {get; set;}

    [Required(ErrorMessage ="Este campo es requerido")]
    [PasswordPropertyText]
    [Display(Name ="Contraseña")]
    public string Password {get; set;}
}