using Application.Manipuladores;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ManipuladorVersao _manipuladorVersao;
        public HomeController(ILogger<HomeController> logger, ManipuladorVersao manipuladorVersao)
        {
            _logger = logger;
            _manipuladorVersao = manipuladorVersao;
        }

        public IActionResult Index()
        {
            var arquivos = _manipuladorVersao.BuscarVersoes();
            var dadosUltimaRelease = _manipuladorVersao.BuscarUltimaRelease();
            var model = new ArquivosViewModel
            {
                Arquivos = arquivos.Select(a => new ArquivoModel
                { 
                    Nome = a.Nome                    
                }).ToList(),
                Numero = dadosUltimaRelease.Item1,
                Release = dadosUltimaRelease.Item2,
                VersaoTeste = dadosUltimaRelease.Item3
            };

            return View(model);
        }       

        public async Task<IActionResult> GerarVersao(string numero, string versao, string release)
        {
            await _manipuladorVersao.GerarVersao(numero,release,versao);
            return RedirectToAction("Index");
        }

        public IActionResult DownloadArquivo(string nomeArquivo)
        {
            var arquivoBytes = _manipuladorVersao.BuscarArquivo(nomeArquivo);
            return File(arquivoBytes, "application/zip", nomeArquivo);
        }

        public IActionResult ExcluirArquivo(string nomeArquivo)
        {
            _manipuladorVersao.ExcluirArquivo(nomeArquivo);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
