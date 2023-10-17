using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bootstrap_datepicker.Models;

namespace bootstrap_datepicker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(DateTime? selecionada)
    {

        var lista = new List<DateTime>()
        {
            // new DateTime(2023,10,16),
            new DateTime(2023,10,17),
            new DateTime(2023,10,18)
        };
        var data = selecionada ?? DateTime.Now;
        if (selecionada != null)
            lista.Add((DateTime)selecionada);
        var viewModel = new TesteViewModel()
        {
            Data = data.ToString("dd/MM/yyyy"),
            Datas = lista
        };
        return View(viewModel);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
