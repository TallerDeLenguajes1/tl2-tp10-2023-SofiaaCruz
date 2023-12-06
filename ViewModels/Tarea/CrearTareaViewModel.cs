using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class CrearTareaViewModel
{
    public int Id{get; set;}
    public int IdTablero {get; set;}
    public string Nombre {get; set;}
    public Estados Estado {get; set;}
    public string Descripcion {get; set;}
    public string Color {get; set;}
    public int IdUsuarioAsignado {get; set;}
    public CrearTareaViewModel(){}
    public CrearTareaViewModel(Tarea tarea)
    {
        Id = tarea.Id;
        IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        Estado = tarea.Estado;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
}