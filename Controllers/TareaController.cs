using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_SofiaaCruz.Models;
using tl2_tp10_2023_SofiaaCruz.ViewModels;
using tl2_tp10_2023_SofiaaCruz.Repositorio;

namespace tl2_tp10_2023_SofiaaCruz.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;

    private readonly ITareaRepository tareaRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository)
    {
        _logger = logger;
        this.tareaRepository = tareaRepository;
    }

    public IActionResult Index()
    {
        if(!IsLogin()) return RedirectToRoute(new {Controller ="Login", Action ="Index"});
        if(EsAdmin())
        {
            return View(new GetTareasViewModel(tareaRepository.GetAll()));
        }
        else
        {
            var idUSuario = HttpContext.Session.GetString("Id");
            var tareas = tareaRepository.GetAllTareasUsuario(Convert.ToInt32(idUSuario));
            if(tareas!=null && tareas.Any())
            {
                return View(new GetTareasViewModel(tareas));
            }
            else
            {
            return RedirectToRoute(new {Controller = "Usuario", Action = "Index"});
            }
        }
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        if(!EsAdmin()) return RedirectToRoute(new {Controller = "Usuario", Action = "Index"});
        return View(new UpdateTareaViewModel(tareaRepository.GetTareaById(id)));
    }
    [HttpPost]
    public IActionResult Update(UpdateTareaViewModel tarea)
    {
        if(!ModelState.IsValid) return RedirectToAction("Update");
        var tareaAModificar = tareaRepository.GetAll().FirstOrDefault(t => t.Id == tarea.Id);
        tareaAModificar.Nombre = tarea.Nombre;
        tareaAModificar.Estado = tarea.Estado;
        tareaAModificar.Descripcion = tarea.Descripcion;
        tareaAModificar.Color = tarea.Color;
        tareaRepository.ModificarTarea(tareaAModificar.Id, tareaAModificar);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        if(!EsAdmin()) return RedirectToRoute(new {Controller = "Usuario", Action = "Index"}); 
        var tarea = tareaRepository.GetTareaById(id);
        return View(new DeleteTareaViewModel(tarea));
    }

    public IActionResult DeleteConfirmed(DeleteTareaViewModel tarea)
    {
        if(ModelState.IsValid)
        {
            var tareaAEliminar = tareaRepository.GetAll().FirstOrDefault(t => t.Id == tarea.Id);
            int result = tareaRepository.Delete(tareaAEliminar.Id);
            if(result == 0) BadRequest();
        }
        return RedirectToRoute(new {Controller = "Usuario", Action = "Index"}); 
    }

    [HttpGet]
    public IActionResult Crear()
    {
        if(!EsAdmin()) return RedirectToRoute(new {Controller = "Usuario", Action = "Index"});
        return View(new CrearTareaViewModel());
    }

    [HttpPost]
    public IActionResult Crear(CrearTareaViewModel tarea)
    {
        if(!ModelState.IsValid) return RedirectToAction("Crear");
        var nuevaTarea = new Tarea()
        {
            IdTablero = tarea.IdTablero,
            Nombre = tarea.Nombre,
            Estado = tarea.Estado,
            Descripcion = tarea.Descripcion,
            Color = tarea.Color,
            IdUsuarioAsignado = tarea.IdUsuarioAsignado
        };
        tareaRepository.CrearTarea(nuevaTarea);
        return RedirectToRoute(new {Controller = "Usuario", Action = "Index"});
    } 

    private bool EsAdmin()
    {
        if(HttpContext.Session != null && HttpContext.Session.GetString("Rol") == Enum.GetName(Roles.administrador)) return true;
        return false;
    }
    private bool IsLogin()
    {
        if (HttpContext.Session.GetString("Id") != null) 
            return true;
            return false;
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}