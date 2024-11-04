using Application.Dto;
using Infra.Core.Manipuladores;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Manipuladores
{
    public class ManipuladorVersao
    {
        private string _caminhoArquivoRelease;
        private string _caminhoVersoes;
        private string _caminhoPastaZip;
        private ManipuladorArquivos _manipuladorArquivos { get; set; } = new ManipuladorArquivos();
        
        private string NomeVersaoZip { get; set; }
        public ManipuladorVersao(IConfiguration configuration)
        {
            _caminhoVersoes = configuration["CaminhoVersoes"];
            _caminhoPastaZip = configuration["CaminhoPastaZip"];
            _caminhoArquivoRelease = configuration["CaminhoArquivoRelease"];
        }
        public async Task GerarVersao(string numero, string release, string versaoTeste)
        {
            NomeVersaoZip = $"Binario{numero}.{release}.{versaoTeste}.zip";

            _manipuladorArquivos.EditarXml(_caminhoArquivoRelease, numero, release, versaoTeste);
            _manipuladorArquivos.EditarArquivoControleVersao(Path.Combine(_caminhoVersoes, "ControleVersao.Json"), NomeVersaoZip);

            ManipuladorZip manipuladorZip = new ManipuladorZip();
            await manipuladorZip.CriarArquivoZipAsync(_caminhoPastaZip, Path.Combine(_caminhoVersoes, NomeVersaoZip));
        }
        public Tuple<string,string,string> BuscarUltimaRelease()
        {            
            return _manipuladorArquivos.BuscarDadosUltimaRelease(_caminhoArquivoRelease);
        }
        public List<Arquivo> BuscarVersoes()
        {
            List<Arquivo> versoes = new List<Arquivo>();

            if (Directory.Exists(_caminhoVersoes))
            {
                var arquivosZip = Directory.GetFiles(_caminhoVersoes, "*.zip");
                foreach (var arquivoPath in arquivosZip)
                {
                    versoes.Add(new Arquivo
                    {
                        Nome = Path.GetFileName(arquivoPath),
                    });
                }
            }
            return versoes;
        }
        public void ExcluirArquivo(string nome)
        {
            string caminhoArquivo = Path.Combine(_caminhoVersoes, nome);

            if (File.Exists(caminhoArquivo))
            {
                File.Delete(caminhoArquivo);
                Console.WriteLine($"Arquivo {nome} excluído com sucesso.");
            }
            else
            {
                Console.WriteLine($"Arquivo {nome} não encontrado.");
            }
        }
        public byte[] BuscarArquivo(string nome)
        {
            return _manipuladorArquivos.BuscarArquivo(nome, _caminhoVersoes);
        }
    }
}
