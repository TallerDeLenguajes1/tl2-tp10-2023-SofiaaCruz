using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_SofiaaCruz.Models;
using tl2_tp10_2023_SofiaaCruz.ViewModels;
using tl2_tp10_2023_SofiaaCruz.Repositorio;

namespace tl2_tp10_2023_SofiaaCruz.Controllers;

public class UsuarioController : Controller
{

    private readonly ILogger<UsuarioController> _logger;

    private readonly IUsuarioRepository usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        this.usuarioRepository = usuarioRepository;
    }


    public IActionResult Index()
    {
        if(IsLogin())
        {
            ViewBag.Nombre = HttpContext.Session.GetString("NombreUsuario");
            return View (new GetUsuarioViewModel(usuarioRepository.GetAll()));
        }
        return RedirectToRoute(new {Controller = "login", Action = "Index"});
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var idUSuario = HttpContext.Session.GetString("Id");
        if(!EsAdmin() && Convert.ToInt32(idUSuario) != id) return RedirectToAction("Index"); 
        return View(new UpdateUsuarioViewmodel(usuarioRepository.GetById(id)));
    }
    [HttpPost]
    public IActionResult Update(UpdateUsuarioViewmodel usuario)
    {
        if(!ModelState.IsValid) return RedirectToAction("Update");
        try
        {
            var UsuarioAModificar = usuarioRepository.GetAll().FirstOrDefault(u => u.Id == usuario.Id);
            if(UsuarioAModificar == null) return RedirectToAction("Index");
            UsuarioAModificar.NombreUsuario = usuario.NombreUsuario;
            UsuarioAModificar.Password = usuario.Password;
            //Debug.WriteLine(UsuarioAModificar.Password);
            usuarioRepository.ModificarUsuario(UsuarioAModificar.Id, UsuarioAModificar);
            _logger.LogInformation("El Usuario " + UsuarioAModificar.NombreUsuario + " Fue modificado correctamente");
            TempData["ErrorMessage"] = "El usuario fue modificado correctamente.";
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Hubo un error al modificar el usuario.";
        }
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var idUSuario = HttpContext.Session.GetString("Id");
        if(!EsAdmin() && Convert.ToInt32(idUSuario) != id) return RedirectToAction("Index");
        return View(new DeleteUsuarioViewModel(usuarioRepository.GetById(id)));
    }
    public IActionResult DeleteConfirmed(DeleteUsuarioViewModel usuario)
    {
        if(!ModelState.IsValid) return Redirect("Index");
        try
        {
            var UsuarioAEliminar = usuarioRepository.GetAll().FirstOrDefault(u => u.Id == usuario.Id); 
            usuarioRepository.Delete(UsuarioAEliminar.Id);
            _logger.LogInformation("El Usuario " + UsuarioAEliminar.NombreUsuario + " fue eliminado correctamente");
            TempData["ErrorMessage"] = "El usuario " + UsuarioAEliminar.NombreUsuario + " fue eliminado correctamente.";
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Hubo un error al eliminar el usuario.";
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Crear()
    {
        //if(!EsAdmin()) return RedirectToAction("Index");
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult Crear(CrearUsuarioViewModel usuario)
    {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Crear");
            var nuevoUsuario = new Usuario()
            {
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuario.Rol,
                Password = usuario.Password,
            };
            usuarioRepository.NuevoUsuario(nuevoUsuario);
            _logger.LogInformation("El Usuario: " + nuevoUsuario.NombreUsuario + " Clave: " + nuevoUsuario.Password + " fue creado correctamente");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            TempData["ErrorMessage"] = "Hubo un error al crear el usuario.";
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