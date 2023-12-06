using System.Diagnostics;
using EspacioIUsuarioRepository;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_SofiaaCruz.Models;
using tl2_tp10_2023_SofiaaCruz.ViewModels;

namespace tl2_tp10_2023_SofiaaCruz.Controllers;

public class UsuarioController : Controller
{

    private readonly ILogger<UsuarioController> _logger;

    private readonly IUsuarioRepository usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
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
        var UsuarioAModificar = usuarioRepository.GetAll().FirstOrDefault(u => u.Id == usuario.Id);
        if(UsuarioAModificar == null) return RedirectToAction("Index");
        UsuarioAModificar.NombreUsuario = usuario.NombreUsuario;
        UsuarioAModificar.Password = usuario.Password;
        Debug.WriteLine(UsuarioAModificar.Password);
        usuarioRepository.ModificarUsuario(UsuarioAModificar.Id, UsuarioAModificar);
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
        if(ModelState.IsValid)
        {
            var UsuarioAEliminar = usuarioRepository.GetAll().FirstOrDefault(u => u.Id == usuario.Id);
            int result = usuarioRepository.Delete(UsuarioAEliminar.Id);
            if(result == 0) BadRequest();
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Create()
    {
        //if(!EsAdmin()) return RedirectToAction("Index");
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult Create(CrearUsuarioViewModel usuario)
    {
        if(!ModelState.IsValid) return RedirectToAction("Create");
        var nuevoUsuario = new Usuario()
        {
            NombreUsuario = usuario.NombreUsuario,
            Rol = usuario.Rol,
            Password = usuario.Password,
        };
        usuarioRepository.NuevoUsuario(nuevoUsuario);
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