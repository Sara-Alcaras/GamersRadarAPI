using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace GamersRadarAPI.Utils
{
    // Singleton -> Static para não precisar referenciar
    public static class Upload
    {
        // Upload de arquivos em geral
        public static string UploadFile(IFormFile arquivo, string[] extensoesPermitidas, string diretorio)
        {
            try
            {
                // Verifica a pasta onde os arquivos vão ficar salvos
                var pasta = Path.Combine("StaticFiles", diretorio);

                // Pega a pasta e a raiz(diretorio) onde vai ficar salvo o arquivo
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);

                // Verificamos se existe um arquivo para ser salvo
                if (arquivo.Length > 0)
                {
                    // Pegar o nome do arquivo
                    string nomeArquivo = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');

                    // Validamos se a extensão é permitida
                    if (ValidarExtensao(extensoesPermitidas, nomeArquivo))
                    {
                        var extensao = RetornarExtensao(nomeArquivo);
                        var novoNome = $"{Guid.NewGuid()}.{extensao}";
                        var caminhoCompleto = Path.Combine(caminho, novoNome);

                        // Salvar o arquivo na aplicação
                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            arquivo.CopyTo(stream);
                        }
                        return novoNome;
                    }
                }
                // Retorna vazio
                return "";
            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }
        }

        internal static string UploadFile(object arquivo, string[] extensoesPermitidas, string v)
        {
            throw new NotImplementedException();
        }

        // Remover arquivo

        // Validar extensão de arquivo
        //
        public static bool ValidarExtensao(string[] extensoesPermitidas, string nomeArquivo)
        {

            string extensao = RetornarExtensao(nomeArquivo);

            // Verifica o tipo de extensão permitida
            foreach (string ext in extensoesPermitidas)
            {
                if (ext == extensao)
                {
                    return true;
                }
    
            }
            return false;

        }
        // Retornar a extensão
        public static string RetornarExtensao(string nomeArquivo)
        {
            // [0]
            // arquivo.jpeg ==3
            // lenght(3) - 1 = 2

            //Le o tamanho total do arquivo
            string[] dados = nomeArquivo.Split('.');

            //Verifica o tamanho e retorna -1
            return dados[dados.Length - 1];
        }
    }
}
