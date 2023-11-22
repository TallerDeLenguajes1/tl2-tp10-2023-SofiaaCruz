using System.Diagnostics;
using EspacioITableroRepository;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_SofiaaCruz.Models;

namespace tl2_tp10_2023_SofiaaCruz.Controllers;

public class TableroController : Controller
{

    private readonly ILogger<TableroController> _logger;

    private readonly ITableroRepository tableroRepository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    public IActionResult Index()
    {
        var tableros = tableroRepository.GetAllTableros();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var tablero = tableroRepository.GetById(id);
        return View(tablero);
    }

    [HttpPost]
    public IActionResult Update(int id, Tablero tablero)
    {
        tableroRepository.ModificarTablero(id, tablero);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var tablero = tableroRepository.GetById(id);
        return View(tablero);
    }

    public IActionResult DeleteConfirmed(int id)
    {
        int result = tableroRepository.Delete(id);
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
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult Create(Tablero tablero)
    {
        tableroRepository.CrearTablero(tablero);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}