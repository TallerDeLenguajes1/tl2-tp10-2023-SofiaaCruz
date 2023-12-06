using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class UpdateUsuarioViewmodel
{
    public int Id {get; set;}
    public string NombreUsuario {get; set;}
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