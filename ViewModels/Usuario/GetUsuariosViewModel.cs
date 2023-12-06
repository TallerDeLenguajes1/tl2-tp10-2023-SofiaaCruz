using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class GetUsuarioViewModel
{
    public List<Usuario> Usuarios {get; set;}
    public GetUsuarioViewModel(List<Usuario> usuarios)
    {
        Usuarios = usuarios;
    }
}