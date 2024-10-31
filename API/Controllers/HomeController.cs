using Application.Manipuladores;
using Infra.Core.Manipuladores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ManipuladorVersao _manipuladorVersao;
        public HomeController(ManipuladorVersao manipuladorVersao)
        {
            _manipuladorVersao = manipuladorVersao;
        }
        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GerarVersao(string numero, string versao,string release)
        {
            try
            {                
                byte[] fileContent = _manipuladorVersao.GerarVersao(numero, versao, release);
                
                return File(fileContent, "application/octet-stream", "versao.bin");
            }
            catch (Exception ex)
            {                
                return StatusCode(500, $"Erro ao gerar versão: {ex.Message}");
            }
        }        
    }
}
