using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_SofiaaCruz.Models;
using tl2_tp10_2023_SofiaaCruz.ViewModels;
using tl2_tp10_2023_SofiaaCruz.Repositorio;

namespace tl2_tp10_2023_SofiaaCruz.Controllers;

public class TableroController : Controller
{

    private readonly ILogger<TableroController> _logger;

    private readonly ITableroRepository tableroRepository;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository)
    {
        _logger = logger;
        this.tableroRepository = tableroRepository;
    }

    public IActionResult Index()
    {
        if(!IsLogin()) return RedirectToRoute (new {Controller = "Login", Action = "Index"});
        if(EsAdmin())
        {
            return View(new GetTableroViewModel(tableroRepository.GetAllTableros()));
        }
        else
        {
            var idUSuario = HttpContext.Session.GetString("Id");
            var tableros = tableroRepository.GetAllTablerosUsuario(Convert.ToInt32(idUSuario));
            if(tableros != null && tableros.Any())
            {
                return View (new GetTableroViewModel(tableros));
            } 
            return RedirectToRoute(new {Controller = "Usuario", Action = "Index"});
        }    
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        if(!EsAdmin()) return RedirectToRoute(new {Controller = "Usuario", Action = "Index"});
        return View(new UpdateTableroViewModel(tableroRepository.GetById(id)));
    }

    [HttpPost]
    public IActionResult Update(UpdateTableroViewModel tablero)
    {
        if(!ModelState.IsValid) return RedirectToAction("Update");
        var tableroAModificar = tableroRepository.GetAllTableros().FirstOrDefault(t => t.Id == tablero.Id);
        tableroAModificar.Nombre = tablero.Nombre;
        tableroAModificar.Descripcion = tablero.Descripcion;
        tableroRepository.ModificarTablero(tableroAModificar.Id, tableroAModificar);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        if(!EsAdmin()) return RedirectToRoute(new {Controller = "Usuario", Action = "Index"}); 
        var tablero = tableroRepository.GetById(id);
        return View(new DeleteTableroViewModel(tablero));
    }

    public IActionResult DeleteConfirmed(DeleteTableroViewModel tablero)
    {
        if(ModelState.IsValid)
        {
            var tareaAEliminar = tableroRepository.GetAllTableros().FirstOrDefault(t => t.Id == tablero.Id);
            int result = tableroRepository
            .Delete(tareaAEliminar.Id);
            if(result == 0) BadRequest();
        }
        return RedirectToRoute(new {Controller = "Usuario", Action = "Index"}); 
    }

    [HttpGet]
    public IActionResult Crear()
    {
        if(!EsAdmin()) return RedirectToRoute(new {Controller = "Usuario", Action = "Index"});
        return View(new CrearTableroViewModel());
    }

    [HttpPost]
    public IActionResult Crear(CrearTableroViewModel tablero)
    {
        if(!ModelState.IsValid) return RedirectToAction("Crear");
        var nuevoTablero = new Tablero()
        {
            Id = tablero.Id,
            IdUsuarioPropietario = tablero.IdUsuarioPropietario,
            Nombre = tablero.Nombre,
            Descripcion = tablero.Descripcion
        };
        tableroRepository.CrearTablero(nuevoTablero);
        return RedirectToRoute("Index");
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