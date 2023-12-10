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
        try
        {
            var tableroAModificar = tableroRepository.GetById(tablero.Id);
            tableroAModificar.ActualizarDatos(tablero);
            tableroRepository.ModificarTablero(tableroAModificar.Id, tableroAModificar);
            _logger.LogInformation("El tablero fue modificado correctamente");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            _logger.LogWarning("El tablero no pudo ser modificado");
        }
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
        if(!ModelState.IsValid) return RedirectToAction ( "Index"); 
        try
        {
            var tareaAEliminar = tableroRepository.GetAllTableros().FirstOrDefault(t => t.Id == tablero.Id);
            tableroRepository.Delete(tareaAEliminar.Id);
            _logger.LogInformation("El tablero se elimino de forma correcta");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            _logger.LogWarning("No se pudo eliminar el tablero");
        }
        return RedirectToAction("Index");
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
        if(!ModelState.IsValid) return RedirectToRoute(new {Controller = "Usuario", Action = "Index"});
        try
        {
            var nuevoTablero = new Tablero()
            {
                Id = tablero.Id,
                IdUsuarioPropietario = tablero.IdUsuarioPropietario,
                Nombre = tablero.Nombre,
                Descripcion = tablero.Descripcion
            };
            tableroRepository.CrearTablero(nuevoTablero);
            _logger.LogInformation("El tablero fue creado de forma correcta");

        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            _logger.LogWarning("No se pudo crear el tablero");
        }
        return RedirectToAction("Index");
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