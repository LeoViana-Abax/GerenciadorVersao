using Infra.Core.Manipuladores;
using Microsoft.Extensions.Configuration;

namespace Application.Manipuladores
{
    public class ManipuladorVersao
    {
        
        private string _caminhoArquivo;
        private string _caminhoPasta;
        private string _caminhoArquivoControleVersao;
        private string NomeVersaoZip { get; set; }
        public ManipuladorVersao(IConfiguration configuration)
        {
            _caminhoArquivo = configuration["CaminhoArquivo"];
            _caminhoPasta = configuration["CaminhoPasta"];
            _caminhoArquivoControleVersao = configuration["CaminhoArquivoControleVersao"];
        }
        public Byte[] GerarVersao(string numero,string release,string versaoTeste) 
        {
            NomeVersaoZip = $"Binario{numero}.{release}.{versaoTeste}.zip";

            ManipuladorArquivos manipuladorArquivos = new ManipuladorArquivos();
            manipuladorArquivos.EditarXml(_caminhoArquivo, numero, release, versaoTeste);

            manipuladorArquivos.EditarArquivoControleVersao(Path.Combine(_caminhoArquivoControleVersao,"ControleVersao.Json"),NomeVersaoZip);

            ManipuladorZip manipuladorZip = new ManipuladorZip();
            return manipuladorZip.CriarArquivoZip(_caminhoPasta,Path.Combine( _caminhoPasta, NomeVersaoZip));
        }
    }
}
