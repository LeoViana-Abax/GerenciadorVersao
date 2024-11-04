using System.Diagnostics;
using System.IO.Compression;




namespace Infra.Core.Manipuladores
{
    public class ManipuladorZip
    {
        public async Task CriarArquivoZipAsync(string pastaOrigem, string caminhoZipDestino)
        {
            ZipFile.CreateFromDirectory(pastaOrigem, caminhoZipDestino);
        }

        public void ExtrairArquivoZip(string caminhoZipOrigem, string pastaDestino)
        {
            ZipFile.ExtractToDirectory(caminhoZipOrigem, pastaDestino);
        }
    }
}
