using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class DeleteTareaViewModel
{
    public int Id {get; set;}
    public string Nombre {get; set;}

    public DeleteTareaViewModel(){}
    public DeleteTareaViewModel(Tarea tarea)
    {
        Id = tarea.Id;
        Nombre = tarea.Nombre;
    }
}