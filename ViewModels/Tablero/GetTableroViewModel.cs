using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class GetTableroViewModel
{
    public List<Tablero> Tableros;

    public GetTableroViewModel(List<Tablero> tableros)
    {
        Tableros = tableros;
    }
}