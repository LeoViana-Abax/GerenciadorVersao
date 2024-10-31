using Domain.Entidades;
using Infra.Core.ValueTypes;
using System.Text.Json;
using System.Xml.Linq;

namespace Infra.Core.Manipuladores
{
    public class ManipuladorArquivos
    {
        public void EditarXml(string caminhoXml, string numero, string release, string versaoTeste)
        {
            caminhoXml = caminhoXml + @"RELEASE.xml";

            if (!File.Exists(caminhoXml))
                return;

            XDocument xmlDoc = XDocument.Load(caminhoXml);

            XElement? ultimoBuild = xmlDoc.Descendants(PatchVt.Build).LastOrDefault();

            if (ultimoBuild != null)
            {

                ultimoBuild.Element(PatchVt.Numero).Value = numero;
                ultimoBuild.Element(PatchVt.Release).Value = release;
                ultimoBuild.Element(PatchVt.VersaoTeste).Value = versaoTeste;

                xmlDoc.Save(caminhoXml);
            }
        }
        public void EditarArquivoControleVersao(string caminhoArquivo, string versao)
        {
            List<VersaoSistema> versoes;

            if (File.Exists(caminhoArquivo))
            {
                string jsonExistente = File.ReadAllText(caminhoArquivo);
                versoes = JsonSerializer.Deserialize<List<VersaoSistema>>(jsonExistente) ?? new List<VersaoSistema>();
            }
            else
            {
                versoes = new List<VersaoSistema>();
            }

            var novaVersao = new VersaoSistema
            {
                Versao = versao,
                Data = DateTime.Now
            };

            versoes.Add(novaVersao);

            string jsonNovo = JsonSerializer.Serialize(versoes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(caminhoArquivo, jsonNovo);
        }
    }
}

