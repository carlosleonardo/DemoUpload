using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace DemoUpload.Components.Servicos;

public class ServicoUploadArquivos
{
    private readonly HttpClient httpClient;
    private readonly string urlServidorUpload = "http://localhost:5022/upload";

    public ServicoUploadArquivos(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task UploadArquivo(IBrowserFile arquivo)
    {
        // Precisamos enviar o conteúdo do arquivo para o servidor
        // Para isso usamos como dados do formulário e adicionamos o conteúdo do arquivo
        var content = new MultipartFormDataContent();
        var conteudoArquivo = new StreamContent(arquivo.OpenReadStream());
        if (arquivo is not null)
        {
            content.Add(conteudoArquivo, "file", arquivo.Name);
            var resposta = await httpClient.PostAsync(urlServidorUpload, content);

        }

    }
}
