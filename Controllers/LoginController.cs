using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_SofiaaCruz.Models;
using tl2_tp10_2023_SofiaaCruz.ViewModels;
using tl2_tp10_2023_SofiaaCruz.Repositorio;

namespace tl2_tp10_2023_SofiaaCruz.Controllers;

public class LoginController : Controller
{

    private readonly ILogger<LoginController> _logger;

    private readonly IUsuarioRepository usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        this.usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel usuario)
    {
        try 
        {
            if(!ModelState.IsValid) return RedirectToAction("Index");
            var usuarioLog = usuarioRepository.BuscarCuenta(usuario.NombreUsuario, usuario.Password);
            logearUsuario(usuarioLog);
            _logger.LogInformation("El usuario " + usuarioLog.NombreUsuario + " ingreso correctamente");
            return RedirectToRoute(new {controller = "Usuario", action = "Index"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());

            _logger.LogWarning("Intento de acceso invalido - Usuario: " + usuario.NombreUsuario + " Clave ingresada: " + usuario.Password);
            TempData["ErrorMessage"] = "Nombre de usuario o contraseña incorrectos.";
            return RedirectToAction("Index");
        }
    }

    private void logearUsuario(Usuario usuario)
    {
        HttpContext.Session.SetString("Id", usuario.Id.ToString());
        //Debug.WriteLine($"Valor de IdUsuario en Sesión: {idUsuario}");
        HttpContext.Session.SetString("NombreUsuario",usuario.NombreUsuario);
        HttpContext.Session.SetString("Password",usuario.Password);
        HttpContext.Session.SetString("Rol",usuario.Rol.ToString());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}