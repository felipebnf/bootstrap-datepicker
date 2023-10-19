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
            // agosto
            new (2023,8,5),
            new (2023,8,6),
            // setembro
            new (2023,9,10),
            new (2023,9,11),
            // outubro
            new (2023,10,17),
            new (2023,10,18),
            new (2023,10,30),
            new (2023,10,31),
            // novembro
            new (2023,11,5),
            new (2023,11,7),
        };
        var novaData = dataSelecionada ?? DateTime.Now.ToString("dd/MM/yyyy");
        var mesAtual = int.Parse(novaData.Split("/")[1]);
        var anoAtual = int.Parse(novaData.Split("/")[2]);

        var mesAnterior = ObterMesAnterior(mesAtual);
        var anoAnterior = ObterAnoAnterior(mesAtual, anoAtual);

        var proximoMes = ObterProximoMes(mesAtual);
        var proximoAno = ObterProximoAno(mesAtual, anoAtual);

        var dataInicialString = lista.Where(x => x.Month == mesAnterior && x.Year == anoAnterior).ToList().Count > 0 ? ObterPrimeiroDiaMes(mesAnterior, anoAnterior) : ObterPrimeiroDiaMes(mesAtual, anoAtual);
        var dataFinalString = lista.Where(x => x.Month == proximoMes && x.Year == proximoAno).ToList().Count > 0 ? ObterUltimoDiaMes(proximoMes, proximoAno) : ObterUltimoDiaMes(mesAtual, anoAtual);

        var dataInicial = ConverterStringParaDateTime(dataInicialString);
        var dataFinal = ConverterStringParaDateTime(dataFinalString);
        
        var novaDataDateTime = ConverterStringParaDateTime(novaData);
        var viewModel = new TesteViewModel
        {
            Data =novaData,// lista.Where(x => x == novaDataDateTime ).FirstOrDefault().ToString("dd/MM/yyyy"),
            Datas = lista.Where(x => x >= dataInicial && x <= dataFinal).ToList()
        };

        return View(viewModel);
    }


    private static int ObterMesAnterior(int mesAtual)
    {
        var mod = mesAtual % 12;
        return mod == 0 ? 11 : (mod - 1);
    }

    private static int ObterAnoAnterior(int mesAtual, int anoAtual)
    {
        var mod = mesAtual % 12;
        return mod == 1 ? (anoAtual - 1) : anoAtual;
    }

    private static int ObterProximoMes(int mesAtual)
    {
        var mod = mesAtual % 12;
        return mod == 0 ? 1 : (mod + 1);
    }

    private static int ObterProximoAno(int mesAtual, int anoAtual)
    {
        var mod = mesAtual % 12;
        return mod == 0 ? (anoAtual + 1) : anoAtual;
    }

    private static string ObterPrimeiroDiaMes(int mes, int ano)
    {
        var mesString = mes >= 10 ? mes.ToString() : $"0{mes}";

        return $"01/{mesString}/{ano}";
    }

    private static string ObterUltimoDiaMes(int mes, int ano)
    {
        var data = new DateTime(ano, mes, 28);
        while (data.Month == mes)
        {
            data = data.AddDays(1);
        }
        return data.AddDays(-1).ToString("dd/MM/yyyy");
    }

    private static DateTime ConverterStringParaDateTime(string dataString)
    {
        var array = dataString.Split("/");
        var dia = int.Parse(array[0]);
        var mes = int.Parse(array[1]);
        var ano = int.Parse(array[2]);
        return new DateTime(ano, mes, dia);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
