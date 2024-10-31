using System.IO.Compression;


namespace Infra.Core.Manipuladores
{
    public class ManipuladorZip
    {
        public byte[] CriarArquivoZip(string pastaOrigem, string caminhoZipDestino)
        {
            ZipFile.CreateFromDirectory(pastaOrigem, caminhoZipDestino);

            byte[] zipBytes = File.ReadAllBytes(caminhoZipDestino);

            if (File.Exists(caminhoZipDestino))
                File.Delete(caminhoZipDestino);

            return zipBytes;
        }

        public void ExtrairArquivoZip(string caminhoZipOrigem, string pastaDestino)
        {
            ZipFile.ExtractToDirectory(caminhoZipOrigem, pastaDestino);
        }
    }
}
