using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class DeleteUsuarioViewModel
{
    public int Id {get; set;}
    public string NombreUsuario {get; set;}

    public DeleteUsuarioViewModel(){}
    public DeleteUsuarioViewModel(Usuario usuario)
    {
        Id = usuario.Id;
        NombreUsuario = usuario.NombreUsuario;
    }
}