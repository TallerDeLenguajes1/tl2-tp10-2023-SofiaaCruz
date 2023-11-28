using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class CrearUsuarioViewModel
{
    public int Id {get; set;}
    public string NombreUsuario {get; set;}
    public string Password {get; set;}
    public Roles Rol {get;set;}

    public CrearUsuarioViewModel(){}
}