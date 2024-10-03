using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;

namespace DemoUpload.Client.Componentes.Servicos;

public class ServicoUploadArquivos
{
    private readonly HttpClient httpClient;
    private readonly string urlServidorUpload = "http://localhost:5022/upload";

    public ServicoUploadArquivos(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<string> UploadArquivo(IBrowserFile arquivo)
    {
        // Precisamos enviar o conteúdo do arquivo para o servidor
        // Para isso usamos como dados do formulário e adicionamos o conteúdo do arquivo
        var content = new MultipartFormDataContent();
        var conteudoArquivo = new StreamContent(arquivo.OpenReadStream());
        if (arquivo is not null)
        {
            conteudoArquivo.Headers.ContentType = new MediaTypeHeaderValue(arquivo.ContentType);
            content.Add(conteudoArquivo, "file", arquivo.Name);


            var resposta = await httpClient.PostAsync(urlServidorUpload, content);
            if (resposta.IsSuccessStatusCode)
            {
                var resultado = await resposta.Content.ReadFromJsonAsync<ResultadoUpload>();
                return resultado?.Url ?? "";
            }
        }
        return "";
    }
    private class ResultadoUpload
    {
        public string? Url { get; set; }
    }
}
