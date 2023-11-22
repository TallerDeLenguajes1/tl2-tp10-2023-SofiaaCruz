using System.Diagnostics;
using EspacioITareaRepository;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.Controllers;

public class TareaController : Controller
{

    private readonly ILogger<TareaController> _logger;

    private readonly ITareaRepository tareaRepository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    public IActionResult Index()
    {
        var tareas = tareaRepository.GetAll();
        return View(tareas);
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var tarea = tareaRepository.GetTareaById(id);
        return View(tarea);
    }
    [HttpPost]
    public IActionResult Update(int id, Tarea tarea)
    {
        tareaRepository.ModificarTarea(id, tarea);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var tarea = tareaRepository.GetTareaById(id);
        return View(tarea);
    }

    public IActionResult DeleteConfirmed(int id)
    {
        int result = tareaRepository.Delete(id);
        if(result > 0)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Tarea());
    }

    [HttpPost]
    public IActionResult Create(Tarea tarea)
    {
        tareaRepository.CrearTarea(tarea);
        return RedirectToAction("Index");
    } 

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}