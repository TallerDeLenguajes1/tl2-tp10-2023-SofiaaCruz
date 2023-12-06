using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class DeleteTableroViewModel
{
    public int Id {get; set;}
    public string Nombre {get; set;}

    public DeleteTableroViewModel(){}
    public DeleteTableroViewModel(Tablero tablero)
    {
        Id = tablero.Id;
        Nombre = tablero.Nombre;
    }
}