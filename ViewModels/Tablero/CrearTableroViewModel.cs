using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class CrearTableroViewModel
{
    public int Id{get; set;}
    public int IdUsuarioPropietario {get; set;}
    public string Nombre {get; set;}
    public string Descripcion {get; set;}
    public CrearTableroViewModel(){}
    public CrearTableroViewModel(Tablero tablero)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
}