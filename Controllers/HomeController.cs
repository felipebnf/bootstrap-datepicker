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

    [HttpGet]
    public IActionResult Index(string? dataSelecionada)
    {

        var lista = new List<DateTime>()
        {
            new (2023,9,10),
            new (2023,9,11),
            new (2023,10,17),
            new (2023,10,18),
            new (2023,10,30),
            new (2023,10,31),
            new (2023,11,5),
            new (2023,11,7),
        };
        var viewModel = new TesteViewModel();
        if (dataSelecionada == null)
        {
            viewModel.Data = DateTime.Now.ToString("dd/MM/yyyy");
        }
        else
        {
            var dataArray = dataSelecionada.Split('/');
            viewModel.Data = new DateTime(int.Parse(dataArray[2]), int.Parse(dataArray[1]), int.Parse(dataArray[0])).ToString("dd/MM/yyyy");
        }
        viewModel.Datas = lista;
        return View(viewModel);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
