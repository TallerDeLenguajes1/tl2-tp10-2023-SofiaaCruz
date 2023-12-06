using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class UpdateTableroViewModel
{
    public int Id {get; set;}
    public string Nombre {get; set;}
    public string Descripcion {get; set;}
    public int IdUsuarioPropietario {get; set;}
    public UpdateTableroViewModel(){}
    public UpdateTableroViewModel (Tablero tablero)
    {
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
    }
}