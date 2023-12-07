using tl2_tp10_2023_SofiaaCruz.Models;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_SofiaaCruz.ViewModels;

public class UpdateTareaViewModel
{
    public int Id {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(30)]
    [Display(Name = "Nombre de Tarea")]
    public string Nombre {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Estado de la Tarea")]
    public Estados Estado {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(30)]
    [Display(Name = "Descripción de la Tarea")]
    public string Descripcion {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    public string Color {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    public int IdUsuarioAsignado {get; set;}
    public UpdateTareaViewModel(){}
    public UpdateTareaViewModel (Tarea tarea)
    {
        Nombre = tarea.Nombre;
        Estado = tarea.Estado;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
    }
}