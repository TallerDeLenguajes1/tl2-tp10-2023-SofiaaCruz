using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class GetTareasViewModel
{
    public List<Tarea> Tareas {get; set;}

    public GetTareasViewModel (List<Tarea> tareas)
    {
        Tareas = tareas;
    }
}