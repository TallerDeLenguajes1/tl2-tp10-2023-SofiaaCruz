using System.Diagnostics;
using EspacioIUsuarioRepository;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_SofiaaCruz.Models;

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
        var usuarios = usuarioRepository.GetAll();
        return View(usuarios);
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var usuario = usuarioRepository.GetById(id);
        return View(usuario);
    }
    [HttpPost]
    public IActionResult Update(int id, Usuario user)
    {
        usuarioRepository.ModificarUsuario(id, user);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var usuario = usuarioRepository.GetById(id);
        return View(usuario);
    }
    public IActionResult DeleteConfirmed(int id)
    {
        int result = usuarioRepository.Delete(id);

        if (result > 0)
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
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult Create(Usuario usuario)
    {
        usuarioRepository.NuevoUsuario(usuario);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}