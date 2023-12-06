using tl2_tp10_2023_SofiaaCruz.Models;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class UpdateUsuarioViewmodel
{
    public int Id {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(30)]
    [Display(Name = "Nombre de Usuario")]
    public string NombreUsuario {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(8)]
    [Display(Name ="Contraseña")]
    public string Password {get; set;}
    public Roles rol {get; set;}

    public UpdateUsuarioViewmodel(){}
    public UpdateUsuarioViewmodel(Usuario usuario)
    {
        Id = usuario.Id;
        NombreUsuario = usuario.NombreUsuario;
        Password = usuario.Password;
        rol = usuario.Rol;
    }
}